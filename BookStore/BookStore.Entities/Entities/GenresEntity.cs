using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities.Entities
{
    [Table("tbl_Genres")]
    public class GenresEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GenreId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime IsCreatedOn { get; set; }
        public DateTime IsUpdatedon { get; set; }

        public virtual ICollection<BookEntity> Books { get; set; }

    }                                          
}                                              
