using FlightManagement.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagement.Web.Controllers
{
    public class PassengerController : Controller
    {
        private readonly IPassengerDAO passengerDao;
        public PassengerController(IPassengerDAO passengerDAO)
        {
            this.passengerDao = passengerDAO;
        }
        public IActionResult Index()
        {
            IEnumerable<Passenger> model = passengerDao.GetAllRecords();
            return View(model);
        }

        public IActionResult Create()
        {
            
            return View();
        }

        // POST: ReservationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Passenger passenger)
        {
                if (ModelState.IsValid)
                {
                    passengerDao.AddRecord(passenger);
                    return RedirectToAction("Index");
                }
            return View(passenger);
        }

        public IActionResult Edit(int id)
        {
            return View(passengerDao.GetRecord(id));
        }

        // POST: FlightController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind] Passenger model, IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                passengerDao.UpdateRecord(model);
                return RedirectToAction("Index");
            }
            return View(model.Id);
        }

        public IActionResult Delete(int id)
        {
            return View(passengerDao.GetRecord(id));
        }

        // POST: FlightController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                passengerDao.DeleteRecord(id);
                return RedirectToAction("Index");
            }
            return View(passengerDao.GetRecord(id));
        }

    }
}
