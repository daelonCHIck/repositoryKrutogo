using Microsoft.AspNetCore.Mvc;

namespace Таск_15_1_.Controllers
{

    public class WeatherData
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int Degree { get; set; }
        public string Location { get; set; }
    }


    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        public static List<WeatherData> weatherDatas = new()
        {
            new WeatherData() { Id = 1, Date = "21.01.2022", Degree = 10, Location = "Мурманск" },
            new WeatherData() { Id = 2, Date = "10.08.2019", Degree = 20, Location = "Пермь" },
            new WeatherData() { Id = 24, Date = "205,11,2020", Degree = 20, Location = "Омск" },
            new WeatherData() { Id = 25, Date = "07,02,2021", Degree = 0, Location = "Томск" },
            new WeatherData() { Id = 30, Date = "30.05.2022", Degree = 3, Location = "Калининград" },

        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<WeatherData> GetAll()
        {
            return weatherDatas;
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int id)
        {
            for (int i = 0; i < weatherDatas.Count; i++) // цикл, который обходит каждый элемент weatherDatas
            {
                if (weatherDatas[i].Id == id) // в случае, если идентификаторы одинаковые - выполним следующее
                {
                    return Ok(weatherDatas[i]); // возвращение результата "Успешно" с данными о записи
                }
            }

            return BadRequest("Такая запись не обнаружена"); // возвращение результата "Ошибка" с сообщением

        }

        [HttpGet("find-by-city")]
        public IActionResult GetByCityName(string location)
        {
            for (int i = 0; i < weatherDatas.Count; i++)
            {
                if (weatherDatas[i].Location == location)
                { 
                    return Ok(weatherDatas[i]);
                }
            }
            return BadRequest("Запись с указанным городом не обнаружена");
        }

        [HttpPost]
        public IActionResult Add(WeatherData data)
        {

            for (int i = 0; i < weatherDatas.Count; i++) // цикл, который обходит кадлый элемент массива weatherDatas
            {
                if (weatherDatas[i].Id == data.Id) // в случае, если идентификаторы одинаковые - выполни следующее
                {
                    return BadRequest("Запись с таким Id уже есть"); // возвращение результата "Ошибка" с сообщением
                } 
                
                if (weatherDatas[i].Id < 0)
                {
                    return BadRequest("Ошибка: некорректный Id");
                }
            }
            weatherDatas.Add(data); // добавим в список новую запись
            return Ok(); // возвращение результата "Успешно"


        }

        [HttpPut]
        public IActionResult Update(WeatherData data)
        {
            for (int i = 0; i < weatherDatas.Count; i++) // цикл, котоый обходит кадлый элемент массива weatherDatas
            {
                if (weatherDatas[i].Id == data.Id) // в случае, если идентификаторы одинаковые - выполним следующее
                {
                    weatherDatas[i] = data; // заменяем значение для данной ячейки массива
                    return Ok(); // возвращение результата "Успешно"
                }

                if (weatherDatas[i].Id < 0)
                {
                    return BadRequest("Ошибка: некорректный Id");
                }
            }
            return BadRequest("Такая запись не обнаружена"); // Возвращение результата "Ошибка" с сообщением
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            for (int i = 0; i < weatherDatas.Count; i++)
            {
                if (weatherDatas[i].Id == Id)
                {
                    Summaries.RemoveAt(i);
                    return Ok();
                }
            }
            return BadRequest("Такая запись не обнаружена");
        }
    }
}