using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Responses
{
    class RSNRCTownship
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int nrcRegionId { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }
    }
}
