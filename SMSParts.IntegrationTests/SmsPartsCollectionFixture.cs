using Xunit;

namespace SMSParts.IntegrationTests
{
    [CollectionDefinition("SMS Parts Collection")]
    public class SmsPartsCollectionFixture : ICollectionFixture<SmsPartsFixture>
    {
    }
}
