using System.Reflection;
using System.Text;
using Educator.ConsoleApp;
using org.matheval;
using System.IO;
using HTMLQuestPDF.Extensions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Educator.ExercisesGenerators
{
    public class MathDocGeneratorService : IMathDocGeneratorService
    {
        public async Task<string> GenerateExpressions(IntegerExpressionConfig config, string exerciseHeaderTemplate)
        {
            List<string> expressions = GetExpressions(config, config.ExpressionsCount);

            Exercise exercise = new Exercise(exerciseHeaderTemplate)
            {
                Header = config.InvisibleOperand ? "Подставь числа" : "Реши примеры",
                Content = GetContent(expressions, config.Columns)
            };
            QuestPDF.Settings.License = LicenseType.Community;
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Content().Column(col =>
                    {
                        col.Item().HTML(handler =>
                        {
                            handler.SetHtml(exercise.Build());
                        });
                    });
                });
            }).GeneratePdf($"output/educator_{DateTime.Now:yyyy_dd_MM-HH_mm_ss_fff}.pdf");
            //var generatedPdf = PdfGenerator.GeneratePdf(htmlStringSb.ToString(), PageSize.A4, 10, css);
            //using var ms = new MemoryStream();
            //generatedPdf.Save(ms);
            //Directory.CreateDirectory("output");
            //await using var fs = File.Create($"output/educator_{DateTime.Now:yyyy_dd_MM-HH_mm_ss_fff}.pdf");
            //ms.Position = 0;
            //await ms.CopyToAsync(fs);

            return exercise.Build();
        }

        public async Task<string> GenerateExpressionsMulBy10(IntegerExpressionConfig config, string exerciseHeaderTemplate)
        {
            string exerciseHeaderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, exerciseHeaderTemplate);
            var exerciseHeader = await File.ReadAllTextAsync(exerciseHeaderPath);

            List<string> expressions = GetRoundNumber10Expressions(config, config.ExpressionsCount);

            Exercise exercise = new Exercise(exerciseHeader)
            {
                Header = config.InvisibleOperand ? "Подставь числа" : "Реши примеры",
                Content = GetContent(expressions, config.Columns)
            };

            return exercise.Build();
        }

        private List<string> GetExpressions(IntegerExpressionConfig config, int expressionsCount)
        {
            List<string> expressions = new List<string>();
            HashSet<string> nonDistinctExpressions = new HashSet<string>();

            while (true)
            {
                var expressionAndResult = GetExpression(config);
                if (nonDistinctExpressions.Add(expressionAndResult.expression.ToString()))
                {
                    expressions.Add(config.InvisibleOperand ? $"<span style=\"width:60px;\">{expressionAndResult.expression}</span> = {expressionAndResult.result}" :
                    $"<span style=\"width:50px;\">{expressionAndResult.expression}</span> = ");

                    if (expressions.Count() == expressionsCount)
                    {
                        return expressions;
                    }
                }
            }
        }

        private List<string> GetRoundNumber10Expressions(IntegerExpressionConfig config, int expressionsCount)
        {
            List<string> expressions = new List<string>();
            HashSet<string> nonDistinctExpressions = new HashSet<string>();

            while (true)
            {
                var expressionAndResult = GetRoundNumber10Expression(config);
                if (nonDistinctExpressions.Add(expressionAndResult.expression.ToString()))
                {
                    expressions.Add(config.InvisibleOperand ? $"<span style=\"width:60px;\">{expressionAndResult.expression}</span> = {expressionAndResult.result}" :
                    $"<span style=\"width:50px;\">{expressionAndResult.expression}</span> = ");

                    if (expressions.Count() == expressionsCount)
                    {
                        return expressions;
                    }
                }
            }
        }
        public async Task<string> Generate2LineSegments(int macLength1, int maxLength2)
        {
            Random rnd = new Random();
            int line1Length = rnd.Next(2, macLength1 + 1);
            bool less = rnd.Next(0, 2) == 0;
            int line2Length = less ? rnd.Next(1, line1Length) : rnd.Next(1, Math.Min(maxLength2 + 1, 16 - line1Length + 1));

            string exerciseHeaderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"HtmlTemplates\TaskHeaderTemplate.html");
            var exerciseHeader = File.ReadAllText(exerciseHeaderPath);
            Exercise exercise = new Exercise(exerciseHeader)
            {
                Header = $"Начерти два отрезка: один длиной {line1Length} см, а другой на {line2Length} см {(less ? "короче" : "длиннее")}",
                Content = HtmlGenerator.GetBackgroundInTheBox(3, 33)
            };
            return await Task.FromResult(exercise.Build());
        }

        public async Task<string> GenerateIncDecBy(int count, int max, int incByMax, int decByMax)
        {
            Random rnd = new Random();

            string exerciseHeaderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"HtmlTemplates\TaskHeaderTemplate.html");
            var exerciseHeader = await File.ReadAllTextAsync(exerciseHeaderPath);

            var incBy = rnd.Next(1, incByMax);
            HashSet<int> numbersToIncrement = new HashSet<int>();
            while (numbersToIncrement.Count < count)
            {
                numbersToIncrement.Add(rnd.Next(1, max + 1 - incBy));
            }

            var decBy = rnd.Next(1, decByMax);

            HashSet<int> numbersToDecrement = new HashSet<int>();
            while (numbersToDecrement.Count < count)
            {
                numbersToDecrement.Add(rnd.Next(decBy, max + 1));
            }

            string content =
                HtmlGenerator.GetNumbers(numbersToIncrement, "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;") +
                HtmlGenerator.GetBackgroundInTheBox(1, 33);
            Exercise exercise1 = new Exercise(exerciseHeader)
            {
                Header = $"Увеличь числа на {decBy}",
                Content = content
            };

            content =
                HtmlGenerator.GetNumbers(numbersToDecrement, "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;") +
                HtmlGenerator.GetBackgroundInTheBox(1, 33);

            Exercise exercise2 = new Exercise(exerciseHeader)
            {
                Header = $"Уменьши числа на {incBy}",
                Content = content
            };

            return await Task.FromResult($"{exercise1.Build()}{exercise2.Build()}");
        }

        public async Task<string> WriteInOrder()
        {
            Random rnd = new Random();
            bool asc = rnd.Next(0, 2) == 0;

            string exerciseHeaderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"HtmlTemplates\TaskHeaderTemplate.html");
            var exerciseHeader = File.ReadAllText(exerciseHeaderPath);
            var numbers = Enumerable.Range(0, 6).Select(_ => rnd.Next(30)).Distinct().ToList();
            Exercise exercise = new Exercise(exerciseHeader)
            {
                Header = $"Запиши числа в порядке {(asc ? "возрастания" : "убывания")}",
                Content = HtmlGenerator.GetNumbers(numbers, "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;") +
                          HtmlGenerator.GetBackgroundInTheBox(1, 33)
            };

            return await Task.FromResult(exercise.Build());
        }

        public async Task<string> GenerateSeriesWithMissingValues()
        {
            Random rnd = new Random();

            int initValue = rnd.Next(1, 50);
            string fillerPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"HtmlTemplates\InputFiller.html");
            var inputFiller = File.ReadAllText(fillerPath);
            string values = string.Join(", ", Enumerable.Range(initValue, 3));
            string fillers = string.Join(", ", Enumerable.Repeat(inputFiller, 2));

            string exerciseHeaderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"HtmlTemplates\TaskHeaderTemplate.html");
            var exerciseHeader = File.ReadAllText(exerciseHeaderPath);
            Exercise exercise = new Exercise(exerciseHeader)
            {
                Header = "Запиши пропущенные числа в ряду",
                Content = $"{values},{fillers}, {string.Join(", ", Enumerable.Range(initValue + 5, 3))}, {fillers}, {string.Join(", ", Enumerable.Range(initValue + 10, 3))}"
            };
            return await Task.FromResult(exercise.Build());
        }

        private ExpressionResult GetExpression(IntegerExpressionConfig config)
        {
            Random rnd = new Random();
            string sign = config.AllowedSigns[rnd.Next(config.AllowedSigns.Count)];

            while (true)
            {
                int[] operands = new int[2];
                for(int i = 0; i < 2; i++)
                {
                    operands[i] = rnd.Next(config.MinOperandValue, config.MaxOperandValue + 1);
                    if (i > 0)
                    {
                        // not allowed to divide by zero, increase by one
                        if (sign == "/" && operands[i] == 0)
                        {
                            ++operands[i];
                        }
                    }
                }

                ExpressionResult parenthesisExpression;
                if( config.WithParenthesis) 
                {
                    parenthesisExpression = GetExpression(config with { WithParenthesis = false });
                }
                else
                {
                    parenthesisExpression = new ExpressionResult(string.Empty, default);
                }

                StringBuilder stringExpressionBuilder = new StringBuilder();
                if (rnd.Next(0, 2) % 2 == 0)
                {
                    stringExpressionBuilder.Append($"{operands[0]} {sign} ");
                    if (config.WithParenthesis)
                    {
                        stringExpressionBuilder.Append($"({parenthesisExpression.expression})");
                    }
                    else
                    {
                        stringExpressionBuilder.Append(operands[1]);
                    }
                }
                else
                {
                    if (config.WithParenthesis)
                    {
                        stringExpressionBuilder.Append($"({parenthesisExpression.expression})");
                    }
                    else
                    {
                        stringExpressionBuilder.Append(operands[0]);
                    }

                    stringExpressionBuilder.Append($" {sign} {operands[1]}");
                }
                string stringExpression = stringExpressionBuilder.ToString();
                decimal result = 0M;
                try
                {
                    Expression expression = new Expression(stringExpression);
                    result = (decimal)expression.Eval();
                }
                catch (DivideByZeroException exc)
                {
                   continue;
                }
                if (result < 0 && !config.AllowNegative)
                {
                    continue;
                }

                if (config.MaxResultValue.HasValue && Math.Abs(result) > config.MaxResultValue)
                {
                    continue;
                }

                if (config.InvisibleOperand)
                {
                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"HtmlTemplates\InputFiller.html");
                    var inputFiller = "X";//File.ReadAllText(path);
                    string stringExpr = rnd.Next(0, 2) == 1
                        ? $"{operands[0]} {sign} {inputFiller}"
                        : $"{inputFiller} {sign} {operands[1]}";
                    return new ExpressionResult(stringExpr, result);
                }

                return new ExpressionResult(stringExpression, default);

            }
        }

        private ExpressionResult GetRoundNumber10Expression(IntegerExpressionConfig config)
        {
            Random rnd = new Random();
            string sign = config.AllowedSigns[rnd.Next(config.AllowedSigns.Count)];

            if (sign == "+")
            {
                var operand1 = rnd.Next(1, 10) * 10;
                var operand2 = rnd.Next(1, config.MaxResultValue.Value - operand1);
                string stringExpression = $"{operand1} {sign} {operand2}";
                Expression expression = new Expression(stringExpression);
                var result = (decimal)expression.Eval();
                return new ExpressionResult(stringExpression, default);
            }
            else// if (sign == "-")
            {
                var operand1 = rnd.Next(1, 10) * 10;
                var operand2 = rnd.Next(1, config.MaxResultValue.Value - operand1);
                string stringExpression = $"{operand1} + {operand2}";
                Expression expression = new Expression(stringExpression);
                var result = (decimal)expression.Eval();
                return new ExpressionResult($"{result} - {operand2}", default);
            }
        }
        private string GetContent(List<string> expressions, int columns)
        {
            Random rnd = new Random();
            rnd.Next(10);
            StringBuilder contentSb = new StringBuilder();
            contentSb.Append("<table class=\"main\">");

            int expressionsCount = expressions.Count - 1;
            while (true)
            {
                contentSb.Append("<tr>");
                for (int columnIndex = 0; columnIndex < columns; ++columnIndex)
                {
                    if (expressionsCount < 0)
                    {
                        contentSb.Append("</tr>");
                        contentSb.Append("</table>");
                        return contentSb.ToString();
                    }
                    contentSb.Append("<td>");

                    contentSb.Append($"{expressions[expressionsCount]}");
                    --expressionsCount;
                    contentSb.Append("</td>");

                }
                contentSb.Append("</tr>");
            }
        }
    }
}
