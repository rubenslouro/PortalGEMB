using Domain.Entities;

namespace Domain.Interfaces.Repository.AdvancedRepository;

/// <summary>
/// Repositório de Cadastro de Assistidos
/// </summary>
public interface IAssistidoRepository : IRepository<Assistido>
{
    /// <summary>
    /// Existe Pessoa com o nome informado
    /// </summary>
    /// <param name="nome"></param>
    /// <returns></returns>
    Task<bool> ExisteComNome(string nome);

    /// <summary>
    /// Existe Pessoa com o nome informado que seja diferente do código do técnico informado
    /// </summary>
    /// <param name="nome"></param>
    /// <param name="codPessoa"></param>
    /// <returns></returns>
    Task<bool> ExisteComNome(string nome, int codPessoa);

    /// <summary>
    /// Existe Pessoa com o CPF informado
    /// </summary>
    /// <param name="cpf"></param>
    /// <returns></returns>
    Task<bool> ExisteComCpf(string cpf);

    /// <summary>
    /// Existe Pessoa com o CPF informado que seja diferente do código do técnico informado
    /// </summary>
    /// <param name="cpf"></param>
    /// <param name="codPessoa"></param>
    /// <returns></returns>
    Task<bool> ExisteComCpf(string cpf, int codPessoa);

    /// <summary>
    /// Retorna um Pessoa pelo código
    /// </summary>
    /// <param name="codigo"></param>
    /// <returns></returns>
    Task<Assistido?> RetornaPorCodigoAsync(int codigo);
}
