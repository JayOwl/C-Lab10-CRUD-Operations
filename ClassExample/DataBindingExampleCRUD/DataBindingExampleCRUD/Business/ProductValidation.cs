using DataBindingExampleCRUD.Common;
using DataBindingExampleCRUD.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingExampleCRUD.Business
{
    class ProductValidation
    {

        private static List<string> errors;

        public static string ErrorMessage
        {
            get
            {
                string result = "";

                foreach (string message in errors)
                {
                    result += message + "\r\n";
                }

                return result;
            }
        }

        static ProductValidation()
        {
            errors = new List<string>();
        }
        public static int AddProduct(Product product)
        {
            if (validate(product))
            {
                return ProductRepository.AddProduct(product);
            }
            else {
                return -1;
            }
        }
        public static int UpdateProduct(Product product)
        {
            if (validate(product))
            {
                return ProductRepository.UpdateProduct(product);
            }
            else
            {
                return -1;
            }
        }

        public static int DeleteProduct(Product product)
        {
            return ProductRepository.DeleteProduct(product);
        }

        public static ProductCollection GetProducts()
        {
            return ProductRepository.GetProducts();
        }

        private static bool validate(Product product)
        {
          
            bool result = true;

            errors.Clear();

            if (product.Quantity < 0)
            {
                errors.Add("Quantity cannot be less that zero.");
                result = false;
            }

       
            if (string.IsNullOrWhiteSpace(product.Sku))
            {
                errors.Add("Sku cannot be blank.");
                result = false;
            }

            if (product.Cost < 0 || product.SellPrice < 0 || product.SellPrice < product.Cost)
            {
                errors.Add("Cost cannot be less than zero or higher than Sell Price.");
                result = false;
            }

            return result;
        }
    }
}
