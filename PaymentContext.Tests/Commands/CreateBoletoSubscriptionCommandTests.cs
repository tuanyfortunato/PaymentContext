using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enum;

namespace PaymentContext.Tests
{
    [TestClass]
    public class CreateBoletoSubscriptionCommandTests
    {
        private readonly CreateBoletoSubscriptionCommand _commandBoleto;

        public CreateBoletoSubscriptionCommandTests(){
            _commandBoleto = new CreateBoletoSubscriptionCommand(){
                FirstName = "Ana",
                LastName = "Silva",
                BarCode = "123456",
                BoletoNumber = "12345678",
                Email = "ana@teste.com.br",
                Document = "02258621456",
                Total = 100,
                TotalPaid = 100,
                Payer = "Ana Silva",
                PayerEmail = "ana@teste.com.br",
                PaidDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddDays(10),
                PayerDocument = "02258621456",
                PayerDocumentType = EDocumentType.CPF,
                PaymentNumber = "123",
                Street = "Rua 01",
                Number = "01",
                NeighBorhood = "Centro",
                City = "São Paulo",
                State = "SP",
                Country = "Brasil",
            };
        }

        [TestMethod]
        public void ShouldReturnErrorWhenNameIsInvalid()
        {
            var commandBoleto = _commandBoleto;
            commandBoleto.FirstName = "";
            commandBoleto.Validate();
            Assert.IsTrue(commandBoleto.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenCommandIsValid()
        {
            var commandBoleto = _commandBoleto;
            commandBoleto.Validate();
            Assert.IsTrue(commandBoleto.Valid);
        }

    }
}