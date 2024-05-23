
using Domain.DomainServices.Interfaces;
using Domain.Interfaces;
using Infra.MessageErrors;
using Infra.Services.ApplicationGetServiceInstance;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace DomainTests;

public abstract class TestClassBase
{
    //Service & Service Provider
    public readonly IServiceCollection _services = new ServiceCollection();
    public readonly IServiceProvider _serviceProvider;

    //Mocks Objects
    public readonly Mock<IUnitOfWork> _mockIUnitOfWork;
    public readonly Mock<ILogGenericoService> _mockILogGenericoService;
    public readonly Mock<IPermissaoService> _mockIPermissaoService;
    public readonly Mock<IRegraSistemaService> _mockIRegraSistemaService;
    public readonly Mock<ITipoUsuarioCheckerService> _mockITipoUsuarioCheckerService;
    public readonly Mock<ITipoUsuarioService> _mockITipoUsuarioService;
    public readonly Mock<IUsuarioCheckerService> _mockIUsuarioCheckerService;
    public readonly Mock<IUsuarioService> _mockIUsuarioService;
    public readonly Mock<IConfiguracaoGeralService> _mockIConfiguracaoGeralService;

    //No Mock Objects
    public IExceptions _exceptions;

    /// <summary>
    /// Constructor e registrador de dependências
    /// </summary>
    public TestClassBase()
    {
        _services = new ServiceCollection();
        _services.AddSingleton<IExceptions, Exceptions>();
        _serviceProvider = _services.BuildServiceProvider();
        _exceptions = _serviceProvider.Get<IExceptions>();

        _mockIUnitOfWork = new Mock<IUnitOfWork>();
        _mockILogGenericoService = new Mock<ILogGenericoService>();
        _mockIPermissaoService = new Mock<IPermissaoService>();
        _mockIRegraSistemaService = new Mock<IRegraSistemaService>();
        _mockITipoUsuarioCheckerService = new Mock<ITipoUsuarioCheckerService>();
        _mockITipoUsuarioService = new Mock<ITipoUsuarioService>();
        _mockIUsuarioCheckerService = new Mock<IUsuarioCheckerService>();
        _mockIUsuarioService = new Mock<IUsuarioService>();
        _mockIConfiguracaoGeralService = new Mock<IConfiguracaoGeralService>();
    }
}