using FlightManagement.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagement.Web.Controllers
{
    public class FlightController : Controller
    {
        private readonly IFlightDAO flightDAO;

        public FlightController(IFlightDAO flightDao)
        {
            this.flightDAO = flightDao;
            
        }
        // GET: FlightController
        public IActionResult Index()
        {
            IEnumerable<Flight> model = flightDAO.GetAllRecords();
            return View(model);
        }

        // GET: FlightController/Details/5
        

        // GET: FlightController/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: FlightController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Flight flight)
        {
            if (ModelState.IsValid)
            {
                flightDAO.AddRecord(flight);
                return RedirectToAction("Index");
            }
            return View(flight);
        }

        // GET: FlightController/Edit/5
        public IActionResult Edit(int id)
        {
            return View(flightDAO.GetRecord(id));
        }

        // POST: FlightController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind] Flight model, IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                flightDAO.UpdateRecord(model);
                return RedirectToAction("Index");
            }
            return View(model.Id);
        }
        
        
        

        // GET: FlightController/Delete/5
        public IActionResult Delete(int id)
        {
            return View(flightDAO.GetRecord(id));
        }

        // POST: FlightController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                flightDAO.DeleteRecord(id);
                return RedirectToAction("Index");
            }
            return View(flightDAO.GetRecord(id));
        }
    }
}
