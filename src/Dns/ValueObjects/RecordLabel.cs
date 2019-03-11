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
        // TXT records can also contain _ and .
        private static readonly Regex DnsLabelRegex = new Regex(@"^(?![0-9]+$)(?!-)[a-zA-Z0-9-]{0,63}(?<!-)$", RegexOptions.IgnoreCase);
        private static readonly Regex DnsLabelTxtRegex = new Regex(@"^(?![0-9]+$)(?!-)[a-zA-Z0-9-_\.]{0,63}(?<!-)$", RegexOptions.IgnoreCase);

        public const int MaxLength = 63;

        public RecordLabel([JsonProperty("value")] string label) : base(label?.ToLowerInvariant().Trim())
        {
            if (string.IsNullOrWhiteSpace(label))
                throw new EmptyRecordLabelException();

            if (label.Length > MaxLength)
                throw new RecordLabelTooLongException();

            if (Value == "@")
                return;

            // We assume if it is a valid TXT label, it is also a valid normal label, a more strict check needs to happen at the Record level
            if (DnsLabelTxtRegex.IsMatch(Value))
                return;

            // At this point, we know it is invalid, but for which reason?
            var exceptions = new List<InvalidRecordLabelException>();
            if (Value.StartsWith("-"))
                exceptions.Add(new RecordLabelCannotStartWithDashException());

            if (Value.EndsWith("-"))
                exceptions.Add(new RecordLabelCannotEndWithDashException());

            if (Value.All(char.IsDigit))
                exceptions.Add(new RecordLabelCannotBeAllDigitsException());

            // At this point, the only thing left are invalid characters
            exceptions.Add(new RecordLabelContainsInvalidCharactersException());

            throw new AggregateException("Record label contains validation errors.", exceptions);
        }

        public bool IsValid(RecordType recordType)
        {
            if (Value == "@")
                return true;

            // We already validated using the TXT regex, so we know it is correct
            return recordType == RecordType.txt || DnsLabelRegex.IsMatch(Value);
        }
    }
}
