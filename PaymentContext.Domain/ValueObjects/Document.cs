using PaymentContext.Domain.Enum;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Document : ValueObject
    {
        public Document(string number, EDocumentType documentType)
        {
            Number = number;
            DocumentType = documentType;
        }

        public string Number { get; private set; }

        public EDocumentType DocumentType { get; private set; }
    }
}