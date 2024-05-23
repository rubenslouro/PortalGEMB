using Domain.DomainServices.Services;
using Domain.Dtos.ConfiguracaoGeral.Editar.Input;
using Domain.Dtos.ConfiguracaoGeral.ListarLog.Input;
using Domain.Dtos.ConfiguracaoGeral.Retorna.Input;
using Domain.Dtos.ConfiguracaoGeral.RetornaDetalhado.Input;
using Domain.Dtos.LogGenerico.Retorna.Input;
using Domain.Dtos.LogGenerico.Retorna.Output;
using Domain.Dtos.Permissao.AvaliaNivel.Input;
using Domain.Entities;
using Moq;
using Xunit;

namespace DomainTests;

public class ConfiguracaoGeralServiceTests : TestClassBase
{

    //ListarLogAsync

    [Fact(DisplayName = "Lista log de alteração com sucesso, retornará LogGenericoListarLogOutModel")]
    public async Task ListarLogAsync_ListarLogsComSucesso_LogGenericoListarLogOutModel()
    {
        //Arrange

        var configuracaoGeralListarLogInModel = new ConfiguracaoGeralListarLogInModel
        {
            CodUsuarioSolicitacaoLog = 1
        };

        _mockILogGenericoService.Setup(o => o.ListarAsync(It.IsAny<LogGenericoListarInModel>())).ReturnsAsync(
            new LogGenericoListarLogOutModel
            {
                ListaLogGenerico = new List<LogGenericoListarItemOutModel>
                {
                    new LogGenericoListarItemOutModel(new LogGenerico
                    {
                        Campo = "Teste",
                        Codigo = 1,
                        CodUsuarioAcao = 1,
                        DataHoraAcao = DateTime.Now,
                        Referencia="Teste",
                        Tabela = "Teste",
                        ValorAlterado = "Teste",
                        ValorAnterior = "Teste",
                        CodUsuarioAcaoNavigation = new Usuario
                        {
                            Codigo =1,
                            Nome ="Teste"
                        }
                    })
                }
            });
        //Act
        var configuracaoGeralService = new ConfiguracaoGeralService(
            _mockIUnitOfWork.Object,
            _mockILogGenericoService.Object,
            _mockIPermissaoService.Object,
            _exceptions);

        var act = await configuracaoGeralService.ListarLogAsync(configuracaoGeralListarLogInModel);

        //Assert
        Assert.Single(act.ListaLogGenerico);


    }

    [Fact(DisplayName = "Lista log de alteração sem itens com sucesso, retornará LogGenericoListarLogOutModel")]
    public async Task ListarLogAsync_ListarLogsSemItensComSucesso_LogGenericoListarLogOutModel()
    {
        //Arrange
        var configuracaoGeralListarLogInModel = new ConfiguracaoGeralListarLogInModel
        {
            CodUsuarioSolicitacaoLog = 1
        };

        _mockILogGenericoService.Setup(o => o.ListarAsync(It.IsAny<LogGenericoListarInModel>())).ReturnsAsync(
            new LogGenericoListarLogOutModel
            {
                ListaLogGenerico = new List<LogGenericoListarItemOutModel>()
            });

        //Act
        var configuracaoGeralService = new ConfiguracaoGeralService(
            _mockIUnitOfWork.Object,
            _mockILogGenericoService.Object,
            _mockIPermissaoService.Object,
            _exceptions);

        var act = await configuracaoGeralService.ListarLogAsync(configuracaoGeralListarLogInModel);

        //Assert
        Assert.Empty(act.ListaLogGenerico);


    }

    //RetornaAsync

    [Fact(DisplayName = "Retorna uma configuração do sistema com sucesso, retornará ConfiguracaoGeralRetornaOutModel")]
    public async Task RetornaAsyncAsync_RetornaUmaConfiguracaoComSucesso_ConfiguracaoGeralRetornaOutModel()
    {
        //Arrange
        var configuracaoGeral = new ConfiguracaoGeral
        {
            Codigo = 1,
            UrlSite = "http://www.somesite.com"
        };
        _mockIUnitOfWork.Setup(o => o.ConfiguracaoGeralRepository.FirstAsync()).ReturnsAsync(configuracaoGeral);

        //Act
        var configuracaoGeralService = new ConfiguracaoGeralService(
            _mockIUnitOfWork.Object,
            _mockILogGenericoService.Object,
            _mockIPermissaoService.Object,
            _exceptions);

        var act = await configuracaoGeralService.RetornaAsync();

        //Assert
        Assert.NotNull(act);
        Assert.Equal(configuracaoGeral.UrlSite, act.UrlSite);
    }

