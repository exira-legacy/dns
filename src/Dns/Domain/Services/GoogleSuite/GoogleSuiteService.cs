namespace Dns.Domain.Services.GoogleSuite
{
    public class GoogleSuiteService : IService
    {
        private readonly GoogleVerificationToken _verificationToken;

        public GoogleSuiteService(GoogleVerificationToken verificationToken) => _verificationToken = verificationToken;

        public RecordSet GetRecords()
        {
            // TODO: Add real values
            return new RecordSet
            {
                new Record("@", $"google-site-verification={_verificationToken}")
            };
        }
    }
}
