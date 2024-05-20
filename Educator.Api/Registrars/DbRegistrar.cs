using Educator.Dal;
using Microsoft.EntityFrameworkCore;

namespace Educator.Api.Registrars
{
    public class DbRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var cs = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseNpgsql(cs);
            });

            //builder.Services.AddIdentityCore<IdentityUser>(options =>
            //    {
            //        options.Password.RequireDigit = false;
            //        options.Password.RequiredLength = 5;
            //        options.Password.RequireLowercase = false;
            //        options.Password.RequireUppercase = false;
            //        options.Password.RequireNonAlphanumeric = false;
            //        options.ClaimsIdentity.UserIdClaimType = "IdentityId";
            //    })
            //    .AddEntityFrameworkStores<DataContext>();

        }
    }
}
