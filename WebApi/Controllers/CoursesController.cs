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

namespace WebApi.Controllers
{
    [RoutePrefix("api/Courses")]
    public class CoursesController : ApiController
    {
        private readonly IUow _uow;
        private readonly IMapper _autoMapper;

        public CoursesController(IUow uow, IMapper autoMapper)
        {
            _autoMapper = autoMapper;
            _uow = uow;
        }

        // GET: api/Courses
        [HttpGet]
        public IHttpActionResult GetCourses()
        {
            return Ok(_autoMapper.Map<List<CourseDto>>(_uow.Courses.All));
        }

        // GET: api/Courses/5
        [HttpGet]
        public IHttpActionResult GetCourse(int id)
        {
            Course course = _uow.Courses.GetById(id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(_autoMapper.Map<Course, CourseDto>(course));
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

            if (MemberExists(course.Id, user.Id))
            {
                return BadRequest();
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

        private bool MemberExists(int courseId, int userId)
        {
            return _uow.Courses.GetById(courseId).Members.FirstOrDefault(m => m.UserId == userId) != null;
        }
    }
}
