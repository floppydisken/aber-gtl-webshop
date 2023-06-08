using EnsureThat;

namespace Webshop.Payment.Api.Models
{
    /// <summary>
    /// This is the entity payment
    /// </summary>
    public class PaymentRequest
    {
        public PaymentRequest(int amount, string cardnumber, string expirationDate, int cvc)
        {
            Ensure.That(amount, nameof(amount)).IsGt<int>(0);
            Ensure.That(cardnumber, nameof(cardnumber)).IsNotNullOrEmpty();
            Ensure.That(expirationDate, nameof(expirationDate)).IsNotNullOrEmpty();
            Ensure.That(cvc, nameof(cvc)).IsGte(100);
            Ensure.That(cvc, nameof(cvc)).IsLt(1000);
            //set the properties
            CardNumber = cardnumber;
            ExpirationDate = expirationDate;
            CVC = cvc;
            Amount = amount;
        }

        public PaymentRequest()
        {
        }
        
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public int CVC { get; set; }
        public int Amount { get; set; }
    }
}
