using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Domain.Common;
using Webshop.Customer.Domain;

namespace Webshop.Customer.Application.Features.Requests;

public class CreateCustomerRequest
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Email { get; set; }

    public class Validator : AbstractValidator<CreateCustomerRequest>
    {
        public Validator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(Name)).Code.Value);
            RuleFor(r => r.Address).NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(Name)).Code.Value);
            RuleFor(r => r.City).NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(Name)).Code.Value);
            RuleFor(r => r.Region).NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(Name)).Code.Value);
            RuleFor(r => r.PostalCode).NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(Name)).Code.Value);
            RuleFor(r => r.Country).NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(Name)).Code.Value);
            RuleFor(r => r.Email).NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(Name)).Code.Value);
            RuleFor(r => r.Email).EmailAddress().WithMessage(Errors.General.UnexpectedValue(nameof(Email)).Code.Value);
        }
    }
}
