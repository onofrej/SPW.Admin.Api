using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests;

[CollectionDefinition(CollectionDefinition)]
public class TestCollection : ICollectionFixture<MainFixture>
{
    internal const string CollectionDefinition = "Test Collection";
}