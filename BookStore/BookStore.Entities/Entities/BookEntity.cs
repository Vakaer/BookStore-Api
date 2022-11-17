using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities.Entities
{
    [Table("tbl_books")]
    public class BookEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{ get; set; }
        public string Name { get; set; }
        public int Price{ get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }


        //navigation properties
        [ForeignKey(nameof(AuthorEntity))]
        public int AuthorId { get; set; }
        public AuthorEntity AuthorEntity { get; set; }
        [ForeignKey(nameof(GenresEntity))]
        public int GenreId { get; set; }
        public GenresEntity GenresEntity { get; set; }
    }
}
