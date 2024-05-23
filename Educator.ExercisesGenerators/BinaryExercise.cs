namespace Educator.ExercisesGenerators
{
    public class BinaryExercise
    {
        public string FirstOperand { get; set; }
        public string SecondOperand { get; set; }
        public string Operator { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstOperand, SecondOperand, Operator);
        }

        public override bool Equals(object? obj)
        {
            if (obj is BinaryExercise other)
            {
                return FirstOperand == other.FirstOperand &&
                       SecondOperand == other.SecondOperand &&
                       Operator == other.Operator;
            }
            return false;
        }
    }
}
