using AutoMapper;
using HDP.Application.Exceptions;
using HDP.Application.Services.Contracts;
using HDP.Application.ViewModels.Hospedagem;
using HDP.Domain.Models;
using HDP.Persistence.Repository.Contracts;

public class HospedagemService : IHospedagemService
{
    private readonly IMapper _mapper;
    private readonly IHospedagemRepository _hospedagemRepository;
    private readonly IPetRepository _petRepository;
    private readonly ITutorRepository _tutorRepository;
    // private readonly IServicoRepository _servicoRepository;
    private readonly IDietaRepository _dietaRepository;
    // private readonly ICuidadosEspeciaisRepository _cuidadosEspeciaisRepository;

    public HospedagemService(
        IMapper mapper,
        IHospedagemRepository hospedagemRepository,
        IPetRepository petRepository,
        ITutorRepository tutorRepository,
        // IServicoRepository servicoRepository,
        IDietaRepository dietaRepository
        // ICuidadosEspeciaisRepository cuidadosEspeciaisRepository
        )
    {
        _mapper = mapper;
        _hospedagemRepository = hospedagemRepository;
        _petRepository = petRepository;
        _tutorRepository = tutorRepository;
        // _servicoRepository = servicoRepository;
        _dietaRepository = dietaRepository;
        // _cuidadosEspeciaisRepository = cuidadosEspeciaisRepository;
    }

    public async Task<HospedagemViewModel> CadastrarHospedagem(CadastroHospedagemViewModel dados)
    {
        List<string> errors = new();

        // Valida se o pet existe
        var pet = await _petRepository.GetPetPorId(dados.PetId);
        if (pet == null) errors.Add("Pet não encontrado.");

      
        // Valida se as datas são consistentes
        if (dados.DataCheckin >= dados.DataCheckout)
            errors.Add("A data de check-in deve ser anterior à data de check-out.");

        // Valida o status da hospedagem
        if (string.IsNullOrEmpty(dados.Status) || !(dados.Status.ToLower() == "reserva" || dados.Status.ToLower() == "hospedagem"))
            errors.Add("Status inválido. Deve ser 'Reserva' ou 'Hospedagem'.");

        // Retorna erros, se houver
        if (errors.Any()) throw new Exception(string.Join(", ", errors));

        // Mapeia os dados para a entidade Hospedagem
        var hospedagem = _mapper.Map<Hospedagem>(dados);
        hospedagem.Status = dados.Status.ToUpper().Trim();
        hospedagem.Paga = false;

        _hospedagemRepository.Add(hospedagem);

        if (await _hospedagemRepository.SaveChangesAsync())
        {
            return _mapper.Map<HospedagemViewModel>(await _hospedagemRepository.GetHospedagemPorId(hospedagem.Hospedagemid, true, true, true, true));
        }

        return null;
    }

    public async Task<HospedagemViewModel> GetHospedagemPorId(Guid id)
    {
        var hospedagem = await _hospedagemRepository.GetHospedagemPorId(id, true, true, true, true);
        if (hospedagem == null)
            throw new NotFoundException("Hospedagem não encontrada.");

        return _mapper.Map<HospedagemViewModel>(hospedagem);
    }

    public async Task<IEnumerable<HospedagemViewModel>> ListarHospedagens()
    {
        var hospedagens = await _hospedagemRepository.ListarHospedagens(true, true, true, true);
        return _mapper.Map<IEnumerable<HospedagemViewModel>>(hospedagens);
    }

    public async Task<IEnumerable<HospedagemViewModel>> ListarHospedagensPorStatus(string status)
    {
        var hospedagens = await _hospedagemRepository.ListarHospedagensPorStatus(status, true, true, true, true);
        return _mapper.Map<IEnumerable<HospedagemViewModel>>(hospedagens);
    }

    public async Task<IEnumerable<HospedagemViewModel>> ListarHospedagensPorPeriodo(DateTime dataInicio, DateTime dataFim)
    {
        var hospedagens = await _hospedagemRepository.ListarHospedagensPorPeriodo(dataInicio, dataFim, true, true, true, true);
        return _mapper.Map<IEnumerable<HospedagemViewModel>>(hospedagens);
    }

    public async Task<HospedagemViewModel> AtualizarHospedagem(Guid id, AlterarHospedagemViewModel dados)
    {
        List<string> errors = new();

        var hospedagem = await _hospedagemRepository.GetHospedagemPorId(id, true, true, true, true);
        if (hospedagem == null)
            throw new NotFoundException("Hospedagem não encontrada.");

        // Valida Pet e Tutor
        var pet = await _petRepository.GetPetPorId(dados.PetId);
        if (pet == null) errors.Add("Pet não encontrado.");

        // Valida datas de check-in e check-out
        if (dados.DataCheckin >= dados.DataCheckout)
            errors.Add("A data de check-in deve ser anterior à data de check-out.");

        if (errors.Any()) throw new Exception(string.Join(", ", errors));

        // Atualiza os dados da hospedagem
        _mapper.Map(dados, hospedagem);
        _hospedagemRepository.Update(hospedagem);

        if (await _hospedagemRepository.SaveChangesAsync())
        {
            return _mapper.Map<HospedagemViewModel>(await _hospedagemRepository.GetHospedagemPorId(hospedagem.Hospedagemid, true, true, true, true));
        }

        return null;
    }

    public async Task<bool> DeletarHospedagem(Guid id)
    {
        var hospedagem = await _hospedagemRepository.GetHospedagemPorId(id, true, true, true, true);
        if (hospedagem == null)
            throw new NotFoundException("Hospedagem não encontrada.");

        _hospedagemRepository.Delete(hospedagem);
        return await _hospedagemRepository.SaveChangesAsync();
    }

    public async Task<HospedagemViewModel> CancelarHospedagem(Guid idHospedagem)
    {
        var hospedagem = await _hospedagemRepository.GetHospedagemPorId(idHospedagem, true, true, true, true);
        if (hospedagem == null)
            throw new NotFoundException("Hospedagem não encontrada.");
        hospedagem.Status = "cancelada";
        await _hospedagemRepository.SaveChangesAsync();
        return _mapper.Map<HospedagemViewModel>(await _hospedagemRepository.GetHospedagemPorId(hospedagem.Hospedagemid, true, true, true, true));;
    }
}
