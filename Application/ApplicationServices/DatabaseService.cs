using Domain.DomainServicesInterfaces;
using Domain.Dtos.ConfiguracaoGeral.Input;
using Domain.Interfaces.Exception;
using Domain.Interfaces.Repository;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace ApplicationServices;

/// <inheritdoc />
public class DatabaseService : IDatabaseService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryCreation _repositoryCreation;
    private readonly IExceptions _exception;
    private readonly IConfiguration _configuration;
    /// <summary>
    /// Construtor
    /// </summary>
    public DatabaseService(
        IUnitOfWork unitOfWork,
        IRepositoryCreation repositoryCreation,
        IExceptions exception, IConfiguration configuration) 
    {
        _unitOfWork = unitOfWork;
        _repositoryCreation = repositoryCreation;
        _exception = exception;
        _configuration = configuration;
    }

    /// <inheritdoc />
    public async Task<bool> BancoDeDadosEstaConfiguradoAsync()
    {
        try
        {
            if (!await BancoDadosEstaCriadoAsync())
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }

        var dado = await _unitOfWork.ConfiguracaoGeralRepository.FirstAsync();

        return dado != null;
    }

    /// <inheritdoc />
    public async Task<bool> BancoDadosEstaCriadoAsync()
    {
        try
        {
            return await _repositoryCreation.CanConnectAsync();
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <inheritdoc />
    public async Task ConfigurarBancoDadosECriaAsync(ConfiguracaoGeralConfigurarBancoDadosECriaInModel model)
    {
        if (File.Exists("AppConfig/DbOk.txt"))
            throw new Exception(_exception.NaoEPermitidoCriarOutroBancoDeDados);

        if (await BancoDadosEstaCriadoAsync())
            return;

        var strCon = $"Server={model.Server};Database={model.DataBaseName};User Id={model.UserId};Password={model.Password};TrustServerCertificate=True";
        //var strCon = $"Server={model.Server},{model.ServerPort};Database={model.DataBaseName};User Id={model.UserId};Password={model.Password};TrustServerCertificate=True";

        var strAppSettings = await File.ReadAllTextAsync(@"AppConfig/mainservice.appsettings.json");

        var parseResponse = JToken.Parse(strAppSettings);

        var selectedPath = parseResponse.SelectToken("$.SqlServer");
        if (selectedPath == null)
            throw new ArgumentNullException("Section Sql Server in AppSettings.json");
        selectedPath = selectedPath.SelectToken("$.ConnectionString");
        if (selectedPath == null)
            throw new ArgumentNullException("Section Sql Server:ConnectionString in AppSettings.json");

        selectedPath.Replace(strCon);

        await File.WriteAllTextAsync(@"AppConfig/mainservice.appsettings.json", parseResponse.ToString());

        _ = _configuration.GetSection("SqlServer").GetSection("ConnectionString").Value;
        
        await _repositoryCreation.EnsureCreateAsync(strCon);
        await File.WriteAllTextAsync(@"AppConfig/DbOk.txt", strCon);
    }
}