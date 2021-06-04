using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using webMVC.Models;
using webMVC.Service;

namespace webMVC.Controllers
{
    public class EmployeeController : Controller
    {
        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private EmployeeServices _empServices = new EmployeeServices();
        StringBuilder errorMessages = new StringBuilder();

        public ActionResult List()
        {
            var model = _empServices.GetEmployeeList();
            
            return View(model);
        }

        public ActionResult AddEmployee()
        {
            ViewBag.DepartamentoModel = new SelectList(_empServices.All(), "Id", "DepName");

            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(EmployeeModel model)
        {
            try
            {
                _empServices.InsertEmployee(model);
            }
            catch(SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                Console.WriteLine(errorMessages.ToString());
            }
            

            return RedirectToAction("List");
        }

        public ActionResult EditEmployee(int Id)
        {
            var model = _empServices.GetEditById(Id);
            ViewBag.DepartamentoModel = new SelectList(_empServices.All(), "Id", "DepName");

            return View(model);
        }

        [HttpPost]
        public ActionResult EditEmployee(EmployeeModel model)
        {
            _empServices.UpdateEmp(model);

            return RedirectToAction("List");
        }

        public ActionResult DeleteEmployee(int Id)
        {
            _empServices.DeleteEmp(Id);

            return RedirectToAction("List");
        }
    }
}