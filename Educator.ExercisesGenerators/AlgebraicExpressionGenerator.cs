namespace Educator.ExercisesGenerators
{
    public class AlgebraicExpressionGenerator
    {
        private readonly AlgebraicExpressionGeneratorConfig _config;

        public AlgebraicExpressionGenerator(AlgebraicExpressionGeneratorConfig config)
        {
            _config = config;
        }

        public BinaryExercise GenerateExpression()
        {
            var random = new Random();
            var operand1 = random.Next(_config.MinOperand, _config.MaxOperand + 1);
            var operand2 = random.Next(_config.MinOperand, _config.MaxOperand + 1);
            var @operator = _config.AllowedOperators.ElementAt(random.Next(_config.AllowedOperators.Count()));

            var expression = $"{operand1} {@operator} {operand2}";
            var answer = EvaluateExpression(expression);

            while (answer < _config.MinAnswer || answer > _config.MaxAnswer)
            {
                operand1 = random.Next(_config.MinOperand, _config.MaxOperand + 1);
                operand2 = random.Next(_config.MinOperand, _config.MaxOperand + 1);
                @operator = _config.AllowedOperators.ElementAt(random.Next(_config.AllowedOperators.Count()));

                expression = $"{operand1} {@operator} {operand2}";
                answer = EvaluateExpression(expression);
            }

            return new BinaryExercise { FirstOperand = operand1.ToString(), SecondOperand = operand2.ToString(), Operator = @operator};
        }

        private int EvaluateExpression(string expression)
        {
            var parts = expression.Split(' ');
            var operand1 = int.Parse(parts[0]);
            var @operator = parts[1][0];
            var operand2 = int.Parse(parts[2]);

            return @operator switch
            {
                '+' => operand1 + operand2,
                '-' => operand1 - operand2,
                '*' => operand1 * operand2,
                '/' => operand1 / operand2,
                _ => throw new ArgumentException($"Invalid operator: {@operator}")
            };
        }
    }
}
