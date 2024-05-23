using System.ComponentModel;

namespace Domain.Enums;

/// <summary>
/// Enum com as regras de sistema
/// </summary>
public enum TipoRegraSistema
{

    /// <summary>
    /// Cria, edita e visualiza usuários e suas regras de acesso
    /// </summary>
    [DefaultValue("Cadastro de usuário")]
    [Description("Cria, edita e visualiza usuários e suas regras de acesso")]
    CadastroUsuario = 1,

    /// <summary>
    /// Visualiza alterações no cadastro dos usuários
    /// </summary>
    [DefaultValue("Log usuário")]
    [Description("Visualiza alterações no cadastro dos usuários")]
    LogUsuario = 2,

    /// <summary>
    /// Cria e altera tipos de usuários e suas regas de de acesso
    /// </summary>
    [DefaultValue("Tipo de usuário")]
    [Description("Cria e altera tipos de usuários e suas regas de de acesso")]
    CadastroTipoUsuario = 3,

    /// <summary>
    /// Visualiza alterações realizadas no perfil e no tipo do usuário
    /// </summary>
    [DefaultValue("Logs de tipo de usuário")]
    [Description("Visualiza alterações realizadas no perfil e no tipo do usuário")]
    LogTipoUsuario = 4,

    /// <summary>
    /// Visualiza e altera as configurações gerais do sistema
    /// </summary>
    [DefaultValue("Configurações gerais")]
    [Description("Visualiza e altera as configurações gerais do sistema")]
    ConfiguracaoGeral = 5,

    /// <summary>
    /// Visualiza alterações nas configurações gerais
    /// </summary>
    [DefaultValue("Log configurações gerais")]
    [Description("Visualiza alterações nas configurações gerais")]
    LogConfiguracaoGeral = 6,
}