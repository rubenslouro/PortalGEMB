using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Presenca
{
    /// <summary>
    /// ID
    /// </summary>
    [Display(Name = "Código")]
    public int Pres_ID_Presenca { get; set; }

    /// <summary>
    /// Código de Identificação do Assistido
    /// </summary>
    [Display(Name = "Código do Assistido")]
    public int Pres_ID_Assistido { get; set; }

    /// <summary>
    /// Código de Identificação da Turma
    /// </summary>
    [Display(Name = "Código da Turma")]
    public int Pres_ID_Turma { get; set; }

    /// <summary>
    /// Data de aula
    /// </summary>
    [Display(Name = "Data da Aula")]
    public DateTime Pres_DT_Aula { get; set; }

    /// <summary>
    /// Define a presença do aluno
    /// </summary>
    [Display(Name = "Presença")]
    public bool Pres_CD_Presenca { get; set; }

    /// <summary>
    /// Usuário que efetuou o cadastro
    /// </summary>
    [Display(Name = "Usuário de Cadastro")]
    public int Pres_ID_UsuarioCadastro { get; set; }

    /// <summary>
    /// Definição de relacionamento com outras tabelas
    /// </summary>

    public virtual Assistido Pres_ID_AssistidoNavigation { get; set; }

    public virtual Turma Pres_ID_TurmaNavigation { get; set; }

    public virtual Usuario Turm_ID_UsuarioCadastroNavigation { get; set; }
}

public class PresencaAluno
{
    /// <summary>
    /// Usuário logado no sistema
    /// </summary>
    [Required(ErrorMessage = "O código do usuário desta ação é obrigatório.")]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "O código do usuário desta ação é obrigatório.")]
    public int IDUsuarioCadastro { get; set; }

    /// <summary>
    /// ID da turma selecionado
    /// </summary>
    [Required(ErrorMessage = "O código da turma é obrigatório.")]
    [Display(Name = "Selecione a Turma")]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "O código da turma é obrigatório.")]
    public int PrAl_ID_Turma { get; set; }

    /// <summary>
    /// Lista de Tipo de Atendimento
    /// </summary>
    [Display(Name = "Lista de Atendimento")]
    public List<Assistido_Presenca> ListaAssistido { get; set; }

}