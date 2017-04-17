using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Domain;
using Domain.Event;
using Interfaces.UOW;
using WebApi.Models.Events;

namespace WebApi.Controllers
{
    public class EventsController : ApiController
    {
        private readonly IUow _uow;
        private readonly IMapper _autoMapper;

        public EventsController(IUow uow, IMapper autoMapper)
        {
            _autoMapper = autoMapper;
            _uow = uow;
        }

        // GET: api/Events
        [HttpGet]
        public IHttpActionResult GetEvents()
        {
            return Ok(_autoMapper.Map<List<EventDto>>(_uow.Events.All));
        }

        // GET: api/Events/5
        [HttpGet]
        public IHttpActionResult GetEvent(int id)
        {
            Event Event = _uow.Events.GetById(id);
            if (Event == null)
            {
                return NotFound();
            }

            return Ok(_autoMapper.Map<Event, EventDto>(Event));
        }

        //// PUT: api/Events/5
        //[HttpPut]
        //public IHttpActionResult PutEvent(int id, Event Event)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != Event.Id)
        //    {
        //        return BadRequest();
        //    }
        //    _uow.Events.Update(Event);

        //    try
        //    {
        //        _uow.Commit();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EventExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            return InternalServerError();
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}
    }
}
