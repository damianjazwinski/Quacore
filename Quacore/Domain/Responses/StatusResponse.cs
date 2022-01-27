using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Domain.Responses
{
    public class StatusResponse : BaseResponse
    {
        public StatusResponse(bool isSuccess) : base(isSuccess)
        {
        }
    }
}
