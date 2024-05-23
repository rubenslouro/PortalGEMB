using Domain.DomainServicesInterfaces;
using Domain.Dtos.Disciplina.Input;
using Domain.Dtos.Turma.Input;
using Domain.Entities;
using Domain.Interfaces.Repository;
using UtilService.Util;

namespace ApplicationServices;

/// <inheritdoc />
public class TurmaService : ITurmaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogGenericoService _logGenericoService;

    /// <summary>
    /// Construtor do núcleo de serviço de Tipo Moradia
    /// </summary>
    /// <param name="unitOfWork"></param>
    public TurmaService(IUnitOfWork unitOfWork, ILogGenericoService logGenericoService)
    {
        _unitOfWork = unitOfWork;
        _logGenericoService = logGenericoService;
    }

    /// <inheritdoc />
    public async Task<List<Turma>> ListarAsync()
    {
        return (await _unitOfWork.TurmaRepository.AllAsync()).ToList();
    }

    /// <inheritdoc />
    public async Task<Turma> AdicionarAsync(TurmaAdicionarInModel model)
    {
        model.TrimStringProperties();
        model.ValidateModel();

        var turma = new Turma
        {
            Turm_ID_Disciplina = model.Turm_ID_Disciplina,
            Turm_NR_Turma = model.Turm_NR_Turma,
            Turm_TX_Descricao = model.Turm_TX_Descricao,
            Turm_DT_Inicio = Convert.ToDateTime(model.Turm_DT_Inicio),
            Turm_DT_Final = Convert.ToDateTime(model.Turm_DT_Final),
            Turm_TX_Observacao = model.Turm_TX_Observacao,
            Turm_ID_UsuarioCadastro = model.IDUsuarioCadastro,
            Turm_DT_Cadastro = DateTime.UtcNow
        };

        await _unitOfWork.TurmaRepository.AddAsync(turma);
        await _unitOfWork.SaveAsync();

        return turma;
    }

    /// <inheritdoc />
    public async Task<Turma> EditarAsync(TurmaEditarInModel model)
    {
        model.TrimStringProperties();
        model.ValidateModel();

        var assistidoAntesAlteracao = await _unitOfWork.TurmaRepository.FirstAsync(o => o.Turm_ID_Turma == model.Turm_ID_Turma);
        var assistidoAlterado = await _unitOfWork.TurmaRepository.FirstAsync(o => o.Turm_ID_Turma == model.Turm_ID_Turma);

        if (assistidoAntesAlteracao == null || assistidoAlterado == null)
            throw new Exception("Turma não localizada no banco de dados.");

        assistidoAlterado.Turm_ID_Disciplina = model.Turm_ID_Disciplina;
        assistidoAlterado.Turm_NR_Turma = model.Turm_NR_Turma;
        assistidoAlterado.Turm_TX_Descricao = model.Turm_TX_Descricao;
        assistidoAlterado.Turm_DT_Inicio = Convert.ToDateTime(model.Turm_DT_Inicio);
        assistidoAlterado.Turm_DT_Final = Convert.ToDateTime(model.Turm_DT_Final);
        assistidoAlterado.Turm_TX_Observacao = model.Turm_TX_Observacao;

        await _unitOfWork.SaveAsync();

        await _logGenericoService.AdicionarAsync(
            assistidoAntesAlteracao,
            assistidoAlterado,
            assistidoAlterado.Turm_ID_Turma.ToString(),
            model.IDUsuarioCadastro);

        return assistidoAlterado;
    }

    /// <inheritdoc />
    public async Task<Turma> RetornarAsync(TurmaRetornarInModel model)
    {
        var dado = await _unitOfWork.TurmaRepository.FirstAsync(o => o.Turm_ID_Turma == model.Turm_ID_Turma);
        if (dado == null)
        {
            throw new Exception("Turma não localizada.");
        }

        return dado;
    }
}