    [Fact(DisplayName = "Não retornará configuração válida de sistema já que o sistema~não estará configurado, retornará null")]
    public async Task RetornaAsyncAsync_NaoRetornaConfiguracaoDoSistemaPoisNaoExisteConfiguracao_Null()
    {
        //Arrange            
        _mockIUnitOfWork.Setup(o => o.ConfiguracaoGeralRepository.FirstAsync()).ReturnsAsync(null as ConfiguracaoGeral);

        //Act
        var configuracaoGeralService = new ConfiguracaoGeralService(
            _mockIUnitOfWork.Object,
            _mockILogGenericoService.Object,
            _mockIPermissaoService.Object,
            _exceptions);

        var act = await configuracaoGeralService.RetornaAsync();

        //Assert
        Assert.Null(act);
    }

    //RetornaDetalhadoAsync

    [Fact(DisplayName = "Retornara uma configuracao detalhada válida incluindo log de alteração, retornará ConfiguracaoGeralRetornaDetalhadoOutModel")]
    public async Task RetornaDetalhadoAsync_RetornaConfiguracaoGeralDeFormaDetalhadaIncluindoLogAlteracaoComSucesso_ConfiguracaoGeralRetornaDetalhadoOutModel()
    {
        //Arrange            
        var configuracaoGeral = new ConfiguracaoGeral
        {
            Codigo = 1,
            UrlSite = "http://www.somesite.com"
        };

        _mockIUnitOfWork.Setup(o => o.ConfiguracaoGeralRepository.FirstAsync()).ReturnsAsync(configuracaoGeral);
        _mockIPermissaoService.Setup(o => o.AvaliarNivelAsync(It.IsAny<PermissaoAvaliarNivelInModel>())).ReturnsAsync(true);
        _mockILogGenericoService.Setup(o => o.ListarAsync(It.IsAny<LogGenericoListarInModel>())).ReturnsAsync(
            new LogGenericoListarLogOutModel
            {
                ListaLogGenerico = new List<LogGenericoListarItemOutModel>
                {
                    new LogGenericoListarItemOutModel(new LogGenerico
                    {
                        Campo = "Teste",
                        Codigo = 1,
                        CodUsuarioAcao = 1,
                        DataHoraAcao = DateTime.Now,
                        Referencia="Teste",
                        Tabela = "Teste",
                        ValorAlterado = "Teste",
                        ValorAnterior = "Teste",
                        CodUsuarioAcaoNavigation = new Usuario
                        {
                            Codigo =1,
                            Nome ="Teste"
                        }
                    })
                }
            });

        //Act
        var configuracaoGeralService = new ConfiguracaoGeralService(
            _mockIUnitOfWork.Object,
            _mockILogGenericoService.Object,
            _mockIPermissaoService.Object,
            _exceptions);

        var act = await configuracaoGeralService.RetornaDetalhadoAsync(new ConfiguracaoGeralRetornaDetalhadoInModel { CodUsuarioSolicitacao = 1 });

        //Assert
        Assert.NotNull(act);
        Assert.True(act.PermiteLog);
        Assert.NotNull(act.Log);
        Assert.NotEmpty(act.Log.ListaLogGenerico);
        Assert.Equal(configuracaoGeral.UrlSite, act.UrlSite);

    }

