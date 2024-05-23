using Domain.DomainServicesInterfaces;
using Domain.Dtos.TipoDependente.Input;
using Domain.Entities;
using Domain.Interfaces.Repository;
using UtilService.Util;

namespace ApplicationServices;

/// <inheritdoc />
public class TipoDependenteService : ITipoDependenteService
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Construtor do núcleo de serviço de Tipo Moradia
    /// </summary>
    /// <param name="unitOfWork"></param>
    public TipoDependenteService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<List<TipoDependente>> ListarAsync()
    {
        return (await _unitOfWork.TipoDependenteRepository.AllAsync()).ToList();
    }

    /// <inheritdoc />
    public async Task<TipoDependente> EditarAsync(TipoDependenteEditarInModel model)
    {
        model.TrimStringProperties();
        model.ValidateModel();

        var assistidoAntesAlteracao = await _unitOfWork.TipoDependenteRepository.FirstAsync(o => o.TpDe_ID_TipoDependente == model.TpDe_ID_TipoDependente);

        if (assistidoAntesAlteracao == null)
            throw new Exception("Tipo de dependente não localizada no banco de dados.");

        model.TpDe_NM_Descricao = model.TpDe_NM_Descricao;

        var assistidoAlterado = await _unitOfWork.TipoDependenteRepository.FirstAsync(o => o.TpDe_ID_TipoDependente == model.TpDe_ID_TipoDependente);

        assistidoAlterado.TpDe_NM_Descricao = model.TpDe_NM_Descricao;

        await _unitOfWork.SaveAsync();

        //    await _logGenericoService.AdicionarAsync(
        //        assistidoAntesAlteracao,
        //        assistidoAlterado,
        //        assistidoAlterado.TpDe_ID_TipoDependente.ToString(),
        //        model.IDUsuarioCadastro);

        return assistidoAntesAlteracao;
    }

    /// <inheritdoc />
    public async Task<TipoDependente> RetornarAsync(TipoDependenteRetornarInModel model)
    {
        var dado = await _unitOfWork.TipoDependenteRepository.FirstAsync(o => o.TpDe_ID_TipoDependente == model.TpDe_ID_TipoDependente);

        if (dado == null)
        {
            throw new Exception("Tipo de dependente não localizado.");
        }

        return dado;
    }

    /// <inheritdoc />
    public async Task<TipoDependente> RetornarDadosBasicosAsync(int? codTipoDependente)
    {
        var dado = await _unitOfWork.TipoDependenteRepository.FirstAsync(o => o.TpDe_ID_TipoDependente == codTipoDependente);
        if (dado == null)
        {
            throw new Exception("Tipo de Dependente não localizada.");
        }

        return dado;
    }

    /// <inheritdoc />
    public async Task<TipoDependente> RetornarDadosPorDescricaoAsync(TipoDependenteRetornaPorDescricaoInModel model)
    {
        var dado = await _unitOfWork.TipoDependenteRepository.FirstAsync(o => o.TpDe_NM_Descricao == model.TpDe_NM_Descricao);

        if (dado == null)
        {
            throw new Exception("Tipo de dependente não localizado.");
        }

        return dado;
    }

    /// <inheritdoc />
    public async Task AdicionarTipoDependenteAsync()
    {
        if (await _unitOfWork.TipoDependenteRepository.AnyAsync())
            throw new Exception("Os tipos de dependentes já estão cadastradas no banco de dados");

        await _unitOfWork.TipoDependenteRepository.AddAsync(new TipoDependente { TpDe_NM_Descricao = "Não Tem" });
        await _unitOfWork.TipoDependenteRepository.AddAsync(new TipoDependente { TpDe_NM_Descricao = "Somente 1" });
        await _unitOfWork.TipoDependenteRepository.AddAsync(new TipoDependente { TpDe_NM_Descricao = "Possui 2" });
        await _unitOfWork.TipoDependenteRepository.AddAsync(new TipoDependente { TpDe_NM_Descricao = "Entre 3 a 5" });
        await _unitOfWork.TipoDependenteRepository.AddAsync(new TipoDependente { TpDe_NM_Descricao = "Entre 6 a 10" });
        await _unitOfWork.TipoDependenteRepository.AddAsync(new TipoDependente { TpDe_NM_Descricao = "Com Deficiência" });

        await _unitOfWork.SaveAsync();
    }
}