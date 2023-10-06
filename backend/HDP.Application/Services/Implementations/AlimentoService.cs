using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HDP.Application.Exceptions;
using HDP.Application.Services.Contracts;
using HDP.Application.ViewModels.Alimento;
using HDP.Persistence;
using HDP.Persistence.Repository.Contracts;

namespace HDP.Application.Services.Implementations
{
    public class AlimentoService : IAlimentoService
    {
        private readonly IAlimentoRepository _alimentoRepository;

        private readonly IMapper _mapper;
        public AlimentoService(IAlimentoRepository alimentoRepository,IMapper mapper){
            _alimentoRepository = alimentoRepository;
            _mapper = mapper;
        }
        public async Task<AlimentoViewModel> AlterarAlimento(AlterarAlimentoViewModel dados)
        {
            try {
                var alimento = await _alimentoRepository.GetAlimentoPorId(dados.AlimentoId);
                if(alimento == null)
                    throw new NotFoundException("O alimento especificado não existe");
                
                alimento.DataEntrada = dados.DataEntrada;
                alimento.Nome = dados.Nome;
                alimento.PrecoReabastecimento = dados.PrecoReabastecimento;
                alimento.QuantidadeEstoque = dados.QuantidadeEstoque;


                if(await _alimentoRepository.SaveChangesAsync())
                    return _mapper.Map<AlimentoViewModel>(alimento);
                
                return null;
            }catch(NotFoundException NFE){
                throw NFE;
            }
            catch(BusinessException<AlimentoViewModel> BE){
                throw BE;
            }
            catch(Exception e){
                throw e;
            }
        }

        public async Task<AlimentoViewModel> CadastrarAlimento(CadastroAlimentoViewModel dados)
        {
            try {
                var alimentoCadastrar = _mapper.Map<Alimento>(dados);
                _alimentoRepository.Add(alimentoCadastrar);
                if(await _alimentoRepository.SaveChangesAsync())
                    return _mapper.Map<AlimentoViewModel>(alimentoCadastrar);
                
                return null;
            }catch(NotFoundException NFE){
                throw NFE;
            }
            catch(BusinessException<AlimentoViewModel> BE){
                throw BE;
            }
            catch(Exception e){
                throw e;
            }
        }

        public async Task<AlimentoViewModel[]> GetAlimento()
        {
             try {
                
                
                return _mapper.Map<AlimentoViewModel[]>(await _alimentoRepository.GetAlimento());
            }catch(NotFoundException NFE){
                throw NFE;
            }
            catch(BusinessException<AlimentoViewModel> BE){
                throw BE;
            }
            catch(Exception e){
                throw e;
            }
        }

        public async Task<AlimentoViewModel> GetAlimentoPorId(int idAlimento)
        {
             try {
                var alimento = await _alimentoRepository.GetAlimentoPorId(idAlimento);
                if(alimento == null)
                    throw new NotFoundException("O alimento especificado não existe");
                
     
                return _mapper.Map<AlimentoViewModel>(alimento);
                
                
            }catch(NotFoundException NFE){
                throw NFE;
            }
            catch(BusinessException<AlimentoViewModel> BE){
                throw BE;
            }
            catch(Exception e){
                throw e;
            }
        }

        public async Task<bool> ApagarAlimento(int idAlimento)
        {
            try {
                var alimentoDomain = await _alimentoRepository.GetAlimentoPorId(idAlimento);
            
                _alimentoRepository.Delete(alimentoDomain);
                
                return await _alimentoRepository.SaveChangesAsync();
                
            }catch(NotFoundException NFE){
                throw NFE;
            }
            catch(BusinessException<AlimentoViewModel> BE){
                throw BE;
            }
            catch(Exception e){
                throw e;
            }
        }
    }
}