    [Fact(DisplayName = "Retornara uma configuracao detalhada válida sem log de alteração, retornará ConfiguracaoGeralRetornaDetalhadoOutModel")]
    public async Task RetornaDetalhadoAsync_RetornaConfiguracaoGeralDeFormaDetalhadaSemLogAlteracaoComSucesso_ConfiguracaoGeralRetornaDetalhadoOutModel()
    {
        //Arrange            
        var configuracaoGeral = new ConfiguracaoGeral
        {
            Codigo = 1,
            UrlSite = "http://www.somesite.com"
        };

        _mockIUnitOfWork.Setup(o => o.ConfiguracaoGeralRepository.FirstAsync()).ReturnsAsync(configuracaoGeral);
        _mockIPermissaoService.Setup(o => o.AvaliarNivelAsync(It.IsAny<PermissaoAvaliarNivelInModel>())).ReturnsAsync(false);

        //Act
        var configuracaoGeralService = new ConfiguracaoGeralService(
            _mockIUnitOfWork.Object,
            _mockILogGenericoService.Object,
            _mockIPermissaoService.Object,
            _exceptions);

        var act = await configuracaoGeralService.RetornaDetalhadoAsync(new ConfiguracaoGeralRetornaDetalhadoInModel { CodUsuarioSolicitacao = 1 });

        //Assert
        Assert.NotNull(act);
        Assert.False(act.PermiteLog);
        Assert.Null(act.Log);
        Assert.Equal(configuracaoGeral.UrlSite, act.UrlSite);

    }

    [Fact(DisplayName = "Retornara uma configuracao detalhada válida incluindo log de alteração, porém o log será vazio, retornará ConfiguracaoGeralRetornaDetalhadoOutModel")]
    public async Task RetornaDetalhadoAsync_RetornaConfiguracaoGeralDeFormaDetalhadaIncluindoLogAlteracaoComSucessoPoremComLogVazio_ConfiguracaoGeralRetornaDetalhadoOutModel()
    {
        //Arrange            
        var configuracaoGeral = new ConfiguracaoGeral
        {
            Codigo = 1,
            UrlSite = "http://www.somesite.com"
        };

        _mockIUnitOfWork.Setup(o => o.ConfiguracaoGeralRepository.FirstAsync()).ReturnsAsync(configuracaoGeral);
        _mockIPermissaoService.Setup(o => o.AvaliarNivelAsync(It.IsAny<PermissaoAvaliarNivelInModel>())).ReturnsAsync(true);
        _mockILogGenericoService.Setup(o => o.ListarAsync(It.IsAny<LogGenericoListarInModel>())).ReturnsAsync(
            new LogGenericoListarLogOutModel
            {
                ListaLogGenerico = new List<LogGenericoListarItemOutModel>()
            });

        //Act
        var configuracaoGeralService = new ConfiguracaoGeralService(
            _mockIUnitOfWork.Object,
            _mockILogGenericoService.Object,
            _mockIPermissaoService.Object,
            _exceptions);

        var act = await configuracaoGeralService.RetornaDetalhadoAsync(new ConfiguracaoGeralRetornaDetalhadoInModel { CodUsuarioSolicitacao = 1 });

        //Assert
        Assert.NotNull(act);
        Assert.True(act.PermiteLog);
        Assert.NotNull(act.Log);
        Assert.Empty(act.Log.ListaLogGenerico);
        Assert.Equal(configuracaoGeral.UrlSite, act.UrlSite);

    }

    [Fact(DisplayName = "Não retornará uma configuracao detalhada válida pois, não existe no banco de dados, retornará null")]
    public async Task RetornaDetalhadoAsync_NaoRetornaConfiguracaoGeralDeFormaDetalhadaPoisNaoExisteNoBancoDados_Null()
    {
        //Arrange         
        _mockIUnitOfWork.Setup(o => o.ConfiguracaoGeralRepository.FirstAsync()).ReturnsAsync(null as ConfiguracaoGeral);

        //Act
        var configuracaoGeralService = new ConfiguracaoGeralService(
            _mockIUnitOfWork.Object,
            _mockILogGenericoService.Object,
            _mockIPermissaoService.Object,
            _exceptions);

        var act = await configuracaoGeralService.RetornaDetalhadoAsync(new ConfiguracaoGeralRetornaDetalhadoInModel { CodUsuarioSolicitacao = 1 });

        //Assert
        Assert.Null(act);
    }

