using Domain.Dtos.LogGenerico.Output;
using Domain.Dtos.Usuario.Afastar.Input;
using Domain.Dtos.Usuario.AlterarSenha.Input;
using Domain.Dtos.Usuario.Criar.Input;
using Domain.Dtos.Usuario.Criar.Output;
using Domain.Dtos.Usuario.Editar.Input;
using Domain.Dtos.Usuario.Editar.Output;
using Domain.Dtos.Usuario.EsqueciSenha.Input;
using Domain.Dtos.Usuario.EsqueciSenha.Output;
using Domain.Dtos.Usuario.Listar.Input;
using Domain.Dtos.Usuario.Listar.Output;
using Domain.Dtos.Usuario.ListarLog.Input;
using Domain.Dtos.Usuario.Readmitir.Input;
using Domain.Dtos.Usuario.RetornaDetalhado.Input;
using Domain.Dtos.Usuario.RetornaDetalhado.Output;
using Domain.Dtos.Usuario.RetornaParaEdicao.Input;
using Domain.Dtos.Usuario.RetornaParaEdicao.Output;
using Domain.Entities;

namespace Domain.DomainServicesInterfaces;

/// <summary>
/// Núcleo de usuários
/// </summary>
public interface IUsuarioService
{
    /// <summary>
    /// Lista todas as alterações realizadas em um usuário
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<LogGenericoListarLogOutModel> ListarLogAsync(UsuarioListarLogLogInModel model);
    /// <summary>
    /// Lista de todos os usuários
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<UsuarioListarOutModel> ListarAsync(UsuarioListarInModel model);
    /// <summary>
    /// Cria um novo usuário
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<UsuarioAdicionarOutModel> AdicionarAsync(UsuarioAdicionarInModel model);
    /// <summary>
    /// Edita um usuário existente
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<UsuarioEditarOutModel> EditarAsync(UsuarioEditarInModel model);
    /// <summary>
    /// Envia dados de senha para email (e futuramente para outros meios). Isso não é uma política sadia e no futuro enviará token para mudar senha
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<UsuarioEsqueciSenhaOutModel> EsqueciSenhaAsync(UsuarioEsqueciSenhaInModel model);
    /// <summary>
    /// Altera senha para um determinado usuário logado
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task AlterarSenhaAsync(UsuarioAlterarSenhaInModel model);
    /// <summary>
    /// Retorna o usuário de forma detalhada
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<UsuarioRetornaDetalhadoOutModel> RetornaDetalhadoAsync(UsuarioRetornaDetalhadoInModel model);
    /// <summary>
    /// Afasta um usuário de utilizar o sistema, impedindo seu login e acesso a plataforma
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task AfastarAsync(UsuarioAfastarInModel model);
    /// <summary>
    /// Reativa um usuário para utilização do sistema, desta forma o usuário poderá realizar login novamente na plataforma
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task ReativarAsync(UsuarioReativarInModel model);
    /// <summary>
    /// Método usado principalmente em aplicação .NET MVC com foco em criar um modelo para edição de usuário
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<UsuarioRetornaParaEdicaoOutModel> RetornaParaEdicaoAsync(UsuarioRetornaParaEdicaoInModel model);

    /// <summary>
    /// Pesquisa usuário por e-mail
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<Usuario?> ReturnByEmailAsync(string email);
    /// <summary>
    /// Informa se existe um usuário cadastrado no banco de dados
    /// </summary>
    /// <returns></returns>
    Task<bool> ExisteUsuarioCadastradoAsync();
    /// <summary>
    /// Cria usuário master no sistema
    /// </summary>
    /// <param name="usuario"></param>
    /// <returns></returns>
    Task CriarUsuarioMasterAsync(Usuario? usuario);

}