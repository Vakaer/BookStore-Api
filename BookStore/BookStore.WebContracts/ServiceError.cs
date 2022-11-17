using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.WebContracts
{
    public class ServiceError
    {
        public ServiceError(string description)
        {
            
            Description = description;
        }

        public string Code { get; set; }
        public string Description { get; set; }
    }
}
