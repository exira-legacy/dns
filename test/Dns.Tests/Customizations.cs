namespace Dns.Tests
{
    using System;
    using System.Linq;
    using AutoFixture;
    using AutoFixture.Dsl;
    using Domain.Services.GoogleSuite;
    using DomainName = DomainName;

    internal static class Customizations
    {
        public static IPostprocessComposer<T>
            FromFactory<T>(this IFactoryComposer<T> composer, Func<Random, T> factory) =>
            composer.FromFactory<int>(value => factory(new Random(value)));

        public static void CustomizeSecondLevelDomain(this IFixture fixture) =>
            fixture.Customize<SecondLevelDomain>(composer =>
                composer.FromFactory(generator =>
                    new SecondLevelDomain(new string(
                        (char) generator.Next(97, 123), // a-z
                        generator.Next(1, SecondLevelDomain.MaxLength + 1)))));

        public static void CustomizeTopLevelDomain(this IFixture fixture) =>
            fixture.Customize<TopLevelDomain>(composer =>
                composer.FromFactory<int>(value => TopLevelDomain.GetAll()[value % TopLevelDomain.GetAll().Length]));

        public static void CustomizeDomainName(this IFixture fixture) =>
            fixture.Customize<DomainName>(composer => composer.FromFactory(_ =>
                new DomainName(
                    fixture.Create<SecondLevelDomain>(),
                    fixture.Create<TopLevelDomain>())));

        public static void CustomizeGoogleVerificationToken(this IFixture fixture)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            fixture.Customize<GoogleVerificationToken>(composer =>
                composer.FromFactory(generator =>
                    new GoogleVerificationToken(
                        new string(Enumerable
                            .Range(1, GoogleVerificationToken.MaxLength)
                            .Select(_ => chars[generator.Next(chars.Length)]).ToArray()))));
        }
    }
}
