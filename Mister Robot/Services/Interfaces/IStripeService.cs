using Stripe.Checkout;

namespace Mister_Robot.Services.Interfaces
{
   public interface IStripeService
   {

	   string CreateCheckoutSession(List<SessionLineItemOptions> lineItems, string successUrl, string cancelUrl);

   }
}
