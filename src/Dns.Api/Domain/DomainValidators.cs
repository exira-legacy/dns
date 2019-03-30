namespace Dns.Api.Domain
{
    using System;
    using System.Linq;
    using Be.Vlaanderen.Basisregisters.Api.Localization;
    using FluentValidation;
    using Microsoft.Extensions.Localization;

    public class DomainValidatorResources
    {
        public string RequiredMessage => "{PropertyName} is required.";
        public string MaxLengthMessage => "{PropertyName} cannot be longer than {MaxLength} characters.";
        public string ValidHostNameMessage => "{PropertyName} must be a valid hostname.";
        public string ValidEnumerationMessage => "{{PropertyName}} must be one of {0}.";
    }

    public static class DomainValidators
    {
        private static readonly IStringLocalizer<DomainValidatorResources> Localizer =
            GlobalStringLocalizer.Instance.GetLocalizer<DomainValidatorResources>();

        public static IRuleBuilderOptions<T, Guid?> Required<T>(this IRuleBuilder<T, Guid?> ruleBuilder)
            => ruleBuilder
                .NotEmpty()
                .WithMessage(Localizer.GetString(x => x.RequiredMessage));

        public static IRuleBuilderOptions<T, string> Required<T>(this IRuleBuilder<T, string> ruleBuilder)
            => ruleBuilder
                .NotEmpty()
                .WithMessage(Localizer.GetString(x => x.RequiredMessage));

        public static IRuleBuilderOptions<T, string> MaxLength<T>(this IRuleBuilder<T, string> ruleBuilder, int length)
            => ruleBuilder
                .Length(0, length)
                .WithMessage(Localizer.GetString(x => x.MaxLengthMessage));

        public static IRuleBuilderOptions<T, string> ValidHostName<T>(this IRuleBuilder<T, string> ruleBuilder)
            => ruleBuilder
                .Must(property => Uri.CheckHostName(property) == UriHostNameType.Dns)
                .WithMessage(Localizer.GetString(x => x.ValidHostNameMessage));

        public static IRuleBuilderOptions<T, string> ValidTopLevelDomain<T>(this IRuleBuilder<T, string> ruleBuilder)
            => ruleBuilder
                .ValidEnumeration<T, TopLevelDomain, InvalidTopLevelDomainException>();

        public static IRuleBuilderOptions<T, string> ValidEnumeration<T, TEnum, TException>(this IRuleBuilder<T, string> ruleBuilder)
            where TEnum : Enumeration<TEnum, string, TException>
            where TException : EnumerationException
            => ruleBuilder
                .Must(property => Enumeration<TEnum, string, TException>.TryParse(property, out _))
                .WithMessage(
                    Localizer.GetString(x =>
                        x.ValidEnumerationMessage,
                        string.Join(", ", Enumeration<TEnum, string, TException>.GetAll().Select(x => $"'{x.Value}'"))));
    }
}
