using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreMovies.Entities
{
    public abstract class AuditableEntity
    {
        public string CreateBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}