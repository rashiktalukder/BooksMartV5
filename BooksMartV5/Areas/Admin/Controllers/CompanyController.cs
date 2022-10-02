using BooksMartV5.DataAccess.Repository.IRepository;
using BooksMartV5.Models;
using Microsoft.AspNetCore.Mvc;

namespace BooksMartV5.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Company company = new Company();

            if(id== null)
            {
                //this is for create
                return View(company);
            }
            else
            {
                //this is for edit
                company=_unitOfWork.Company.Get(id.GetValueOrDefault());
                if(company==null)
                {
                    return NotFound();
                }
                else
                {
                    return View(company);
                }
            }

           /* return View();*/
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if(ModelState.IsValid)
            {
                if(company.Id==0)
                {
                    _unitOfWork.Company.Add(company);
                    
                }
                else
                {
                    _unitOfWork.Company.Update(company);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }


        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Company.GetAll();
            return Json(new {data = allObj});
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Company.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            else
            {
                _unitOfWork.Company.Remove(objFromDb);
                _unitOfWork.Save();
                return Json(new {success= true, message = "Deleted Successfully"});
            }
        }

        #endregion
    }
}
