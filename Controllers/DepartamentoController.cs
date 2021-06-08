using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webMVC.Models;
using webMVC.Service;

namespace webMVC.Controllers
{
    public class DepartamentoController : Controller
    {
        private DepartamentoServices _depServices = new DepartamentoServices();

        public ActionResult ListDep()
        {
            var model = _depServices.GetDepartamentoList();
            
            return View(model);
        }

        public ActionResult AddDepartamento()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDepartamento(DepartamentoModel model)
        {
            _depServices.InsertDepartamento(model);

            return RedirectToAction("ListDep");
        }

        public ActionResult EditDepartamento(int id)
        {
            var model = _depServices.GetEditById(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditDepartamento(DepartamentoModel model)
        {
            _depServices.UpdateDep(model);

            return RedirectToAction("ListDep");
        }

        public ActionResult DeleteDep(int id)
        {
            _depServices.DeleteDep(id);

            return RedirectToAction("ListDep");
        }
    }
}