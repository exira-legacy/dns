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
                new Record(RecordType.Txt, 3600, "@", $"google-site-verification={_verificationToken}"),

                // https://support.google.com/a/answer/174125?hl=en
                new Record(RecordType.Mx, 3600, "@", "1 ASPMX.L.GOOGLE.COM."),
                new Record(RecordType.Mx, 3600, "@", "5 ALT1.ASPMX.L.GOOGLE.COM."),
                new Record(RecordType.Mx, 3600, "@", "5 ALT2.ASPMX.L.GOOGLE.COM."),
                new Record(RecordType.Mx, 3600, "@", "10 ALT3.ASPMX.L.GOOGLE.COM."),
                new Record(RecordType.Mx, 3600, "@", "10 ALT4.ASPMX.L.GOOGLE.COM."),

                new Record(RecordType.CName, 3600, "mail", "ghs.google.com."),
            };
        }
    }
}
