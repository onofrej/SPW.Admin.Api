# SPW.Admin.Api

This API aims to maintain all administrative modules of the special public witnessing service.

## Setup environment

### Setup your environment

- Install GIT
  https://www.git-scm.com/downloads
- Install .NET 6 SDK
  https://dotnet.microsoft.com/en-us/download/dotnet/6.0
- Install Visual Studio (latest version)
  https://visualstudio.microsoft.com/pt-br/downloads/
- Install Docker Desktop
  https://www.docker.com/products/docker-desktop/
- Install AWS CLI
  https://docs.aws.amazon.com/cli/latest/userguide/getting-started-install.html
- Install AWS Lambda Tools
  Run in your terminal: `dotnet tool install -g Amazon.Lambda.Tools`

### How to run locally (create local environment)

- Create table "user" on DynamoDB

  `aws dynamodb --endpoint-url http://localhost:4566 --region us-east-1 create-table --table-name user --attribute-definitions AttributeName=id,AttributeType=S --key-schema AttributeName=id,KeyType=HASH --provisioned-throughput ReadCapacityUnits=25,WriteCapacityUnits=25`

- Create table "circuit" on DynamoDB

  `aws dynamodb --endpoint-url http://localhost:4566 --region us-east-1 create-table --table-name circuit --attribute-definitions AttributeName=id,AttributeType=S --key-schema AttributeName=id,KeyType=HASH --provisioned-throughput ReadCapacityUnits=25,WriteCapacityUnits=25`

- Create table "point" on DynamoDB

  `aws dynamodb --endpoint-url http://localhost:4566 --region us-east-1 create-table --table-name point --attribute-definitions AttributeName=id,AttributeType=S --key-schema AttributeName=id,KeyType=HASH --provisioned-throughput ReadCapacityUnits=25,WriteCapacityUnits=25`

- Create table "validity" on DynamoDB

  `aws dynamodb --endpoint-url http://localhost:4566 --region us-east-1 create-table --table-name validity --attribute-definitions AttributeName=id,AttributeType=S --key-schema AttributeName=id,KeyType=HASH --provisioned-throughput ReadCapacityUnits=25,WriteCapacityUnits=25`

- Create table "schedule" on DynamoDB

  `aws dynamodb --endpoint-url http://localhost:4566 --region us-east-1 create-table --table-name schedule --attribute-definitions AttributeName=id,AttributeType=S --key-schema AttributeName=id,KeyType=HASH --provisioned-throughput ReadCapacityUnits=25,WriteCapacityUnits=25`

  - Create table "announcement" on DynamoDB

  `aws dynamodb --endpoint-url http://localhost:4566 --region us-east-1 create-table --table-name announcement --attribute-definitions AttributeName=id,AttributeType=S --key-schema AttributeName=id,KeyType=HASH --provisioned-throughput ReadCapacityUnits=25,WriteCapacityUnits=25`