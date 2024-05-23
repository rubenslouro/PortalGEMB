using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.ConfiguracaoGeral.Input;

public class ConfiguracaoGeralConfigurarBancoDadosECriaInModel
{
    public const string ServerMaxLength = "O servidor deve ter até 255 caracteres. Ex: 127.0.0.1 ou db.dominio.com";
    public const string ServerMinLength = "O servidor deve ter no mínimo 6 caracteres. Ex: 127.0.0.1 ou db.dominio.com";
    public const string ServerRequired = "O servidor é obrigatório";
    public const string ServerPortRequired = "A porta do servidor de banco de dados é obrigatória.";
    public const string ServerPortRange = "Informe uma porta válida entre 1 e 65535.";
    public const string UserIdRequired = "O usuário do servidor de banco de dados é obrigatório.";
    public const string UserIdStringLength = "O campo usuário do banco de dados deve ter entre 1 e 70 caracteres.";
    public const string PasswordRequired = "A senha do servidor é obrigatória.";
    public const string PasswordStringLength = "O campo senha do servidor de SMTP deve ter entre 1 e 255 caracteres.";
    public const string DataBaseNameRequired = "O nome do banco de dados é obrigatório.";
    public const string DataBaseNameStringLength = "O campo nome do banco de dados deve ter entre 1 e 70 caracteres.";

    /// <summary>
    /// Servidor de banco de dados podendo ser IP ou nome
    /// </summary>
    [MaxLength(100, ErrorMessage = ServerMaxLength)]
    [MinLength(6, ErrorMessage = ServerMinLength)]
    [Required(ErrorMessage = ServerRequired)]
    [Display(Name = "Servidor")]
    public string Server { get; set; }
    /// <summary>
    /// Porta do servidor de banco de dados
    /// </summary>
    [Required(ErrorMessage = ServerPortRequired)]
    [Display(Name = "Porta do servidor")]
    [Range(1, 65535, ErrorMessage = ServerPortRange)]
    public int ServerPort { get; set; }
    /// <summary>
    /// Usuário do banco de dados
    /// </summary>
    [Required(ErrorMessage = UserIdRequired)]
    [StringLength(70, MinimumLength = 1, ErrorMessage = UserIdStringLength)]
    [Display(Name = "Usuário")]
    public string UserId { get; set; }
    /// <summary>
    /// Senha do servidor de banco de dados
    /// </summary>
    [Required(ErrorMessage = PasswordRequired)]
    [StringLength(255, MinimumLength = 1, ErrorMessage = PasswordStringLength)]
    [Display(Name = "Senha")]
    public string Password { get; set; }
    /// <summary>
    /// Nome do banco de dados
    /// </summary>
    [Required(ErrorMessage = DataBaseNameRequired)]
    [Display(Name = "Nome")]
    [StringLength(70, MinimumLength = 1, ErrorMessage = DataBaseNameStringLength)]
    public string DataBaseName { get; set; }

}