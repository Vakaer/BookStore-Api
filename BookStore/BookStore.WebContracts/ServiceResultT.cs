using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.WebContracts
{
    public class ServiceResult<T> : ServiceResult
    {
        public T Result { get; protected set; } 
        //passing false in base constructor of ServiceResult means there is an error in the code 
        //IsError => !isOk --- IsError => !false --- isError => true
        public ServiceResult(HttpStatusCode status) : base(status, false)
        {
        }
        public ServiceResult(HttpStatusCode status, T result): base(status, true)
        {
            Result = result;
        }
        public ServiceResult(T result) :base(HttpStatusCode.OK, true)
        {
            Result = result;
        }
    }
}
