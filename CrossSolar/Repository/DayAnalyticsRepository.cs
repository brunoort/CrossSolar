using CrossSolar.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossSolar.Repository
{
    public class DayAnalyticsRepository : OneDayElectricityModel, IDayAnalyticsRepository
    {
        readonly CrossSolarDbContext _dbContext;
        public DayAnalyticsRepository(CrossSolarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<OneDayElectricityModel>> GetHistory(int panelId)
        {
            var oneHourElectricity = _dbContext.OneHourElectricitys
                .Where(panel => panel.Id == panelId)
                .OrderByDescending(panel => panel.DateTime)
                .GroupBy(panel => new
                {
                    panel.DateTime.Year,
                    panel.DateTime.Month,
                    panel.DateTime.Day
                });
            //day[sum, min, max, average]
            var history = oneHourElectricity.Select(
                s => new OneDayElectricityModel
                {
                    Sum = s.Sum(panel => panel.KiloWatt),
                    Minimum = s.Min(panel => panel.KiloWatt),
                    Maximum = s.Max(panel => panel.KiloWatt),
                    Average = s.Average(panel => panel.KiloWatt),
                    DateTime = new DateTime(s.Key.Year, s.Key.Month, s.Key.Day, 0, 0, 0)
                }).ToListAsync();
            return history;
        }


        //todo
        public Task<OneDayElectricityModel> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<OneDayElectricityModel> Query()
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(OneDayElectricityModel entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(OneDayElectricityModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<OneDayElectricityModel> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<OneDayElectricityModel>.InsertAsync(OneDayElectricityModel entity)
        {
            throw new NotImplementedException();
        }
    }
}