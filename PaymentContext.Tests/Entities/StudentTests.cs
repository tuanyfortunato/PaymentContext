using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enum;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Email _email;
        private readonly Address _address;
        private readonly Student _student;
        private readonly Subscription _subscription;

        public StudentTests(){
            _name = new Name("Maria", "Joaquina");
            _document = new Document("41515108007", EDocumentType.CPF);
            _address = new Address("Rua Almeida", "100", "Centro", "São Paulo", "SP", "Brasil", "02200000");
            _email = new Email("maria@teste.com.br");

            _student = new Student(_name, _document, _email);
            _subscription = new Subscription(null);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            var pay = new BoletoPayment("123456","1234567",DateTime.Now, DateTime.Now.AddDays(5), 100,100,"Maria Joaquina", _document, _address, _email);
            _subscription.AddPayment(pay);

            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            var pay = new BoletoPayment("123456","1234567",DateTime.Now, DateTime.Now.AddDays(5), 100,100,"Maria Joaquina", _document, _address, _email);
            _subscription.AddPayment(pay);

            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Valid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadSubscriptionHasNoPayment()
        {
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }
    }
}
