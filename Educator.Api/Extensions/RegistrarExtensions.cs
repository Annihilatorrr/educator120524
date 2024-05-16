using Educator.Api.Registrars;

namespace Educator.Api.Extensions
{
    public static class RegistrarExtensions
    {
        public static void RegisterServices(this WebApplicationBuilder builder, Type scanningType)
        {
            var registrars = GetRegistrars<IWebApplicationBuilderRegistrar>(scanningType);

            foreach (var registrar in registrars)
            {
                registrar.RegisterServices(builder);
            }
        }

        public static void RegisterPipelineComponents(this WebApplication app, Type scanningType)
        {
            var registrars = GetRegistrars<IWebApplicationRegistrar>(scanningType);
            foreach (var registrar in registrars)
            {
                registrar.RegisterPipelineComponents(app);
            }
        }

        //public static async Task EnsureDataBaseExists(this WebApplication app)
        //{
        //    try
        //    {
        //        using var scope = app.Services.CreateScope();
        //        var context = scope.ServiceProvider.GetRequiredService<CleanArchDbContext>();
        //        await DbInitializer.Initialize(context);
        //    }
        //    catch (Exception exc)
        //    {

        //    }
        //}

        private static IEnumerable<T> GetRegistrars<T>(Type scanningType) where T: IRegistrar
        {
            return scanningType.Assembly.GetTypes()
                .Where(t => t.IsAssignableTo(typeof(T)) && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<T>();
        }


    }
}
