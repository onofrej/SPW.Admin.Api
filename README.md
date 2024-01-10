# SPW.Admin.Api
This API aims to maintain all administrative modules of the special public witnessing service.

## Setup environment

- Create table "user" on DynamoDB

    `` yml
    aws dynamodb --endpoint-url http://localhost:4566 --region us-east-1 create-table --table-name user --attribute-definitions AttributeName=id,AttributeType=S --key-schema AttributeName=id,KeyType=HASH --provisioned-throughput ReadCapacityUnits=25,WriteCapacityUnits=25
    ``