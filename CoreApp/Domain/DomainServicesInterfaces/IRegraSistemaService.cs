using Domain.Dtos.RegraSistema.AdicionarRegra.Input;
using Domain.Dtos.RegraSistema.AdicionarRegraPerfil.Input;
using Domain.Dtos.RegraSistema.AdicionarRegraPerfil.Output;
using Domain.Dtos.RegraSistema.AdicionarTodas.Input;
using Domain.Dtos.RegraSistema.AdicionarTodasPerfil.Input;
using Domain.Dtos.RegraSistema.AdicionarTodasPerfil.Output;
using Domain.Dtos.RegraSistema.ListaRegraAusente.Input;
using Domain.Dtos.RegraSistema.ListaRegraAusente.Output;
using Domain.Dtos.RegraSistema.ListarRegraExedente.Input;
using Domain.Dtos.RegraSistema.ListarRegraExedente.Output;
using Domain.Dtos.RegraSistema.RedefinirPadraoUsuario.Input;
using Domain.Dtos.RegraSistema.RemoverPermissaoPerfil.Input;
using Domain.Dtos.RegraSistema.RemoverPermissaoPerfil.Output;
using Domain.Dtos.RegraSistema.RemoverRegra.Input;
using Domain.Dtos.RegraSistema.RemoverTodas.Input;
using Domain.Dtos.RegraSistema.RemoverTodasPerfil.Input;
using Domain.Dtos.RegraSistema.RemoverTodasPerfil.Output;
using Domain.Dtos.RegraSistema.Retorna.Input;
using Domain.Dtos.RegraSistema.Retorna.Output;
using Domain.Dtos.RegraSistema.RetornaRegrasNegadasTipoUsuario.Input;
using Domain.Dtos.RegraSistema.RetornaRegrasNegadasTipoUsuario.Output;
using Domain.Dtos.RegraSistema.RetornaRegrasNegadasUsuario.Input;
using Domain.Dtos.RegraSistema.RetornaRegrasNegadasUsuario.Output;
using Domain.Dtos.RegraSistema.RetornaRegrasTipoUsuario.Input;
using Domain.Dtos.RegraSistema.RetornaRegrasTipoUsuario.Output;
using Domain.Dtos.RegraSistema.RetornaRegrasUsuario.Input;
using Domain.Dtos.RegraSistema.RetornaRegrasUsuario.Output;
using Domain.Dtos.RegraSistema.UpdateDiretivasPerfilTipoUsuario.Input;
using Domain.Dtos.RegraSistema.UpdateDiretivasPerfilTipoUsuario.Output;
using Domain.Dtos.RegraSistema.UsuarioRegraCustomizada.Input;

namespace Domain.DomainServicesInterfaces;

/// <summary>
/// Núcleo de regras de sistema
/// </summary>
public interface IRegraSistemaService
{

    /// <summary>
    /// Lista todas as Regras de Sistme
    /// </summary>
    /// <returns></returns>
    Task<List<RegraSistemaRetornaOutModel>> ListaTodasRegrasSistemaAsync();

    /// <summary>
    /// Redefine todos os usuários de um determinado tipo para que fiquem todos dentro das permissões do tipo de usuário removendo toda e qualquer permissão customizada
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<RegraSistemaRedefinirRegrasSistemaPadraoTipoUsuarioOutModel> RedefinirRegrasSistemaPadraoTipoUsuarioAsync(RegraSistemaRedefinirRegrasSistemaPadraoTipoUsuarioInModel model);
    /// <summary>
    /// Informa se tem regras de sistema que estão fora do padrão para o tipo do usuário em um determinado usuário
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<bool> RegrasSistemaCustomizadasUsuario(RegrasSistemaCustomizadasUsuarioInModel model);
    /// <summary>
    /// Retorna uma regra de sistema com seus detalhes
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<RegraSistemaRetornaOutModel> RetornarAsync(RegraSistemaRetornarInModel model);
    /// <summary>
    /// Adiciona todas as regras de sistema a um usuário com exeção das regras cadastro de usuário e cadastro de tipos de usuário
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task AdicionarTodasRegrasSistemaUsuarioAsync(RegraSistemaAdicionarTodasRegrasSistemaUsuarioInModel model);
    /// <summary>
    /// Remove todas as regras de sistema de um usuário
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task RegrasSistemaRemoverTodasUsuarioAsync(RegraSistemaRemoverTodasUsuarioInModel model);
    /// <summary>
    /// Adiciona uma regra de sistema para um usuário específico
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task RegraSistemaAdicionarUsuarioAsync(RegraSistemaAdicionarUsuarioInModel model);
    /// <summary>
    /// Remove uma determinada regra de sistema para um usuário
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task RemoverRegraSistemaUsuarioAsync(RegraSistemaRemoverRegraSistemaUsuarioInModel model);

