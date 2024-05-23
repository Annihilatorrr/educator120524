namespace Educator.ExercisesGenerators
{
    public interface IMathDocGeneratorService
    {
        public Task<HashSet<BinaryExercise>> GenerateExpressions(AlgebraicExpressionGeneratorConfig config,
            string exerciseHeaderTemplate);
        public Task<string> GenerateExpressionsMulBy10(IntegerExpressionConfig config, string exerciseHeaderTemplate);
        Task<string> Generate2LineSegments(int macLength1, int maxLength2);
        Task<string> GenerateSeriesWithMissingValues();
        Task<string> WriteInOrder();
        Task<string> GenerateIncDecBy(int count, int max, int incByMax, int decByMax);
    }
}
