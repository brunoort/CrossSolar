using CrossSolar.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossSolar.Repository
{
    public interface IDayAnalyticsRepository : IGenericRepository<OneDayElectricityModel>
    {        
        Task<List<OneDayElectricityModel>> GetHistory(int panelId);     
    }
}