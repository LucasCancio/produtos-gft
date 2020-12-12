using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProdutosGFT.Data;

namespace ProdutosGFT.Server.Util
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<ProdutosGFTDbContext>())
                {
                    appContext.Database.Migrate();
                }
            }
            return host;
        }
    }
}