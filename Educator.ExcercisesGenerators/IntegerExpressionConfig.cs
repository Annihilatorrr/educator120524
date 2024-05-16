namespace Educator.ExercisesGenerators
{
    public record IntegerExpressionConfig
    {
        public List<string>? AllowedSigns { get; set; }
        public bool AllowNegative { get; set; }

        public int MinOperandValue { get; set; }
        public int MaxOperandValue { get; set; }
        public int? MaxResultValue { get; set; }
        public int ExpressionsCount { get; set; }
        public int Columns { get; set; } = 5;
        public bool InvisibleOperand { get; set; }
        public bool WithParenthesis { get; set; }
    }
}
