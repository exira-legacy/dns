namespace Dns.Api.Domain.Requests
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Dns.Domain.Commands;
    using FluentValidation;

    public class RemoveServiceRequest
    {
        /// <summary>Second level domain of the domain to remove a domain service from.</summary>
        [Required]
        [Display(Name = "Second Level Domain")]
        public string SecondLevelDomain { get; set; }

        /// <summary>Top level domain of the domain to remove a domain service from.</summary>
        [Required]
        [Display(Name = "Top Level Domain")]
        public string TopLevelDomain { get; set; }

        /// <summary>Unique service id to remove.</summary>
        [Required]
        [Display(Name = "Service Id")]
        public Guid? ServiceId { get; set; }
    }

    public class RemoveServiceRequestValidator : AbstractValidator<RemoveServiceRequest>
    {
        public RemoveServiceRequestValidator()
        {
            RuleFor(x => x.SecondLevelDomain)
                .Required()
                .ValidHostName()
                .MaxLength(SecondLevelDomain.MaxLength);

            RuleFor(x => x.TopLevelDomain)
                .Required()
                .ValidTopLevelDomain();

            RuleFor(x => x.ServiceId)
                .Required();
        }
    }

    public static class RemoveServiceRequestMapping
    {
        public static RemoveService Map(RemoveServiceRequest message)
        {
            return new RemoveService(
                new DomainName(
                    new SecondLevelDomain(message.SecondLevelDomain),
                    TopLevelDomain.FromValue(message.TopLevelDomain)),
                new ServiceId(message.ServiceId.Value));
        }
    }
}
