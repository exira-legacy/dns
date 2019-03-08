namespace Dns.Domain.Services.GoogleSuite.Commands
{
    public class AddGoogleSuite
    {
        public DomainName DomainName { get; }

        public ServiceId ServiceId { get; }

        public GoogleVerificationToken VerificationToken { get; }

        public AddGoogleSuite(
            DomainName domainName,
            ServiceId serviceId,
            GoogleVerificationToken verificationToken)
        {
            DomainName = domainName;
            ServiceId = serviceId;
            VerificationToken = verificationToken;
        }
    }
}
