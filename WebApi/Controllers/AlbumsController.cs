using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Domain.Album;
using Interfaces.UOW;
using WebApi.Models.Albums;

namespace WebApi.Controllers
{
    [RoutePrefix("api/Albums")]
    public class AlbumsController : ApiController
    {
        private readonly IUow _uow;
        private readonly IMapper _autoMapper;

        public AlbumsController(IUow uow, IMapper autoMapper)
        {
            _uow = uow;
            _autoMapper = autoMapper;
        }

        // GET: api/Albums
        [HttpGet]
        public IHttpActionResult GetAlbums()
        {
            return Ok(_autoMapper.Map<List<AlbumDto>>(_uow.Albums.All));
        }

        // GET: api/Albums/5
        [HttpGet]
        public IHttpActionResult GetAlbum(int id)
        {
            return Ok(_autoMapper.Map<Album, AlbumDto>(_uow.Albums.GetById(id)));
        }
    }
}
