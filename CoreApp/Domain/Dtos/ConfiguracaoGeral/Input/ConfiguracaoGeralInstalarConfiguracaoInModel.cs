using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.ConfiguracaoGeral.Input;

public class ConfiguracaoGeralInstalarConfiguracaoInModel
{
    public const string CodUsuarioAcaoRange = "O código do usuário que está solicitando os dados deve ser informado.";
    public const string UrlSiteRequired = "O endereço do site é obrigatório.";
    public const string UrlSiteStringLength = "O campo endereço do site deve ter entre 8 e 255 caracteres.";
    public const string EmailUsuarioMasterStringLength = "O endereço de e-mail do usuário Master deve ter entre 5 e 70 caracteres.";
    public const string EmailUsuarioMasterRegularExpression = "E-mail do usuário Master em formato inválido.";
    public const string EmailUsuarioMasterRequired = "Informe o endereço de e-mail do usuário Master que será usado para os envios.";
    public const string SenhaMasterRequired = "Senha obrigatória";
    public const string SenhaMasterStringLength = "A senha tem que ter entre 5 e 20 dígitos";
    public const string SenhaConfirmacaoMasterRequired = "A confirmação de senha do usuário Master é necessária";
    public const string SenhaConfirmacaoMasterStringLength = "A senha do usuário Master tem que ter entre 5 e 20 dígitos";
    public const string SenhaConfirmacaoMasterCompare = "A senha do usuário Master e a confirmação de senha devem ser idênticas";

    /// <summary>
    /// Url do domínio onde a aplicação será instalada ex: http://www.google.com ou http://10.10.10.1
    /// </summary>
    [Required(ErrorMessage = UrlSiteRequired)]
    [Display(Name = "Endereço do site (ex: http://www.google.com)")]
    [StringLength(255, MinimumLength = 8, ErrorMessage = UrlSiteStringLength)]
    public string UrlSite { get; set; }
    /// <summary>
    /// Email no qual será utilizado para criação do usuário do tipo master. O email não precisa existir na prática já que os usuários master não podem ter seus dados alterados e só desem ser usados em última instância 
    /// </summary>
    [Display(Name = "E-mail para usuário master")]
    [StringLength(70, MinimumLength = 5, ErrorMessage = EmailUsuarioMasterStringLength)]
    [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = EmailUsuarioMasterRegularExpression)]
    [Required(ErrorMessage = EmailUsuarioMasterRequired)]
    public string EmailUsuarioMaster { get; set; }
    /// <summary>
    /// Senha para ser utilizado no usuário master. Utilize sempre uma senha muito complexa
    /// </summary>
    [Required(ErrorMessage = SenhaMasterRequired)]
    [StringLength(20, ErrorMessage = SenhaMasterStringLength, MinimumLength = 5)]
    [DataType(DataType.Password)]
    [DisplayName("Senha")]
    public string SenhaMaster { get; set; }
    /// <summary>
    /// Confirmação de senha para usuário master
    /// </summary>
    [Required(ErrorMessage = SenhaConfirmacaoMasterRequired)]
    [StringLength(20, ErrorMessage = SenhaConfirmacaoMasterStringLength, MinimumLength = 5)]
    [DataType(DataType.Password)]
    [Compare("SenhaMaster", ErrorMessage = SenhaConfirmacaoMasterCompare)]
    [DisplayName("Confirmação de senha")]
    public string SenhaConfirmacaoMaster { get; set; }
    /// <summary>
    /// Usuário que realiza a alteração nas configurações de sistema. Em caso de inclusão não será necessário informar
    /// </summary>
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioAcaoRange)]
    public int? CodUsuarioAcao { get; set; }


}