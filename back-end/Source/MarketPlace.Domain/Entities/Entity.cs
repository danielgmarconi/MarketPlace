using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Domain.Entities
{
    public class Entity
    {
        public int Id { get; set; }
        public int IdUserCreate { get; set; }
        public int? IdUserUpdate { get; set; }
        public int DateCreate { get; set; }
        public int? DateUpdate { get; set; }
    }
}
