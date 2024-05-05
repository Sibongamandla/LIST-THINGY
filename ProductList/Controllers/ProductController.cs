using ProductList.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProductList.Controllers
    {
    public class ProductController : Controller
        {
        public ActionResult Index()
            {
            var viewModel = new ProductViewModel
                {
                Suppliers = ProductRepository.GetSuppliers(),
                Categories = ProductRepository.GetCategories(),
                Products = ProductRepository.GetProducts(),
                };
            return View(viewModel);
            }

     

        public ActionResult GetProductsByCategory(int categoryId)
            {
            var products = ProductRepository.GetProducts()
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new { p.Id, p.Name });

            var productNames = new List<string>();
            var productIds = new List<int>();
            foreach (var product in products)
                {
                productNames.Add(product.Name);
                productIds.Add(product.Id);
                }

            return Content(
                string.Format("{0};{1}", string.Join(",", productNames), string.Join(",", productIds)),
                "text/plain"
            );
            }

        public ActionResult AddProduct(int productId, string productName)
        {
            // Here you can add the product to your data store or perform other actions
            return Content("OK", "text/plain");
        }

        // New Action for getting suppliers by product
        public ActionResult GetSuppliersByProduct(int productId)
        {
            var suppliers = ProductRepository.GetProducts()
                .Where(p => p.Id == productId)
                .Select(p => p.SupplierId)
                .Distinct()
                .ToList();

            var supplierNames = new List<string>();
            var supplierIds = new List<int>();
            foreach (var supplierId in suppliers)
            {
                var supplier = ProductRepository.GetSuppliers().FirstOrDefault(s => s.Id == supplierId);
                if (supplier != null)
                {
                    supplierNames.Add(supplier.Name);
                    supplierIds.Add(supplier.Id);
                }
            }

            return Content(
                string.Format("{0};{1}", string.Join(",", supplierNames), string.Join(",", supplierIds)),
                "text/plain"
            );
        }
    }
}