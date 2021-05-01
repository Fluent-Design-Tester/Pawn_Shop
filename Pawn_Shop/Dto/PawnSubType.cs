using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Dto
{
    class PawnSubType
    {
        public int id { get; set; }
        public int displayNo { get; set; }
        public string name { get; set; }
        public int typeId { get; set; }
        public DateTime? createdDate { get; set; }
        public DateTime? updatedDate { get; set; }
    }
}
