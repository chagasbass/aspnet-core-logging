using EfectiveLogging.Domain.Entities;
using EfectiveLogging.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EfectiveLogging.Infrastruture.Repositories
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private List<WeatherForecast> _database = new List<WeatherForecast>();

        public WeatherForecastRepository()
        {
            CarregarDados();
        }

        private static readonly string[] Summaries = new[]
      {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private void CarregarDados()
        {
            var rng = new Random();
            _database = Enumerable.Range(1, 50).Select(index => new WeatherForecast
            {
                Id = rng.Next(1, 9999),
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = $"Summarie[{rng.Next(Summaries.Length)}]"
            }).ToList();
        }

        public WeatherForecast Delete(WeatherForecast weatherForecast)
        {
            throw new NotImplementedException();
        }

        public WeatherForecast Insert(WeatherForecast weatherForecast)
        {
            weatherForecast.Id = _database.Count + 1;
            _database.Add(weatherForecast);
            return weatherForecast;
        }

        public IEnumerable<WeatherForecast> ListAll()
        {
            string teste = string.Empty;

            teste.Substring(0, 34);

            return _database;
        }

        public WeatherForecast ListById(int id)
        {
            return _database.FirstOrDefault(x => x.Id == id);
        }

        public WeatherForecast Update(WeatherForecast weatherForecast)
        {
            var updadatedData = _database.FirstOrDefault(x => x.Id == weatherForecast.Id);

            if (!(updadatedData is null))
            {
                _database.Remove(updadatedData);
                _database.Add(weatherForecast);
                _database = _database.OrderBy(x => x.Id).ToList();
                return weatherForecast;
            }

            return weatherForecast;
        }
    }
}
