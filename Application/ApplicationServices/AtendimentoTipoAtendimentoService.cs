using Domain.DomainServicesInterfaces;
using Domain.Dtos.Atendimento.Input;
using Domain.Dtos.Atendimento.Output;
using Domain.Dtos.AtendimentoTipoAtendimento.Input;
using Domain.Dtos.AtendimentoTipoAtendimento.Output;
using Domain.Dtos.RegraSistema.Retorna.Output;
using Domain.Dtos.TipoAtendimento.Input;
using Domain.Entities;
using Domain.Interfaces.Repository;
using System.Reflection;
using UtilService.Util;

namespace ApplicationServices;

/// <inheritdoc />
public class Atendimento_TipoAtendimentoService : IAtendimento_TipoAtendimentoService
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Construtor do núcleo de serviço de Tipo Moradia
    /// </summary>
    /// <param name="unitOfWork"></param>
    public Atendimento_TipoAtendimentoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<List<Atendimento_TipoAtendimento>> ListarAsync()
    {
        return (await _unitOfWork.Atendimento_TipoAtendimentoRepository.AllAsync()).ToList();
    }

    ///// <inheritdoc />
    //public async Task<Atendimento_TipoAtendimento> EditarAsync(Atendimento_TipoAtendimentoEditarInModel model)
    //{
    //    model.TrimStringProperties();
    //    model.ValidateModel();

    //    var assistidoAntesAlteracao = await _unitOfWork.Atendimento_TipoAtendimentoRepository.FirstAsync(o => o.TpMo_ID_Atendimento_TipoAtendimento == model.TpMo_ID_Atendimento_TipoAtendimento);

    //    if (assistidoAntesAlteracao == null)
    //        throw new Exception("Tipo de moradia não localizada no banco de dados.");

    //    model.TpMo_NM_Descricao = model.TpMo_NM_Descricao;

    //    var assistidoAlterado = await _unitOfWork.Atendimento_TipoAtendimentoRepository.FirstAsync(o => o.TpMo_ID_Atendimento_TipoAtendimento == model.TpMo_ID_Atendimento_TipoAtendimento);

    //    assistidoAlterado.TpMo_NM_Descricao = model.TpMo_NM_Descricao;

    //    await _unitOfWork.SaveAsync();

    //    //    await _logGenericoService.AdicionarAsync(
    //    //        assistidoAntesAlteracao,
    //    //        assistidoAlterado,
    //    //        assistidoAlterado.TpMo_ID_Atendimento_TipoAtendimento.ToString(),
    //    //        model.IDUsuarioCadastro);

    //    return assistidoAntesAlteracao;
    //}

    /// <inheritdoc />
    public async Task<Atendimento_TipoAtendimento> RetornarAsync(Atendimento_TipoAtendimentoRetornarInModel model)
    {
        var dado = await _unitOfWork.Atendimento_TipoAtendimentoRepository.FirstAsync(o => o.AtTA_ID_Atendimento == model.AtTA_ID_Atendimento);
        if (dado == null)
        {
            throw new Exception("Tipo de atendimento não localizado.");
        }

        return dado;
    }

    /// <inheritdoc />
    public async Task<List<AtendimentoTipoAtendimentoRetornarOutModel>> RetornarDadosBasicosAsync(Atendimento_TipoAtendimentoRetornarInModel model)
    {
        var dado = (await _unitOfWork.Atendimento_TipoAtendimentoRepository.AllAsync()).Select(o => new AtendimentoTipoAtendimentoRetornarOutModel(o)).ToList();
        //var dado = (await _unitOfWork.Atendimento_TipoAtendimentoRepository.AllAsync()).Select(o => o.AtTA_ID_Atendimento == model.AtTA_ID_Atendimento);
        //var dado1 = await _unitOfWork.Atendimento_TipoAtendimentoRepository.FirstAsync(o => o.AtTA_ID_Atendimento == model.AtTA_ID_Atendimento);
        //var dado3 = (await _unitOfWork.Atendimento_TipoAtendimentoRepository.AllAsync()).Select(o => o.AtTA_ID_Atendimento == model.AtTA_ID_Atendimento).ToList();
        //var dados = (await _unitOfWork.Atendimento_TipoAtendimentoRepository.FindAsync(o => o.AtTA_ID_Atendimento == model.AtTA_ID_Atendimento)).Select(o => o.AtTA_ID_Atendimento).ToList();
        //var dado = await _unitOfWork.Atendimento_TipoAtendimentoRepository.FirstAsync(o => o.AtTA_ID_Atendimento == model.AtTA_ID_Atendimento);
        if (dado == null)
        {
            throw new Exception("Tipo de atendimento não localizado.");
        }

        //return (await _unitOfWork.Atendimento_TipoAtendimentoRepository.AllAsync()).ToList();
        return dado;
    }

    /// <inheritdoc />
    public async Task<List<AtendimentoTipoAtendimentoRetornarOutModel>> RetornarListaDadosAsync(int codAtendimento)
    {
        var retlista = (await _unitOfWork.Atendimento_TipoAtendimentoRepository.FindAsync(o => o.AtTA_ID_Atendimento == codAtendimento)).Select(o => new AtendimentoTipoAtendimentoRetornarOutModel(o)).ToList();
        if (retlista == null)
        {
            throw new Exception("Tipo de atendimento não localizado.");
        }

        return retlista;
    }

    ///// <inheritdoc />
    //public async Task<List<AtendimentoTipoAtendimentoRetornarOutModel>> RetornarDadosPorDescricaoAsync(Atendimento_TipoAtendimentoRetornaPorDescricaoInModel model)
    //{
    //    var dado = (await _unitOfWork.Atendimento_TipoAtendimentoRepository.AllAsync()).Select(o => new AtendimentoTipoAtendimentoRetornarOutModel(o)).ToList();
    //    if (dado == null)
    //    {
    //        throw new Exception("Tipo de atendimento não localizado.");
    //    }

    //    return dado;
    //}
}