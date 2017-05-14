using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Domain.Course;
using Domain.Identity;
using Interfaces.UOW;
using WebApi.Models.Courses;
using WebApi.Models.Errors;
using static WebApi.Controllers.Helpers.AuthorizationHelper;

namespace WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/Courses")]
    public class CoursesController : ApiController
    {
        private readonly IUow _uow;
        private readonly IMapper _autoMapper;
        private readonly ErrorMessages _errorMessages = new ErrorMessages();

        public CoursesController(IUow uow, IMapper autoMapper)
        {
            _autoMapper = autoMapper;
            _uow = uow;
        }

        // GET: api/Courses
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetCourses()
        {
            return Ok(_autoMapper.Map<List<CourseDto>>(_uow.Courses.All));
        }

        // GET: api/Courses/5
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetCourse(int id)
        {
            Course course = _uow.Courses.GetById(id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(_autoMapper.Map<Course, CourseDto>(course));
        }

        // POST: api/Courses
        [HttpPost]
        public IHttpActionResult PostCourse(CreateCourseDto course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var courseToAdd = _autoMapper.Map<CreateCourseDto, Course>(course);
            _uow.Courses.Add(courseToAdd);
            _uow.Commit();

            return CreatedAtRoute("AsunikAPI", new { id = courseToAdd.Id}, courseToAdd);
        }

        // PUT: api/Courses/5
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IHttpActionResult PutCourse(int id, UpdateCourseDto course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != course.Id)
            {
                return BadRequest();
            }
            var course1 =_autoMapper.Map<UpdateCourseDto, Course>(course);
            _uow.Courses.Update(course1);
            try
            {
                _uow.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                return InternalServerError();
            }

            return Ok();
        }

        // DELETE: api/Courses/5
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IHttpActionResult DeleteCourse(int id)
        {
            Course course = _uow.Courses.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            _uow.Courses.Delete(course);
            try
            {
                _uow.Commit();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok();
        }


        // POST: api/Courses/5/Members
        [Route("{id}/Members")]
        [HttpPost]
        public IHttpActionResult PostMember(int id, MemberRegistrationDto member)
        {
            Course course = _uow.Courses.GetById(id);
            User user = _uow.Users.GetById(member.UserId);
            if (course == null || user == null)
            {
                return NotFound();
            }

            if (!IsValidAuthorization(user.Id, Request))
            {
                return Unauthorized();
            }

            if (MemberExists(course.Id, user.Id))
            {
                _errorMessages.Errors.Add("Already registered");
                return Content(HttpStatusCode.BadRequest, _errorMessages);
            }

            course.Members.Add(new CourseMember()
            {
                User = user,
                Course = course,
                MemberRole = member.MemberRole
            });

            try
            {
                _uow.Commit();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok();
        }


        // DELETE: api/Courses/5/Members/5
        [Authorize(Roles = "Admin")]
        [Route("{id}/Members/{userId}")]
        [HttpDelete]
        public IHttpActionResult DeleteMember(int id, int userId)
        {
            Course course = _uow.Courses.GetById(id);
            User user = _uow.Users.GetById(userId);
            if (course == null || user == null)
            {
                return NotFound();
            }

            if (!MemberExists(course.Id, user.Id))
            {
                return NotFound();
            }
            var courseMember = GetMember(course.Id, user.Id);
            _uow.CourseMembers.Delete(courseMember);

            try
            {
                _uow.Commit();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok();
        }


        private bool CourseExists(int id)
        {
            return _uow.Courses.GetById(id) == null;
        }

        private bool MemberExists(int courseId, int userId)
        {
            return GetMember(courseId, userId) != null;
        }

        private CourseMember GetMember(int courseId, int userId)
        {
            return _uow.Courses.GetById(courseId).Members.FirstOrDefault(m => m.UserId == userId);
        }
    }
}
