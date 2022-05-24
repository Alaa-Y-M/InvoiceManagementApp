using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceManagementApp.Application.Common.Interfaces;
using InvoiceManagementApp.Application.Invoices.Commands;
using InvoiceManagementApp.Application.Invoices.Queries;
using InvoiceManagementApp.Application.Invoices.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagementApp.API.Controllers
{
    [Authorize]
    public class InvoiceController : ApiController
    {
        private readonly ICurrentUserService service;

        public InvoiceController(ICurrentUserService service)
        {
            this.service = service;
        }
        [HttpPost]
        public async Task<ActionResult<int>>Create(CreateInvoiceCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpGet]
        public async Task<IList<InvoiceVm>> Get()
        {
            return await Mediator.Send(new GetUserInvoicesQuery { UserID=service.UserId});
        }
    }
}
