namespace Dns.Tests.Infrastructure
{
    using System;
    using System.Linq;
    using System.Net;
    using AutoFixture;
    using AutoFixture.Dsl;
    using Domain.Services.GoogleSuite;
    using Domain.Services.Manual;
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
                        generator.Next(1, SecondLevelDomain.MaxLength)))));

        public static void CustomizeTopLevelDomain(this IFixture fixture) =>
            fixture.Customize<TopLevelDomain>(composer =>
                composer.FromFactory<int>(value => TopLevelDomain.GetAll()[value % TopLevelDomain.GetAll().Length]));

        public static void CustomizeDomainName(this IFixture fixture) =>
            fixture.Customize<DomainName>(composer => composer.FromFactory(_ =>
                new DomainName(
                    fixture.Create<SecondLevelDomain>(),
                    fixture.Create<TopLevelDomain>())));

        public static void CustomizeRecordType(this IFixture fixture) =>
            fixture.Customize<RecordType>(composer =>
                composer.FromFactory<int>(value => RecordType.GetAll()[value % RecordType.GetAll().Length]));

        public static void CustomizeRecordLabel(this IFixture fixture) =>
            fixture.Customize<RecordLabel>(composer =>
                composer.FromFactory(generator =>
                    new RecordLabel(new string(
                        (char) generator.Next(97, 123), // a-z
                        generator.Next(1, RecordLabel.MaxLength)))));

        public static void CustomizeRecordValue(this IFixture fixture) =>
            fixture.Customize<RecordValue>(composer =>
                composer.FromFactory(generator =>
                    new RecordValue(new string(
                        (char) generator.Next(97, 123), // a-z
                        generator.Next(1, RecordValue.MaxLength)))));

        public static void CustomizeTimeToLive(this IFixture fixture) =>
            fixture.Customize<TimeToLive>(composer =>
                composer.FromFactory(generator =>
                    new TimeToLive(generator.Next(0, TimeToLive.MaxValue))));

        public static void CustomizeRecord(this IFixture fixture) =>
            fixture.Customize<Record>(composer => composer.FromFactory(_ =>
            {
                var recordType = fixture.Create<RecordType>();
                return new Record(
                    recordType,
                    fixture.Create<TimeToLive>(),
                    fixture.Create<RecordLabel>(),
                    fixture.CreateRecordValue(recordType));
            }));

    private static RecordValue CreateRecordValue(this IFixture fixture, RecordType recordType)
        {
            if (recordType == RecordType.ns)
                return new RecordValue($"{fixture.Create<RecordValue>()}.");

            if (recordType == RecordType.a)
                return new RecordValue(fixture.Create<IPAddress>().ToString());

            if (recordType == RecordType.cname)
                return new RecordValue($"{fixture.Create<RecordValue>()}.");

            if (recordType == RecordType.mx)
                return new RecordValue($"10 {fixture.Create<RecordValue>()}.");

            if (recordType == RecordType.txt)
                return fixture.Create<RecordValue>();

            if (recordType == RecordType.spf)
                return fixture.Create<RecordValue>();

            throw new ArgumentException("Invalid RecordType.", nameof(recordType));
        }

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

        public static void CustomizeManualLabel(this IFixture fixture) =>
            fixture.Customize<ManualLabel>(composer =>
                composer.FromFactory(generator =>
                    new ManualLabel(new string(
                        (char)generator.Next(97, 123), // a-z
                        generator.Next(1, ManualLabel.MaxLength)))));
    }
}
