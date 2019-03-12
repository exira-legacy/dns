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
        }

        public bool TryValidate(RecordType recordType, out List<InvalidRecordValueException> exceptions)
        {
            exceptions = new List<InvalidRecordValueException>();

            if (recordType == RecordType.a)
                return IsValidAValue(Value, out exceptions);

            if (recordType == RecordType.cname)
                return IsValidCnameValue(Value, out exceptions);

            if (recordType == RecordType.mx)
                return IsValidMxValue(Value, out exceptions);

            if (recordType == RecordType.ns)
                return IsValidNsValue(Value, out exceptions);

            if (recordType == RecordType.spf)
                return IsValidSpfValue();

            if (recordType == RecordType.txt)
                return IsValidTxtValue();

            return false;
        }

        private static bool IsValidAValue(string value, out List<InvalidRecordValueException> exceptions)
        {
            //Host record(A) syntax
            //Moet een geldig IP adres zijn
            //bv. 198.51.100.1
            exceptions = new List<InvalidRecordValueException>();

            // A record - must be a dotted-quad IP address
            if (IpAddressRegex.IsMatch(value))
                return true;

            exceptions.Add(new RecordValueARecordMustBeValidIpException());
            return false;
        }

        private static bool IsValidCnameValue(string value, out List<InvalidRecordValueException> exceptions)
        {
            //Alias record(CNAME) syntax
            //Kan een FQDN zijn(eindigen met punt)
            //bv.alias.example.com.
            //Kan hostname zijn(eindigen zonder punt)
            exceptions = new List<InvalidRecordValueException>();

            // CNAME record - must be some other legal label, or a FQDN ending with a dot
            if (value.EndsWith("."))
            {
                if (!HostNameRegex.IsMatch(value.Substring(0, value.Length - 1)))
                {
                    exceptions.Add(new RecordValueCNameRecordInvalidHostnameException());
                    return false;
                }

                return true;
            }

            if (!DnsLabelRegex.IsMatch(value))
            {
                exceptions.Add(new RecordValueCNameRecordInvalidLabelException());
                return false;
            }

            return true;
        }

        private static bool IsValidMxValue(string value, out List<InvalidRecordValueException> exceptions)
        {
            //Mail record(MX) syntax
            //Moet bestaan uit numerieke prioriteit en een van volgende:
            //een FQDN(eindigen met punt)
            //bv. 10 mail.example.com.
            //een hostname(eindigen zonder punt)
            //bv. 10 mail
            exceptions = new List<InvalidRecordValueException>();

            // MX record - 16-bit integer priority field, and a legal hostname or dns label.
            var mxParts = value.Split(new[] { ' ' }, 2);
            if (mxParts.Length < 2)
            {
                exceptions.Add(new RecordValueMxRecordMustHavePriorityAndHostnameException());
                return false;
            }

            if (!int.TryParse(mxParts[0], out var priority) || (priority < 0))
            {
                exceptions.Add(new RecordValueMxRecordMustHaveIntegerPriorityException());
                return false;
            }

            if (string.IsNullOrWhiteSpace(mxParts[1]))
            {
                exceptions.Add(new RecordValueMxRecordMustHaveHostnameException());
                return false;
            }

            if (mxParts[1].EndsWith("."))
            {
                if (!HostNameRegex.IsMatch(mxParts[1].Substring(0, mxParts[1].Length - 1)))
                {
                    exceptions.Add(new RecordValueMxRecordInvalidHostnameException());
                    return false;
                }

                return true;
            }

            if (!DnsLabelRegex.IsMatch(mxParts[1]))
            {
                exceptions.Add(new RecordValueMxRecordInvalidLabelException());
                return false;
            }

            return true;
        }

        private static bool IsValidNsValue(string value, out List<InvalidRecordValueException> exceptions)
        {
            //Name server(NS) syntax
            //Kan een FQDN zijn(eindigen met punt)
            //bv.ns1.example.com.
            //Kan hostname zijn(eindigen zonder punt)
            exceptions = new List<InvalidRecordValueException>();

            // NS record - can be a dns label or a FQDN.
            if (value.EndsWith("."))
            {
                if (!HostNameRegex.IsMatch(value.Substring(0, value.Length - 1)))
                {
                    exceptions.Add(new RecordValueNsRecordInvalidHostnameException());
                    return false;
                }

                return true;
            }

            if (!DnsLabelRegex.IsMatch(value))
            {
                exceptions.Add(new RecordValueNsRecordInvalidLabelException());
                return false;
            }

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
