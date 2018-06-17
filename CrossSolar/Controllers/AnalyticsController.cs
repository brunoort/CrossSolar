using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossSolar.Domain;
using CrossSolar.Models;
using CrossSolar.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrossSolar.Controllers
{
    [Route("panel")]
    public class AnalyticsController : Controller
    {
        private readonly IAnalyticsRepository _analyticsRepository;

        private readonly IPanelRepository _panelRepository;

        private readonly IDayAnalyticsRepository _dayAnalyticsRepository;

        public AnalyticsController(IAnalyticsRepository analyticsRepository, IPanelRepository panelRepository, IDayAnalyticsRepository dayAnalyticsRepository)
        {
            _analyticsRepository = analyticsRepository;
            _panelRepository = panelRepository;
            _dayAnalyticsRepository = dayAnalyticsRepository;
        }

        // GET panel/XXXX1111YYYY2222/analytics
        [HttpGet("{panelId}/[controller]")]
        public async Task<IActionResult> Get([FromRoute]int panelId)
        {
            //easier to use int instead of string
            //var panel = await _panelRepository.Query()
            //    .FirstOrDefaultAsync(x => x.Serial.Equals(panelId, StringComparison.CurrentCultureIgnoreCase));
            var analytics = _analyticsRepository.GetPanel(panelId);

            var result = new OneHourElectricityListModel
            {
                OneHourElectricitys = analytics.Select(c => new OneHourElectricityModel
                {
                    Id = c.Id,
                    KiloWatt = c.KiloWatt,
                    DateTime = c.DateTime
                })
            };

            return Ok(result);
        }

        // GET panel/XXXX1111YYYY2222/analytics/day
        [HttpGet("{panelId}/[controller]/day")]
        public async Task<IActionResult> DayResults([FromRoute]int panelId)
        {
            //var result = new List<OneDayElectricityModel>();
            //return Ok(result);
            var result = await _dayAnalyticsRepository.GetHistory(panelId);
            var resultModel = new OneDayElectricityListModel
            {
                OneDayElectricityModels = result
            };
            return Ok(resultModel);
        }

        // POST panel/XXXX1111YYYY2222/analytics
        [HttpPost("{panelId}/[controller]")]
        public async Task<IActionResult> Post([FromRoute]int panelId, [FromBody]OneHourElectricityModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var oneHourElectricityContent = new OneHourElectricity
            {
                PanelId = panelId.ToString(),
                KiloWatt = value.KiloWatt,
                DateTime = value.DateTime
            };

            await _analyticsRepository.InsertAsync(oneHourElectricityContent);

            var result = new OneHourElectricityModel
            {
                Id = oneHourElectricityContent.Id,
                KiloWatt = oneHourElectricityContent.KiloWatt,
                DateTime = oneHourElectricityContent.DateTime
            };

            return Created($"panel/{panelId}/analytics/{result.Id}", result);
        }
    }
}