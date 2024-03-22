using OpenMeteo.Models;
using System.Data;
using System.Runtime.ConstrainedExecution;
using System.Text.Json;

namespace OpenMeteo
{
    public class Program
    {

        // Lav en menu så man kan bladre mellem menuer. F.eks.. Vejr lige nu, Med evt.tidspunkt hvor der er mest sandsynlighed at det vil regne, Vejret de seneste 24 timer. Vejret den seneste uge som et dagsskema i konsollen. Og meget andet.
        public static void Main()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("            - Menu -           ");
                Console.WriteLine("|-----------------------------|");
                Console.WriteLine("| 1. Vejr lige nu             |");
                Console.WriteLine("| 2. Vejr de næste 24 timer   |");
                Console.WriteLine("| 3. Vejr de seneste 24 timer |");
                Console.WriteLine("| 4. Vejr de seneste uge      |");
                Console.WriteLine("| 5. Gem by                   |");
                Console.WriteLine("| 6. Exit                     |");
                Console.WriteLine("|-----------------------------|");
                Console.Write("Indtast en funktion: ");
                var menu = Console.ReadKey();
                switch (menu.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        GetCurrenctWeather().Wait();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        GetHighstTemp().Wait();
                        GetLowestTemp().Wait();
                        GetAverageWindSpeed().Wait();
                        GetSorteretWeather().Wait();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        GetWeatherSingleCityJson().Wait();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.D4:
                        Console.Clear();
                        GetWeatherSingleCityJson().Wait();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.D5:
                        Console.Clear();
                        SaveCityToFile().Wait();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case ConsoleKey.D6:
                        running = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Ugyldigt valg");
                        Console.Clear();
                        break;
                }
            }
        }

        // Dokumentation: https://open-meteo.com/en/docs/ Der skal hentes og vises følgende data:
        // Vejrudsigten lige nu. Her må du selv vælge hvilke vejrdata du vil bruge.Vælg mindst 2 dataset.
        private async static Task GetCurrenctWeather()
        {
            var baseAddress = new Uri("https://api.open-meteo.com/v1/forecast?latitude=54.909&longitude=9.7892&hourly=temperature_2m,snowfall&timezone=Europe%2FBerlin");
            var weather = await new Deserializer().DeserializeRoot2Async(baseAddress);
            if (weather != null)
            {
                string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:00");
                for (int i = 0; i < weather.hourly.time.Count; i++)
                {
                    weather.hourly.time[i] = weather.hourly.time[i].Replace("T", " ");
                    if (weather.hourly.time[i] == currentTime)
                    {
                        Console.WriteLine($"Time: {weather.hourly.time[i]} - Temperature: {weather.hourly.temperature_2m[i]} Snowfall: {weather.hourly.snowfall[i]}");
                    }
                }
            }
        }
        private async static Task GetWeatherSingleCityJson()
        {
            var baseAddress = new Uri("https://api.open-meteo.com/v1/forecast?latitude=54.909&longitude=9.7892&hourly=temperature_2m,snowfall&timezone=Europe%2FBerlin");
            var weather = await new Deserializer().DeserializeRoot2Async(baseAddress);
            if (weather != null)
            {
                for (int i = 0; i < weather.hourly.time.Count; i++)
                {
                    weather.hourly.time[i] = weather.hourly.time[i].Replace("T", " ");
                    Console.WriteLine($"Time: {weather.hourly.time[i]} - Temperature: {weather.hourly.temperature_2m[i]} Snowfall: {weather.hourly.snowfall[i]}");
                }
            }

        }
        // Højeste Temperatur de næste 24 timer
        private async static Task GetHighstTemp()
        {
            var baseAddress = new Uri("https://api.open-meteo.com/v1/forecast?latitude=54.909&longitude=9.7892&hourly=temperature_2m,snowfall&timezone=Europe%2FBerlin&forecast_days=1");
            var weather = await new Deserializer().DeserializeRoot2Async(baseAddress);

            if (weather != null)
            {
                for (int i = 0; i < weather.hourly.time.Count; i++)
                {
                    weather.hourly.time[i] = weather.hourly.time[i].Replace("T", " ");
                }
                double maxTemp = weather.hourly.temperature_2m.Max();
                string time = weather.hourly.time[weather.hourly.temperature_2m.IndexOf(maxTemp)];
                Console.WriteLine($"Højeste temperatur de næste 24 timer: {maxTemp} - Klokken: {time}");
            }
        }


        // Laveste Temperatur de næste 24 timer
        private async static Task GetLowestTemp()
        {
            var baseAddress = new Uri("https://api.open-meteo.com/v1/forecast?latitude=54.909&longitude=9.7892&hourly=temperature_2m,snowfall&timezone=Europe%2FBerlin&forecast_days=1");
            var weather = await new Deserializer().DeserializeRoot2Async(baseAddress);

            if (weather != null)
            {
                for (int i = 0; i < weather.hourly.time.Count; i++)
                {
                    weather.hourly.time[i] = weather.hourly.time[i].Replace("T", " ");
                }
                double minTemp = weather.hourly.temperature_2m.Min();
                string time = weather.hourly.time[weather.hourly.temperature_2m.IndexOf(minTemp)];
                Console.WriteLine($"Laveste temperatur de næste 24 timer: {minTemp} - Klokken: {time}");
            }
        }


        // Gennemsnitlig Vind hastighed for de næste 24 timer.
        private async static Task GetAverageWindSpeed()
        {
            var baseAddress = new Uri("https://api.open-meteo.com/v1/forecast?latitude=54.909&longitude=9.7892&hourly=temperature_2m,snowfall,wind_speed_10m,wind_direction_10m,wind_gusts_10m&timezone=Europe%2FBerlin&forecast_days=1");
            var weather = await new Deserializer().DeserializeRoot2Async(baseAddress);

            if (weather != null)
            {
                for (int i = 0; i < weather.hourly.time.Count; i++)
                {
                    weather.hourly.time[i] = weather.hourly.time[i].Replace("T", " ");
                }
                double averageWindSpeed = weather.hourly.wind_speed_10m.Average();
                Console.WriteLine($"Gennemsnitlig vind hastighed de næste 24 timer: {averageWindSpeed}");
            }
        }

        // En liste over vejret time for time med hvad vind og temperautren er de næste 24 timer sorteret efter faldende på tid.
        private async static Task GetSorteretWeather()
        {
            var baseAddress = new Uri("https://api.open-meteo.com/v1/forecast?latitude=54.909&longitude=9.7892&hourly=temperature_2m,snowfall,wind_speed_10m,wind_direction_10m,wind_gusts_10m&timezone=Europe%2FBerlin&forecast_days=1");
            var weather = await new Deserializer().DeserializeRoot2Async(baseAddress);

            if (weather != null)
            {
                for (int i = 0; i < weather.hourly.time.Count; i++)
                {
                    weather.hourly.time[i] = weather.hourly.time[i].Replace("T", " ");
                }
                List<string> time = weather.hourly.time;
                List<double> temperature = weather.hourly.temperature_2m;
                List<double> windSpeed = weather.hourly.wind_speed_10m;
                List<int> windDirection = weather.hourly.wind_direction_10m;
                List<double> windGusts = weather.hourly.wind_gusts_10m;

                List<HourlyUnits2> weatherList = [];
                for (int i = 0; i < time.Count; i++)
                {
                    weatherList.Add(new HourlyUnits2()
                    {
                        time = time[i],
                        temperature_2m = temperature[i].ToString(),
                        wind_speed_10m = windSpeed[i].ToString(),
                        wind_direction_10m = windDirection[i].ToString(),
                        wind_gusts_10m = windGusts[i].ToString()
                    });
                }

                weatherList = [.. weatherList.OrderBy(x => x.time)];
                foreach (var item in weatherList)
                {
                    Console.WriteLine($"Time: {item.time} - Temperature: {item.temperature_2m} - WindSpeed: {item.wind_speed_10m} - WindDirection: {item.wind_direction_10m} - WindGusts: {item.wind_gusts_10m}");
                }
            }
        }

        // Gem det i en fil så dit program kan huske byen næste gang du åbner programmet.
        private async static Task SaveCityToFile()
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast?latitude=54.909&longitude=9.7892&hourly=temperature_2m,snowfall,wind_speed_10m,wind_direction_10m,wind_gusts_10m&timezone=Europe%2FBerlin&forecast_days=1")
            };
            var response = await client.GetAsync(client.BaseAddress);
            string stringResult = await response.Content.ReadAsStringAsync();

            using StreamWriter writer = new("city.json");
            await writer.WriteLineAsync(stringResult);
        }



    }
}