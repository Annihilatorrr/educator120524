namespace Educator.Api.Registrars
{
    public class MvcRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsAllowAll",
                    x =>
                    {
                        x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });
            builder.Services.AddSwaggerGen();
            //builder.Services.AddApiVersioning(config =>
            //{
            //    config.DefaultApiVersion = new ApiVersion(1, 0);
            //    config.AssumeDefaultVersionWhenUnspecified = true;
            //    config.ReportApiVersions = true;
            //    config.ApiVersionReader = new UrlSegmentApiVersionReader();
            //});

            //builder.Services.AddVersionedApiExplorer(config =>
            //{
            //    config.GroupNameFormat = "'v'VVV";
            //    config.SubstituteApiVersionInUrl = true;
            //});
            //builder.Services.AddEndpointsApiExplorer();
        }
    }
}
