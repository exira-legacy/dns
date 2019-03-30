namespace Dns.Api.Infrastructure.Localization
{
    using System;
    using Microsoft.Extensions.Localization;

    public class SharedStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly ResourceManagerStringLocalizerFactory _resourceManagerStringLocalizerFactory;

        public SharedStringLocalizerFactory(ResourceManagerStringLocalizerFactory resourceManagerStringLocalizerFactory)
            => _resourceManagerStringLocalizerFactory = resourceManagerStringLocalizerFactory;

        public IStringLocalizer Create(Type resourceSource)
            => resourceSource == typeof(SharedResources)
                ? _resourceManagerStringLocalizerFactory.Create(resourceSource)
                : new SharedStringLocalizer(_resourceManagerStringLocalizerFactory.Create(resourceSource));

        public IStringLocalizer Create(string baseName, string location) => throw new NotSupportedException();
    }
}
