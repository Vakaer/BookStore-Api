using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities.Entities
{
    [Table("tbl_Authors")]
    public class AuthorEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public bool IsDeleted{ get; set; }
        public DateTime IsCreatedOn { get; set; }
        public DateTime IsUpdatedon { get; set; }

        //navigation property
        public virtual List<BookEntity> Books { get; set; }
       

    }
}