    /// <summary>
    /// Remove uma regra de sistema de um perfil de usuário
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<RegraSistemaRemoverRegraSistemaTipoUsuarioOutModel> RemoverRegraSistemaTipoUsuarioAsync(RegraSistemaRemoverRegraSistemaTipoUsuarioInModel model);
    /// <summary>
    /// Utilizado durante a instalação do sistema para que sejam criadas as regras de sistema
    /// </summary>
    /// <returns></returns>
    Task InstalarRegrasSistemaAsync();
    /// <summary>
    /// Adiciona todas as regras de sistema para um perfil/tipo de usuário
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<RegraSistemaAdicionarTodasRegraSistemaPerfilUsuarioOutModel> AdicionarTodasRegraSistemaPerfilUsuarioAsync(RegraSistemaAdicionarTodasRegraSistemaPerfilUsuarioInModel model);

    /// <summary>
    /// Adiciona uma regra de sistema para um perfil/tipo de usuário
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<RegraSistemaAdicionarRegraSistemaPerfilUsuarioOutModel> AdicionarRegraSistemaPerfilUsuarioAsync(RegraSistemaAdicionarRegraSistemaPerfilUsuarioInModel model);

    /// <summary>
    /// Remove todas as regras de sistema de um tipo/perfil usuário
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<RegraSistemaRemoverTodasRegrasSistemaPerfilUsuarioOutModel> RemoverTodasRegrasSistemaPerfilUsuarioAsync(RegraSistemaRemoverTodasRegrasSistemaPerfilUsuarioInModel model);

    /// <summary>
    /// Redefine todas as regras de sistema padrão para um usuário baseado em seu tipo/perfil usuário
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task RedefinirRegrasSistemaPadraoUsuarioAsync(RegraSistemaRedefinirRegrasSistemaPadraoUsuarioInModel model);
    /// <summary>
    /// Lista todas as regras de sistema de um usuário que não estão enquadradas em seu perfil/tipo usuário
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<RegraSistemaRegrasSistemaExcedenteOutModel> RegrasSistemaExcedenteAsync(RegraSistemaRegrasSistemaExcedenteInModel model);
    /// <summary>
    /// Lista todas as degras de sistema que estão ausentes/faltantes em um determinado usuário baseado em seu perfil/tipo usuário
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<RegraSistemaListaRegrasSistemaAusentesOutModel> ListaRegrasSistemaAusentesAsync(RegraSistemaListaRegrasSistemaAusentesInModel model);
    /// <summary>
    /// Retorna todas as regras de sistema de um determinado tipo de usuário/perfil
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<RegraSistemaRetornaRegrasSistemaTipoUsuarioOutModel> RetornaRegrasSistemaTipoUsuarioAsync(RegraSistemaRetornaRegrasSistemaTipoUsuarioInModel model);
    /// <summary>
    /// Retorna todas as regras de sistema que não foram aplicadas a um determinado perfil/tipo usuário
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<RegrasSistemaNegadasTipoUsuarioOutModel> RegrasSistemaNegadasTipoUsuarioAsync(RegrasSistemaNegadasTipoUsuarioInModel model);
    /// <summary>
    /// Exibe todas as regras de sistema que não foram aplicadas a um determinado usuário do sistema
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<RegrasSistemaNegadasUsuarioOutModel> RegrasSistemaNegadasUsuarioAsync(RegrasSistemaNegadasUsuarioInModel model);
    /// <summary>
    /// Retornas todas as regras de sistema vinculadas a um determinado usuário
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<RegraSistemaRetornaRegrasSistemaUsuarioOutModel> RetornaRegrasSistemaUsuarioAsync(RegraSistemaRetornaRegrasSistemaUsuarioInModel model);
}