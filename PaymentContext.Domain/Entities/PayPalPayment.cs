using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class PayPalPayment : Payment
    {
        public string TransactionCode { get; private set; }

        public PayPalPayment(
            string transactionCode,
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
            TransactionCode = transactionCode;
        }
    }
}
