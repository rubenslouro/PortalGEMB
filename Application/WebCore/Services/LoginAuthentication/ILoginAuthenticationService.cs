using System.Threading.Tasks;
using Domain.Dtos.Usuario.AlterarSenha.Input;
using Domain.Dtos.Usuario.Criar.Input;
using Domain.Dtos.Usuario.Criar.Output;
using WebCore.DTO.LoginWeb.Input;
using WebCore.DTO.LoginWeb.Output;

namespace WebCore.Services.LoginAuthentication;

/// <summary>
/// Serviço de autenticação
/// </summary>
public interface ILoginAuthenticationService
{
    /// <summary>
    /// Método de login
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<LoginOutModel> LoginAsync(LoginInModel model);
    /// <summary>
    /// Verifica se o usuário está logado
    /// </summary>
    /// <returns></returns>
    Task<bool> LogadoAsync();
    /// <summary>
    /// Retorna o usuário logado
    /// </summary>
    /// <returns></returns>
    Task<LoginOutModel> UsuarioLogadoAsync();
    /// <summary>
    /// Retorna o código do usuário logado
    /// </summary>
    /// <returns></returns>
    Task<int> CodUsuarioLogadoAsync();
    /// <summary>
    /// Método responsável pela alteração de senha du usuário
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task AlterarSenhaAsync(UsuarioAlterarSenhaInModel model);
    /// <summary>
    /// Cria um novo usuário no sistema
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<UsuarioAdicionarOutModel> AdicionarAsync(UsuarioAdicionarInModel model);

    /// <summary>
    /// Logout
    /// </summary>
    void Logout();
}