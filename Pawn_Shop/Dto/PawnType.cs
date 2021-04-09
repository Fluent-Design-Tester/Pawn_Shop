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

        public PawnType(int type_id, string name)
        {
            this.type_id = type_id;
            this.name = name;
        }
    }
}
