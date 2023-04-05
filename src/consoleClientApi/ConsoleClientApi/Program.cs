using System.Text;

namespace ConsoleClientApi
{
    internal class Program
    {

        static HttpClient? httpClient;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            httpClient = new HttpClient();

            var response = await httpClient.GetAsync("https://localhost:7210/WeatherForecast");

            Console.WriteLine(response);

            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // добавление данных POST-метод

            string newRecord = "{\r\n   \"id\": 5,\r\n   \"date\": \"01.01.2002\", \r\n   \"degree\": -30,\r\n \"location\": \"Самара\"\r\n}";

            var stringContent = new StringContent(newRecord, Encoding.UTF8, "application/json");

            response = await httpClient.PostAsync("https://localhost:7210/WeatherForecast", stringContent);

            Console.WriteLine(response);

            // обновление данных PUT-метод

            string updateRecord = "{\r\n   \"id\": 5,\r\n   \"date\":  \"01.02.2002\",\r\n   \"degree\":  -3,\r\n   \"location\":  \"Самара\"\r\n}";

            stringContent = new StringContent(updateRecord, Encoding.UTF8, "application/json");

            response = await httpClient.PutAsync("https://localhost:7210/WeatherForecast", stringContent);

            Console.WriteLine(response);

            // Повторое получение данных GET-метод

            response = await httpClient.GetAsync("https://localhost:7210/WeatherForecast");

            Console.WriteLine(response);

            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Получение конкретной записи GetById-метод

            response = await httpClient.GetAsync("https://localhost:7210/WeatherForecast/5");

            Console.WriteLine(response);

            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // удаление данных DELETE-метод

            response = await httpClient.DeleteAsync("https://localhost:7210/WeatherForecast?id=5");

            Console.WriteLine(response);

            Console.WriteLine(await response.Content.ReadAsStringAsync());


            
        }
    }
}