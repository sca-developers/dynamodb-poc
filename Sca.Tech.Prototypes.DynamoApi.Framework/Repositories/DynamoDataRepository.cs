using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;

using Microsoft.Extensions.Options;

using Sca.Tech.Prototypes.DynamoApi.Framework.Models;

namespace Sca.Tech.Prototypes.DynamoApi.Framework.Repositories
{
    public class DynamoDataRepository : IDynamoDataRepository
    {
        private readonly AmazonDynamoDBClient _client;

        public DynamoDataRepository(IOptions<AppSettings> settings)
        {
            var credentials = new BasicAWSCredentials("Fake", "Fake");

            _client = new AmazonDynamoDBClient(credentials, new AmazonDynamoDBConfig()
            {
                ServiceURL = settings.Value.ConnectionString,
                LogResponse = true
            });

            Init();
        }

        public void Init()
        {
            ListTablesResponse tableListResponse = null;

            tableListResponse = _client.ListTablesAsync().Result;

            if (tableListResponse == null || !tableListResponse.TableNames.Any(x => x == "Network"))
                CreateTable("Network");

            if (tableListResponse == null || !tableListResponse.TableNames.Any(x => x == "Station"))
                CreateTable("Station");

            if (tableListResponse == null || !tableListResponse.TableNames.Any(x => x == "Show"))
                CreateTable("Show");

            var table = Table.LoadTable(_client, "Network");

            /*
            if (table.GlobalSecondaryIndexes.Any(x => x.Key == "Network_Global_Index_01"))
            {
                var updateTableRequest = new UpdateTableRequest()
                {
                    TableName = "Network",
                    GlobalSecondaryIndexUpdates = new List<GlobalSecondaryIndexUpdate>()
                    {
                        new GlobalSecondaryIndexUpdate()
                        {
                            Delete = new DeleteGlobalSecondaryIndexAction
                            {
                                IndexName = "Network_Global_Index_01"
                            }
                        }
                    }
                };

                var response = _client.UpdateTableAsync(updateTableRequest).Result;
                var status = response.HttpStatusCode;
            }
            */

            if (!table.GlobalSecondaryIndexes.Any(x => x.Key == "Network_Global_Index_01"))
            {
                var updateTableRequest = new UpdateTableRequest()
                {
                    TableName = "Network",
                    AttributeDefinitions = new List<AttributeDefinition>()
                    {
                        new AttributeDefinition
                        {
                            AttributeName = "MasterId",
                            AttributeType = "S"
                        },
                        new AttributeDefinition
                        {
                            AttributeName = "Id",
                            AttributeType = "S"
                        }
                    },
                    GlobalSecondaryIndexUpdates = new List<GlobalSecondaryIndexUpdate>()
                    {
                        new GlobalSecondaryIndexUpdate()
                        {
                            Create = new CreateGlobalSecondaryIndexAction()
                            {
                                IndexName = "Network_Global_Index_01",
                                KeySchema = new List<KeySchemaElement>()
                                {
                                    new KeySchemaElement()
                                    {
                                        AttributeName = "MasterId",
                                        KeyType = "HASH"
                                    },
                                    new KeySchemaElement()
                                    {
                                        AttributeName = "Id",
                                        KeyType = "RANGE"
                                    }
                                },
                                Projection = new Projection() { ProjectionType = ProjectionType.ALL },
                                ProvisionedThroughput = new ProvisionedThroughput() { ReadCapacityUnits = 10, WriteCapacityUnits = 10 }
                            }
                        }
                    }
                };

                var response = _client.UpdateTableAsync(updateTableRequest).Result;
                var status = response.HttpStatusCode;
            }
        }

        public async Task<List<T>> List<T>()
        {
            using (var context = new DynamoDBContext(_client))
            {
                DynamoDBOperationConfig opConfig = typeof(T) == typeof(Network) ? new DynamoDBOperationConfig() { IndexName = "Network_Global_Index_01" } : null;

                var scanOperation = context.ScanAsync<T>(null, opConfig);
                return await scanOperation.GetRemainingAsync();
            }
        }

        public async Task<List<T>> Get<T>(object key)
        {
            using (var context = new DynamoDBContext(_client))
            {
                if (typeof(T) == typeof(Network))
                {
                    var queryOperation = context.FromQueryAsync<T>(new QueryOperationConfig()
                    {
                        IndexName = "Network_Global_Index_01",
                        KeyExpression = new Expression() { ExpressionStatement = "MasterId = :mid", ExpressionAttributeValues = new Dictionary<string, DynamoDBEntry>() { { ":mid", key.ToString() } } },
                        AttributesToGet = new List<string>() { "Name", "Code", "Tags", "Id", "MasterId" },
                        Select = SelectValues.SpecificAttributes
                    });

                    return await queryOperation.GetRemainingAsync();
                }
                else
                {
                    var queryOperation = context.FromQueryAsync<T>(new QueryOperationConfig()
                    {
                        ConsistentRead = true,
                        Filter = new QueryFilter("MasterId", QueryOperator.Equal, key.ToString())
                    });

                    return await queryOperation.GetRemainingAsync();
                }
            }
        }

        public async Task<List<T>> SearchByTags<T>(string[] tags)
        {
            using (var context = new DynamoDBContext(_client))
            {
                var expression = new StringBuilder("");
                var expressionAttributeValues = new Dictionary<string, DynamoDBEntry>();

                var index = 1;
                foreach (var tag in tags)
                {
                    var valueVariable = ":val" + index.ToString();

                    if (expression.Length > 1)
                        expression.Append(" OR ");

                    expression.Append($"contains(Tags, {valueVariable})");
                    expressionAttributeValues.Add(valueVariable, tag);
                    index++;
                }

                var opConfig = new ScanOperationConfig()
                {
                    FilterExpression = new Expression()
                    {
                        ExpressionStatement = expression.ToString(),
                        ExpressionAttributeValues = expressionAttributeValues
                    },
                    IndexName = "Network_Global_Index_01"
                };

                var queryOperation = context.FromScanAsync<T>(opConfig);

                return await queryOperation.GetRemainingAsync();
            }
        }

        public async Task AddOrUpdate<T>(T obj)
        {
            using (var context = new DynamoDBContext(_client))
            {
                var saveTask = context.SaveAsync<T>(obj);
                await saveTask;
            }
        }

        private HttpStatusCode CreateTable(string tableName)
        {
            var networkTableRequest = new CreateTableRequest
            {
                TableName = tableName,
                AttributeDefinitions = new List<AttributeDefinition>()
                {
                    new AttributeDefinition
                    {
                        AttributeName = "MasterId",
                        AttributeType = "S"
                    }
                },
                KeySchema = new List<KeySchemaElement>()
                {
                    new KeySchemaElement
                    {
                        AttributeName = "MasterId",
                        KeyType = "HASH"
                    }
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 5,
                    WriteCapacityUnits = 5
                }
            };

            var createResponse = _client.CreateTableAsync(networkTableRequest).Result;
            return createResponse.HttpStatusCode;
        }
    }
}
