using Microsoft.Extensions.DependencyInjection;

namespace ElearningDemoRepositories.Test;

public static class Utilities
{
    public static DbContextOptions<ElearningDemoContext> TestDbContextOptions()
    {
        // Create a new service provider to create a new in-memory database.
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        // Create a new options instance using an in-memory database and 
        // IServiceProvider that the context should resolve all of its 
        // services from.
        var builder = new DbContextOptionsBuilder<ElearningDemoContext>()
            .UseInMemoryDatabase("InMemoryDb")
            .UseInternalServiceProvider(serviceProvider);

        return builder.Options;
    }
}

