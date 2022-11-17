using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.WebContracts.Author.Response
{
    public class AuthorResponse
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime IsCreatedOn { get; set; }
        public DateTime IsUpdatedon { get; set; }
    }
}
