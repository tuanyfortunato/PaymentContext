using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void AdicionarAssinatura()
        {
            var subscription = new Subscription(null);
            var student = new Student("Tuany", "Fortunato do Carmo", "472609889", "tuany.fortunato1990@gmail.com");
            student.AddSubscription(subscription);
        }
    }
}
