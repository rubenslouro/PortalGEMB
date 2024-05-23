using Domain.Dtos.LogGenerico.Output;
using Domain.Dtos.TipoUsuario.Criar.Input;
using Domain.Dtos.TipoUsuario.Criar.Output;
using Domain.Dtos.TipoUsuario.Editar.Input;
using Domain.Dtos.TipoUsuario.Listar.Output;
using Domain.Dtos.TipoUsuario.ListarLog.Input;
using Domain.Dtos.TipoUsuario.Visualizar.Input;
using Domain.Dtos.TipoUsuario.Visualizar.Output;
using Domain.Dtos.Usuario.ListarPorTipoUsuario.Input;
using Domain.Dtos.Usuario.ListarPorTipoUsuario.Output;

namespace Domain.DomainServicesInterfaces;

/// <summary>
/// Núcleo de tipos de usuário/perfil
/// </summary>
public interface ITipoUsuarioService
{
    /// <summary>
    /// Cria um novo tipo de usuário/perfil
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<TipoUsuarioAdicionarOutModel> AdicionarAsync(TipoUsuarioAdicionarInModel model);
    /// <summary>
    /// Edita um determinado tipo de usuário/perfil
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task EditarTipoUsuarioAsync(TipoUsuarioEditarTipoUsuarioInModel model);
    /// <summary>
    /// Lista de inteiros com os códigos de tipos de usuário que tem sua edição bloqueada
    /// </summary>
    /// <returns></returns>
    int[] ListaTiposUsuarioEdicaoBloqueada();
    /// <summary>
    /// Utilizado durante a instalação para criar os tipos de usuário padrão do sistema Master e Gerente
    /// </summary>
    /// <returns></returns>
    Task CriarTiposUsuariosBasicosAsync();
    /// <summary>
    /// Retorna dados detalhados de um determinado tipo de usuário/perfil
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<TipoUsuarioVisualizarOutModel> VisualizarAsync(TipoUsuarioVisualizarInModel model);
    /// <summary>
    /// Listagem de todos os tipos de usuários/perfil
    /// </summary>
    /// <returns></returns>
    Task<TipoUsuarioListarOutModel> ListarAsync();
    /// <summary>
    /// Listagem de todos os tipos de usuários/perfil sem o perfil Master
    /// </summary>
    /// <returns></returns>
    Task<TipoUsuarioListarOutModel> ListarTodosSemMasterAsync();
    /// <summary>
    /// Lista de todos os usuários que estão vinculados ao tipo de usuário/perfil
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<UsuarioListarUsuariosPorTipoOutModel> ListarUsuariosPorTipoAsync(UsuarioListarUsuariosPorTipoInModel model);
    /// <summary>
    /// Lista todas as alterações realizadas em um determinado tipo de usuário/perfil
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<LogGenericoListarLogOutModel> ListarLogAsync(TipoUsuarioListarLogInModel model);
}