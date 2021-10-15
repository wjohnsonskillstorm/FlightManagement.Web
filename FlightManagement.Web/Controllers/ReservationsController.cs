using FlightManagement.Data;
using FlightManagement.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagement.Web.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationDAO reservationDao;
        private readonly IPassengerDAO passengerDao;
        private readonly IFlightDAO flightDao;
        
        public ReservationsController(IReservationDAO reservationDAO, IPassengerDAO passengerDAO, IFlightDAO flightDAO)
        {
            this.reservationDao = reservationDAO;
            this.passengerDao = passengerDAO;
            this.flightDao = flightDAO;
        }
        // GET: ReservationsController
        public IActionResult Index(int id)
        {
            
            List<Reservation> reservations = (List<Reservation>)reservationDao.GetRecordsForFlight(id);
            ReservationsHolder holder = new ReservationsHolder(flightDao.GetRecord(id), reservations);
            return View(holder);
        }


        //GET: ReservationsController/Create
        public ActionResult Create(int id)
        {
            ReservationsHolder model = new ReservationsHolder();
            model.reservations = (List<Reservation>)reservationDao.GetRecordsForFlight(id);
            model.flight = flightDao.GetRecord(id);
            model.passengers = (List<Passenger>)passengerDao.GetAllRecords();
            foreach(var passenger in model.passengers)
            {
                foreach(var reservation in model.reservations)
                {
                    if(reservation.passenger == passenger)
                    {
                        model.passengers.Remove(passenger);
                    }
                }
            }
            ViewBag.IsError = false;
            return View(model);
        }

        // POST: ReservationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, IFormCollection collection)
        {
            try
            {

                Reservation tmp = new Reservation(flightDao.GetRecord(id), passengerDao.GetRecord(int.Parse(collection["SelectedPassenger"].ToString())));
                if (reservationDao.AddRecord(tmp))
                {
                    return RedirectToAction("Index", "Reservations", new { id = id });
                }
                else
                {
                    ViewBag.IsError = true;
                    ReservationsHolder model = new ReservationsHolder();
                    model.reservations = (List<Reservation>)reservationDao.GetRecordsForFlight(id);
                    model.flight = flightDao.GetRecord(id);
                    model.passengers = (List<Passenger>)passengerDao.GetAllRecords();
                    foreach (var passenger in model.passengers)
                    {
                        foreach (var reservation in model.reservations)
                        {
                            if (reservation.passenger == passenger)
                            {
                                model.passengers.Remove(passenger);
                            }
                        }
                    }
                    
                    return View(model);
                }

            }
            catch
            {
                return View();
            }
        }


        // GET: ReservationsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(reservationDao.GetRecord(id));            
        }

        // POST: ReservationsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            int flightid = reservationDao.GetRecord(id).flight.Id;          
            try
            {
                reservationDao.DeleteRecord(id);
                return RedirectToAction("Index", new { Id = flightid });
            }
            catch
            {
                return View(reservationDao.GetRecord(id));
            }
        }
    }
}
