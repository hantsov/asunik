using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Domain.Identity;
using Interfaces.UOW;
using WebApi.Models.Users;
using System;
using WebApi.Models.Courses;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNet.Identity;

namespace WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private readonly IUow _uow;
        private readonly IMapper _autoMapper;

        public UsersController(IUow uow, IMapper autoMapper)
        {
            _autoMapper = autoMapper;
            _uow = uow;
        }

        // GET: api/Users
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetUsers()
        {
            return Ok(_autoMapper.Map<List<UserDto>>(_uow.Users.All));
        }

        // GET: api/Users/5
        [HttpGet]
        public IHttpActionResult GetUser(int id)
        {
            User user = _uow.Users.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            if (!IsValidAuthorization(id))
            {
                return Unauthorized();
            }
            return Ok(_autoMapper.Map<User, UserDto>(user));
        }

        // PUT: api/Users/5
        [HttpPut]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!IsValidAuthorization(id))
            {
                return Unauthorized();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }
            _uow.Users.Update(user);

            try
            {
                _uow.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                return InternalServerError();
            }

            return Ok();
        }


        [Route("{id}/Courses")]
        [HttpGet]
        public IHttpActionResult GetCourses(int id)
        {
            if (!IsValidAuthorization(id))
            {
                return Unauthorized();
            }
            return Ok(_autoMapper.Map<List<UsersCourseMemberDto>>(_uow.Users.GetById(id).Courses));
        }

        [Route("{id}/Roles")]
        [HttpGet]
        public IHttpActionResult GetRoles(int id)
        {
            if (!IsValidAuthorization(id))
            {
                return Unauthorized();
            }
            return Ok(_autoMapper.Map<UserRoleDto>(_uow.Users.GetById(id).Roles));
        }

        //// POST: api/Users
        //[HttpPost]
        //[ResponseType(typeof(User))]
        //public IHttpActionResult PostUser(User user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _uow.Users.Add(user);
        //    _uow.Commit();

        //    return CreatedAtRoute("AsunikAPI", new { id = user.UserId }, user);
        //}

        //// DELETE: api/Users/5
        //[HttpDelete]
        //public IHttpActionResult DeleteUser(int id)
        //{
        //    User user = _uow.Users.Find(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _uow.Users.Remove(user);
        //    _uow.Commit();

        //    return Ok(user);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _uow.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool UserExists(int id)
        {
            return _uow.Users.GetById(id) == null;
        }

        private bool IsValidAuthorization(int userId)
        {
            if (Request.GetRequestContext().Principal.IsInRole("Admin"))
            {
                return true;
            }
            return userId == Convert.ToInt32(Request.GetRequestContext().Principal.Identity.GetUserId());
        }
    }
}