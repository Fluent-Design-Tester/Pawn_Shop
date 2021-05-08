using Pawn_Shop.Dto.ShopPrice.UpdateShopPrice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.IServices.UpdatePrices
{
    interface IUpdateShopPriceService
    {
        Task<bool> Save(UpdatingShopPrice newShopPrice);
    }
}
