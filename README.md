## Run DynamoDB Locally

* Simply follow the instructions on in this link (https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DynamoDBLocal.html)

* If you need a GUI to view the data then here is how you can install the client:

    * Run the following commands in command line:

        1. npm install dynamodb-admin -g
        2. export DYNAMO_ENDPOINT=http://localhost:8000
        3. dynamodb-admin
        

## Testing the API

* There are three types of entities available: Network, Station and Show and you can query them by the following route structure:
    
    * <strong>/api/v2/admin/{entitytype}/{masterid?}</strong> - this will either retrieve the entire table or just the single item with the id
    * <strong>/api/v2/admin/{entitytype}/save/</strong> - this will create a random entity and insert it into the table
    * <strong>/api/v2/search/tags?tags={tagword1}&tags={tagword2}...</strong> - this will search on the tags property of the Network table

* Use the above routes to create several dummy content into the tables and then query them out