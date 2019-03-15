namespace Dns
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;

    [Serializable]
    [DebuggerDisplay("{DisplayName} - {Value}")]
    public abstract class Enumeration<TEnumeration> : Enumeration<TEnumeration, int, EnumerationException>
        where TEnumeration : Enumeration<TEnumeration>
    {
        protected Enumeration(int value, string displayName)
            : base(value, displayName) { }

        public static TEnumeration FromInt32(int value)
            => FromValue(value);

        public static bool TryFromInt32(int listItemValue, out TEnumeration result)
            => TryParse(listItemValue, out result);
    }

    [Serializable]
    [DebuggerDisplay("{DisplayName} - {Value}")]
    [DataContract(Namespace = "http://github.com/HeadspringLabs/Enumeration/5/13")]
    public abstract class Enumeration<TEnumeration, TValue, TException> : IComparable<TEnumeration>, IEquatable<TEnumeration>
        where TEnumeration : Enumeration<TEnumeration, TValue, TException>
        where TValue : IComparable
        where TException : EnumerationException
    {
        private static readonly Lazy<TEnumeration[]> Enumerations = new Lazy<TEnumeration[]>(GetEnumerations);

        [DataMember(Order = 0)]
        public TValue Value { get; }

        [DataMember(Order = 1)]
        public string DisplayName { get; }

        protected Enumeration(TValue value, string displayName)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            Value = value;
            DisplayName = displayName;
        }

        public int CompareTo(TEnumeration other)
            => Value.CompareTo(other == default(TEnumeration) ? default : other.Value);

        public sealed override string ToString()
            => DisplayName;

        public static TEnumeration[] GetAll()
            => Enumerations.Value;

        private static TEnumeration[] GetEnumerations()
        {
            var enumerationType = typeof(TEnumeration);
            return enumerationType
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(info => enumerationType.IsAssignableFrom(info.FieldType))
                .Select(info => info.GetValue(null))
                .Cast<TEnumeration>()
                .ToArray();
        }

        public override bool Equals(object obj)
            => Equals(obj as TEnumeration);

        public bool Equals(TEnumeration other)
            => other != null && ValueEquals(other.Value);

        public override int GetHashCode()
            => Value.GetHashCode();

        public static bool operator ==(Enumeration<TEnumeration, TValue, TException> left, Enumeration<TEnumeration, TValue, TException> right)
            => Equals(left, right);

        public static bool operator !=(Enumeration<TEnumeration, TValue, TException> left, Enumeration<TEnumeration, TValue, TException> right)
            => !Equals(left, right);

        public static TEnumeration FromValue(TValue value)
            => Parse(value, "value", item => item.Value.Equals(value));

        public static TEnumeration Parse(string displayName)
            => Parse(displayName, "display name", item => item.DisplayName == displayName);

        private static bool TryParse(Func<TEnumeration, bool> predicate, out TEnumeration result)
        {
            result = GetAll().FirstOrDefault(predicate);
            return result != null;
        }

        private static TEnumeration Parse(object value, string description, Func<TEnumeration, bool> predicate)
        {
            if (TryParse(predicate, out var result))
                return result;

            var message = $"'{value}' is not a valid {description} in {typeof(TEnumeration)}";
            throw CreateException(message, value, typeof(TEnumeration).Name, nameof(value));
        }

        private static EnumerationException CreateException(string message, object value, string type, string paramName)
        {
            try
            {
                return Activator.CreateInstance(
                    typeof(TException),
                    message,
                    paramName,
                    value,
                    type) as TException;
            }
            catch
            {
                return new EnumerationException(message, paramName, value, type);
            }
        }

        public static bool TryParse(TValue value, out TEnumeration result)
            => TryParse(e => e.ValueEquals(value), out result);

        public static bool TryParseDisplayName(string displayName, out TEnumeration result)
            => TryParse(e => e.DisplayName == displayName, out result);

        protected virtual bool ValueEquals(TValue value)
            => Value.Equals(value);
    }

    public class EnumerationException : ArgumentException
    {
        public object Value { get; }
        public string Type { get; }

        public EnumerationException(string message, string paramName, object value, string type) : base(message, paramName)
        {
            Value = value;
            Type = type;
        }
    }
}
