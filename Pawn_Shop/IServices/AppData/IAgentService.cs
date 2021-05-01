using Pawn_Shop.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.IServices.AppData
{
    interface IAgentService
    {
        Task<ObservableCollection<Agent>> GetAllAgents<Agent>(ObservableCollection<Agent> list);

        Task<bool> Save(Agent newAgent);

        Task<bool> Update(Agent updatedAgent);

        Task<bool> Delete(int id);
    }
}
