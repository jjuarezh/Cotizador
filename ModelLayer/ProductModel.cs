using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class ProductModel
    {
       
        public ProductModel()
        {
            Metadata = new List<string>();
        }

        public string Id { get; set; }
        public string BusinessLine { get; set; }
        public string Label { get; set; }
        public string Level { get; set; }
        public string ProductCode { get; set; }
        public string Subject { get; set; }
        public string Type { get; set; }
        public string Grade { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Title { get; set; }
        public List<string> Metadata { get; set; }

    }
}
