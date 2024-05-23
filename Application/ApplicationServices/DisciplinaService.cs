using Domain.DomainServicesInterfaces;
using Domain.Dtos.Assistido.Input;
using Domain.Dtos.Disciplina.Input;
using Domain.Entities;
using Domain.Interfaces.Repository;
using UtilService.Util;

namespace ApplicationServices;

/// <inheritdoc />
public class DisciplinaService : IDisciplinaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogGenericoService _logGenericoService;

    /// <summary>
    /// Construtor do núcleo de serviço de Tipo Moradia
    /// </summary>
    /// <param name="unitOfWork"></param>
    public DisciplinaService(IUnitOfWork unitOfWork, ILogGenericoService logGenericoService)
    {
        _unitOfWork = unitOfWork;
        _logGenericoService = logGenericoService;
    }

    /// <inheritdoc />
    public async Task<List<Disciplina>> ListarAsync()
    {
        return (await _unitOfWork.DisciplinaRepository.AllAsync()).ToList();
    }

    /// <inheritdoc />
    public async Task<Disciplina> AdicionarAsync(DisciplinaAdicionarInModel model)
    {
        model.TrimStringProperties();
        model.ValidateModel();

        var disciplina = new Disciplina
        {
            Disc_NM_Nome = model.Disc_NM_Nome,
            Disc_TX_Observacao = model.Disc_TX_Observacao,
            Disc_ID_UsuarioCadastro = model.IDUsuarioCadastro,
            Disc_DT_Cadastro = DateTime.UtcNow
        };

        await _unitOfWork.DisciplinaRepository.AddAsync(disciplina);
        await _unitOfWork.SaveAsync();

        return disciplina;
    }

    /// <inheritdoc />
    public async Task<Disciplina> EditarAsync(DisciplinaEditarInModel model)
    {
        model.TrimStringProperties();
        model.ValidateModel();

        var disciplinaAntesAlteracao = await _unitOfWork.DisciplinaRepository.FirstAsync(o => o.Disc_ID_Disciplina == model.Disc_ID_Disciplina);

        if (disciplinaAntesAlteracao == null)
            throw new Exception("Disciplina não localizada no banco de dados.");

        model.Disc_NM_Nome = model.Disc_NM_Nome;

        var disciplinaAlterado = await _unitOfWork.DisciplinaRepository.FirstAsync(o => o.Disc_ID_Disciplina == model.Disc_ID_Disciplina);

        disciplinaAlterado.Disc_NM_Nome = model.Disc_NM_Nome;
        disciplinaAlterado.Disc_TX_Observacao = model.Disc_TX_Observacao;

        await _unitOfWork.SaveAsync();

        await _logGenericoService.AdicionarAsync(
            disciplinaAntesAlteracao,
            disciplinaAlterado,
            disciplinaAlterado.Disc_ID_Disciplina.ToString(),
            model.IDUsuarioCadastro);

        return disciplinaAntesAlteracao;
    }

    /// <inheritdoc />
    public async Task<Disciplina> RetornarAsync(DisciplinaRetornarInModel model)
    {
        model.ValidateModel();

        var retorno = await _unitOfWork.DisciplinaRepository.FirstAsync(o => o.Disc_ID_Disciplina == model.Disc_ID_Disciplina);
        if (retorno == null)
            throw new Exception("Disciplina não localizada.");

        return retorno;
    }

    /// <inheritdoc />
    public async Task AdicionarDisciplinaAsync()
    {
        if (await _unitOfWork.DisciplinaRepository.AnyAsync())
            throw new Exception("As disciplinas já estão cadastradas no banco de dados");

        await _unitOfWork.DisciplinaRepository.AddAsync(new Disciplina { Disc_NM_Nome = "Alfabetização", Disc_TX_Observacao = "Curso de Alfabetização", Disc_ID_UsuarioCadastro = 1, Disc_DT_Cadastro = DateTime.UtcNow });
        await _unitOfWork.DisciplinaRepository.AddAsync(new Disciplina { Disc_NM_Nome = "Psicopedagogia / Alfabetização", Disc_TX_Observacao = "Curso de Psicopedagogia e Alfabetização", Disc_ID_UsuarioCadastro = 1, Disc_DT_Cadastro = DateTime.UtcNow });
        await _unitOfWork.DisciplinaRepository.AddAsync(new Disciplina { Disc_NM_Nome = "Autoestima", Disc_TX_Observacao = "Curso de Autoestima", Disc_ID_UsuarioCadastro = 1, Disc_DT_Cadastro = DateTime.UtcNow });
        await _unitOfWork.DisciplinaRepository.AddAsync(new Disciplina { Disc_NM_Nome = "Customização Criativa", Disc_TX_Observacao = "Curso de Customização Criativa", Disc_ID_UsuarioCadastro = 1, Disc_DT_Cadastro = DateTime.UtcNow });
        await _unitOfWork.DisciplinaRepository.AddAsync(new Disciplina { Disc_NM_Nome = "Espiritismo 1", Disc_TX_Observacao = "Curso de Espiritismo 1", Disc_ID_UsuarioCadastro = 1, Disc_DT_Cadastro = DateTime.UtcNow });
        await _unitOfWork.DisciplinaRepository.AddAsync(new Disciplina { Disc_NM_Nome = "Espiritismo 2", Disc_TX_Observacao = "Curso de Espiritismo 2", Disc_ID_UsuarioCadastro = 1, Disc_DT_Cadastro = DateTime.UtcNow });
        await _unitOfWork.DisciplinaRepository.AddAsync(new Disciplina { Disc_NM_Nome = "Espiritismo 3", Disc_TX_Observacao = "Curso de Espiritismo 3", Disc_ID_UsuarioCadastro = 1, Disc_DT_Cadastro = DateTime.UtcNow });

        await _unitOfWork.SaveAsync();
    }
}