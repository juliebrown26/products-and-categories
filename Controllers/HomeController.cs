using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using productscategories.Models;
using Microsoft.EntityFrameworkCore;

namespace productscategories.Controllers
{
    public class HomeController : Controller
    {
        private ProdCatContext dbContext;

        public HomeController(ProdCatContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            IndexViewModel model = new IndexViewModel()
            {
                Products = dbContext.Products.ToList()
            };
            return View(model);
        }

        [HttpPost]
        [Route("CreateProduct")]
        public IActionResult CreateProduct(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(newProduct);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        [HttpGet]
        [Route("categories")]
        public IActionResult Categories()
        {
            CategoryViewModel model = new CategoryViewModel()
            {
                Categories = dbContext.Categorys.ToList()
            };
            return View(model);
        }

        [HttpPost]
        [Route("CreateCategory")]
        public IActionResult CreateCategory(Category newCategory)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(newCategory);
                dbContext.SaveChanges();
                return RedirectToAction("Categories");
            }
            return View("Categories");
        }

        [HttpGet]
        [Route("products/{ProductId}")]
        public IActionResult ShowProduct(int ProductId)
        {
            AssocCattoProdViewModel model = new AssocCattoProdViewModel()
            {
                Product = dbContext.Products
                .Include(prod => prod.Associations)
                .ThenInclude(assoc => assoc.Category)
                .FirstOrDefault(prod => prod.ProductId == ProductId),

                Categories = dbContext.Categorys.Where(c => c.Associations.All(a => a.ProductId != ProductId))
                .ToList()
            };
            return View(model);
        }

        [HttpGet]
        [Route("category/{CategoryId}")]
        public IActionResult ShowCategory(int CategoryId)
        {
            AssocProdToCatViewModel model = new AssocProdToCatViewModel()
            {
                Category = dbContext.Categorys
                .Include(cat => cat.Associations)
                .ThenInclude(assoc => assoc.Product)
                .FirstOrDefault(cat => cat.CategoryId == CategoryId),

                Products = dbContext.Products.Where(p => p.Associations.All(a => a.CategoryId != CategoryId))
                .ToList()
            };
            return View(model);
        }

        [HttpPost]
        [Route("AddCategory")]
        public IActionResult AddCategory(AssocCattoProdViewModel model)
        {
            dbContext.Add(model.Association);
            dbContext.SaveChanges();
            return Redirect($"products/{model.Association.ProductId}");
        }

        [HttpPost]
        [Route("AddProduct")]
        public IActionResult AddProduct(AssocProdToCatViewModel model)
        {
            dbContext.Add(model.Association);
            dbContext.SaveChanges();
            return Redirect($"category/{model.Association.CategoryId}");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
