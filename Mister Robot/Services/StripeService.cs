using Stripe;
using Stripe.Checkout;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Services
{
	public class StripeService : IStripeService
	{
		public StripeService(IConfiguration configuration)
		{
			StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
		}

		public string CreateCheckoutSession(List<SessionLineItemOptions> lineItems, string successUrl, string cancelUrl)
		{
			var options = new SessionCreateOptions
			{
				PaymentMethodTypes = new List<string>
				{
					"card",
				},
				LineItems = lineItems,
				Mode = "payment",
				SuccessUrl = successUrl,
				CancelUrl = cancelUrl
			};

			var service = new SessionService();
			var session = service.Create(options);

			return session.Url;
		}
	}
}