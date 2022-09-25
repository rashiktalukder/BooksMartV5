using BooksMartV5.DataAccess.Repository.IRepository;
using BooksMartV5.Models;
using BooksMartV5.Utility;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace BooksMartV5.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if(id==null)
            {
                return View(coverType);
            }
            else
            {
                /*coverType = _unitOfWork.CoverType.Get(id.GetValueOrDefault());*/
                var parameter = new DynamicParameters();
                parameter.Add("@Id", id);
                coverType=_unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get, parameter);

                if(coverType==null)
                {
                    return NotFound();
                }
                else
                {
                    return View(coverType);
                }
            }

        }

        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            //var allObj = _unitOfWork.CoverType.GetAll(); 
            var allObj = _unitOfWork.SP_Call.List<CoverType>(SD.Proc_CoverType_GetAll, null);//this is for store procedure
            return Json(new {data=allObj});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            if(ModelState.IsValid)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Name", coverType.Name);
                if(coverType.Id == 0)
                {
                    /*_unitOfWork.CoverType.Add(coverType);*/
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Create, parameter);
                }
                else
                {
                    parameter.Add("@Id",coverType.Id);
                    /*_unitOfWork.CoverType.Update(coverType);*/
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Update, parameter);   
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(coverType);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var parameter = new DynamicParameters();//For store proc
            parameter.Add("@Id", id);//For store proc

            //var objFromDb = _unitOfWork.CoverType.Get(id);

            var objFromDb=_unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get,parameter);//For store proc

            if (objFromDb==null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }
            else
            {
                /*_unitOfWork.CoverType.Remove(objFromDb);*/
                _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Delete,parameter); //For store proc
                _unitOfWork.Save();
                return Json(new { success = true, message = "Deleted Successfully" });
            }
        }


        #endregion
    }
}
