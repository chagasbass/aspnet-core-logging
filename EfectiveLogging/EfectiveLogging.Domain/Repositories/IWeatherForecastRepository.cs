using EfectiveLogging.Domain.Entities;
using System.Collections.Generic;

namespace EfectiveLogging.Domain.Repositories
{
    public  interface IWeatherForecastRepository
    {
        IEnumerable<WeatherForecast> ListAll();
        WeatherForecast ListById(int id);
        WeatherForecast Insert(WeatherForecast weatherForecast);
        WeatherForecast Update(WeatherForecast weatherForecast);
        WeatherForecast Delete(WeatherForecast weatherForecast);
    }
}
