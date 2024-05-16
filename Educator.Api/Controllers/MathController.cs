using Educator.ExercisesGenerators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using org.matheval;

namespace Educator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MathController : ControllerBase
    {
        private readonly IMathDocGeneratorService _mathDocGenerator;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public MathController(IMathDocGeneratorService mathDocGenerator, IWebHostEnvironment hostingEnvironment)
        {
            _mathDocGenerator = mathDocGenerator;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("elementary/binaryexercises")]
        public async Task<IActionResult> GetExercise(int numberOfExercises)
        {
            //    var FileProvider = new PhysicalFileProvider(
            //            Path.Combine(Directory.GetCurrentDirectory(), @"MyStaticFiles")),
            //        RequestPath = new PathString("/StaticFiles")
            //});

            IntegerExpressionConfig config = new IntegerExpressionConfig();
            string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, @"HtmlTemplates\TaskHeaderTemplate.html");
            string content = await System.IO.File.ReadAllTextAsync(filePath);
            var expressionsContent = await _mathDocGenerator.GenerateExpressions(new IntegerExpressionConfig()
            {
                ExpressionsCount = 8,
                AllowedSigns = new List<string>() { "*" },
                MinOperandValue = 0,
                MaxOperandValue = 4,
                MaxResultValue = 100,
                Columns = 4,
                InvisibleOperand = false,
                WithParenthesis = false
            }, content);

            return Ok(expressionsContent);
        }
    }
}
