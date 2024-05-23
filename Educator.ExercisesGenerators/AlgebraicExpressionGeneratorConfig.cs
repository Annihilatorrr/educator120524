namespace Educator.ExercisesGenerators
{
    public class AlgebraicExpressionGeneratorConfig
    {
        public required List<string> AllowedOperators { get; set; }
        public int MinOperand { get; set; }
        public int MaxOperand { get; set; }
        public int MinAnswer { get; set; }
        public int MaxAnswer { get; set; }

        public int Count { get; set; }
    }
}
