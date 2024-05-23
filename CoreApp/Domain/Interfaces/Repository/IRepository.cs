using System.Linq.Expressions;

namespace Domain.Interfaces.Repository;

/// <summary>
/// Repositório de dados
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Retorna o primeiro registro de uma pesquisa sem rastrear alterações
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="asNoTraking"></param>
    /// <returns></returns>
    Task<T?> FirstAsync(Expression<Func<T, bool>> predicate, bool asNoTraking);

    /// <summary>
    /// Retorna o primeiro registro de uma pesquisa
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<T?> FirstAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Retorna o primeiro registro
    /// </summary>
    /// <returns></returns>
    Task<T?> FirstAsync();

    /// <summary>
    /// Retorna o primeiro registro sem rastrear alterações
    /// </summary>
    /// <param name="asNoTraking"></param>
    /// <returns></returns>
    Task<T?> FirstAsync(bool asNoTraking);

    /// <summary>
    /// Retorna dados de uma pesquisa no banco de dados
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Retorna todos os registro de uma entidade
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<T>> AllAsync();

    /// <summary>
    /// Retorna todos os registro de uma entidade com opção de realizar injeção de tabelas dependêntes
    /// </summary>
    /// <param name="includes"></param>
    /// <returns></returns>
    Task<IEnumerable<T>> AllAsync(string[] includes);

    /// <summary>
    /// Retorna todos os registro de uma entidade sem rastreamento de alterações
    /// </summary>
    /// <param name="asNoTracking"></param>
    /// <returns></returns>
    Task<IEnumerable<T>> AllAsync(bool asNoTracking);

    /// <summary>
    /// Adiciona uma entidade nova
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task AddAsync(T entity);

    /// <summary>
    /// Remove uma entidade 
    /// </summary>
    /// <param name="entity"></param>
    void Delete(T entity);

    /// <summary>
    /// Atualiza uma entidade
    /// </summary>
    /// <param name="entity"></param>
    void Update(T entity);

    /// <summary>
    /// Verifica se uma pesquisa retorna registros
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Verifica se uma entidade tem registros
    /// </summary>
    /// <returns></returns>
    Task<bool> AnyAsync();

}