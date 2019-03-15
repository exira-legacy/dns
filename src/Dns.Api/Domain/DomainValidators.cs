namespace Dns.Api.Domain
{
    using System;
    using System.Linq;
    using FluentValidation;

    public static class DomainValidators
    {
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
                .Must(property => TopLevelDomain.TryParse(property, out _))
                .WithMessage($"Top Level Domain must be one of {string.Join(", ", TopLevelDomain.GetAll().Select(x => $"'{x.Value}'"))}.");
    }
}
