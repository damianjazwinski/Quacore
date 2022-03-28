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
        public bool AreAnyQuacksLeft { get; }
        public GetQuacksResponse(bool isSuccess, IEnumerable<Quack> quacks, bool isAnyQuacksLeft) : base(isSuccess)
        {
            Quacks = quacks;
            AreAnyQuacksLeft = isAnyQuacksLeft;
        }
    }
}
