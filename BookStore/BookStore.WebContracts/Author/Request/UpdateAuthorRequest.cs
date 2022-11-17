using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.WebContracts.Author.Request
{
    public class UpdateAuthorRequest
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}
