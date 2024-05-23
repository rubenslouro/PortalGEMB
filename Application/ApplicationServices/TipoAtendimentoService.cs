using Domain.DomainServicesInterfaces;
using Domain.Dtos.Atendimento.Input;
using Domain.Dtos.AtendimentoTipoAtendimento.Input;
using Domain.Dtos.TipoAtendimento.Input;
using Domain.Entities;
using Domain.Interfaces.Repository;
using UtilService.Util;

namespace ApplicationServices;

/// <inheritdoc />
public class TipoAtendimentoService : ITipoAtendimentoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAtendimento_TipoAtendimentoService _atendimento_tipoAtendimentoService;
    private readonly ITipoAtendimentoService _tipoAtendimentoService;
    private readonly ILogGenericoService _logGenericoService;

    /// <summary>
    /// Construtor do núcleo de serviço de Tipo Atendimento
    /// </summary>
    /// <param name="unitOfWork"></param>
    public TipoAtendimentoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<List<TipoAtendimento>> ListarAsync()
    {
        return (await _unitOfWork.TipoAtendimentoRepository.AllAsync()).ToList();
    }

    /// <inheritdoc />
    public async Task<TipoAtendimento> EditarAsync(TipoAtendimentoEditarInModel model)
    {
        model.TrimStringProperties();
        model.ValidateModel();

        var assistidoAntesAlteracao = await _unitOfWork.TipoAtendimentoRepository.FirstAsync(o => o.TpAt_ID_TipoAtendimento == model.TpAt_ID_TipoAtendimento);

        if (assistidoAntesAlteracao == null)
            throw new Exception("Tipo de atendimento não localizada no banco de dados.");

        model.TpAt_NM_Descricao = model.TpAt_NM_Descricao;

        var assistidoAlterado = await _unitOfWork.TipoAtendimentoRepository.FirstAsync(o => o.TpAt_ID_TipoAtendimento == model.TpAt_ID_TipoAtendimento);

        assistidoAlterado.TpAt_NM_Descricao = model.TpAt_NM_Descricao;

        await _unitOfWork.SaveAsync();

        //    await _logGenericoService.AdicionarAsync(
        //        assistidoAntesAlteracao,
        //        assistidoAlterado,
        //        assistidoAlterado.TpAt_ID_TipoAtendimento.ToString(),
        //        model.IDUsuarioCadastro);

        return assistidoAntesAlteracao;
    }

    /// <inheritdoc />
    public async Task<TipoAtendimento> RetornarAsync(TipoAtendimentoRetornarInModel model)
    {
        var dado = await _unitOfWork.TipoAtendimentoRepository.FirstAsync(o => o.TpAt_ID_TipoAtendimento == model.TpAt_ID_TipoAtendimento);

        if (dado == null)
        {
            throw new Exception("Tipo de atendimento não localizado.");
        }

        return dado;
    }

    /// <inheritdoc />
    public async Task<TipoAtendimento> RetornarDadosPorDescricaoAsync(TipoAtendimentoRetornaPorDescricaoInModel model)
    {
        var dado = await _unitOfWork.TipoAtendimentoRepository.FirstAsync(o => o.TpAt_NM_Descricao == model.TpAt_NM_Descricao);

        if (dado == null)
        {
            throw new Exception("Tipo de atendimento não localizado.");
        }

        return dado;
    }

    /// <inheritdoc />
    public async Task AdicionarTipoAtendimentoAsync()
    {
        if (await _unitOfWork.TipoAtendimentoRepository.AnyAsync())
            throw new Exception("Os tipos de atendimento já estão cadastrados no banco de dados");

        //await _unitOfWork.TipoAtendimentoRepository.AddAsync(new TipoAtendimento { TpAt_CD_TipoAtendimento = "M", TpAt_NM_Descricao = "Barbearia Social" });
        await _unitOfWork.TipoAtendimentoRepository.AddAsync(new TipoAtendimento { TpAt_NM_Descricao = "Barbearia Social" });
        await _unitOfWork.TipoAtendimentoRepository.AddAsync(new TipoAtendimento { TpAt_NM_Descricao = "Cesta Aluno(a)" });
        await _unitOfWork.TipoAtendimentoRepository.AddAsync(new TipoAtendimento { TpAt_NM_Descricao = "Cesta Emergencial" });
        await _unitOfWork.TipoAtendimentoRepository.AddAsync(new TipoAtendimento { TpAt_NM_Descricao = "Desistência Curso" });
        await _unitOfWork.TipoAtendimentoRepository.AddAsync(new TipoAtendimento { TpAt_NM_Descricao = "Fralda Geriátrica" });
        await _unitOfWork.TipoAtendimentoRepository.AddAsync(new TipoAtendimento { TpAt_NM_Descricao = "Gestantes" });
        await _unitOfWork.TipoAtendimentoRepository.AddAsync(new TipoAtendimento { TpAt_NM_Descricao = "Jurídico" });
        await _unitOfWork.TipoAtendimentoRepository.AddAsync(new TipoAtendimento { TpAt_NM_Descricao = "Matrícula Curso Alfabetização", TpAt_ID_Disciplina = 1 });
        await _unitOfWork.TipoAtendimentoRepository.AddAsync(new TipoAtendimento { TpAt_NM_Descricao = "Matrícula Curso Autoestima", TpAt_ID_Disciplina = 3 });
        await _unitOfWork.TipoAtendimentoRepository.AddAsync(new TipoAtendimento { TpAt_NM_Descricao = "Matrícula Curso Customização Criativa", TpAt_ID_Disciplina = 4 });
        await _unitOfWork.TipoAtendimentoRepository.AddAsync(new TipoAtendimento { TpAt_NM_Descricao = "Matrícula Curso Espiritismo 1", TpAt_ID_Disciplina = 5 });
        await _unitOfWork.TipoAtendimentoRepository.AddAsync(new TipoAtendimento { TpAt_NM_Descricao = "Matrícula Curso Espiritismo 2", TpAt_ID_Disciplina = 6 });
        await _unitOfWork.TipoAtendimentoRepository.AddAsync(new TipoAtendimento { TpAt_NM_Descricao = "Matrícula Curso Espiritismo 3", TpAt_ID_Disciplina = 7 });
        await _unitOfWork.TipoAtendimentoRepository.AddAsync(new TipoAtendimento { TpAt_NM_Descricao = "Matrícula Curso Gestante", TpAt_ID_Disciplina = 0 });
        await _unitOfWork.TipoAtendimentoRepository.AddAsync(new TipoAtendimento { TpAt_NM_Descricao = "Novo Cadastro para Cestas" });
        await _unitOfWork.TipoAtendimentoRepository.AddAsync(new TipoAtendimento { TpAt_NM_Descricao = "Outros" });
        await _unitOfWork.TipoAtendimentoRepository.AddAsync(new TipoAtendimento { TpAt_NM_Descricao = "Passagens de Ônibus - Viagem" });
        await _unitOfWork.TipoAtendimentoRepository.AddAsync(new TipoAtendimento { TpAt_NM_Descricao = "Roupa Aluno(a)" });

        await _unitOfWork.TipoAtendimentoRepository.AddAsync(new TipoAtendimento { TpAt_NM_Descricao = "Roupa Infantil" });
        await _unitOfWork.TipoAtendimentoRepository.AddAsync(new TipoAtendimento { TpAt_NM_Descricao = "Roupa Feminina" });
        await _unitOfWork.TipoAtendimentoRepository.AddAsync(new TipoAtendimento { TpAt_NM_Descricao = "Roupa Masculina" });
        await _unitOfWork.TipoAtendimentoRepository.AddAsync(new TipoAtendimento { TpAt_NM_Descricao = "Serviço Social" });

        await _unitOfWork.SaveAsync();
    }
}