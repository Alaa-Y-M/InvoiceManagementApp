using FluentValidation;
using InvoiceManagementApp.Application.Invoices.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceManagementApp.Application.Invoices.Validators
{
    class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceCommandValidator()
        {
            RuleFor(v => v.AmountPaid).NotNull();
            RuleFor(v => v.To).NotNull().MinimumLength(3);
            RuleFor(v => v.From).NotNull().MinimumLength(3);
            RuleFor(v => v.Date).NotNull();
            RuleFor(v => v.InvoiceItems).SetValidator(new MustHaveInvoiceItemPropertyValidator());
        }
    }
}
