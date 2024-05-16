namespace Educator.ExercisesGenerators
{
    public class Exercise
    {
        private string _headerTemplate;
        public string Header { get; set; }
        public string Content { get; set; }

        public Exercise(string headerTemplate)
        {
            _headerTemplate = headerTemplate;
        }
        public string Build()
        {
            var header = _headerTemplate.Replace("%%EXERCISEHEADER%%", Header);
            return $"<div class=\"exercise\">{header}{Content}</div>";
        }
    }
}
