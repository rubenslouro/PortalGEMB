namespace Domain.Interfaces.Exception;

/// <summary>
/// Classe para constantes e métodos de exceções
/// </summary>
public interface IExceptions
{
    /// <summary>
    /// Classe de exceptions
    /// </summary>
    public string ErroDesconhecido { get; }

    #region Configuração Geral
    /// <summary>
    /// Configuração geral não encontrada no banco de dados
    /// </summary>
    public string ConfiguracaoGeralNaoEncontradaNoBancoDeDados { get; }
    /// <summary>
    /// Não é permitido criar outro banco de dados
    /// </summary>
    public string NaoEPermitidoCriarOutroBancoDeDados { get; }
    /// <summary>
    /// A tabela configuração geral já está alimentada
    /// </summary>
    public string ATabelaConfiguiracaoGeralJaEstaAlimentada { get; }

    #endregion Configuração Geral

    #region Log Genérico
    /// <summary>
    /// Classe original não informada para log
    /// </summary>
    public string ClasseOriginalNaoInformadaParaLog { get; }
    /// <summary>
    /// Existem campos que não podem ser exibidos no log de alteração!
    /// </summary>
    public string ExistemCamposQueNaoPodemSerExibidosNoLogDeAlteracao { get; }

    #endregion Log Genérico

    #region Regra de Sistema
    /// <summary>
    /// Regra de sistema não localizada no banco de dados
    /// </summary>
    public string RegraDeSistemaNaoLocalizadaNoBancoDeDados { get; }
    /// <summary>
    /// Não é permitido cadastrar a regra de sistema [{regraSistemaDescricao}] para este usuário, pois ele já tem esta regra cadastrada
    /// </summary>
    /// <param name="regraSistemaDescricao"></param>
    /// <returns></returns>
    public string NaoEPermitidoCadastrarARegraDeSistemaRegraDescricaoParaEsteUsuarioPoisEleJaTemARegraCadastrada(string regrasistemaDescricao);
    /// <summary>
    /// Não é permitido cadastrar a regra de sistema [{regraSistemaDescricao}] para este perfil/tipo usuário, pois ele já tem esta regra cadastrada
    /// </summary>
    /// <param name="regraSistemaDescricao"></param>
    /// <returns></returns>
    public string NaoEPermitidoCadastrarARegraDeSistemaRegraSistemaDescricaoParaEstePerfilTipoUsuatioEspecificoPoisEleJaTemEstaRegraCadastrada(string regrasistemaDescricao);
    /// <summary>
    /// A regra de sistema que você está tentando remover [{regraSistemaDescricao}] nunca existiu para o usuário ou já foi removida anteriormente
    /// </summary>
    /// <param name="regraSistemaDescricao"></param>
    /// <returns></returns>
    public string ARegraQueVoceEstaTentandoRemoverRegraSistemaDescricaoNuncaExistiuParaOUsuarioOuJaFoiRemovidaAnteriormente(string regraSistemaDescricao);

    #endregion Regra de Sistema

    #region Tipo Usuário Checker
    /// <summary>
    /// Tipo de usuário não localiado no banco de dados
    /// </summary>
    public string TipoUsuarioNaoLocalizadoNoBancoDeDados { get; }
    /// <summary>
    /// Não é permitido alterar este tipo de usuário
    /// </summary>
    public string NaoEPermitidoAlterarEsteTipoDeUsuario { get; }

    #endregion Tipo Usuário Checker

    #region Tipo Usuário
    /// <summary>
    /// Já existe um tipo de usuário cadastrado com esta descricao
    /// </summary>
    public string JaExisteUmTipoDeUsuarioCadastradoComEstaDescricao { get; }

    #endregion Tipo Usuário

    #region Usuário Checker
    /// <summary>
    /// Usuário não localizado no banco de dados
    /// </summary>
    public string UsuarioNaoLocalizadoNoBancoDeDados { get; }
    /// <summary>
    /// O e-mail informado é inválido
    /// </summary>
    public string OEmailInformadoEInvalido { get; }
    /// <summary>
    /// Não é permitido alterar este usuário
    /// </summary>
    public string OUsuarioCodigoNomeNaoEstaAtivo(int codigo, string nome);
    /// <summary>
    /// Não é permitido criar um usuário do tipo {tipoUsuario}
    /// </summary>
    /// <param name="tipoUsuario"></param>
    /// <returns></returns>
    public string NaoEPermitidoAlterarEsteUsuario { get; }

    #endregion Usuário Checker

    #region Usuário
    /// <summary>
    /// O usuário {codigo} - {nome} não está ativo
    /// </summary>
    /// <param name="codigo"></param>
    /// <param name="nome"></param>
    /// <returns></returns>
    public string NaoEPermitidoCriarUmUsuarioDoTipo(string tipoUsuario);
    /// <summary>
    /// Já existe um usuário com o e-mail informado
    /// </summary>
    public string JaExisteUmUsuarioComOEmailInformado { get; }
    /// <summary>
    /// Não é permitido alterar este usuário pois, ele pertence ao grupo dos usuários que não tem suporte a edição de cadastro
    /// </summary>
    public string NaoEPermitidoAlterarEsteUsuarioPisElePertenceAoGrupoDosUsuariosQueNaoTemSuporteAEdicaoDeCadastro { get; }
    /// <summary>
    /// Não é permitido definir usuários com o tipo Master
    /// </summary>
    public string NaoEPermitidoDefinirUsuariosComOTipoMaster { get; }
    /// <summary>
    /// Não é permitido alterar usuários do tipo Master"
    /// </summary>
    public string NaoEPermitidoAlterarUsuariosDoTipoMaster { get; }
    /// <summary>
    /// Não é permitido continuar, login inválido
    /// </summary>
    public string NaoEPermitidoContinuarLoginInvalido { get; }
    /// <summary>
    /// Não é permitido alterar a senha do usuário master
    /// </summary>
    public string NaoEPermitidoALterarASenhaDoUsuarioMaster { get; }
    /// <summary>
    /// A senha antiga é inválida
    /// </summary>
    public string ASenhaAntigaEInvalida { get; }
    /// <summary>
    /// Não é permitido afastar a si mesmo no sistema
    /// </summary>
    public string NaoEPermitidoAfastarASiMesmoNoSistema { get; }
    /// <summary>
    /// Não é permitido readmitir um usuário que já está ativo no sistema
    /// </summary>
    public string NaoEPermitidoReadmitirUmUsuarioQueJaEstaAtivoNoSistema { get; }

    #endregion Usuário

}