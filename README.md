# SPW.Admin.Api
This API aims to maintain all administrative modules of the special public witnessing service.


aws dynamodb --endpoint-url http://localhost:4566 --region us-east-1 create-table --table-name users --attribute-definitions AttributeName=id,AttributeType=S AttributeName=name,AttributeType=S --key-schema AttributeName=id,KeyType=HASH AttributeName=name,KeyType=RANGE --provisioned-throughput ReadCapacityUnits=100,WriteCapacityUnits=500