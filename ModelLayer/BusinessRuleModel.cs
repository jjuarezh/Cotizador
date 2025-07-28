using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class BusinessRuleModel
    {
        public string Id { get; set; }
        public string Level { get; set; }
        public string BusinessLine { get; set; }
        public string ProductType { get; set; }
        public double Quantity  { get; set; }
        public double Discount { get; set; }
    }
}
