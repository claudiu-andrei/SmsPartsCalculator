using Xunit;
using Xunit.Abstractions;

namespace SMSParts.IntegrationTests
{
    [Collection("SMS Parts Collection")]
    public class IntegrationTestBase : XunitContextBase
    {
        public SmsPartsFixture ApiHelper { get; }

        protected IntegrationTestBase(SmsPartsFixture fixture, ITestOutputHelper output) : base(output)
        {
            ApiHelper = fixture;
            
            XunitContext.EnableExceptionCapture();
        }
    }
}
