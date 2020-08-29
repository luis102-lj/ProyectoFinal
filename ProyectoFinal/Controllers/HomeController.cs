using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        static UsersContext _miContexto = new UsersContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult Custumers()
        {
            var laConsulta = _miContexto.Customers;
            laConsulta.ToList();
            return View(laConsulta);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Customers model)
        {

            if (ModelState.IsValid)
            {
                    Customers cost = new Customers
                    {
                        CustomerId = model.CustomerId,
                        CompanyName = model.CompanyName,
                        ContactName = model.ContactName,
                        ContactTitle = model.ContactTitle
                    };
                    _miContexto.Customers.Add(cost);
                    _miContexto.SaveChanges();
                    return RedirectToAction("Custumers");
               
            }
            
            return View(model);
        }
       

    public ActionResult Details(string id)
        {
            Customers consulta = _miContexto.Customers.Where(s => s.CustomerId.Equals(id)).FirstOrDefault();
            return View(consulta);
        }

    public ActionResult Edit(string id)
        {
            Customers consulta = _miContexto.Customers.Where(s => s.CustomerId.Equals(id)).FirstOrDefault();
            return View(consulta);
        }
        [HttpPost]
        public ActionResult Edit(Customers model)
        {
            if (ModelState.IsValid)
            {
                Customers customer =_miContexto.Customers.Where(s => s.CustomerId.Equals(model.CustomerId)).FirstOrDefault();
                customer.CompanyName = model.CompanyName;
                customer.ContactName = model.ContactName;
                customer.ContactTitle = model.ContactTitle;
                customer.Address = model.Address;
                customer.City = model.City;
                customer.Region = model.Region;
                customer.PostalCode = model.PostalCode;
                customer.Country = model.Country;
                customer.Phone = model.Phone;
                customer.Fax = model.Fax;
                _miContexto.SaveChanges();
                return RedirectToAction("Custumers");
            }
            return View(model);
        }

        public ActionResult Delete(string id)
        {
            var consulta = _miContexto.Customers.Where(s => s.CustomerId.Equals(id)).FirstOrDefault();
            return View(consulta);
        }
        [HttpPost]
        public ActionResult Delete(Customers model)
        {
            var consulta = _miContexto.Customers.Where(s => s.CustomerId.Equals(model.CustomerId)).FirstOrDefault();
            _miContexto.Customers.Remove(consulta);
            _miContexto.SaveChanges();
            return RedirectToAction("Custumers");
        }
    }
}
