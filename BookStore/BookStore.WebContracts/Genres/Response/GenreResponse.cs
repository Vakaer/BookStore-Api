using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.WebContracts.Genres.Response
{
    public class GenreResponse
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public DateTime IsCreatedOn { get; set; }
        public DateTime IsUpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
