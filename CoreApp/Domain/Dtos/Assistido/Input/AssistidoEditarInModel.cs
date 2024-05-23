using Domain.Dtos.Assistido.Input;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Assistido.Input;

public class AssistidoEditarInModel : AssistidoAdicionarInModel
{
    public AssistidoEditarInModel()
    {
    }

    public AssistidoEditarInModel(Entities.Assistido cadastro)
    {
        Assi_ID_Assistido = cadastro.Assi_ID_Assistido;
        Assi_MM_Imagem = cadastro.Assi_MM_Imagem;
        Assi_NM_Nome = cadastro.Assi_NM_Nome;
        Assi_CD_Sexo = cadastro.Assi_CD_Sexo;
        Assi_DT_Nascimento = cadastro.Assi_DT_Nascimento.ToString();
        Assi_NR_Idade = cadastro.Assi_NR_Idade.ToString();
        Assi_NR_RG = cadastro.Assi_NR_RG;
        Assi_NR_CPF = cadastro.Assi_NR_CPF;
        Assi_NR_Telefone = cadastro.Assi_NR_Telefone;
        Assi_NM_Mae = cadastro.Assi_NM_Mae;
        Assi_NM_Endereco = cadastro.Assi_NM_Endereco;

        Assi_NM_Profissao = cadastro.Assi_NM_Profissao;
        Assi_ID_AtividadeRemunerada = cadastro.Assi_ID_AtividadeRemunerada.ToString();
        Assi_CD_DeficienteFisico = cadastro.Assi_CD_DeficienteFisico;
        Assi_CD_DeficienteMental = cadastro.Assi_CD_DeficienteMental;
        Assi_ID_Dependente = cadastro.Assi_ID_Dependente.ToString();
        Assi_ID_Escolaridade = cadastro.Assi_ID_Escolaridade.ToString();
        Assi_ID_EstadoCivil = cadastro.Assi_ID_EstadoCivil.ToString();
        Assi_CD_ImpossibilidadeTrabalho = cadastro.Assi_CD_ImpossibilidadeTrabalho;
        Assi_ID_Moradia = cadastro.Assi_ID_Moradia.ToString();
        Assi_CD_Score = cadastro.Assi_CD_Score;
        Assi_NR_Score = cadastro.Assi_NR_Score.ToString();

        Assi_TX_Observacao = cadastro.Assi_TX_Observacao;
    }

    [Required(ErrorMessage = "O código do assistido a ser alterada é obrigatório.")]
    public int Assi_ID_Assistido { get; set; }
}