namespace Dns.Domain.Services.GoogleSuite.Commands
{
    public class AddGoogleSuite
    {
        public DomainName DomainName { get; }

        public GoogleVerificationToken VerificationToken { get; }

        public AddGoogleSuite(
            DomainName domainName,
            GoogleVerificationToken verificationToken)
        {
            DomainName = domainName;
            VerificationToken = verificationToken;
        }
    }
}
