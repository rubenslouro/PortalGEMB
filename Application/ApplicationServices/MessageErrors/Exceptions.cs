using Domain.Interfaces.Exception;

namespace ApplicationServices.MessageErrors;

/// <inheritdoc/>
public class Exceptions : IExceptions
{
    /// <inheritdoc/>
    public string ErroDesconhecido => "Erro desconhecido";

    #region Configuração Geral
    /// <inheritdoc/>
    public string ConfiguracaoGeralNaoEncontradaNoBancoDeDados => "Configuração geral não encontrada no banco de dados";

    /// <inheritdoc/>
    public string NaoEPermitidoCriarOutroBancoDeDados => "Não é permitido criar outro banco de dados";

    /// <inheritdoc/>
    public string ATabelaConfiguiracaoGeralJaEstaAlimentada => "A tabela configuração geral já está alimentada";

    #endregion Configuração Geral

    #region Log Genérico
    /// <inheritdoc/>
    public string ClasseOriginalNaoInformadaParaLog => "Classe original não informada para log";

    /// <inheritdoc/>
    public string ExistemCamposQueNaoPodemSerExibidosNoLogDeAlteracao => "Existem campos que não podem ser exibidos no log de alteração!";

    #endregion Log Genérico

    #region Regra de Sistema
    /// <inheritdoc/>
    public string RegraDeSistemaNaoLocalizadaNoBancoDeDados => "Regra de sistema não localizada no banco de dados";

    /// <inheritdoc/>
    public string NaoEPermitidoCadastrarARegraDeSistemaRegraDescricaoParaEsteUsuarioPoisEleJaTemARegraCadastrada(string regraSistemaDescricao)
    {
        return $"Não é permitido cadastrar a regra de sistema [{regraSistemaDescricao}] para este usuário, pois ele já tem esta regra cadastrada";
    }
    /// <inheritdoc/>
    public string NaoEPermitidoCadastrarARegraDeSistemaRegraSistemaDescricaoParaEstePerfilTipoUsuatioEspecificoPoisEleJaTemEstaRegraCadastrada(string regraSistemaDescricao)
    {
        return $"Não é permitido cadastrar a regra de sistema [{regraSistemaDescricao}] para este perfil/tipo usuário, pois ele já tem esta regra cadastrada";
    }
    /// <inheritdoc/>
    public string ARegraQueVoceEstaTentandoRemoverRegraSistemaDescricaoNuncaExistiuParaOUsuarioOuJaFoiRemovidaAnteriormente(string regraSistemaDescricao)
    {
        return $"A regra de sistema que você está tentando remover [{regraSistemaDescricao}] nunca existiu para o usuário ou já foi removida anteriormente.";
    }

    #endregion Regra de Sistema

    #region Tipo Usuário Checker
    /// <inheritdoc/>
    public string TipoUsuarioNaoLocalizadoNoBancoDeDados => "Tipo de usuário não localiado no banco de dados";

    /// <inheritdoc/>
    public string NaoEPermitidoAlterarEsteTipoDeUsuario => "Não é permitido alterar este tipo de usuário";

    #endregion Tipo Usuário Checker

    #region Tipo Usuário
    /// <inheritdoc/>
    public string JaExisteUmTipoDeUsuarioCadastradoComEstaDescricao => "Já existe um tipo de usuário cadastrado com esta descricao";

    #endregion Tipo Usuário

    #region Usuário Checker
    /// <inheritdoc/>
    public string UsuarioNaoLocalizadoNoBancoDeDados => "Usuário não localizado no banco de dados";

    /// <inheritdoc/>
    public string OEmailInformadoEInvalido => "O e-mail informado é inválido";

    /// <inheritdoc/>
    public string NaoEPermitidoAlterarEsteUsuario => "Não é permitido alterar este usuário";

    #endregion Usuário Checker

    #region Usuário
    /// <inheritdoc/>
    public string NaoEPermitidoCriarUmUsuarioDoTipo(string tipoUsuario)
    {
        return $"Não é permitido criar um usuário do tipo {tipoUsuario}";
    }
    /// <inheritdoc/>
    public string OUsuarioCodigoNomeNaoEstaAtivo(int codigo, string nome)
    {
        return $"O usuário {codigo} - {nome} não está ativo";
    }
    /// <inheritdoc/>
    public string JaExisteUmUsuarioComOEmailInformado => "Já existe um usuário com o e-mail informado";

    /// <inheritdoc/>
    public string NaoEPermitidoAlterarEsteUsuarioPisElePertenceAoGrupoDosUsuariosQueNaoTemSuporteAEdicaoDeCadastro => "Não é permitido alterar este usuário pois, ele pertence ao grupo dos usuários que não tem suporte a edição de cadastro";

    /// <inheritdoc/>
    public string NaoEPermitidoDefinirUsuariosComOTipoMaster => "Não é permitido definir usuários com o tipo Master";

    /// <inheritdoc/>
    public string NaoEPermitidoAlterarUsuariosDoTipoMaster => "Não é permitido alterar usuários do tipo Master";

    /// <inheritdoc/>
    public string NaoEPermitidoContinuarLoginInvalido => "Não é permitido continuar, login inválido";

    /// <inheritdoc/>
    public string NaoEPermitidoALterarASenhaDoUsuarioMaster => "Não é permitido alterar a senha do usuário master";

    /// <inheritdoc/>
    public string ASenhaAntigaEInvalida => "A senha antiga é inválida";

    /// <inheritdoc/>
    public string NaoEPermitidoAfastarASiMesmoNoSistema => "Não é permitido afastar a si mesmo no sistema";

    /// <inheritdoc/>
    public string NaoEPermitidoReadmitirUmUsuarioQueJaEstaAtivoNoSistema => "Não é permitido readmitir um usuário que já está ativo no sistema";

    #endregion Usuário

}