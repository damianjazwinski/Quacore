using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Domain.Responses
{
    public class LogoutResponse : BaseResponse
    {
        public LogoutResponse(bool isSuccess) : base(isSuccess)
        {
        }
    }
}
