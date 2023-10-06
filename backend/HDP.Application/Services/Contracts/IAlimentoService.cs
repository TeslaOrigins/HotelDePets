using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HDP.Application.ViewModels.Alimento;

namespace HDP.Application.Services.Contracts
{
    public interface IAlimentoService
    {
        Task<AlimentoViewModel[]> GetAlimento();
        Task<AlimentoViewModel> GetAlimentoPorId(int idAlimento);
        Task<AlimentoViewModel> CadastrarAlimento(CadastroAlimentoViewModel dados);
        Task<AlimentoViewModel> AlterarAlimento(AlterarAlimentoViewModel dados);
        Task<bool> ApagarAlimento(int idAlimento);
    }
}