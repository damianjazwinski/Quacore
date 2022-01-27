using Quacore.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Domain.Responses
{
    public class GetQuacksResponse : BaseResponse
    {
        public IEnumerable<Quack> Quacks { get; }
        public GetQuacksResponse(bool isSuccess, IEnumerable<Quack> quacks) : base(isSuccess)
        {
            Quacks = quacks;
        }
    }
}
