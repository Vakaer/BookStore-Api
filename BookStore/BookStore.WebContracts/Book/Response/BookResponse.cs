using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.WebContracts.Book.Response
{
    public class BookResponse
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public string AuthorName { get; set; }
        public string GenreName { get; set; }
        //public GenresResponse Genre { get; set; }
        //public AuthorResponse Author { get; set; }

    }
}
