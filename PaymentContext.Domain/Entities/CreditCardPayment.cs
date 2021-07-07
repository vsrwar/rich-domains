using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class CreditCardPayment : Payment
    {
        public string CardHolderName { get; private set; }
        public string CardNumeber { get; private set; }
        public string LastTransactionNumber { get; private set; }
        
        public CreditCardPayment(
            string cardHolderName,
            string cardNumeber,
            string lastTransactionNumber,
            string payer,
            Document document,
            Email email,
            Address address,
            DateTime paidDate,
            DateTime expireDate,
            decimal total,
            decimal totalPaid)
            : base(
                  payer,
                  document,
                  email,
                  address,
                  paidDate,
                  expireDate,
                  total,
                  totalPaid)
        {
            CardHolderName = cardHolderName;
            CardNumeber = cardNumeber;
            LastTransactionNumber = lastTransactionNumber;
        }
    }
}
