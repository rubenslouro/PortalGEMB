using Domain.DomainServicesInterfaces;
using Domain.Dtos.UF.RetornaPorDescricao;
using Domain.Dtos.UF.RetornarInModel;
using Domain.Entities;
using Domain.Interfaces.Repository;

namespace ApplicationServices;

/// <inheritdoc />
public class UfService : IUfService
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Construtor do núcleo de serviço de UF
    /// </summary>
    /// <param name="unitOfWork"></param>
    public UfService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<List<Uf>> ListarAsync()
    {
        return (await _unitOfWork.UfRepository.AllAsync()).ToList();
    }

    /// <inheritdoc />
    public async Task<Uf> RetornarAsync(UFRetornarInModel model)
    {
        var dado = await _unitOfWork.UfRepository.FirstAsync(o => o.Codigo == model.CodUf);

        if (dado == null)
        {
            throw new Exception("UF não localizada.");
        }

        return dado;
    }

    /// <inheritdoc />
    public async Task<Uf> RetornarDadosPorDescricaoAsync(UFRetornaPorDescricaoInModel model)
    {
        var dado = await _unitOfWork.UfRepository.FirstAsync(o => o.Descricao == model.Descricao);

        if (dado == null)
        {
            throw new Exception("UF não localizada.");
        }

        return dado;
    }

    /// <inheritdoc />
    public async Task AdicionarUfAsync()
    {
        if (await _unitOfWork.UfRepository.AnyAsync())
            throw new Exception("As unidades federativas já estão cadastradas no banco de dados");

        var newAc = new Uf { Descricao = "AC" };
        var newAl = new Uf { Descricao = "AL" };
        var newAp = new Uf { Descricao = "AP" };
        var newAm = new Uf { Descricao = "AM" };
        var newBa = new Uf { Descricao = "BA" };
        var newCe = new Uf { Descricao = "CE" };
        var newDf = new Uf { Descricao = "DF" };
        var newEs = new Uf { Descricao = "ES" };
        var newGo = new Uf { Descricao = "GO" };
        var newMa = new Uf { Descricao = "MA" };
        var newMt = new Uf { Descricao = "MT" };
        var newMs = new Uf { Descricao = "MS" };
        var newMg = new Uf { Descricao = "MG" };
        var newPa = new Uf { Descricao = "PA" };
        var newPb = new Uf { Descricao = "PB" };
        var newPr = new Uf { Descricao = "PR" };
        var newPe = new Uf { Descricao = "PE" };
        var newPi = new Uf { Descricao = "PI" };
        var newRj = new Uf { Descricao = "RJ" };
        var newRn = new Uf { Descricao = "RN" };
        var newRs = new Uf { Descricao = "RS" };
        var newRo = new Uf { Descricao = "RO" };
        var newRr = new Uf { Descricao = "RR" };
        var newSc = new Uf { Descricao = "SC" };
        var newSp = new Uf { Descricao = "SP" };
        var newSe = new Uf { Descricao = "SE" };
        var newTo = new Uf { Descricao = "TO" };

        await _unitOfWork.UfRepository.AddAsync(newAc);
        await _unitOfWork.UfRepository.AddAsync(newAl);
        await _unitOfWork.UfRepository.AddAsync(newAp);
        await _unitOfWork.UfRepository.AddAsync(newAm);
        await _unitOfWork.UfRepository.AddAsync(newBa);
        await _unitOfWork.UfRepository.AddAsync(newCe);
        await _unitOfWork.UfRepository.AddAsync(newDf);
        await _unitOfWork.UfRepository.AddAsync(newEs);
        await _unitOfWork.UfRepository.AddAsync(newGo);
        await _unitOfWork.UfRepository.AddAsync(newMa);
        await _unitOfWork.UfRepository.AddAsync(newMt);
        await _unitOfWork.UfRepository.AddAsync(newMs);
        await _unitOfWork.UfRepository.AddAsync(newMg);
        await _unitOfWork.UfRepository.AddAsync(newPa);
        await _unitOfWork.UfRepository.AddAsync(newPb);
        await _unitOfWork.UfRepository.AddAsync(newPr);
        await _unitOfWork.UfRepository.AddAsync(newPe);
        await _unitOfWork.UfRepository.AddAsync(newPi);
        await _unitOfWork.UfRepository.AddAsync(newRj);
        await _unitOfWork.UfRepository.AddAsync(newRn);
        await _unitOfWork.UfRepository.AddAsync(newRs);
        await _unitOfWork.UfRepository.AddAsync(newRo);
        await _unitOfWork.UfRepository.AddAsync(newRr);
        await _unitOfWork.UfRepository.AddAsync(newSc);
        await _unitOfWork.UfRepository.AddAsync(newSp);
        await _unitOfWork.UfRepository.AddAsync(newSe);
        await _unitOfWork.UfRepository.AddAsync(newTo);

        await _unitOfWork.SaveAsync();

    }
}