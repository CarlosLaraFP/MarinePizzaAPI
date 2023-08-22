namespace MarinePizza.Data;

public static class Extensions
{
    // The CreateDbIfNotExists method is defined as an extension of IHost.
    public static void CreateDbIfNotExists(this IHost host)
    {
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<PizzaContext>();
                // If a database doesn't exist, EnsureCreated creates a new database.
                // The new database isn't configured for migrations, so use this method with caution.
                context.Database.EnsureCreated();
                DbInitializer.Initialize(context);
            }
        }
    }
}

