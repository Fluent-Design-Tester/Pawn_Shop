using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Dto
{
    public class Major
    {
        [DisplayName("No")]
        public int major_id { get; set; }

        [DisplayName("Short Name")]
        public String short_name { get; set; }

        public Major(int major_id, String short_name)
        {
            this.major_id = major_id;
            this.short_name = short_name;
        }
    }
}
