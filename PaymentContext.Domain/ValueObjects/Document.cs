using Flunt.Validations;
using PaymentContext.Domain.Enum;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Document : ValueObject
    {
        public Document(string number, EDocumentType type)
        {
            Number = number;
            Type = type;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Number, "Document.Number", "Documento nulo")
                .IsTrue(Validate(), "Document.Number", "Documento inválido")
            );
        }

        public string Number { get; private set; }

        public EDocumentType Type { get; private set; }

        public bool Validate(){
            if(Type == EDocumentType.CNPJ && !string.IsNullOrEmpty(Number) && Number.Length == 14)
                return true;

            if(Type == EDocumentType.CPF && !string.IsNullOrEmpty(Number) && Number.Length == 11)
                return true;

            return false;
        }
    }
}