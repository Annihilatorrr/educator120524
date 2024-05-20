namespace Educator.ConsoleApp
{
    internal class HtmlGenerator
    {
        public static string GetBackgroundInTheBox(int rows, int columns)
        {
            return "<table class=\"backgroundinthebox\">" +
                string.Join("", Enumerable.Repeat("<tr>" + string.Join("", Enumerable.Repeat("<td></td>", columns)) + "</tr>", rows)) +
                                "</table>";
        }

        public static string GetNumbers(IEnumerable<int> numbers, string separator = " ")
        {
            return $"<div style=\"page-break-inside: avoid;\">{string.Join(separator, numbers)}</div>";
        }

    }
}
