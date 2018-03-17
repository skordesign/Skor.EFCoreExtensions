using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skor.EFCoreExtensions.Example.Models
{
    public class Book
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}
