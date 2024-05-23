using Domain.DomainServicesInterfaces;
using Domain.Dtos.LogGenerico.Input;
using Domain.Dtos.LogGenerico.Output;
using Domain.Entities;
using Domain.Interfaces.Exception;
using Domain.Interfaces.Repository;
using UtilService.Util;

namespace ApplicationServices;

/// <inheritdoc />
public class LogGenericoService : ILogGenericoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsuarioCheckerService _usuarioCheckerService;
    private readonly IExceptions _exception;
    /// <summary>
    /// Lista de campos ignorados para log
    /// </summary>
    public readonly List<string> ListaIgnorados = new List<string>
    {
        "senha",
        "password",
        "navigation"
    };

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="unitOfWork"></param>
    /// <param name="usuarioCheckerService"></param>
    /// <param name="Exception"></param>
    public LogGenericoService(
        IUnitOfWork unitOfWork,
        IUsuarioCheckerService usuarioCheckerService,
        IExceptions Exception)
    {
        _unitOfWork = unitOfWork;
        _usuarioCheckerService = usuarioCheckerService;
        _exception = Exception;
    }

    /// <inheritdoc />
    public async Task<LogGenericoListarLogOutModel> ListarAsync(LogGenericoListarInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();

        var dados = (await _unitOfWork.LogGenericoRepository.FindAsync(o => o.Tabela == model.Tabela &&
                                                                            o.Referencia == model.Referencia &&
                                                                            !ListaIgnorados.Contains(o.Campo.ToLower())))
                                                            .OrderByDescending(o => o.DataHoraAcao)
                                                            .ToList();

        if (dados.Any(o => ListaIgnorados.Contains(o.Campo.ToLower())))
            throw new Exception(_exception.ExistemCamposQueNaoPodemSerExibidosNoLogDeAlteracao);                          

        return new LogGenericoListarLogOutModel
        {
            ListaLogGenerico = dados.Select(o => new LogGenericoListarItemOutModel(o)).ToList()
        };
    }

    /// <inheritdoc />
    public async Task AdicionarAsync(object itemOriginal, object itemAtual, string referencia, int codUsuarioAcao)
    {
        _unitOfWork.EnableLazyLoader(false);
        await _usuarioCheckerService.CriticarUsuarioInativoAsync(codUsuarioAcao);

        var nomeTabelaByClass = itemAtual.GetType().BaseType;
        if (nomeTabelaByClass == null)
        {
            throw new Exception("Classe original não informada para log.");
        }

        foreach (var obj in ClassAnalizer.GetDiferences(ClassAnalizer.Compare(itemOriginal, itemAtual)))
        {
            var newLog = new LogGenerico
            {
                Tabela = nomeTabelaByClass.Name == "Object" ? itemAtual.GetType().Name : nomeTabelaByClass.Name,
                Referencia = referencia,
                Campo = obj.NomeCampo,
                ValorAnterior = obj.ValorCampoAnterior.IsNullOrWhiteSpace() ? string.Empty : obj.ValorCampoAnterior,
                ValorAlterado = obj.ValorCampoAtual.IsNullOrWhiteSpace() ? string.Empty : obj.ValorCampoAtual,
                CodUsuarioAcao = codUsuarioAcao,
                DataHoraAcao = DateTime.UtcNow
            };
            await _unitOfWork.LogGenericoRepository.AddAsync(newLog);
        }
        await _unitOfWork.SaveAsync();
        _unitOfWork.EnableLazyLoader(true);
    }
}