    [Fact(DisplayName = "Não retornará uma configuracao detalhada válida pois, o usuário não foi informado, retornará Exception")]
    public async Task RetornaDetalhadoAsync_RetornaraErroPoisUsuarioNaoFoiInformado_Exception()
    {
        //Arrange         
        var configuracaoGeralRetornaDetalhadoInModel = new ConfiguracaoGeralRetornaDetalhadoInModel { CodUsuarioSolicitacao = 0 };

        //Act
        var configuracaoGeralService = new ConfiguracaoGeralService(
            _mockIUnitOfWork.Object,
            _mockILogGenericoService.Object,
            _mockIPermissaoService.Object,
            _exceptions);

        var act = await Assert.ThrowsAsync<Exception>(() => configuracaoGeralService.RetornaDetalhadoAsync(configuracaoGeralRetornaDetalhadoInModel));
        //Assert
        Assert.Equal(ConfiguracaoGeralRetornaInModel.ErroUsuarioInvalido, act.Message);
    }

    //EditarConfiguracaoAsync

    [Fact(DisplayName = "Edita a configuração de sistema com sucesso, retornará ConfiguracaoGeralEditarConfiguracaoOutModel")]
    public async Task RetornaDetalhadoAsync_EditaraConfiguracaoSistemaComSucesso_ConfiguracaoGeralEditarConfiguracaoOutModel()
    {
        //Arrange         
        var configuracaoGeralEditarConfiguracaoInModel = new ConfiguracaoGeralEditarConfiguracaoInModel
        {
            CodUsuarioAcao = 1,
            UrlSite = "http://www.calangoskeleton.com"
        };
        var configuracaoGeralAntes = new ConfiguracaoGeral
        {
            Codigo = 1,
            UrlSite = "http://wwwsomesite.com"
        };
        var configuracaoGeralPos = new ConfiguracaoGeral
        {
            Codigo = 1,
            UrlSite = string.Empty
        };

        _mockIUnitOfWork.Setup(o => o.ConfiguracaoGeralRepository.AnyAsync()).ReturnsAsync(true);
        _mockIUnitOfWork.Setup(o => o.ConfiguracaoGeralRepository.FirstAsync(It.IsAny<bool>())).ReturnsAsync(configuracaoGeralAntes);
        _mockIUnitOfWork.Setup(o => o.ConfiguracaoGeralRepository.FirstAsync()).ReturnsAsync(configuracaoGeralAntes);
        _mockIUnitOfWork.Setup(o => o.SaveAsync()).Verifiable();
        _mockILogGenericoService.Setup(o => o.CriarAsync(It.IsAny<object>(), It.IsAny<object>(), It.IsAny<string>(), It.IsAny<int>())).Verifiable();

        //Act
        var configuracaoGeralService = new ConfiguracaoGeralService(
            _mockIUnitOfWork.Object,
            _mockILogGenericoService.Object,
            _mockIPermissaoService.Object,
            _exceptions);

        var act = await configuracaoGeralService.EditarConfiguracaoAsync(configuracaoGeralEditarConfiguracaoInModel);
        //Assert
        Assert.NotNull(act);
        Assert.Equal(configuracaoGeralEditarConfiguracaoInModel.UrlSite, act.UrlSite);
    }

