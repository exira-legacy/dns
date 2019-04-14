namespace Dns.Tests.Infrastructure
{
    using System;
    using AutoFixture.Dsl;

    public static partial class Customizations
    {
        public static IPostprocessComposer<T>
            FromFactory<T>(this IFactoryComposer<T> composer, Func<Random, T> factory) =>
            composer.FromFactory<int>(value => factory(new Random(value)));
    }
}
