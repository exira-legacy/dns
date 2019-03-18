namespace Dns
{
    using System.Diagnostics.CodeAnalysis;
    using Domain.Services.GoogleSuite;
    using Domain.Services.Manual;

    public class InvalidServiceTypeException : EnumerationException
    {
        public InvalidServiceTypeException(string message, string paramName, object value, string type) :
            base(message, paramName, value, type) { }
    }

    public class ServiceType : Enumeration<ServiceType, string, InvalidServiceTypeException>
    {
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public static readonly ServiceType
            manual = new ServiceType(ManualService.ServiceType, ManualService.ServiceName),
            googlesuite = new ServiceType(GoogleSuiteService.ServiceType, GoogleSuiteService.ServiceName);

        private ServiceType(
            string type,
            string displayName)
            : base(type?.ToLowerInvariant(), displayName) { }
    }
}
