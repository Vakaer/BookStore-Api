using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.WebContracts.Book.Request
{
    public class UpdatedBookRequest
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public int Price { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }

    }






    //public class Books{
    //    public int BookId { get; set; }
    //    public string Title { get; set; }
    //    public decimal Price { get; set; }
    //}
    //public class Author
    //{
    //    public int Id { get; set; }
    //    public string AuthorName { get; set; }
    //}
    //public class Genre{
    //    public int GenreId { get; set; }
    //    public string GenreName { get; set; }
    //}
}
