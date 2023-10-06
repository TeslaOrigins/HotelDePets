using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDP.Persistence.Repository.Contracts
{
    public interface IAlimentoRepository : IGeneralRepository
    {
        Task<Alimento[]> GetAlimento();
        Task<Alimento> GetAlimentoPorId(int idAlimento);
        
    }
}