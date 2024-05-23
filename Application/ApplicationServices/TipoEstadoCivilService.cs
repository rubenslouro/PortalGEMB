using Domain.DomainServicesInterfaces;
using Domain.Dtos.TipoEstadoCivil.Input;
using Domain.Dtos.TipoEstadoCivil.Input;
using Domain.Entities;
using Domain.Interfaces.Repository;
using UtilService.Util;

namespace ApplicationServices;

/// <inheritdoc />
public class TipoEstadoCivilService : ITipoEstadoCivilService
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Construtor do núcleo de serviço de Tipo Moradia
    /// </summary>
    /// <param name="unitOfWork"></param>
    public TipoEstadoCivilService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<List<TipoEstadoCivil>> ListarAsync()
    {
        return (await _unitOfWork.TipoEstadoCivilRepository.AllAsync()).ToList();
    }
    /// <inheritdoc />
    public async Task<TipoEstadoCivil> EditarAsync(TipoEstadoCivilEditarInModel model)
    {
        model.TrimStringProperties();
        model.ValidateModel();

        var assistidoAntesAlteracao = await _unitOfWork.TipoEstadoCivilRepository.FirstAsync(o => o.TpEC_ID_TipoEstadoCivil == model.TpEC_ID_TipoEstadoCivil);

        if (assistidoAntesAlteracao == null)
            throw new Exception("Tipo de estado civil não localizada no banco de dados.");

        model.TpEC_NM_Descricao = model.TpEC_NM_Descricao;

        var assistidoAlterado = await _unitOfWork.TipoEstadoCivilRepository.FirstAsync(o => o.TpEC_ID_TipoEstadoCivil == model.TpEC_ID_TipoEstadoCivil);

        assistidoAlterado.TpEC_NM_Descricao = model.TpEC_NM_Descricao;

        await _unitOfWork.SaveAsync();

        //    await _logGenericoService.AdicionarAsync(
        //        assistidoAntesAlteracao,
        //        assistidoAlterado,
        //        assistidoAlterado.TpEC_ID_TipoEstadoCivil.ToString(),
        //        model.IDUsuarioCadastro);

        return assistidoAntesAlteracao;
    }


    /// <inheritdoc />
    public async Task<TipoEstadoCivil> RetornarAsync(TipoEstadoCivilRetornarInModel model)
    {
        var dado = await _unitOfWork.TipoEstadoCivilRepository.FirstAsync(o => o.TpEC_ID_TipoEstadoCivil == model.TpEC_ID_TipoEstadoCivil);

        if (dado == null)
        {
            throw new Exception("Tipo de estado civil não localizada.");
        }

        return dado;
    }

    /// <inheritdoc />
    public async Task<TipoEstadoCivil> RetornarDadosBasicosAsync(int? codTipoEstadoCivil)
    {
        var dado = await _unitOfWork.TipoEstadoCivilRepository.FirstAsync(o => o.TpEC_ID_TipoEstadoCivil == codTipoEstadoCivil);
        if (dado == null)
        {
            throw new Exception("Tipo de EstadoCivil não localizada.");
        }

        return dado;
    }

    /// <inheritdoc />
    public async Task<TipoEstadoCivil> RetornarDadosPorDescricaoAsync(TipoEstadoCivilRetornaPorDescricaoInModel model)
    {
        var dado = await _unitOfWork.TipoEstadoCivilRepository.FirstAsync(o => o.TpEC_NM_Descricao == model.TpEC_NM_Descricao);

        if (dado == null)
        {
            throw new Exception("Tipo de estado civil não localizada.");
        }

        return dado;
    }

    /// <inheritdoc />
    public async Task AdicionarTipoEstadoCivilAsync()
    {
        if (await _unitOfWork.TipoEstadoCivilRepository.AnyAsync())
            throw new Exception("Os tipos de estado civil já estão cadastradas no banco de dados");

        await _unitOfWork.TipoEstadoCivilRepository.AddAsync(new TipoEstadoCivil { TpEC_NM_Descricao = "Solteiro" });
        await _unitOfWork.TipoEstadoCivilRepository.AddAsync(new TipoEstadoCivil { TpEC_NM_Descricao = "Casado" });
        await _unitOfWork.TipoEstadoCivilRepository.AddAsync(new TipoEstadoCivil { TpEC_NM_Descricao = "União Estável" });
        await _unitOfWork.TipoEstadoCivilRepository.AddAsync(new TipoEstadoCivil { TpEC_NM_Descricao = "Divorciado" });
        await _unitOfWork.TipoEstadoCivilRepository.AddAsync(new TipoEstadoCivil { TpEC_NM_Descricao = "Viúvo" });

        await _unitOfWork.SaveAsync();
    }
}