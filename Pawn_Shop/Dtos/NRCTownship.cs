using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Pawn_Shop.Dtos
{
    class NRCTownship : INotifyPropertyChanged
    {
        public int id { get; set; }
        public int displayNo { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int nrcRegionId { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
