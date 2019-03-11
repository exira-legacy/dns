namespace Dns
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Exceptions;
    using Newtonsoft.Json;

    public class RecordValue : StringValueObject<RecordValue>
    {
        private static readonly Regex IpAddressRegex = new Regex(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$", RegexOptions.IgnoreCase);
        private static readonly Regex DnsLabelRegex = new Regex(@"^(?![0-9]+$)(?!-)[a-zA-Z0-9-]{0,63}(?<!-)$", RegexOptions.IgnoreCase);
        private static readonly Regex HostNameRegex = new Regex(@"^(([a-zA-Z]|[a-zA-Z][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z]|[A-Za-z][A-Za-z0-9\-]*[A-Za-z0-9])$", RegexOptions.IgnoreCase);

        // names 255 octets or less
        public const int MaxLength = 255;

        public RecordValue([JsonProperty("value")] string value) : base(value?.Trim())
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyRecordValueException();

            if (value.Length > MaxLength)
                throw new RecordValueTooLongException();

            // TODO: Value has rules to folllow, encode them!
            // TODO: Additional rules depend on the record type!
        }

        public bool TryValidate(RecordType recordType, out List<InvalidRecordValueException> exceptions)
        {
            exceptions = new List<InvalidRecordValueException>();
            return true;

            // TODO: Build exceptions + tests

            if (recordType == RecordType.a)
                return IsValidAValue(Value);

            if (recordType == RecordType.cname)
                return IsValidCnameValue(Value);

            if (recordType == RecordType.mx)
                return IsValidMxValue(Value);

            if (recordType == RecordType.ns)
                return IsValidNsValue(Value);

            if (recordType == RecordType.spf)
                return IsValidSpfValue();

            if (recordType == RecordType.txt)
                return IsValidTxtValue();

            return false;
        }

        private static bool IsValidAValue(string value)
        {
            //Host record(A) syntax
            //Moet een geldig IP adres zijn
            //bv. 198.51.100.1

            // A record - must be a dotted-quad IP address
            if (!IpAddressRegex.IsMatch(value))
                return false;

            return true;
        }

        private static bool IsValidCnameValue(string value)
        {
            //Alias record(CNAME) syntax
            //Kan een FQDN zijn(eindigen met punt)
            //bv.alias.example.com.
            //Kan hostname zijn(eindigen zonder punt)

            // CNAME record - must be some other legal label, or a FQDN ending with a dot
            if (value.EndsWith("."))
            {
                if (!HostNameRegex.IsMatch(value.Substring(0, value.Length - 1)))
                    return false;

                return true;
            }

            if (!DnsLabelRegex.IsMatch(value))
                return false;

            return true;
        }

        private static bool IsValidMxValue(string value)
        {
            //Mail record(MX) syntax
            //Moet bestaan uit numerieke prioriteit en een van volgende:
            //een FQDN(eindigen met punt)
            //bv. 10 mail.example.com.
            //een hostname(eindigen zonder punt)
            //bv. 10 mail

            // MX record - 16-bit integer priority field, and a legal hostname or dns label.
            var mxParts = value.Split(new[] { ' ' }, 2);
            if (mxParts.Length < 2)
                return false;

            if (!int.TryParse(mxParts[0], out var priority))
                return false;

            if (priority < 0)
                return false;

            if (string.IsNullOrWhiteSpace(mxParts[1]))
                return false;

            if (mxParts[1].EndsWith("."))
            {
                if (!HostNameRegex.IsMatch(mxParts[1].Substring(0, mxParts[1].Length - 1)))
                    return false;

                return true;
            }

            if (!DnsLabelRegex.IsMatch(mxParts[1]))
                return false;

            return true;
        }

        private static bool IsValidNsValue(string value)
        {
            //Name server(NS) syntax
            //Kan een FQDN zijn(eindigen met punt)
            //bv.ns1.example.com.
            //Kan hostname zijn(eindigen zonder punt)

            // NS record - can be a dns label or a FQDN.
            if (value.EndsWith("."))
            {
                if (!HostNameRegex.IsMatch(value.Substring(0, value.Length - 1)))
                    return false;

                return true;
            }

            if (!DnsLabelRegex.IsMatch(value))
                return false;

            return true;
        }

        private static bool IsValidSpfValue()
        {
            // SPF record - can contain anything, it's escaped with (" and ") on generation
            return true;
        }

        private static bool IsValidTxtValue()
        {
            // TXT record - can contain anything, it's escaped with (" and ") on generation
            return true;
        }
    }
}
