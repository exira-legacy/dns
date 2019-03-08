namespace Dns.Domain.Services.GoogleSuite
{
    public class GoogleSuiteService : IService
    {
        private readonly GoogleVerificationToken _verificationToken;

        public ServiceId ServiceId { get; }

        public GoogleSuiteService(
            ServiceId serviceId,
            GoogleVerificationToken verificationToken)
        {
            ServiceId = serviceId;

            _verificationToken = verificationToken;
        }

        public RecordSet GetRecords()
        {
            // TODO: Add real values
            return new RecordSet
            {
                CreateRecord(RecordType.txt, 3600, "@", $"google-site-verification={_verificationToken}"),

                // https://support.google.com/a/answer/174125?hl=en
                CreateRecord(RecordType.mx, 3600, "@", "1 ASPMX.L.GOOGLE.COM."),
                CreateRecord(RecordType.mx, 3600, "@", "5 ALT1.ASPMX.L.GOOGLE.COM."),
                CreateRecord(RecordType.mx, 3600, "@", "5 ALT2.ASPMX.L.GOOGLE.COM."),
                CreateRecord(RecordType.mx, 3600, "@", "10 ALT3.ASPMX.L.GOOGLE.COM."),
                CreateRecord(RecordType.mx, 3600, "@", "10 ALT4.ASPMX.L.GOOGLE.COM."),

                CreateRecord(RecordType.cname, 3600, "mail", "ghs.google.com."),
            };
        }

        private static Record CreateRecord(RecordType type, int ttl, string label, string value) =>
            new Record(type, new TimeToLive(ttl), new RecordLabel(label), new RecordValue(value));
    }
}
