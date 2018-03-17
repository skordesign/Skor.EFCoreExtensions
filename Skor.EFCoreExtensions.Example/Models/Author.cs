using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skor.EFCoreExtensions.Example.Models
{
    public class Author
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
