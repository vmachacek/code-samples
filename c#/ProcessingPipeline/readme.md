## ProcessingPipeline

ACME company is using ProcessingPipeline to process their reservations posted from their website. IT department have to make sure they are sending correct email, charging credit cards, saving all data to db. etc. 

ProcessingPipeline is implemented as chain of responsibility / middleware pattern. Class `PipelineBuilder` based on posted data sets up the pipeline for current invocation. `ReservationPipelineExecutor` is class responsible for executing generated pipeline.

### Architecture and considered alternatives
Whole pipeline is executed synchronously from http standpoint which could be potential bottleneck however this approach was selected to fit size and load of ACME corporation which deals with hundreds of reservations every day. This could be further improved with event-driven architecture. Event sourcing is probably not best fit, but simple message queue should work fine.

### Testing
Each step is unit tested. Services like SendEmail, NotifySlack, GenerateEmail don't have interfaces, instead I'm providing HttpClient with test messageHandler which contains canned data. That way I’m testing everything up to http boundary. 

Underlaying database is MongoDb and it's better to use actual mongo instance for unit tests, rather than faking `IMongoQueriable<T>`, because it's not guaranteed fakes would act same as actual MongoDB. Because of that I'm running instance of mongo with random database name for unit tests. That way its 100% tested it will work on environment with real database. 