    [Fact(DisplayName = "Edita a configuração de sistema com sucesso, ajustando os espaços em branco no modelo, retornará ConfiguracaoGeralEditarConfiguracaoOutModel")]
    public async Task RetornaDetalhadoAsync_EditaraConfiguracaoSistemaComSucessoAjustandoEspacosEmBrancoNoModelo_ConfiguracaoGeralEditarConfiguracaoOutModel()
    {
        //Arrange         
        var configuracaoGeralEditarConfiguracaoInModel = new ConfiguracaoGeralEditarConfiguracaoInModel
        {
            CodUsuarioAcao = 1,
            UrlSite = "              http://www.calangoskeleton.com"
        };
        var configuracaoGeralAntes = new ConfiguracaoGeral
        {
            Codigo = 1,
            UrlSite = "http://wwwsomesite.com"
        };
        var configuracaoGeralPos = new ConfiguracaoGeral
        {
            Codigo = 1,
            UrlSite = string.Empty
        };

        _mockIUnitOfWork.Setup(o => o.ConfiguracaoGeralRepository.AnyAsync()).ReturnsAsync(true);
        _mockIUnitOfWork.Setup(o => o.ConfiguracaoGeralRepository.FirstAsync(It.IsAny<bool>())).ReturnsAsync(configuracaoGeralAntes);
        _mockIUnitOfWork.Setup(o => o.ConfiguracaoGeralRepository.FirstAsync()).ReturnsAsync(configuracaoGeralAntes);
        _mockIUnitOfWork.Setup(o => o.SaveAsync()).Verifiable();
        _mockILogGenericoService.Setup(o => o.CriarAsync(It.IsAny<object>(), It.IsAny<object>(), It.IsAny<string>(), It.IsAny<int>())).Verifiable();

        //Act
        var configuracaoGeralService = new ConfiguracaoGeralService(
            _mockIUnitOfWork.Object,
            _mockILogGenericoService.Object,
            _mockIPermissaoService.Object,
            _exceptions);

        var act = await configuracaoGeralService.EditarConfiguracaoAsync(configuracaoGeralEditarConfiguracaoInModel);
        //Assert
        Assert.NotNull(act);
        Assert.Equal(configuracaoGeralEditarConfiguracaoInModel.UrlSite.Trim(), act.UrlSite);
    }

    [Fact(DisplayName = "Não editará a configuração de sistema pois, o usuário do modelo de dados é inválido, retornará Exception")]
    public async Task RetornaDetalhadoAsync_NaoEditaraConfiguracaoSistemaPoisOUsuarioDoModeloEInvalido_Exception()
    {
        //Arrange
        var configuracaoGeralEditarConfiguracaoInModel = new ConfiguracaoGeralEditarConfiguracaoInModel
        {
            CodUsuarioAcao = 0,
            UrlSite = "http://www.calangoskeleton.com"
        };

        //Act
        var configuracaoGeralService = new ConfiguracaoGeralService(
            _mockIUnitOfWork.Object,
            _mockILogGenericoService.Object,
            _mockIPermissaoService.Object,
            _exceptions);

        var act = await Assert.ThrowsAsync<Exception>(() => configuracaoGeralService.EditarConfiguracaoAsync(configuracaoGeralEditarConfiguracaoInModel));
        //Assert
        Assert.NotNull(act);
        Assert.Equal(ConfiguracaoGeralEditarConfiguracaoInModel.UsuarioRange, act.Message);
    }

    [Fact(DisplayName = "Não editará a configuração de sistema pois, a url do site não foi informada, retornará Exception")]
    public async Task RetornaDetalhadoAsync_NaoEditaraConfiguracaoSistemaPoisAUrlDoSiteNaoFoiInformada_Exception()
    {
        //Arrange
        var configuracaoGeralEditarConfiguracaoInModel = new ConfiguracaoGeralEditarConfiguracaoInModel
        {
            CodUsuarioAcao = 1,
            UrlSite = string.Empty
        };

        //Act
        var configuracaoGeralService = new ConfiguracaoGeralService(
            _mockIUnitOfWork.Object,
            _mockILogGenericoService.Object,
            _mockIPermissaoService.Object,
            _exceptions);

        var act = await Assert.ThrowsAsync<Exception>(() => configuracaoGeralService.EditarConfiguracaoAsync(configuracaoGeralEditarConfiguracaoInModel));
        //Assert
        Assert.NotNull(act);
        Assert.Equal(ConfiguracaoGeralEditarConfiguracaoInModel.UrlSiteRequired, act.Message);
    }

