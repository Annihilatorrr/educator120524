
using Educator.Api.Extensions;

namespace Educator.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.RegisterServices(typeof(Program));


            var app = builder.Build();

            app.RegisterPipelineComponents(typeof(Program));

            app.Run();
        }
    }
}
