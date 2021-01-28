using EfectiveLogging.Domain.Entities;
using EfectiveLogging.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace EfectiveLogging.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastRepository _weatherForecastRepository;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastRepository weatherForecastRepository)
        {
            _logger = logger;
            _weatherForecastRepository = weatherForecastRepository;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("requisicao recebida - listagem");

            var values = _weatherForecastRepository.ListAll();

            return values;
        }

        [HttpPost]
        public WeatherForecast Post(WeatherForecast weatherForecast)
        {
            try
            {
                _logger.LogInformation("requisicao recebida - Insert");

                WeatherForecast retorno;

                using (_logger.BeginScope("Inserindo os dados para insert {weatherForecast}", weatherForecast.TemperatureC))
                {
                    retorno = _weatherForecastRepository.Insert(weatherForecast);
                }

                return retorno;

            }
            catch (Exception ex)
            {

                _logger.LogError("Erro encontrado", ex);
                return default;
            }
        }
    }
}
