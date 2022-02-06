using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.DTOs.Responses
{
    public class GetFeedResponseDto
    {
        public IEnumerable<QuackDto> Quacks { get; set; }
        public bool AreAnyQuacksLeft { get; set; }
    }

    public class QuackDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string CreatedAt { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public IEnumerable<string> Mentions { get; set; }
    }
}
