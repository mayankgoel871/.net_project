using Microsoft.AspNetCore.Mvc;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
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
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create() { return View(); }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj) {
            //customised error
            if (obj.Name == obj.DisplayOrder.ToString()) {
                ModelState.AddModelError("name", "both cannot be same");
            }
            //error handling validation
            if(ModelState.IsValid) {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Created successfully";

                return RedirectToAction("Index");
            }
            return View(obj);
            }
        
        //GET
        public IActionResult Edit(int? id) {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDB = _db.Categories.Find(id);
            if(categoryFromDB == null) { return NotFound(); }
            
            
            return View(categoryFromDB); }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj) {
            //customised error
            if (obj.Name == obj.DisplayOrder.ToString()) {
                ModelState.AddModelError("name", "both cannot be same");
            }
            //error handling validation
            if(ModelState.IsValid) {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Updated successfully";

                return RedirectToAction("Index");
            }
            return View(obj);
            }
         //GET
        public IActionResult Delete(int? id) {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDB = _db.Categories.Find(id);
            if(categoryFromDB == null) { return NotFound(); }
            
            
            return View(categoryFromDB); }

        //Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id) {

            var obj = _db.Categories.Find(id);
            if(obj == null) { return NotFound(); }
            
                _db.Categories.Remove(obj);
                _db.SaveChanges();
            TempData["success"] = "Deleted successfully";
                return RedirectToAction("Index");
            
            }

    }
}
