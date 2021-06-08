using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using webMVC.Models;
using webMVC.Service;

namespace webMVC.Controllers
{
    public class EnderecoController : Controller
    {
        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private EnderecoServices _endServices = new EnderecoServices();
        StringBuilder errorMessages = new StringBuilder();

        public ActionResult ListEnd()
        {
            var model = _endServices.GetEnderecoList();

            return View(model);
        }

        public ActionResult AddEndereco()
        {
            ViewBag.EnderecoModel = new SelectList(_endServices.All(),"IdEnd","IdEmp");

            return View();
        }

        [HttpPost]
        public ActionResult AddEndereco(EnderecoModel model)
        {
            try
            {
                _endServices.InsertEndereco(model);
            }
            catch (SqlException ex)
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


            return RedirectToAction("ListEnd");
        }

        public ActionResult EditEndereco(int Id)
        {
            var model = _endServices.GetEditById(Id);
            //ViewBag.EnderecoModel = new SelectList(_endServices.All(), "IdEnd", "IdEmp");

            return View(model);
        }

        [HttpPost]
        public ActionResult EditEndereco(EnderecoModel model)
        {
            _endServices.UpdateEnd(model);

            return RedirectToAction("ListEnd");
        }

        public ActionResult DeleteEndereco(int id)
        {
            try
            {
                _endServices.DeleteEnd(id);
            }catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("ListEnd");
        }
    }
}