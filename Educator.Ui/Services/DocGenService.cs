namespace Educator.Ui.Services
{
    public class DocGenService:IDocGenService
    {
        private readonly HttpClient _httpClient;

        public DocGenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task GetMathDocument(int numberOfExpressions)
        {

            var response = await _httpClient.GetAsync($"api/math/elementary/binaryexercises?numberOfExercises={numberOfExpressions}");
        }
    }
}
