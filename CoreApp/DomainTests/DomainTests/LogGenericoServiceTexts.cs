using Domain.DomainServices.Services;
using Domain.Entities;
using Moq;
using System.Linq.Expressions;
using Domain.Dtos.LogGenerico.Retorna.Input;
using Xunit;

namespace DomainTests;

public class LogGenericoServiceTexts : TestClassBase
{

    //ListarAsync

    [Fact(DisplayName = "Lista log de alteração com sucesso, retornará LogGenericoListarLogOutModel")]
    public async Task ListarAsync_ListarLogDeAlteracoesComSucesso_LogGenericoListarLogOutModel()
    {
        //Arrange
        var logGenericoListarInModel = new LogGenericoListarInModel
        {
            Referencia = "1",
            Tabela = "Usuario"
        };

        var logGenerico = new LogGenerico
        {
            Codigo = 1,
            Campo = "Nome",
            CodUsuarioAcao = 1,
            DataHoraAcao = new DateTime(2000,1,1).AddHours(1),
            Referencia = "1",
            Tabela = "Usuario",
            ValorAlterado = "System Developer",
            ValorAnterior = "System Programmer",
            CodUsuarioAcaoNavigation = new Usuario
            {
                Codigo = 1,
                Nome = "Teste"
            }
        };
        var listLogGenerico = new List<LogGenerico> { logGenerico };
        _mockIUnitOfWork.Setup(x => x.LogGenericoRepository.FindAsync(It.IsAny<Expression<Func<LogGenerico, bool>>>())).ReturnsAsync(listLogGenerico);
           
        //Act
        var logGenericoService = new LogGenericoService(
            _mockIUnitOfWork.Object,
            _mockIUsuarioCheckerService.Object,
            _exceptions
        );

        var act = await logGenericoService.ListarAsync(logGenericoListarInModel);

        //Assert
        Assert.Single(act.ListaLogGenerico);
        Assert.Equal(logGenerico.Campo, act.ListaLogGenerico.FirstOrDefault()?.Campo);
        Assert.Equal(logGenerico.CodUsuarioAcao, act.ListaLogGenerico.FirstOrDefault()?.CodUsuarioAcao);
        Assert.Equal(logGenerico.DataHoraAcao, act.ListaLogGenerico.FirstOrDefault()?.DataHoraAcao);
        Assert.Equal(logGenericoListarInModel.Tabela, act.ListaLogGenerico.FirstOrDefault()?.Tabela);
        Assert.Equal(logGenerico.Tabela, act.ListaLogGenerico.FirstOrDefault()?.Tabela);
        Assert.Equal(logGenerico.ValorAlterado, act.ListaLogGenerico.FirstOrDefault()?.ValorAlterado);
        Assert.Equal(logGenerico.ValorAnterior, act.ListaLogGenerico.FirstOrDefault()?.ValorAnterior);
        Assert.Equal($"{logGenerico.CodUsuarioAcao} - {logGenerico.CodUsuarioAcaoNavigation.Nome}", act.ListaLogGenerico.FirstOrDefault()?.UsuarioAcao);           
    }

    [Fact(DisplayName = "Lista log de alteração com sucesso realizando trim no modelo de entrada, retornará LogGenericoListarLogOutModel")]
    public async Task ListarAsync_ListarLogDeAlteracoesComSucessoRealizandoTrimNoModeloDeEntrada_LogGenericoListarLogOutModel()
    {
        //Arrange
        var logGenericoListarInModel = new LogGenericoListarInModel
        {
            Referencia = "1",
            Tabela = "   Usuario"
        };

        var logGenerico = new LogGenerico
        {
            Codigo = 1,
            Campo = "Nome",
            CodUsuarioAcao = 1,
            DataHoraAcao = new DateTime(2000, 1, 1).AddHours(1),
            Referencia = "1",
            Tabela = "Usuario",
            ValorAlterado = "System Developer",
            ValorAnterior = "System Programmer",
            CodUsuarioAcaoNavigation = new Usuario
            {
                Codigo = 1,
                Nome = "Teste"
            }
        };
        var listLogGenerico = new List<LogGenerico> { logGenerico };
        _mockIUnitOfWork.Setup(x => x.LogGenericoRepository.FindAsync(It.IsAny<Expression<Func<LogGenerico, bool>>>())).ReturnsAsync(listLogGenerico);

        //Act
        var logGenericoService = new LogGenericoService(
            _mockIUnitOfWork.Object,
            _mockIUsuarioCheckerService.Object,
            _exceptions
        );

        var act = await logGenericoService.ListarAsync(logGenericoListarInModel);

        //Assert 
        Assert.Single(act.ListaLogGenerico);
        Assert.Equal(logGenerico.Campo, act.ListaLogGenerico.FirstOrDefault()?.Campo);
        Assert.Equal(logGenerico.CodUsuarioAcao, act.ListaLogGenerico.FirstOrDefault()?.CodUsuarioAcao);
        Assert.Equal(logGenerico.DataHoraAcao, act.ListaLogGenerico.FirstOrDefault()?.DataHoraAcao);
        Assert.Equal(logGenericoListarInModel.Tabela, act.ListaLogGenerico.FirstOrDefault()?.Tabela);
        Assert.Equal(logGenerico.Tabela, act.ListaLogGenerico.FirstOrDefault()?.Tabela);
        Assert.Equal(logGenerico.ValorAlterado, act.ListaLogGenerico.FirstOrDefault()?.ValorAlterado);
        Assert.Equal(logGenerico.ValorAnterior, act.ListaLogGenerico.FirstOrDefault()?.ValorAnterior);
        Assert.Equal($"{logGenerico.CodUsuarioAcao} - {logGenerico.CodUsuarioAcaoNavigation.Nome}", act.ListaLogGenerico.FirstOrDefault()?.UsuarioAcao);
    }

