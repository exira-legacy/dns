namespace Dns.Api.Infrastructure
{
    using System.Linq;
    using Be.Vlaanderen.Basisregisters.Api.Localization;
    using FluentValidation;
    using Microsoft.Extensions.Localization;

    public class ValidationExtensionsResources
    {
        public string ValidEnumerationMessage => "{{PropertyName}} must be one of {0}.";
    }

    public static class ValidationExtensions
    {
        private static readonly IStringLocalizer<ValidationExtensionsResources> Localizer =
            GlobalStringLocalizer.Instance.GetLocalizer<ValidationExtensionsResources>();

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
