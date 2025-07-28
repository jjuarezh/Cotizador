using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    class AccountModel
    {
        public string Id { get; set; }
        public string Contact { get; set; }
        public string Label { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Name { get; set; }
        public List<PopulationModel> Population { get; set; }
    }
}
