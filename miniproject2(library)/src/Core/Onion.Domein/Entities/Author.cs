using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Domein
{
    public class Author:BaseEntity
    {
        public string Name { get; set; }
        public string? Surname { get; set; }
        public Gender Gender { get; set; }
        public List<Book>? Books { get; set; } = new();
    }
}
