namespace Dns
{
    using System.Collections.Generic;

    public class RecordSet : List<Record>
    {
        public RecordSet() { }

        public RecordSet(IEnumerable<Record> collection) : base(collection) { }

        public RecordSet AddRecords(IEnumerable<Record> records)
        {
            foreach (var record in records)
                if (!Contains(record))
                    Add(record);

            return this;
        }
    }
}
