using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enum;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        [DataTestMethod]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("123")]
        [DataRow("582562830001170")]
        public void ShouldReturnErrorWhenCNPJIsInvalid(string cpnj)
        {
            var doc = new Document(cpnj, EDocumentType.CNPJ);
            Assert.IsTrue(doc.Invalid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("48093728000131")]
        [DataRow("18472874000107")]
        [DataRow("58818535000154")]
        [DataRow("58256283000117")]
        public void ShouldReturnSuccessWhenCNPJIsValid(string cpnj)
        {
            var doc = new Document(cpnj, EDocumentType.CNPJ);
            Assert.IsTrue(doc.Valid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("123")]
        [DataRow("724710030360")]
        public void ShouldReturnErrorWhenCPFIsInvalid(string cpf)
        {
            var doc = new Document(cpf, EDocumentType.CPF);
            Assert.IsTrue(doc.Invalid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("98436533003")]
        [DataRow("73174065038")]
        [DataRow("72471003036")]
        [DataRow("41515108007")]
        public void ShouldReturnSuccessWhenCPFIsValid(string cpf)
        {
            var doc = new Document(cpf, EDocumentType.CPF);
            Assert.IsTrue(doc.Valid);
        }
    }
}
