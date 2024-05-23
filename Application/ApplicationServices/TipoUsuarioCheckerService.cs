using Domain.DomainServicesInterfaces;
using Domain.Dtos.TipoUsuario.Retorna.Input;
using Domain.Dtos.TipoUsuario.Retorna.Output;
using Domain.Interfaces.Exception;
using Domain.Interfaces.Repository;
using UtilService.Util;

namespace ApplicationServices;

/// <inheritdoc />
public class TipoUsuarioCheckerService : ITipoUsuarioCheckerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExceptions _exceptions;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="unitOfWork"></param>
    /// <param name="exceptions"></param>
    public TipoUsuarioCheckerService(
        IUnitOfWork unitOfWork,
        IExceptions exceptions)
    {
        _unitOfWork = unitOfWork;
        _exceptions = exceptions;
    }

    /// <inheritdoc />
    public async Task CriticaUsuariosEspeciaisTipoUsuario(int codTipoUsuario)
    {
        var tipoUsuario = await _unitOfWork.TipoUsuarioRepository.FirstAsync(o => o.Codigo == codTipoUsuario);
        if (tipoUsuario == null)
            throw new Exception(_exceptions.TipoUsuarioNaoLocalizadoNoBancoDeDados);

        var tiposUsuariosEspeciais = EnumOperations.ToIntArray<TipoUsuarioService.TipoUsuarioEdicaoBloqueada>();
        if (tiposUsuariosEspeciais.Contains(tipoUsuario.Codigo))
            throw new Exception(_exceptions.NaoEPermitidoAlterarEsteTipoDeUsuario);
    }

    /// <inheritdoc />
    public async Task CriticarTipoUsuarioNaoExistenteAsync(int codTipoUsuario)
    {
        var dado = await _unitOfWork.TipoUsuarioRepository.FirstAsync(o => o.Codigo == codTipoUsuario);
        if (dado == null)
        {
            throw new Exception(_exceptions.TipoUsuarioNaoLocalizadoNoBancoDeDados);
        }
    }

    /// <inheritdoc />
    public async Task<TipoUsuarioRetornaOutModel> RetornarAsync(TipoUsuarioTipoUsuarioRetornarInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();
        var dado = await _unitOfWork.TipoUsuarioRepository.FirstAsync(o => o.Codigo == model.Codigo);
        if (dado == null)
        {
            throw new Exception(_exceptions.TipoUsuarioNaoLocalizadoNoBancoDeDados);
        }

        return new TipoUsuarioRetornaOutModel(dado);
    }

}