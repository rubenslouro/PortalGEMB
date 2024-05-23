using Domain.Entities;
using Domain.Interfaces.Repository.AdvancedRepository;

namespace InfraDatabase.Repository.AdvancedRepository;

public class AtendimentoRepository : Repository<Atendimento>, IAtendimentoRepository
{

    /// <inheritdoc />
    public AtendimentoRepository(CalangoContext calangoContex) : base(calangoContex)
    {
    }

    /// <inheritdoc />
    public async Task<Atendimento?> RetornaPorCodigoAsync(int codigo)
    {
        return await FirstAsync(o => o.Aten_ID_Atendimento == codigo);
    }
}
