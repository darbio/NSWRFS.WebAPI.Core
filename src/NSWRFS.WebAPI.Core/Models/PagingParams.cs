using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSWRFS.WebAPI.Core.Models
{
    public class PagingParams
    {
        public int page { get; set; }

        public int per_page { get; set; }
    }
}
