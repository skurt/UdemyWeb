using Microsoft.AspNetCore.Mvc;
using UdemyWeb.DataAccess.Data;
using UdemyWeb.Models.Models;

namespace UdemyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name.ToLower() == category.DisplayOrder.ToString().ToLower())
            {
                ModelState.AddModelError("name", "Display order cannot exactly match the Category Name");
            }
            if (category.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is a forbidden category name");
            }
            var categories = _db.Categories.Where(x => x.DisplayOrder == category.DisplayOrder);
            var count = categories.Count();
            if (count > 0)
            {
                ModelState.AddModelError("DisplayOrder", "This number is unavailable for a Display Order, please choose another.");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["success"] = "Category created sucessfully.";
                return RedirectToAction("Index", "Category");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            { 
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["success"] = "Category updated sucessfully.";
                return RedirectToAction("Index", "Category");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Category " + category.Name + " deleted sucessfully!";

            return RedirectToAction("Index", "Category");
        }


    }
}
