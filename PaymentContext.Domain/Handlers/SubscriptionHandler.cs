using Flunt.Notifications;
using PaymentContext.Shared.Handlers;
using PaymentContext.Domain.Commands;
using PaymentContext.Shared.Commands;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain.Enum;
using PaymentContext.Domain.Entities;
using System;
using PaymentContext.Domain.Services;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable,
                                       IHandler<CreateBoletoSubscriptionCommand>,
                                       IHandler<CreatePayPalSubscriptionCommand>,
                                       IHandler<CreateCreditCardSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService){
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail Fast Validations
            command.Validate();

            if(command.Invalid){
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar a assinatura.");
            }

            //Verificar se documento já está cadastrado
            if(_repository.DocumentExists(command.Document)){
                AddNotification("Document", "Este CPF já está em uso");
            }
            
            //Verificar se e-mail já está cadastrado
            if(_repository.EmailExists(command.Email)){
                AddNotification("Email", "Este email já está em uso");
            }

            //Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Number, EDocumentType.CNPJ);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.NeighBorhood, command.City, command.State, command.Country, command.ZipCode);

            //Gerar Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.BarCode, 
                                            command.BoletoNumber, 
                                            command.PaidDate, 
                                            command.ExpireDate,
                                            command.Total, 
                                            command.TotalPaid, 
                                            command.Payer, 
                                            document, 
                                            address, 
                                            email);

            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Aplicar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Salvar as informações
            _repository.CreateSubscription(student);

            //Enviar e-mail de boas vindas
            _emailService.Send(name.ToString(),student.Email.Address, "Bem vindo ao nosso site", "Sua assinatura foi realizada com sucesso!");

            //Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            //Fail Fast Validations
            command.Validate();

            if(command.Invalid){
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar a assinatura.");
            }

            //Verificar se documento já está cadastrado
            if(_repository.DocumentExists(command.Document)){
                AddNotification("Document", "Este CPF já está em uso");
            }
            
            //Verificar se e-mail já está cadastrado
            if(_repository.EmailExists(command.Email)){
                AddNotification("Email", "Este email já está em uso");
            }

            //Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Number, EDocumentType.CNPJ);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.NeighBorhood, command.City, command.State, command.Country, command.ZipCode);

            //Gerar Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(command.TransactionCode, 
                                            command.PaidDate, 
                                            command.ExpireDate,
                                            command.Total, 
                                            command.TotalPaid, 
                                            command.Payer, 
                                            document, 
                                            address, 
                                            email);

            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Aplicar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Salvar as informações
            _repository.CreateSubscription(student);

            //Enviar e-mail de boas vindas
            _emailService.Send(name.ToString(),student.Email.Address, "Bem vindo ao nosso site", "Sua assinatura foi realizada com sucesso!");

            //Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            //Fail Fast Validations
            command.Validate();

            if(command.Invalid){
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar a assinatura.");
            }

            //Verificar se documento já está cadastrado
            if(_repository.DocumentExists(command.Document)){
                AddNotification("Document", "Este CPF já está em uso");
            }
            
            //Verificar se e-mail já está cadastrado
            if(_repository.EmailExists(command.Email)){
                AddNotification("Email", "Este email já está em uso");
            }

            //Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Number, EDocumentType.CNPJ);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.NeighBorhood, command.City, command.State, command.Country, command.ZipCode);

            //Gerar Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new CreditCardPayment(command.CardHolderName,
                                                command.CardNumber,
                                                command.LastTransactionNumber,
                                                command.PaidDate, 
                                                command.ExpireDate,
                                                command.Total, 
                                                command.TotalPaid, 
                                                command.Payer, 
                                                document, 
                                                address, 
                                                email);

            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Aplicar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Salvar as informações
            _repository.CreateSubscription(student);

            //Enviar e-mail de boas vindas
            _emailService.Send(name.ToString(),student.Email.Address, "Bem vindo ao nosso site", "Sua assinatura foi realizada com sucesso!");

            //Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }
    }
}