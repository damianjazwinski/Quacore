using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.DTOs.Requests
{
    public class AddQuackRequestDto
    {
        [Required]
        public string Content { get; set; }
    }
}