    [Fact(DisplayName = "Não editará a configuração de sistema pois, a url do site não atendede ao tamanho mínimo, retornará Exception")]
    public async Task RetornaDetalhadoAsync_NaoEditaraConfiguracaoSistemaPoisAUrlDoSiteNaoAtendeAoTamanhoMinimo_Exception()
    {
        //Arrange
        var configuracaoGeralEditarConfiguracaoInModel = new ConfiguracaoGeralEditarConfiguracaoInModel
        {
            CodUsuarioAcao = 1,
            UrlSite = "http://"
        };

        //Act
        var configuracaoGeralService = new ConfiguracaoGeralService(
            _mockIUnitOfWork.Object,
            _mockILogGenericoService.Object,
            _mockIPermissaoService.Object,
            _exceptions);

        var act = await Assert.ThrowsAsync<Exception>(() => configuracaoGeralService.EditarConfiguracaoAsync(configuracaoGeralEditarConfiguracaoInModel));
        //Assert
        Assert.NotNull(act);
        Assert.Equal(ConfiguracaoGeralEditarConfiguracaoInModel.UrlSiteStringLength, act.Message);
    }

    [Fact(DisplayName = "Não editará a configuração de sistema pois, a url do site não atendede ao tamanho máximo, retornará Exception")]
    public async Task RetornaDetalhadoAsync_NaoEditaraConfiguracaoSistemaPoisAUrlDoSiteNaoAtendeAoTamanhoMaximo_Exception()
    {
        //Arrange
        var configuracaoGeralEditarConfiguracaoInModel = new ConfiguracaoGeralEditarConfiguracaoInModel
        {
            CodUsuarioAcao = 1,
            UrlSite = UtilService.Util.GeradorStrings.Gerar(256,false,false)
        };

        //Act
        var configuracaoGeralService = new ConfiguracaoGeralService(
            _mockIUnitOfWork.Object,
            _mockILogGenericoService.Object,
            _mockIPermissaoService.Object,
            _exceptions);

        var act = await Assert.ThrowsAsync<Exception>(() => configuracaoGeralService.EditarConfiguracaoAsync(configuracaoGeralEditarConfiguracaoInModel));
        //Assert
        Assert.NotNull(act);
        Assert.Equal(ConfiguracaoGeralEditarConfiguracaoInModel.UrlSiteStringLength, act.Message);
    }

    [Fact(DisplayName = "Não editará a configuração de sistema pois, já existe uma configuração, retornará Exception")]
    public async Task RetornaDetalhadoAsync_NaoEditaraConfiguracaoSistemaPoisJaExisteUmaConfiguracao_ConfiguracaoGeralEditarConfiguracaoOutModel()
    {
        //Arrange         
        var configuracaoGeralEditarConfiguracaoInModel = new ConfiguracaoGeralEditarConfiguracaoInModel
        {
            CodUsuarioAcao = 1,
            UrlSite = "http://www.calangoskeleton.com"
        };

        _mockIUnitOfWork.Setup(o => o.ConfiguracaoGeralRepository.AnyAsync()).ReturnsAsync(false);           

        //Act
        var configuracaoGeralService = new ConfiguracaoGeralService(
            _mockIUnitOfWork.Object,
            _mockILogGenericoService.Object,
            _mockIPermissaoService.Object,
            _exceptions);

        var act = await Assert.ThrowsAsync<Exception>(() => configuracaoGeralService.EditarConfiguracaoAsync(configuracaoGeralEditarConfiguracaoInModel));
        //Assert
        Assert.Equal(_exceptions.ConfiguracaoGeralNaoEncontradaNoBancoDeDados, act.Message);
    }

    //IncluirPrimeiraConfiguracaoSistemaAsync

    [Fact(DisplayName = "Não inclui a primeira configuração de sistema pois o objeto é nulo")]
    public async Task IncluiPrimeiraConfiguracaoSistemaAsync_NaoIncluiAPrimeiraConfiguracaoDoSistemaPoisOObjetoENulo_Exception()
    {
        //Arrange                 
           
        //Act
        var configuracaoGeralService = new ConfiguracaoGeralService(
            _mockIUnitOfWork.Object,
            _mockILogGenericoService.Object,
            _mockIPermissaoService.Object,
            _exceptions);

        var act = await Assert.ThrowsAsync<Exception>(() => configuracaoGeralService.IncluirPrimeiraConfiguracaoSistemaAsync(null));
        //Assert
        Assert.Equal(_exceptions.ConfiguracaoGeralNaoEncontradaNoBancoDeDados, act.Message);
    }



}