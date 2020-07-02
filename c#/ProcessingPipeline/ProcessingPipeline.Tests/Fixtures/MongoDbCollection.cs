namespace ProcessingPipeline.Tests.Fixtures
{
    using Xunit;

    //this is needed when more than one tests are using MongoDB
    [CollectionDefinition(MongoDbCollecitonName)]
    public class MongoDbCollection : ICollectionFixture<MongoDbFixture>
    {
        public const string MongoDbCollecitonName = "MongoDb";
    }
}
