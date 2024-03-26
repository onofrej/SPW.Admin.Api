using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Common;

[CollectionDefinition(CollectionDefinition)]
public class TestCollection : ICollectionFixture<MainFixture>
{
    internal const string CollectionDefinition = "Test Collection";
}