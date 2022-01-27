using Quacore.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Domain.Responses
{
    public class GetQuackResponse : BaseResponse
    {
        public Quack Quack { get; }
        public GetQuackResponse(bool isSuccess, Quack quack) : base(isSuccess)
        {
            Quack = quack;
        }
    }
}
