using Pawn_Shop.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.IServices.UpdatePrices
{
    interface IShopPriceService
    {
        Task<ShopPrice> GetLatestGoldPrice<ShopPrice>();
    }
}
