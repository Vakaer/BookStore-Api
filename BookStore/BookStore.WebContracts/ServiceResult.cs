using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.WebContracts
{
    public class ServiceResult
    {
        public bool IsOk { get; set; }
        public bool IsError => !IsOk;
        public HttpStatusCode Status { get; set; }
        public List<ServiceError> ErrorMessages { get; set; } = new List<ServiceError>();

        public ServiceResult(HttpStatusCode status,bool isOk)
        {
            Status = status;
            IsOk = isOk;
        }
        public virtual void AddError(string description)
        {
            ServiceError serviceError = new ServiceError(description);
            ErrorMessages.Add(serviceError);
        }
    }
}
