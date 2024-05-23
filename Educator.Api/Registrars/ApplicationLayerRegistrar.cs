
using Educator.ExercisesGenerators;

namespace Educator.Api.Registrars;

public class ApplicationLayerRegistrar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IMathDocGeneratorService, MathDocGeneratorService>();
        builder.Services.AddTransient<IDocumentCreator, PdfDocumentCreator>();
    }
}