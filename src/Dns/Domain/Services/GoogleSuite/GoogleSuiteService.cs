namespace Dns.Domain.Services.GoogleSuite
{
    using Newtonsoft.Json;

    public class GoogleSuiteService : IService
    {
        private readonly GoogleVerificationToken _verificationToken;

        internal static string ServiceType = "googlesuite";
        internal static string ServiceName = "Google Suite";

        public ServiceId ServiceId { get; }
        public ServiceType Type => Dns.ServiceType.googlesuite;
        public ServiceLabel Label => new ServiceLabel(ServiceName);

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

        public string GetServiceData() => JsonConvert.SerializeObject(new ServiceData(this));

        public class ServiceData
        {
            public GoogleVerificationToken VerificationToken { get; set; }

            public ServiceData(GoogleSuiteService service)
            {
                VerificationToken = service._verificationToken;
            }
        }
    }
}
