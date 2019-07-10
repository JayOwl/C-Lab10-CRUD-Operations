using DataBindingExampleCRUD.Business;
using DataBindingExampleCRUD.Common;
//using DataBindingExampleCRUD.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingExampleCRUD
{
    class ProductViewModel : ViewModelBase
    {
        private Product product;

        
        public ProductViewModel()
        {
            this.Products = ProductValidation.GetProducts();
            Product = new Product();
        }

        public Product Product
        {
            get { return product; }
            set
            {
                product = value;
                OnPropertyChanged("Product");
            }
        }
       
        public ProductCollection Products { get; set; }

        public void SetDisplayProduct(Product product)
        {   // pass by value to prevent changing Product instance in collection
            Product = new Product
            {
                ProductId = product.ProductId,
                Quantity = product.Quantity,
                Sku = product.Sku,
                Description = product.Description,
                Cost = product.Cost,
                SellPrice = product.SellPrice,
                Taxable = product.Taxable,
                Active = product.Active,
                Notes = product.Notes
            };
        }

        public Product GetDisplayProduct()
        {
            OnPropertyChanged("Product");
            return Product;
        }
    }
}
