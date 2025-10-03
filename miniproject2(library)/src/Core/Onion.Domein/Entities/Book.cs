using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Domein
{
    public class Book:BaseEntity
    {
        public string Name { get; set; }
        public int PageCount { get; set; }
        public int? AuthorId { get; set; }
        public Author? Author { get; set; } //muellifi bilinmeyen kitablar var. sonradan add oluna bilir muellif
        public List<ReservedItem> ReservedItems{ get; set; }
}
}
