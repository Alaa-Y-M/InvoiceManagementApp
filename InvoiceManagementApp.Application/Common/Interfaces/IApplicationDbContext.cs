using InvoiceManagementApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceManagementApp.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Invoice> Invoices { set; get; }
        DbSet<InvoiceItem> InvoiceItems { set; get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
