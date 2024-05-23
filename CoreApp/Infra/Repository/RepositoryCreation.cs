
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InfraDatabase.Repository;

/// <inheritdoc />
public class RepositoryCreation : IRepositoryCreation
{
    private readonly CalangoContext _calangoContext;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="calangoContext"></param>
    /// <param name="configuration"></param>
    public RepositoryCreation(CalangoContext calangoContext, IConfiguration configuration)
    {
        _calangoContext = calangoContext;
        _configuration = configuration;
    }

    /// <inheritdoc />
    public async Task EnsureCreateAsync(string strCon)
    {
        _calangoContext.Database.SetConnectionString(strCon);
        await _calangoContext.Database.EnsureCreatedAsync();
    }

    /// <inheritdoc />
    public async Task<bool> CanConnectAsync()
    {
        var connectinString = _configuration.GetSection("SqlServer").GetSection("ConnectionString").Value;
        _calangoContext.Database.SetConnectionString(connectinString);
        return await _calangoContext.Database.CanConnectAsync();
    }
}