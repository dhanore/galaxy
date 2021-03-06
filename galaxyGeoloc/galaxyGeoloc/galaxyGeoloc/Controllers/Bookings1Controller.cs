﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using galaxyGeoloc.Models;
using galaxyGeoloc.Repository;

namespace galaxyGeoloc.Controllers
{
    public class Bookings1Controller : ApiController
    {
        private galaxyGeolocContext db2 = new galaxyGeolocContext();

        private IRepository<Booking, galaxyGeolocContext> db;

        public Bookings1Controller()
        {
           // this.db = _db;
            this.db = new Repository<Booking, galaxyGeolocContext>(db2); ;
        }

        // GET: api/Bookings
        public IEnumerable<Booking> GetBookings()
        {
            return db.GetAll();//.Bookings;
        }

        // GET: api/Bookings/5
        [ResponseType(typeof(Booking))]
        public IHttpActionResult GetBooking(int id)
        {
            Booking booking = db.FindBy(o => o.Id == id).FirstOrDefault();
            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        // PUT: api/Bookings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBooking(int id, Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != booking.Id)
            {
                return BadRequest();
            }

            //   db.Entry(booking).State = EntityState.Modified;

            try
            {
                db.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Bookings
        [ResponseType(typeof(Booking))]
        public IHttpActionResult PostBooking(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Create(booking);
            db.Save();

            return CreatedAtRoute("DefaultApi", new { id = booking.Id }, booking);
        }

        // DELETE: api/Bookings/5
        [ResponseType(typeof(Booking))]
        public IHttpActionResult DeleteBooking(int id)
        {
            Booking booking = db.FindBy(o => o.Id == id).FirstOrDefault();
            if (booking == null)
            {
                return NotFound();
            }

            db.Delete(booking);
           // db.SaveChanges();

            return Ok(booking);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookingExists(int id)
        {
            return true;
        }
    }
}