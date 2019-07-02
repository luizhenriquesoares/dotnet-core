using System;
using Microsoft.EntityFrameworkCore;
using Web.Api.Modules.Auth.Domain;

namespace Web.Api.Infrastructure.Database {
    public interface IApplicationDbContext {
        DbSet<User> User { get; }
        int SaveChanges ();
    }
}