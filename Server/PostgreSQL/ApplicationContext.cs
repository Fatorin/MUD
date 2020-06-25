using System;
using System.Collections.Generic;
using System.Text;
using Common.Model.UserComponents;
using Microsoft.EntityFrameworkCore;

namespace Server.PostgreSQL
{
    public class ApplicationContext : DbContext
    {
        private readonly string ConnectionString = "Host=localhost;Database=Test;Username=op;Password=Op@1234";
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql(ConnectionString, options=> options.SetPostgresVersion(new Version(9,6)));
        // PGSQL版本確認
        //=> options.UseNpgsql(ConnectionString);

        public DbSet<User> Users { get; set; }
    }
}
