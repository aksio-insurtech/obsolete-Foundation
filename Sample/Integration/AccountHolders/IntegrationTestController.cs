namespace Integration.AccountHolders
{
    [Route("/api/integration")]

    public class IntegrationTestController : Controller
    {
        readonly ExternalAccountHolderConnector _connector;

        public IntegrationTestController(ExternalAccountHolderConnector connector)
        {
            _connector = connector;
        }

        [HttpGet]
        public async Task Trigger()
        {
            await _connector.ImportOne("03050712345");
        }
    }
}
