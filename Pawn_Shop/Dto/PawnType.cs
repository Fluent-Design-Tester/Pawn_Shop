using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Dto
{
    class PawnType
    {
        [DisplayName("No")]
        public int type_id { get; set; }

        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Category")]
        public int category_id { get; set; }

        public int display_no { get; set; }

        public PawnType(int typeId, string name, int displayNo)
        {
            this.type_id = typeId;
            this.name = name;
            this.display_no = displayNo;
        }
    }
}
