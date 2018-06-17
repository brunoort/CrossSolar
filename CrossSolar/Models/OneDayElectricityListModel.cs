using CrossSolar.Domain;
using System.Collections.Generic;

namespace CrossSolar.Models
{
    public class OneDayElectricityListModel
    {
        public IEnumerable<OneDayElectricityModel> OneDayElectricityModels { get; set; }
    }
}
