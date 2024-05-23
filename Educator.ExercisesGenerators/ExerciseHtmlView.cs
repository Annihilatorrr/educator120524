namespace Educator.ExercisesGenerators
{
    public class ExerciseHtmlView
    {
        private string _headerTemplate;
        public string Header { get; set; }
        public string Content { get; set; }

        public ExerciseHtmlView(string headerTemplate)
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
