namespace Dns.Tests
{
    using System;
    using AutoFixture;
    using AutoFixture.Dsl;
    using DomainName = DomainName;

   internal static class Customizations
    {
        public static IPostprocessComposer<T> FromFactory<T>(this IFactoryComposer<T> composer, Func<Random, T> factory) =>
            composer.FromFactory<int>(value => factory(new Random(value)));

        public static void CustomizeSecondLevelDomain(this IFixture fixture) =>
            fixture.Customize<SecondLevelDomain>(composer =>
                composer.FromFactory(generator =>
                    new SecondLevelDomain(new string(
                        (char)generator.Next(97, 123), // a-z
                        generator.Next(1, SecondLevelDomain.MaxLength + 1)))));

        public static void CustomizeTopLevelDomain(this IFixture fixture) =>
            fixture.Customize<TopLevelDomain>(composer =>
                composer.FromFactory<int>(value => TopLevelDomain.GetAll()[value % TopLevelDomain.GetAll().Length]));

        public static void CustomizeDomainName(this IFixture fixture) =>
            fixture.Customize<DomainName>(composer => composer.FromFactory(_ =>
                new DomainName(
                    fixture.Create<SecondLevelDomain>(),
                    fixture.Create<TopLevelDomain>())));
    }
}
