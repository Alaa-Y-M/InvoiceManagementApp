using InvoiceManagementApp.API.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using InvoiceManagementApp.Application.Common.Interfaces;
using InvoiceManagementApp.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using InvoiceManagementApp.Domain.Common;
using System;

namespace InvoiceManagementApp.API.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly ICurrentUserService service;

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUserService service) : base(options, operationalStoreOptions)
        {
            this.service = service;
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (var entry in ChangeTracker.Entries<AuditEntity>())
            {
                switch (entry.State)
                {

                    case EntityState.Added:
                        entry.Entity.CreatedBy = service.UserId;
                        entry.Entity.Created = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = service.UserId;
                        entry.Entity.LastModified = DateTime.UtcNow;
                        break;
                }

            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
