using Domain.DomainServicesInterfaces;
using Domain.Dtos.TipoMoradia.Input;
using Domain.Entities;
using Domain.Interfaces.Repository;
using UtilService.Util;

namespace ApplicationServices;

/// <inheritdoc />
public class TipoMoradiaService : ITipoMoradiaService
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Construtor do núcleo de serviço de Tipo Moradia
    /// </summary>
    /// <param name="unitOfWork"></param>
    public TipoMoradiaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<List<TipoMoradia>> ListarAsync()
    {
        return (await _unitOfWork.TipoMoradiaRepository.AllAsync()).ToList();
    }

    /// <inheritdoc />
    public async Task<TipoMoradia> EditarAsync(TipoMoradiaEditarInModel model)
    {
        model.TrimStringProperties();
        model.ValidateModel();

        var assistidoAntesAlteracao = await _unitOfWork.TipoMoradiaRepository.FirstAsync(o => o.TpMo_ID_TipoMoradia == model.TpMo_ID_TipoMoradia);

        if (assistidoAntesAlteracao == null)
            throw new Exception("Tipo de moradia não localizada no banco de dados.");

        model.TpMo_NM_Descricao = model.TpMo_NM_Descricao;

        var assistidoAlterado = await _unitOfWork.TipoMoradiaRepository.FirstAsync(o => o.TpMo_ID_TipoMoradia == model.TpMo_ID_TipoMoradia);

        assistidoAlterado.TpMo_NM_Descricao = model.TpMo_NM_Descricao;

        await _unitOfWork.SaveAsync();

        //    await _logGenericoService.AdicionarAsync(
        //        assistidoAntesAlteracao,
        //        assistidoAlterado,
        //        assistidoAlterado.TpMo_ID_TipoMoradia.ToString(),
        //        model.IDUsuarioCadastro);

        return assistidoAntesAlteracao;
    }

    /// <inheritdoc />
    public async Task<TipoMoradia> RetornarAsync(TipoMoradiaRetornarInModel model)
    {
        var dado = await _unitOfWork.TipoMoradiaRepository.FirstAsync(o => o.TpMo_ID_TipoMoradia == model.TpMo_ID_TipoMoradia);
        if (dado == null)
        {
            throw new Exception("Tipo de moradia não localizada.");
        }

        return dado;
    }

    /// <inheritdoc />
    public async Task<TipoMoradia> RetornarDadosBasicosAsync(int? codTipoMoradia)
    {
        var dado = await _unitOfWork.TipoMoradiaRepository.FirstAsync(o => o.TpMo_ID_TipoMoradia == codTipoMoradia);
        if (dado == null)
        {
            throw new Exception("Tipo de moradia não localizada.");
        }

        return dado;
    }

    /// <inheritdoc />
    public async Task<TipoMoradia> RetornarDadosPorDescricaoAsync(TipoMoradiaRetornaPorDescricaoInModel model)
    {
        var dado = await _unitOfWork.TipoMoradiaRepository.FirstAsync(o => o.TpMo_NM_Descricao == model.TpMo_NM_Descricao);
        if (dado == null)
        {
            throw new Exception("Tipo de moradia não localizada.");
        }

        return dado;
    }

    /// <inheritdoc />
    public async Task AdicionarTipoMoradiaAsync()
    {
        if (await _unitOfWork.TipoMoradiaRepository.AnyAsync())
            throw new Exception("Os tipos de moradia já estão cadastradas no banco de dados");

        await _unitOfWork.TipoMoradiaRepository.AddAsync(new TipoMoradia { TpMo_NM_Descricao = "Própria" });
        await _unitOfWork.TipoMoradiaRepository.AddAsync(new TipoMoradia { TpMo_NM_Descricao = "Aluguel" });
        await _unitOfWork.TipoMoradiaRepository.AddAsync(new TipoMoradia { TpMo_NM_Descricao = "Financiado" });
        await _unitOfWork.TipoMoradiaRepository.AddAsync(new TipoMoradia { TpMo_NM_Descricao = "Invasão" });
        await _unitOfWork.TipoMoradiaRepository.AddAsync(new TipoMoradia { TpMo_NM_Descricao = "Albergue/Pensão" });
        await _unitOfWork.TipoMoradiaRepository.AddAsync(new TipoMoradia { TpMo_NM_Descricao = "Situação de Rua" });
        await _unitOfWork.TipoMoradiaRepository.AddAsync(new TipoMoradia { TpMo_NM_Descricao = "Centro de Acolhimento" });

        await _unitOfWork.SaveAsync();
    }
}