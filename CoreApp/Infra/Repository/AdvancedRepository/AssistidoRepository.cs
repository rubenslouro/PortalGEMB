using Domain.Entities;
using Domain.Interfaces.Repository.AdvancedRepository;

namespace InfraDatabase.Repository.AdvancedRepository;

public class AssistidoRepository : Repository<Assistido>, IAssistidoRepository
{

    /// <inheritdoc />
    public AssistidoRepository(CalangoContext calangoContex) : base(calangoContex)
    {
    }

    /// <inheritdoc />
    public async Task<bool> ExisteComNome(string nome)
    {
        return await AnyAsync(o => o.Assi_NM_Nome == nome);
    }

    /// <inheritdoc />
    public async Task<bool> ExisteComNome(string nome, int codigo)
    {
        return await AnyAsync(o => o.Assi_NM_Nome == nome && o.Assi_ID_Assistido != codigo);
    }

    /// <inheritdoc />
    public async Task<bool> ExisteComCpf(string cpf)
    {
        return await AnyAsync(o => o.Assi_NR_CPF == cpf);
    }

    /// <inheritdoc />
    public async Task<bool> ExisteComCpf(string cpf, int codigo)
    {
        return await AnyAsync(o => o.Assi_NR_CPF == cpf && o.Assi_ID_Assistido != codigo);
    }

    /// <inheritdoc />
    public async Task<Assistido?> RetornaPorCodigoAsync(int codigo)
    {
        return await FirstAsync(o => o.Assi_ID_Assistido == codigo);
    }
}
