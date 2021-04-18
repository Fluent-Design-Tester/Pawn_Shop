using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Dtos
{
    class PawnType : INotifyPropertyChanged
    {
        public int id { get; set; }
        public int displayNo { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public int categoryId { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
