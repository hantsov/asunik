using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Course;

namespace Domain.Album
{
    public class Album
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public string Heading { get; set; }

        public int SortOrder { get; set; }

        [MaxLength(512)]
        public string ThumbnailPath { get; set; }

        public virtual List<AlbumPhoto> Photos { get; set; }
    }
}
