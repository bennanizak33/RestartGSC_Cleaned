using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using McDonalds.Data.Context;
using McDonalds.Data.Models;

namespace McDonalds.ApiControllers
{
    public class ServerEventsController : ApiController
    {
        private McDonaldsContext Context = new McDonaldsContext();

        // GET: api/ServerEvents
        public IQueryable<ServerEvent> GetServerEvents()
        {
            return Context.ServerEvents;
        }

        // GET: api/ServerEvents/5
        [ResponseType(typeof(ServerEvent))]
        public IHttpActionResult GetServerEvent(int id)
        {
            ServerEvent serverEvent = Context.ServerEvents.Find(id);
            if (serverEvent == null)
            {
                return NotFound();
            }

            return Ok(serverEvent);
        }

        // PUT: api/ServerEvents/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutServerEvent(int id, ServerEvent serverEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != serverEvent.ServerEventId)
            {
                return BadRequest();
            }

            serverEvent.UpTimes = serverEvent.UpTimes.HasValue ? serverEvent.UpTimes.Value.Date : serverEvent.UpTimes;

            Context.Entry(serverEvent).State = EntityState.Modified;

            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServerEventExists(id))
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

        // POST: api/ServerEvents
        [ResponseType(typeof(ServerEvent))]
        public IHttpActionResult PostServerEvent(ServerEvent serverEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (serverEvent == null)
            {
                return BadRequest($"{nameof(serverEvent)} est null");
            }

            serverEvent.UpTimes = serverEvent.UpTimes.HasValue ? serverEvent.UpTimes.Value.Date : serverEvent.UpTimes;

            Context.ServerEvents.Add(serverEvent);
            Context.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = serverEvent.ServerEventId }, serverEvent);
        }

        // DELETE: api/ServerEvents/5
        [ResponseType(typeof(ServerEvent))]
        public IHttpActionResult DeleteServerEvent(int id)
        {
            ServerEvent serverEvent = Context.ServerEvents.Find(id);
            if (serverEvent == null)
            {
                return NotFound();
            }

            Context.ServerEvents.Remove(serverEvent);
            Context.SaveChanges();

            return Ok(serverEvent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServerEventExists(int id)
        {
            return Context.ServerEvents.Count(e => e.ServerEventId == id) > 0;
        }
    }
}