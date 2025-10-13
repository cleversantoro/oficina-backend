using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Oficina.Financeiro.Infrastructure;

public class FinanceiroDbContextFactory : IDesignTimeDbContextFactory<FinanceiroDbContext>
{
    private const string DefaultConnection = "server=localhost;port=3306;database=oficina_db;user=oficina;password=oficina123";
    private static readonly MySqlServerVersion ServerVersion = new(new Version(8, 0, 36));

    public FinanceiroDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FinanceiroDbContext>();
        var connectionString = Environment.GetEnvironmentVariable("OFICINA_CONNECTION_STRING") ?? DefaultConnection;

        optionsBuilder.UseMySql(connectionString, ServerVersion);

        return new FinanceiroDbContext(optionsBuilder.Options);
    }
}

