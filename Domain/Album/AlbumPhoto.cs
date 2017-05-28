using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Album
{
    public class AlbumPhoto
    {
        public int Id { get; set; }

        [MaxLength(512)]
        public string FilePath { get; set; }

        public int SortOrder { get; set; }

        public virtual Album Photo { get; set; }
    }
}
