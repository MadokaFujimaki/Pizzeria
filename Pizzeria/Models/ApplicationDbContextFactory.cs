using Pizzeria.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Models
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer("Server = tcp:pizzeriasql.database.windows.net, 1433; Initial Catalog = pizzeriasqldb; Persist Security Info = False; User ID = madoka; Password =Pa$$w0rd; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
            var dbContext = new ApplicationDbContext(builder.Options);
            return dbContext;
        }
    }
}
