using System;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class PayPalSubscriptionHandler : IHandler<CreatePayPalSubscriptionCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailService;

        public PayPalSubscriptionHandler(IStudentRepository studentRepository, IEmailService emailService)
        {
            _studentRepository = studentRepository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            if(!command.Validate())
                return new CommandResult(false, "Error on request");
            
            if (_studentRepository.DocumentExists(command.Document))
                return new CommandResult(false, "Document already exists");
            
            if (_studentRepository.EmailExists(command.Email))
                return new CommandResult(false, "E-mail already exists");

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, command.PayerDocumentType);
            var email = new Email(command.Email);
            var address = new Address(
                command.Street,
                command.Complement,
                command.Number,
                command.Neighborhood,
                command.City,
                command.State,
                command.Country,
                command.ZipCode);

            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(
                command.TransactionCode,
                command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                new Email(command.PayerEmail),
                address,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid);

            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            _studentRepository.CreateSubscription(student);

            _emailService.Send(student.Name.ToString(), student.Email.Address, "Welcome", "Subscription done");

            return new CommandResult(true, "OK");
        }
    }
}
