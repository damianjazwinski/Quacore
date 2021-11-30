using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Domain.Responses
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public BaseResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
