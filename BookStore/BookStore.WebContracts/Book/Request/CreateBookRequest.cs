using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.WebContracts.Book.Request
{
    public class CreateBookRequest
    {
        [MaxLength(300)]
        public string Name { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }

        public int GenreId { get; set; }
        public int AuthorId { get; set; }


    }
}
