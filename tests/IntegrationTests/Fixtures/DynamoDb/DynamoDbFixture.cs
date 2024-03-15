﻿using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace SPW.Admin.IntegrationTests.Fixtures.DynamoDb;

internal sealed class DynamoDbFixture : IDisposable
{
    private readonly List<Table> _tables = new()
    {
        new Table("user", "id", default)
    };

    private readonly IConfiguration _configuration;
    private readonly AmazonDynamoDBClient _amazonDynamoDBClient;

    public DynamoDbFixture(IConfiguration configuration)
    {
        _configuration = configuration;

        _amazonDynamoDBClient = new AmazonDynamoDBClient(new AmazonDynamoDBConfig
        {
            ServiceURL = _configuration.GetSection("AWSLocalstackSettings:ServiceURL").Value
        });

        CreateTablesAsync().Wait();
    }

    public async Task<T?> ReadAsync<T>(string hashKey, string? hashKeyValue)
    {
        var dynamoDBContext = new DynamoDBContext(_amazonDynamoDBClient);

        var collections = await dynamoDBContext.FromQueryAsync<T>(new QueryOperationConfig
        {
            Filter = new QueryFilter(hashKey, QueryOperator.Equal, hashKeyValue)
        }).GetRemainingAsync();

        return collections.FirstOrDefault();
    }

    public async Task InsertAsync<T>(T entity)
    {
        var dynamoDBContext = new DynamoDBContext(_amazonDynamoDBClient);

        await dynamoDBContext.SaveAsync(entity);
    }

    private async Task CreateTablesAsync()
    {
        foreach (var table in _tables)
        {
            var attributeDefinitions = new List<AttributeDefinition>()
            {
                new AttributeDefinition
                {
                    AttributeName = table.HashKeyName,
                    AttributeType = ScalarAttributeType.S
                }
            };

            var keySchemas = new List<KeySchemaElement>()
            {
                new KeySchemaElement
                {
                    AttributeName = table.HashKeyName,
                    KeyType = KeyType.HASH
                }
            };

            if (!string.IsNullOrWhiteSpace(table.SortKeyName))
            {
                attributeDefinitions.Add(new AttributeDefinition
                {
                    AttributeName = table.SortKeyName,
                    AttributeType = ScalarAttributeType.S
                });

                keySchemas.Add(new KeySchemaElement
                {
                    AttributeName = table.SortKeyName,
                    KeyType = KeyType.RANGE
                });
            }

            var request = new CreateTableRequest
            {
                TableName = table.Name,
                AttributeDefinitions = attributeDefinitions,
                KeySchema = keySchemas,
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 5,
                    WriteCapacityUnits = 5
                }
            };

            await _amazonDynamoDBClient.CreateTableAsync(request);
        }
    }

    private async Task DeleteTablesAsync()
    {
        foreach (var table in _tables)
        {
            await _amazonDynamoDBClient.DeleteTableAsync(new DeleteTableRequest { TableName = table.Name });
        }
    }

    public void Dispose()
    {
        DeleteTablesAsync().Wait();
    }
}

internal record Table(string? Name, string? HashKeyName, string? SortKeyName);