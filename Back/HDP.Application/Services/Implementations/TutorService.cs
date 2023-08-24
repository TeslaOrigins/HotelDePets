using AutoMapper;
using HDP.Application.Exceptions;
using HDP.Application.Services.Contracts;
using HDP.Application.ViewModels;
using HDP.Application.ViewModels.Caminhao;
using HDP.Application.ViewModels.Tutor;
using HDP.Domain.Models;
using HDP.Domain.Models.Constantes;
using HDP.Persistence.Repository.Contracts;

namespace HDP.Application.Services.Implementations;

public class TutorService : ITutorService
{
    private readonly ITutorRepository _TutorRepository;
    private readonly ICaminhaoRepository _caminhaoRepository;
    private readonly IMapper _mapper;
    public TutorService(IMapper mapper,ITutorRepository TutorRepository,ICaminhaoRepository caminhaoRepository){
        _TutorRepository = TutorRepository;
        _mapper = mapper;
        _caminhaoRepository = caminhaoRepository;
    }
    
    public async Task<TutorViewModel[]> GetTutor()
    {
        try{
            return _mapper.Map<TutorViewModel[]>(await _TutorRepository.GetTutor());
        }catch(Exception e){
            throw new Exception(e.Message);
        }
    }
    
    public async Task<TutorViewModel> GetTutorPorId(Guid idTutor)
    {
        try{
            var Tutor = await _TutorRepository.GetTutorPorId(idTutor);
            if(Tutor == null)
                throw new NotFoundException("Não foi encontrado um Tutor com o id especificado");
            return _mapper.Map<TutorViewModel>(Tutor);
        }
        catch(NotFoundException NFE)
        {
            throw new NotFoundException(NFE.Message);
        }
        catch(Exception e){
            throw new Exception(e.Message);
        }
    }
    
    public async Task<TutorViewModel> CadastrarTutor(CadastroTutorViewModel dados)
    {
        try
        {
            List<string> errors = new();
            List<TipoCaminhao> tiposCaminhoes = new();

            var TutorExiste = await ValidaTutorExistente(dados);
            var caminhaoExiste = await ValidaCaminhaoExistente(dados.Caminhoes);
            
            if (TutorExiste)
            {
                errors.Add("Tutor já existe no sistema");
            }

            if (caminhaoExiste != null)
            {
                foreach (var caminhaoExistente in caminhaoExiste)
                {
                    if (caminhaoExistente.Existe)
                        errors.Add(
                            "O caminhão de placa: " + caminhaoExistente.Placa + " já está cadastrado no sistema");
                }
            }

            if (errors.Any())
                throw new Exception(errors.ToArray().ToString());

            var Tutor = _mapper.Map<Tutor>(dados);
            
            Tutor.NomeNormalizado = dados.Name.ToUpper().Trim();
            
            foreach (var caminhao in Tutor.Caminhoes)
            {
                caminhao.TipoCaminhao = await GetTipoCaminhaoAnotherService(caminhao.TipoCaminhaoId);
            }
            
            _TutorRepository.Add(Tutor);
            if (await _TutorRepository.SaveChangesAsync())
            {
                return _mapper.Map<TutorViewModel>(await _TutorRepository.GetTutorPorId(Tutor.Id));
            }

            return null;
        }
        catch (BusinessException<TutorViewModel> BE)
        {
            throw new BusinessException<TutorViewModel>(BE.messages, BE.obj);
        }
        catch (NotFoundException NFE)
        {
            throw new NotFoundException(NFE.Message);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    public async Task<TutorViewModel> AtualizaTutor(AtualizaTutorViewModel dados)
    {
        try
        {
            var TutorDomain = await GetTutorPorIdAnotherService(dados.Id);

            TutorDomain.Cpf = dados.Cpf;

            if (await _TutorRepository.SaveChangesAsync())
            {
                return _mapper.Map<TutorViewModel>(await _TutorRepository.GetTutorPorId(TutorDomain.Id));
            }

            return null;
        }
        catch (BusinessException<TutorViewModel> BE)
        {
            throw new BusinessException<TutorViewModel>(BE.messages, BE.obj);
        }
        catch (NotFoundException NFE)
        {
            throw new NotFoundException(NFE.Message);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    public async Task<bool> RemoveTutor(Guid idTutor)
    {
        try
        {
            var TutorDomain = await GetTutorPorIdAnotherService(idTutor);
            
            _TutorRepository.Delete(TutorDomain);
            
            return await _TutorRepository.SaveChangesAsync();
        }
        catch (BusinessException<TutorViewModel> BE)
        {
            throw new BusinessException<TutorViewModel>(BE.messages, BE.obj);
        }
        catch (NotFoundException NFE)
        {
            throw new NotFoundException(NFE.Message);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private async Task<bool> ValidaTutorExistente(CadastroTutorViewModel dados)
    {
        try
        {
            var Tutor = await GetTutorPorNomeAnotherService(dados.Name.ToUpper().Trim());

            return Tutor != null;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    private async Task<List<CaminhaoExistenteViewModel>> ValidaCaminhaoExistente(ICollection<CadastroCaminhaoViewModel> dados)
    {
        try
        {
            List<CaminhaoExistenteViewModel> caminhaoExistente = new();

            foreach (var caminhao in dados)
            {
                var truck = await _caminhaoRepository.GetCaminhaoPorPlaca(caminhao.PlacaCaminhao);

                if (truck != null)
                {
                    CaminhaoExistenteViewModel novoCaminhaoExistente = new CaminhaoExistenteViewModel()
                    {
                        Existe = true,
                        Placa = truck.PlacaCaminhao
                    };
                    caminhaoExistente.Add(novoCaminhaoExistente);
                }
            }

            return caminhaoExistente;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private async Task<TipoCaminhao> GetTipoCaminhaoAnotherService(Guid idTipoCaminhao)
    {
        var tiposCaminhoes = await _caminhaoRepository.GetTipoCaminhaoPorId(idTipoCaminhao);

        if (tiposCaminhoes != null)
        {
            return tiposCaminhoes;
        }

        return null;
    }
    
    private async Task<Tutor> GetTutorPorNomeAnotherService(string nomeTutor)
    {
        var Tutor = await _TutorRepository.GetTutorPorNomeNormalizado(nomeTutor);

        if (Tutor != null)
        {
            return Tutor;
        }

        return null;
    }
    
    private async Task<Tutor> GetTutorPorIdAnotherService(Guid idTutor)
    {
        var Tutor = await _TutorRepository.GetTutorPorId(idTutor);

        if (Tutor != null)
        {
            return Tutor;
        }

        return null;
    }
    
    private async Task<Caminhao[]> GetCaminhoesAnotherService(Guid idTutor)
    {
        var Tutor = await _caminhaoRepository.GetCaminhoesPorTutorId(idTutor);

        if (Tutor != null)
        {
            return Tutor;
        }

        return null;
    }
}