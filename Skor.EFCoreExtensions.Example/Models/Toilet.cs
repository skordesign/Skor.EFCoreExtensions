using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skor.EFCoreExtensions.Example.Models
{
    public class Toilet
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ToiletAuthor> ToiletAuthors { get; set; }
    }
}
