using System.Text;
using Educator.ExercisesGenerators;
using Educator.Shared.Models.Contracts;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace Educator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MathController : ControllerBase
    {
        private readonly IMathDocGeneratorService _mathDocGenerator;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IDocumentCreator _documentCreator;
        public MathController(IMathDocGeneratorService mathDocGenerator, IWebHostEnvironment hostingEnvironment, IDocumentCreator documentCreator)
        {
            _mathDocGenerator = mathDocGenerator;
            _hostingEnvironment = hostingEnvironment;
            _documentCreator = documentCreator;
        }

        [HttpGet("elementary/binaryexercises")]
        public async Task<IActionResult> GetExercise([FromQuery] BinaryAlgebraicExpressionRequest expressionsResponse)
        {
            //    var FileProvider = new PhysicalFileProvider(
            //            Path.Combine(Directory.GetCurrentDirectory(), @"MyStaticFiles")),
            //        RequestPath = new PathString("/StaticFiles")
            //});

            IntegerExpressionConfig config = new IntegerExpressionConfig();
            string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, @"HtmlTemplates\TaskHeaderTemplate.html");
            string content = await System.IO.File.ReadAllTextAsync(filePath);
            var expressionsContent = await _mathDocGenerator.GenerateExpressions(new AlgebraicExpressionGeneratorConfig()
            {
                Count = 8,
                AllowedOperators = ["*"],
                MinOperand = 1,
                MaxOperand = 4,
                MaxAnswer = 100,
            }, content);

            StringBuilder contentBuilder = new StringBuilder();
            foreach (var expr in expressionsContent)
            {
                contentBuilder.AppendLine($"{expr.FirstOperand} {expr.Operator} {expr.SecondOperand} = ");
            }

            string fileName = $"educator_{DateTime.Now:yyyy_dd_MM-HH_mm_ss_fff}.pdf";
            var file = _documentCreator.Save(fileName, contentBuilder.ToString());

            return File(file,"application/pdf", fileName);
        }
    }
}
