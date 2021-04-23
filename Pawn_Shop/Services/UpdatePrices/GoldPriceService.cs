using Pawn_Shop.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Services.UpdatePrices
{
    class GoldPriceService : Service
    {
        public GoldPriceService(string uri) : base(uri)
        {
        }

        public async Task<bool> Save(GoldPrice newGoldPrice)
        {
            return await Save<GoldPrice>(newGoldPrice);
        }
    }
}
