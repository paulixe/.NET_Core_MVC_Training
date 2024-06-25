using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UdemyFormation.DataAccess.Data;
using UdemyFormation.DataAccess.Repository.IRepository;
using UdemyFormation.Models;
using System.Web;
using Azure.Core.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using UdemyFormation.Utility;

namespace UdemyFormationWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Consts.Admin_Role)]
    public class CompanyController : Controller
    {
        private IWebHostEnvironment webHostEnvironment;
        private IUnitOfAction unitOfAction;
        public ICompanyRepository CompanyRepository => unitOfAction.Company;

        public CompanyController(IUnitOfAction unitOfAction, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfAction = unitOfAction;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Company> objCompanyList = CompanyRepository.GetAll().ToList();
            return View(objCompanyList);
        }

        public IActionResult Upsert(int? id)
        {
            Company Company = (id == null || id == 0) ? new Company() : unitOfAction.Company.Get(p => p.Id == id);
            return View(Company);
        }

        [HttpPost]
        public IActionResult Upsert(Company Company)
        {
            bool isCreate = Company.Id == 0;
            if (ModelState.IsValid)
            {
                if (isCreate)
                {
                    CompanyRepository.Add(Company);
                }
                else
                {
                    CompanyRepository.Update(Company);
                }

                unitOfAction.Save();
                TempData["success"] = $"Company correctly {(isCreate ? "Created" : "Updated")}  WOWOWOOWOWO!!!";
                return RedirectToAction("Index");
            }
            return View(Company);
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Company? Company = CompanyRepository.Get(cat => cat.Id == id);
            if (Company is null)
            {
                return NotFound();
            }

            CompanyRepository.Remove(Company);
            unitOfAction.Save();
            return Json(new { success = true, message = "Delete successful" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var Companys = CompanyRepository.GetAll();

            return Json(new { data = Companys });
        }
    }
}