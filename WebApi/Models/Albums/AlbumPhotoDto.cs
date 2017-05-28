using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain.Album;

namespace WebApi.Models.Albums
{
    public class AlbumPhotoDto
    {
        public int Id { get; set; }

        [MaxLength(512)]
        public string FilePath { get; set; }

        public int SortOrder { get; set; }
    }
}