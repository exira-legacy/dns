namespace Dns.Api.Domain
{
    using System;
    using System.Linq;
    using FluentValidation;

    public static class DomainValidators
    {
        public static IRuleBuilderOptions<T, Guid?> Required<T>(this IRuleBuilder<T, Guid?> ruleBuilder)
            => ruleBuilder
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

        public static IRuleBuilderOptions<T, string> Required<T>(this IRuleBuilder<T, string> ruleBuilder)
            => ruleBuilder
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

        public static IRuleBuilderOptions<T, string> MaxLength<T>(this IRuleBuilder<T, string> ruleBuilder, int length)
            => ruleBuilder
                .Length(0, length)
                .WithMessage("{PropertyName} cannot be longer than {MaxLength}.");

        public static IRuleBuilderOptions<T, string> ValidHostName<T>(this IRuleBuilder<T, string> ruleBuilder)
            => ruleBuilder
                .Must(property => Uri.CheckHostName(property) == UriHostNameType.Dns)
                .WithMessage("{PropertyName} must be a valid hostname.");

        public static IRuleBuilderOptions<T, string> ValidTopLevelDomain<T>(this IRuleBuilder<T, string> ruleBuilder)
            => ruleBuilder
                .ValidEnumeration<T, TopLevelDomain, InvalidTopLevelDomainException>();

        public static IRuleBuilderOptions<T, string> ValidEnumeration<T, TEnum, TException>(this IRuleBuilder<T, string> ruleBuilder)
            where TEnum : Enumeration<TEnum, string, TException>
            where TException : EnumerationException
            => ruleBuilder
                .Must(property => Enumeration<TEnum, string, TException>.TryParse(property, out _))
                .WithMessage($"{{PropertyName}} must be one of {string.Join(", ", Enumeration<TEnum, string, TException>.GetAll().Select(x => $"'{x.Value}'"))}.");
    }
}
