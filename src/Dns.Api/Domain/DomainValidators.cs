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
                .WithMessage(GlobalStringLocalizer.Instance["{PropertyName} is required."]);

        public static IRuleBuilderOptions<T, string> Required<T>(this IRuleBuilder<T, string> ruleBuilder)
            => ruleBuilder
                .NotEmpty()
                .WithMessage(GlobalStringLocalizer.Instance["{PropertyName} is required."]);

        public static IRuleBuilderOptions<T, string> MaxLength<T>(this IRuleBuilder<T, string> ruleBuilder, int length)
            => ruleBuilder
                .Length(0, length)
                .WithMessage(GlobalStringLocalizer.Instance["{PropertyName} cannot be longer than {MaxLength} characters."]);

        public static IRuleBuilderOptions<T, string> ValidHostName<T>(this IRuleBuilder<T, string> ruleBuilder)
            => ruleBuilder
                .Must(property => Uri.CheckHostName(property) == UriHostNameType.Dns)
                .WithMessage(GlobalStringLocalizer.Instance["{PropertyName} must be a valid hostname."]);

        public static IRuleBuilderOptions<T, string> ValidTopLevelDomain<T>(this IRuleBuilder<T, string> ruleBuilder)
            => ruleBuilder
                .ValidEnumeration<T, TopLevelDomain, InvalidTopLevelDomainException>();

        public static IRuleBuilderOptions<T, string> ValidEnumeration<T, TEnum, TException>(this IRuleBuilder<T, string> ruleBuilder)
            where TEnum : Enumeration<TEnum, string, TException>
            where TException : EnumerationException
            => ruleBuilder
                .Must(property => Enumeration<TEnum, string, TException>.TryParse(property, out _))
                .WithMessage(GlobalStringLocalizer.Instance[
                    "{{PropertyName}} must be one of {0}.",
                    string.Join(", ", Enumeration<TEnum, string, TException>.GetAll().Select(x => $"'{x.Value}'"))]);
    }
}
