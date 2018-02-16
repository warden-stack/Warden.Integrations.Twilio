using System.Threading.Tasks;
using Twilio;
using Twilio.Clients;
using Twilio.Http;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Warden.Integrations.Twilio
{
    /// <summary>
    /// Custom Twilio SMS service.
    /// </summary>
    public interface ITwilioService
    {
        /// <summary>
        /// Sends SMS.
        /// </summary>
        /// <param name="sender">Sender phone number.</param>
        /// <param name="receiver">Receiver phone number.</param>
        /// <param name="message">Body of the SMS.</param>
        /// <returns></returns>
        Task SendSmsAsync(string sender, string receiver, string message);
    }


    /// <summary>
    /// Default implementation of the ITwilioService based on Twilio library.
    /// </summary>
    public class TwilioService : ITwilioService
    {
        public TwilioService(string accountSid, string authToken)
        {
            TwilioClient.Init(accountSid, authToken);
        }

        public async Task SendSmsAsync(string sender, string receiver, string message)
            => await MessageResource.CreateAsync(
                    to: new PhoneNumber(receiver),
                    from: new PhoneNumber(sender),
                    body: message);
    }
}