using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain.Album;

namespace WebApi.Models.Albums
{
    public class AlbumDto
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public string Heading { get; set; }

        public int SortOrder { get; set; }

        [MaxLength(512)]
        public string ThumbnailPath { get; set; }

        public List<AlbumPhotoDto> Photos { get; set; }
    }
}