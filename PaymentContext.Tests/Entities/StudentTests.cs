using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enum;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void AdicionarAssinatura()
        {
            var subscription = new Subscription(null);
            var student = new Student(new Name("Tuany", "Fortunato do Carmo"),
                                      new Document("40098868896", EDocumentType.CPF),
                                      new Email("tuany.fortunato1990@gmail.com"));
            student.AddSubscription(subscription);
        }
    }
}
