using Domain.DomainServicesInterfaces;
using Domain.Dtos.TipoEscolaridade.Input;
using Domain.Dtos.TipoEscolaridade.Input;
using Domain.Entities;
using Domain.Interfaces.Repository;
using UtilService.Util;

namespace ApplicationServices;

/// <inheritdoc />
public class TipoEscolaridadeService : ITipoEscolaridadeService
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Construtor do núcleo de serviço de Tipo Moradia
    /// </summary>
    /// <param name="unitOfWork"></param>
    public TipoEscolaridadeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<List<TipoEscolaridade>> ListarAsync()
    {
        return (await _unitOfWork.TipoEscolaridadeRepository.AllAsync()).ToList();
    }

    /// <inheritdoc />
    public async Task<TipoEscolaridade> EditarAsync(TipoEscolaridadeEditarInModel model)
    {
        model.TrimStringProperties();
        model.ValidateModel();

        var assistidoAntesAlteracao = await _unitOfWork.TipoEscolaridadeRepository.FirstAsync(o => o.TpEs_ID_TipoEscolaridade == model.TpEs_ID_TipoEscolaridade);

        if (assistidoAntesAlteracao == null)
            throw new Exception("Tipo de escolaridade não localizada no banco de dados.");

        model.TpEs_NM_Descricao = model.TpEs_NM_Descricao;

        var assistidoAlterado = await _unitOfWork.TipoEscolaridadeRepository.FirstAsync(o => o.TpEs_ID_TipoEscolaridade == model.TpEs_ID_TipoEscolaridade);

        assistidoAlterado.TpEs_NM_Descricao = model.TpEs_NM_Descricao;

        await _unitOfWork.SaveAsync();

        //    await _logGenericoService.AdicionarAsync(
        //        assistidoAntesAlteracao,
        //        assistidoAlterado,
        //        assistidoAlterado.TpEs_ID_TipoEscolaridade.ToString(),
        //        model.IDUsuarioCadastro);

        return assistidoAntesAlteracao;
    }

    /// <inheritdoc />
    public async Task<TipoEscolaridade> RetornarAsync(TipoEscolaridadeRetornarInModel model)
    {
        var dado = await _unitOfWork.TipoEscolaridadeRepository.FirstAsync(o => o.TpEs_ID_TipoEscolaridade == model.TpEs_ID_TipoEscolaridade);

        if (dado == null)
        {
            throw new Exception("Tipo de escolaridade não localizada.");
        }

        return dado;
    }

    /// <inheritdoc />
    public async Task<TipoEscolaridade> RetornarDadosBasicosAsync(int? codTipoEscolaridade)
    {
        var dado = await _unitOfWork.TipoEscolaridadeRepository.FirstAsync(o => o.TpEs_ID_TipoEscolaridade == codTipoEscolaridade);
        if (dado == null)
        {
            throw new Exception("Tipo de Escolaridade não localizada.");
        }

        return dado;
    }

    /// <inheritdoc />
    public async Task<TipoEscolaridade> RetornarDadosPorDescricaoAsync(TipoEscolaridadeRetornaPorDescricaoInModel model)
    {
        var dado = await _unitOfWork.TipoEscolaridadeRepository.FirstAsync(o => o.TpEs_NM_Descricao == model.TpEs_NM_Descricao);

        if (dado == null)
        {
            throw new Exception("Tipo de escolaridade não localizada.");
        }

        return dado;
    }

    /// <inheritdoc />
    public async Task AdicionarTipoEscolaridadeAsync()
    {
        if (await _unitOfWork.TipoEscolaridadeRepository.AnyAsync())
            throw new Exception("Os tipos de escolaridade já estão cadastradas no banco de dados");

        await _unitOfWork.TipoEscolaridadeRepository.AddAsync(new TipoEscolaridade { TpEs_NM_Descricao = "Analfabeto" });
        await _unitOfWork.TipoEscolaridadeRepository.AddAsync(new TipoEscolaridade { TpEs_NM_Descricao = "Fundamental" });
        await _unitOfWork.TipoEscolaridadeRepository.AddAsync(new TipoEscolaridade { TpEs_NM_Descricao = "Ensino Médio" });
        await _unitOfWork.TipoEscolaridadeRepository.AddAsync(new TipoEscolaridade { TpEs_NM_Descricao = "Superior" });

        await _unitOfWork.SaveAsync();
    }
}