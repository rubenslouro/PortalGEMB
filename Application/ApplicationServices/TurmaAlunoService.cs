using Domain.DomainServicesInterfaces;
using Domain.Dtos.AtendimentoTipoAtendimento.Output;
using Domain.Dtos.TurmaAluno.Input;
using Domain.Entities;
using Domain.Interfaces.Repository;
using UtilService.Util;

namespace ApplicationServices;

/// <inheritdoc />
public class TurmaAlunoService : ITurmaAlunoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogGenericoService _logGenericoService;

    /// <summary>
    /// Construtor do núcleo de serviço de Tipo Moradia
    /// </summary>
    /// <param name="unitOfWork"></param>
    public TurmaAlunoService(IUnitOfWork unitOfWork, ILogGenericoService logGenericoService)
    {
        _unitOfWork = unitOfWork;
        _logGenericoService = logGenericoService;
    }

    /// <inheritdoc />
    public async Task<List<TurmaAluno>> ListarAsync()
    {
        return (await _unitOfWork.TurmaAlunoRepository.AllAsync()).ToList();
    }

    /// <inheritdoc />
    public async Task<TurmaAluno> AdicionarAsync(TurmaAlunoAdicionarInModel model)
    {
        model.TrimStringProperties();
        model.ValidateModel();

        var turmaaluno = new TurmaAluno
        {
            TuAl_ID_Assistido = model.TuAl_ID_Assistido,
            TuAl_ID_Turma = model.TuAl_ID_Turma
        };

        await _unitOfWork.TurmaAlunoRepository.AddAsync(turmaaluno);
        await _unitOfWork.SaveAsync();

        return turmaaluno;
    }

    /// <inheritdoc />
    public async Task<TurmaAluno> EditarAsync(TurmaAlunoEditarInModel model)
    {
        model.TrimStringProperties();
        model.ValidateModel();

        var assistidoAntesAlteracao = await _unitOfWork.TurmaAlunoRepository.FirstAsync(o => o.TuAl_ID_Assistido == model.TuAl_ID_Assistido);
        var assistidoAlterado = await _unitOfWork.TurmaAlunoRepository.FirstAsync(o => o.TuAl_ID_Assistido == model.TuAl_ID_Assistido);

        if (assistidoAntesAlteracao == null || assistidoAlterado == null)
            throw new Exception("Aluno não localizado no banco de dados.");

        //assistidoAlterado.Turm_ID_Disciplina = model.Turm_ID_Disciplina;
        //assistidoAlterado.Turm_NR_Aluno = model.Turm_NR_Aluno;
        //assistidoAlterado.Turm_TX_Descricao = model.Turm_TX_Descricao;
        //assistidoAlterado.Turm_DT_Inicio = Convert.ToDateTime(model.Turm_DT_Inicio);
        //assistidoAlterado.Turm_DT_Final = Convert.ToDateTime(model.Turm_DT_Final);
        //assistidoAlterado.Turm_TX_Observacao = model.Turm_TX_Observacao;

        await _unitOfWork.SaveAsync();

        await _logGenericoService.AdicionarAsync(
            assistidoAntesAlteracao,
            assistidoAlterado,
            assistidoAlterado.TuAl_ID_Assistido.ToString(),
            model.IDUsuarioCadastro);

        return assistidoAlterado;
    }

    /// <inheritdoc />
    public async Task<TurmaAluno> RetornarAsync(TurmaAlunoRetornarInModel model)
    {
        var dado = await _unitOfWork.TurmaAlunoRepository.FirstAsync(o => o.TuAl_ID_Assistido == model.TuAl_ID_Assistido);
        //var dado = await _unitOfWork.TurmaAlunoRepository.FirstAsync(o => o.TuAl_ID_Assistido == model.TuAl_ID_Assistido && o.TuAl_CD_PeriodoLetivo == model.TuAl_CD_PeriodoLetivo && o.TuAl_NR_AnoLetivo == model.TuAl_NR_AnoLetivo);
        if (dado == null)
        {
            throw new Exception("Aluno não localizado.");
        }

        return dado;
    }

    /// <inheritdoc />
    public async Task<List<TurmaAluno>> RetornarListaAlunosTurmaAsync(int codTurma)
    {
        var retlista = (await _unitOfWork.TurmaAlunoRepository.FindAsync(o => o.TuAl_ID_Turma == codTurma)).ToList();

        if (retlista == null)
        {
            throw new Exception("Tipo de atendimento não localizado.");
        }

        return retlista;
    }

}