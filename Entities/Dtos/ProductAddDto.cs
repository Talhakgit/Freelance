using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class ProductAddDto:IDto
    {
        public byte CategoryId { get; set; }
        public string? ProductName { get; set; }
    }
}
