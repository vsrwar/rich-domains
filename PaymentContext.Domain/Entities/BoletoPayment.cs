using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class BoletoPayment : Payment
    {
        public string BoletoNumber { get; private set; }
        public string BarCode { get; private set; }

        public BoletoPayment(
            string boletoNumber,
            string barCode,
            string payer,
            Document document,
            Email email,
            Address address,
            DateTime paidDate,
            DateTime expireDate,
            decimal total,
            decimal totalPaid)
            : base(payer,
                   document,
                   email,
                   address,
                   paidDate,
                   expireDate,
                   total,
                   totalPaid)
        {
            BoletoNumber = boletoNumber;
            BarCode = barCode;
        }
    }
}
