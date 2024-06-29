using Microsoft.AspNetCore.Mvc;
using UdemyWeb.DataAccess.Data;
using UdemyWeb.DataAccess.Repositories;
using UdemyWeb.Models.Models;

namespace UdemyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository db)
        {
            _categoryRepository = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepository.GetAll().ToList();
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
            var categories = _categoryRepository.GetMultiple(x => x.DisplayOrder == category.DisplayOrder);
            var count = categories.Count();
            if (count > 0)
            {
                ModelState.AddModelError("DisplayOrder", "This number is unavailable for a Display Order, please choose another.");
            }

            if (ModelState.IsValid)
            {
                _categoryRepository.Add(category);
                _categoryRepository.Save();
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

            Category? categoryFromDb = _categoryRepository.GetOne(c => c.Id == id);
            //Category? categoryFromDb1 = _categoryRepository.GetOne(u=>u.Id==id);
            //Category? categoryFromDb2 = _categoryRepository.GetOne(u => u.Id == id);
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
                _categoryRepository.Update(category);
                _categoryRepository.Save();
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
            Category? categoryFromDb = _categoryRepository.GetOne(c => c.Id == id);
            //Category? categoryFromDb1 = _categoryRepository.FirstOrDefault(u => u.Id == id);
            //Category? categoryFromDb2 = _categoryRepository.Where(u => u.Id == id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category category = _categoryRepository.GetOne(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _categoryRepository.Delete(category);
            _categoryRepository.Save();
            TempData["success"] = "Category " + category.Name + " deleted sucessfully!";

            return RedirectToAction("Index", "Category");
        }


    }
}
