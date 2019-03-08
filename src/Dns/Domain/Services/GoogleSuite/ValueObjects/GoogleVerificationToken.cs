namespace Dns.Domain.Services.GoogleSuite
{
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Exceptions;
    using Newtonsoft.Json;

    public class GoogleVerificationToken : StringValueObject<GoogleVerificationToken>
    {
        // https://support.google.com/a/answer/2716802?hl=en
        // The token is a 68-character string that begins with google-site-verification=, followed by 43 additional characters.
        public const int MaxLength = 43;

        public GoogleVerificationToken([JsonProperty("value")] string token) : base(token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new EmptyGoogleVerificationTokenException();
        }
    }
}