    [Fact(DisplayName = "Não listará o log de alteração, pois existirá campos que não devem ser exibidos, retornará Exception")]
    public async Task ListarAsync_NaoListaraLogAlteracaoPoisExistiraCamposQueNaoDevemSerExibidos_Exception()
    {
        //Arrange
        var logGenericoListarInModel = new LogGenericoListarInModel
        {
            Referencia = "1",
            Tabela = "Usuario"
        };

        var logGenerico = new LogGenerico
        {
            Codigo = 1,
            Campo = "Senha",
            CodUsuarioAcao = 1,
            DataHoraAcao = new DateTime(2000, 1, 1).AddHours(1),
            Referencia = "1",
            Tabela = "Usuario",
            ValorAlterado = "System Developer",
            ValorAnterior = "System Programmer",
            CodUsuarioAcaoNavigation = new Usuario
            {
                Codigo = 1,
                Nome = "Teste"
            }
        };
        var listLogGenerico = new List<LogGenerico> { logGenerico };
        _mockIUnitOfWork.Setup(x => x.LogGenericoRepository.FindAsync(It.IsAny<Expression<Func<LogGenerico, bool>>>())).ReturnsAsync(listLogGenerico);

        //Act
        var logGenericoService = new LogGenericoService(
            _mockIUnitOfWork.Object,
            _mockIUsuarioCheckerService.Object,
            _exceptions
        );

        var act = await Assert.ThrowsAsync<Exception>(() => logGenericoService.ListarAsync(logGenericoListarInModel));

        //Assert
        Assert.NotNull(act);
        Assert.Equal(_exceptions.ExistemCamposQueNaoPodemSerExibidosNoLogDeAlteracao, act.Message);
    }

    [Fact(DisplayName = "Não listará o log de alteração, pois o modelo de dados a referencia tem mais de 255 caracteres, retornará Exception")]
    public async Task ListarAsync_NaoListaraLogAlteracaoPoisOModeloDeEntradaAReferenciaTemMaisDe255Caracteres_Exception()
    {
        //Arrange
        var logGenericoListarInModel = new LogGenericoListarInModel
        {
            Referencia = UtilService.Util.GeradorStrings.Gerar(256,false,false),
            Tabela = "Usuario"
        };

        //Act
        var logGenericoService = new LogGenericoService(
            _mockIUnitOfWork.Object,
            _mockIUsuarioCheckerService.Object,
            _exceptions
        );

        var act = await Assert.ThrowsAsync<Exception>(() => logGenericoService.ListarAsync(logGenericoListarInModel));

        //Assert
        Assert.NotNull(act);
        Assert.Equal(LogGenericoListarInModel.ReferenciaStringLength, act.Message);
    }

    [Fact(DisplayName = "Não listará o log de alteração, pois o modelo de dados a referencia não foi informado, retornará Exception")]
    public async Task ListarAsync_NaoListaraLogAlteracaoPoisOModeloDeEntradaAReferenciaNaoFoiInformado_Exception()
    {
        //Arrange
        var logGenericoListarInModel = new LogGenericoListarInModel
        {
            Referencia = null,
            Tabela = "Usuario"
        };

        //Act
        var logGenericoService = new LogGenericoService(
            _mockIUnitOfWork.Object,
            _mockIUsuarioCheckerService.Object,
            _exceptions
        );

        var act = await Assert.ThrowsAsync<Exception>(() => logGenericoService.ListarAsync(logGenericoListarInModel));

        //Assert
        Assert.NotNull(act);
        Assert.Equal(LogGenericoListarInModel.ReferenciaRequired, act.Message);
    }

    [Fact(DisplayName = "Não listará o log de alteração, pois o modelo de dados a referencia tem menos de 1 caracter, retornará Exception")]
    public async Task ListarAsync_NaoListaraLogAlteracaoPoisOModeloDeEntradaAReferenciaTemMenosDeUmCaracter_Exception()
    {
        //Arrange
        var logGenericoListarInModel = new LogGenericoListarInModel
        {
            Referencia = string.Empty,
            Tabela = "Usuario"
        };

        //Act
        var logGenericoService = new LogGenericoService(
            _mockIUnitOfWork.Object,
            _mockIUsuarioCheckerService.Object,
            _exceptions
        );

        var act = await Assert.ThrowsAsync<Exception>(() => logGenericoService.ListarAsync(logGenericoListarInModel));

        //Assert
        Assert.NotNull(act);
        Assert.Equal(LogGenericoListarInModel.ReferenciaRequired, act.Message);
    }
                
}