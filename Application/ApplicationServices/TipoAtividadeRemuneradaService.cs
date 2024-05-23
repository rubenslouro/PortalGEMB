using Domain.DomainServicesInterfaces;
using Domain.Dtos.TipoAtividadeRemunerada.Input;
using Domain.Entities;
using Domain.Interfaces.Repository;
using UtilService.Util;

namespace ApplicationServices;

/// <inheritdoc />
public class TipoAtividadeRemuneradaService : ITipoAtividadeRemuneradaService
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Construtor do núcleo de serviço de Tipo Moradia
    /// </summary>
    /// <param name="unitOfWork"></param>
    public TipoAtividadeRemuneradaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<List<TipoAtividadeRemunerada>> ListarAsync()
    {
        return (await _unitOfWork.TipoAtividadeRemuneradaRepository.AllAsync()).ToList();
    }

    /// <inheritdoc />
    public async Task<TipoAtividadeRemunerada> EditarAsync(TipoAtividadeRemuneradaEditarInModel model)
    {
        model.TrimStringProperties();
        model.ValidateModel();

        var assistidoAntesAlteracao = await _unitOfWork.TipoAtividadeRemuneradaRepository.FirstAsync(o => o.TpAR_ID_TipoAtividadeRemunerada == model.TpAR_ID_TipoAtividadeRemunerada);

        if (assistidoAntesAlteracao == null)
            throw new Exception("Tipo de atividade remunerada não localizada no banco de dados.");

        model.TpAR_NM_Descricao = model.TpAR_NM_Descricao;

        var assistidoAlterado = await _unitOfWork.TipoAtividadeRemuneradaRepository.FirstAsync(o => o.TpAR_ID_TipoAtividadeRemunerada == model.TpAR_ID_TipoAtividadeRemunerada);

        assistidoAlterado.TpAR_NM_Descricao = model.TpAR_NM_Descricao;

        await _unitOfWork.SaveAsync();

        //    await _logGenericoService.AdicionarAsync(
        //        assistidoAntesAlteracao,
        //        assistidoAlterado,
        //        assistidoAlterado.TpAR_ID_TipoAtividadeRemunerada.ToString(),
        //        model.IDUsuarioCadastro);

        return assistidoAntesAlteracao;
    }

    /// <inheritdoc />
    public async Task<TipoAtividadeRemunerada> RetornarAsync(TipoAtividadeRemuneradaRetornarInModel model)
    {
        var dado = await _unitOfWork.TipoAtividadeRemuneradaRepository.FirstAsync(o => o.TpAR_ID_TipoAtividadeRemunerada == model.TpAR_ID_TipoAtividadeRemunerada);

        if (dado == null)
        {
            throw new Exception("Tipo de atividade remunerada não localizada.");
        }

        return dado;
    }

    /// <inheritdoc />
    public async Task<TipoAtividadeRemunerada> RetornarDadosBasicosAsync(int? codTipoAtividadeRemunerada)
    {
        var dado = await _unitOfWork.TipoAtividadeRemuneradaRepository.FirstAsync(o => o.TpAR_ID_TipoAtividadeRemunerada == codTipoAtividadeRemunerada);
        if (dado == null)
        {
            throw new Exception("Tipo de atividade remunerada não localizada.");
        }

        return dado;
    }

    /// <inheritdoc />
    public async Task<TipoAtividadeRemunerada> RetornarDadosPorDescricaoAsync(TipoAtividadeRemuneradaRetornaPorDescricaoInModel model)
    {
        var dado = await _unitOfWork.TipoAtividadeRemuneradaRepository.FirstAsync(o => o.TpAR_NM_Descricao == model.TpAR_NM_Descricao);
        if (dado == null)
        {
            throw new Exception("Tipo de atividade remunerada não localizada.");
        }

        return dado;
    }

    /// <inheritdoc />
    public async Task AdicionarTipoAtividadeRemuneradaAsync()
    {
        if (await _unitOfWork.TipoAtividadeRemuneradaRepository.AnyAsync())
            throw new Exception("Os tipos de atividade remunerada já estão cadastradas no banco de dados");

        await _unitOfWork.TipoAtividadeRemuneradaRepository.AddAsync(new TipoAtividadeRemunerada { TpAR_NM_Descricao = "Assalariado" });
        await _unitOfWork.TipoAtividadeRemuneradaRepository.AddAsync(new TipoAtividadeRemunerada { TpAR_NM_Descricao = "Esporádico" });
        await _unitOfWork.TipoAtividadeRemuneradaRepository.AddAsync(new TipoAtividadeRemunerada { TpAR_NM_Descricao = "Aposentado" });
        await _unitOfWork.TipoAtividadeRemuneradaRepository.AddAsync(new TipoAtividadeRemunerada { TpAR_NM_Descricao = "Bolsa Auxílio" });
        await _unitOfWork.TipoAtividadeRemuneradaRepository.AddAsync(new TipoAtividadeRemunerada { TpAR_NM_Descricao = "Sem Atividade Remunerada" });

        await _unitOfWork.SaveAsync();
    }
}