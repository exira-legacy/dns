namespace Dns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Exceptions;
    using Newtonsoft.Json;

    public class RecordLabel : StringValueObject<RecordLabel>
    {
        // TXT and CNAME records can also contain _ and .
        private static readonly Regex DnsLabelRegex = new Regex(@"^(?![0-9]+$)(?!-)[a-zA-Z0-9-]{0,63}(?<!-)$", RegexOptions.IgnoreCase);
        private static readonly Regex DnsLabelRelaxedRegex = new Regex(@"^(?![0-9]+$)(?!-)[a-zA-Z0-9-_\.]{0,63}(?<!-)$", RegexOptions.IgnoreCase);

        // labels 63 octets or less
        public const int MaxLength = 63;

        public RecordLabel([JsonProperty("value")] string label) : base(label?.ToLowerInvariant().Trim())
        {
            if (string.IsNullOrWhiteSpace(Value))
                throw new EmptyRecordLabelException();

            if (Value.Length > MaxLength)
                throw new RecordLabelTooLongException();

            // We assume if it is a valid TXT label, it is also a valid normal label, a more strict check needs to happen at the Record level
            if (DnsLabelRelaxedRegex.IsMatch(Value))
                return;

            var validationErrors = ValidateFormat(Value);

            if (validationErrors.Any())
                throw new InvalidRecordLabelException(
                    "Record label contains validation errors.",
                    new AggregateException(validationErrors));
        }

        public bool TryValidate(RecordType recordType, out List<InvalidRecordLabelException> exceptions)
        {
            exceptions = new List<InvalidRecordLabelException>();

            if (Value == "@")
                return true;

            if (recordType == RecordType.txt || recordType == RecordType.cname)
            {
                if (DnsLabelRelaxedRegex.IsMatch(Value))
                    return true;
            }
            else
            {
                if (DnsLabelRegex.IsMatch(Value))
                    return true;
            }

            exceptions = ValidateFormat(Value);
            return false;
        }

        private static List<InvalidRecordLabelException> ValidateFormat(string value)
        {
            if (value == "@")
                return new List<InvalidRecordLabelException>();

            var exceptions = new List<InvalidRecordLabelException>();

            if (value.StartsWith("-"))
                exceptions.Add(new RecordLabelCannotStartWithDashException());

            if (value.EndsWith("-"))
                exceptions.Add(new RecordLabelCannotEndWithDashException());

            if (value.All(char.IsDigit))
                exceptions.Add(new RecordLabelCannotBeAllDigitsException());

            // At this point, the only thing left are invalid characters
            exceptions.Add(new RecordLabelContainsInvalidCharactersException());

            return exceptions;
        }
    }
}
