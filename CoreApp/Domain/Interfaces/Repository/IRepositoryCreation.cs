namespace Domain.Interfaces.Repository;

/// <summary>
/// Classe de criação de e consultar existência de banco de dados
/// </summary>
public interface IRepositoryCreation
{
    /// <summary>
    /// Confirma criação do banco de dados e caso não exita faz aquela criação marota para você
    /// </summary>
    /// <param name="strCon"></param>
    /// <returns></returns>
    Task EnsureCreateAsync(string strCon);

    /// <summary>
    /// Verifica se é possível de conectar ao banco de dados
    /// </summary>
    /// <returns></returns>
    Task<bool> CanConnectAsync();
}