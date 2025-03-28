using System;
using System.Collections.Generic;

namespace PandaGo
{
    public interface IPayment
    {
        List<string> Pay();
    }
    public class CashPayment : IPayment
    {
        public List<string> Pay()
        {
            return new List<string> { "Cash" };
        }
    }
    public class CreditCardPayment : IPayment
    {
        private string CardNumber;

        public CreditCardPayment(string cardNumber)
        {
            CardNumber = cardNumber;
        }

        public List<string> Pay()
        {
            return new List<string> { "Credit", CardNumber.ToString() };
        }
    }
    public class PaymentGenerator
    {
        private static Random random = new Random();

        public static IPayment GenerateRandomPayment()
        {
            if (random.Next(2) == 0)
            {
                return new CashPayment();
            }
            else
            {
                string cardNumber = $"{random.Next(1000, 9999)}-{random.Next(1000, 9999)}-{random.Next(1000, 9999)}-{random.Next(1000, 9999)}";
                return new CreditCardPayment(cardNumber);
            }
        }
    }
}
