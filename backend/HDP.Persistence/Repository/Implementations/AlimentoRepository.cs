using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HDP.Persistence.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HDP.Persistence.Repository.Implementations
{
    public class AlimentoRepository : GeneralRepository, IAlimentoRepository
    {
        private readonly HDPContext _context;
        public AlimentoRepository(HDPContext context) : base(context)
        {
            _context = context;
        }
    
        public async Task<Alimento[]> GetAlimento()
        {
            var mainQuery = from Alimento in _context.Alimento
                            select Alimento;
            return await mainQuery.ToArrayAsync();    
        }

        public async Task<Alimento> GetAlimentoPorId(int idAlimento)
        {
            var mainQuery = from Alimento in _context.Alimento
                            where idAlimento == Alimento.AlimentoId
                            select Alimento;
            return await mainQuery.FirstOrDefaultAsync();    
        }


    }
}