using CrossSolar.Domain;
using System.Collections.Generic;

namespace CrossSolar.Repository
{
    public interface IAnalyticsRepository :
        IGenericRepository<OneHourElectricity>
    {
        List<OneHourElectricity> GetPanel(int panelId);
    }
}