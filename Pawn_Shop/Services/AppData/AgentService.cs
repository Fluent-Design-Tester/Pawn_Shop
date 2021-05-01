using Pawn_Shop.Dto;
using Pawn_Shop.IServices.AppData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Services.AppData
{
    class AgentService : Service, IAgentService
    {
        public AgentService(string uri) : base(uri)
        {
        }

        public async Task<ObservableCollection<T>> GetAllAgents<T>(ObservableCollection<T> list)
        {
            return await GetAll<T>(list);
        }

        public async Task<bool> Save(Agent newAgent)
        {
            return await Save<Agent>(newAgent);
        }

        public async Task<bool> Update(Agent updatedAgent)
        {
            return await Update<Agent>(updatedAgent);
        }

        public async Task<bool> Delete(int id)
        {
            return await DeleteById(id);
        }
    }
}
