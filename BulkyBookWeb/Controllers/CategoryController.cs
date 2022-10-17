using BulkyBook.Models;
using BulkyBookDataAccess.Repository.IRepository;
using BulkyBookWeb.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ICategoryRepository _db;

        public CategoryController(ICategoryRepository db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.GetAll();
            return View(objCategoryList);
        }

        //////below create is a GET action method
        public IActionResult Create()
        {
           return View();
        }

        //below create is a POST action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            //below adding a custom validation to ensure both fields cannot be the same
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the Name");
            }
            //adding the new category to the database and saving the changes
            //also confirm if the obj is valid
            if (ModelState.IsValid)
            {
                _db.Add(obj);
                _db.Save();
                //tempdata used below for information not stored but only displayed on the action execution
                TempData["success"] = "Category created succesfully";
                //below redirect to the index to show the updated change
                return RedirectToAction("Index");
            }
            //if not valid just return the view
            return View(obj);
        }

        //below EDIT action method
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDbFirst = _db.GetFirstOrDefault(u=>u.Id==id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }
            return View(categoryFromDbFirst);
        }

        //below EDIT action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            //below adding a custom validation to ensure both fields cannot be the same
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the Name");
            }
            //edit an existing category to the database and saving the changes
            //also confirm if the obj is valid
            if (ModelState.IsValid)
            {
                _db.Update(obj);
                _db.Save();
                TempData["success"] = "Category edited succesfully";
                //below redirect to the index to show the updated change
                return RedirectToAction("Index");
            }
            //if not valid just return the view
            return View(obj);
        }


        //below DELETE action method
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var categoryFromDbFirst = _db.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }
            return View(categoryFromDbFirst);
        }

        //below DELETE action method
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
           var obj = _db.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
           
            _db.Remove(obj);
                _db.Save();
            TempData["success"] = "Category deleted succesfully";
            //below redirect to the index to show the updated change
            return RedirectToAction("Index");       
           
        }
    }
}
