using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Domain.Course;
using Interfaces.UOW;
using WebApi.Models.Courses;

namespace WebApi.Controllers
{
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
    }
}
