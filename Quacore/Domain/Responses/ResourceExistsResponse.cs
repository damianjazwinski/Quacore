using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Domain.Responses
{
    public class ResourceExistsResponse : BaseResponse
    {
        public bool Exists { get; }

        public ResourceExistsResponse(bool isSuccess, bool exists) : base(isSuccess)
        {
            Exists = exists;
        }
    }
}
