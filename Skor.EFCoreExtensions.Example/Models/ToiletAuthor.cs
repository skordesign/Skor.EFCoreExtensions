using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skor.EFCoreExtensions.Example.Models
{
    public class ToiletAuthor
    {
        public long AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public long ToiletId { get; set; }
        public virtual Toilet Toilet { get; set; }
        public DateTime Time { get; set; }
    }
}
