using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Dto
{
    class NRCTownship
    {
        public int nrc_township_id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public int nrc_region_id { get; set; }

        public int display_no { get; set; }

        public NRCTownship(int nrc_township_id, string name, string description, int displayNo)
        {
            this.nrc_township_id = nrc_township_id;
            this.display_no = displayNo;
            this.name = name;
            this.description = description;
        }

        public NRCTownship(int nrc_township_id, string name)
        {
            this.nrc_township_id = nrc_township_id;
            this.name = name;
        }

        public NRCTownship()
        {
        }
    }
}
