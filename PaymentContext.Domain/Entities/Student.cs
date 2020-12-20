using System;
using System.Collections.Generic;
using System.Linq;

namespace PaymentContext.Domain.Entities
{
    public class Student
    {
        public Student(string firstName, string lastName, string document, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();

            if (string.IsNullOrEmpty(firstName))
            {
                throw new Exception("Nome inválido");
            }

        }
        private IList<Subscription> _subscriptions;
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Document { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions
        {
            get
            {
                return _subscriptions.ToArray();
            }
        }

        public void AddSubscription(Subscription subscription)
        {
            //Cancela todas as outras assinaturas, e coloca 
            //esta como a principal

            foreach (var sub in this._subscriptions)
                sub.Inactivate();

            _subscriptions.Add(subscription);
        }
    }
}