using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingExampleCRUD.Common
{
    class Product
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Sku { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public bool Taxable { get; set; }
    }
}
