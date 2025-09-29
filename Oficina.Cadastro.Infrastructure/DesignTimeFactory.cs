using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Oficina.Cadastro.Infrastructure.Persistence;

namespace Oficina.Cadastro.Infrastructure;

public class DesignTimeFactory : IDesignTimeDbContextFactory<CadastroDbContext>
{
    public CadastroDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var connectionString = configuration.GetConnectionString("OficinaDb")
            ?? configuration.GetConnectionString("Default")
            ?? configuration["ConnectionStrings:OficinaDb"]
            ?? configuration["ConnectionStrings:Default"];

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Connection string 'OficinaDb' or 'Default' not found for design-time services.");
        }

        var builder = new DbContextOptionsBuilder<CadastroDbContext>();
        builder.UseNpgsql(connectionString, sql =>
        {
            sql.MigrationsAssembly(typeof(CadastroDbContext).Assembly.FullName);
            sql.MigrationsHistoryTable("__EFMigrationsHistory", CadastroDbContext.Schema);
        });

        return new CadastroDbContext(builder.Options);
    }

    private static IConfiguration BuildConfiguration()
    {
        var configurationBuilder = new ConfigurationBuilder();

        var currentDirectory = Directory.GetCurrentDirectory();
        configurationBuilder.SetBasePath(currentDirectory);

        configurationBuilder
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true);

        var solutionRoot = FindSolutionRoot(currentDirectory);
        if (solutionRoot is not null)
        {
            var endpointsDir = Path.Combine(solutionRoot, "src", "Oficina.Cadastro.Endpoints");
            if (Directory.Exists(endpointsDir))
            {
                configurationBuilder
                    .AddJsonFile(Path.Combine(endpointsDir, "appsettings.json"), optional: true)
                    .AddJsonFile(Path.Combine(endpointsDir, "appsettings.Development.json"), optional: true);
            }
        }

        configurationBuilder.AddEnvironmentVariables();

        return configurationBuilder.Build();
    }

    private static string? FindSolutionRoot(string startDirectory)
    {
        var directory = new DirectoryInfo(startDirectory);

        while (directory is not null)
        {
            var solutionFile = Path.Combine(directory.FullName, "src", "Oficina.Cadastro.sln");
            if (File.Exists(solutionFile))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        return null;
    }
}
