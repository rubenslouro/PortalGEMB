using Domain.DomainServicesInterfaces;
using Domain.Dtos.Usuario.Retorna.Input;
using Domain.Dtos.Usuario.Retorna.Output;
using Domain.Entities;
using Domain.Interfaces.Exception;
using Domain.Interfaces.Repository;
using UtilService.Util;

namespace ApplicationServices;

/// <inheritdoc />
public class UsuarioCheckerService : IUsuarioCheckerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExceptions _exceptions;

    /// <summary>
    /// Classe de checagem de usuário
    /// </summary>
    /// <param name="unitOfWork"></param>
    /// <param name="exceptions"></param>
    public UsuarioCheckerService(
        IUnitOfWork unitOfWork,
        IExceptions exceptions)
    {
        _unitOfWork = unitOfWork;
        _exceptions = exceptions;
    }

    /// <inheritdoc />
    public async Task CriticarUsuarioInativoAsync(int codUsuario)
    {

        var dado = await _unitOfWork.UsuarioRepository.FirstAsync(u => u.Codigo == codUsuario);

        if (dado == null)
        {
            throw new Exception(_exceptions.UsuarioNaoLocalizadoNoBancoDeDados);
        }

        if (dado.DataAfastamento.HasValue)
            throw new Exception($"O usuário {dado.Codigo} - {dado.Nome} não está ativo.");
    }

    /// <inheritdoc />
    public async Task<UsuarioRetornaOutModel> RetornarAsync(UsuarioRetornarInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();

        Usuario? dado = null;

        if (model.CodUsuario.HasValue)
            dado = await _unitOfWork.UsuarioRepository.FirstAsync(u => u.Codigo == model.CodUsuario.Value);

        if (!model.Email.IsNullOrWhiteSpace())
            dado = await _unitOfWork.UsuarioRepository.FirstAsync(u => u.Email == model.Email);

        if (dado == null)
        {
            throw new Exception(_exceptions.UsuarioNaoLocalizadoNoBancoDeDados);
        }

        return new UsuarioRetornaOutModel(dado);
    }

    /// <inheritdoc />
    public async Task<UsuarioRetornaOutModel> RetornaAtivoAsync(UsuarioRetornarInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();

        Usuario? dado = null;

        if (model.CodUsuario.HasValue)
            dado = await _unitOfWork.UsuarioRepository.FirstAsync(u => u.Codigo == model.CodUsuario.Value);

        if (!model.Email.IsNullOrWhiteSpace())
            dado = await _unitOfWork.UsuarioRepository.FirstAsync(u => u.Email == model.Email);

        if (dado == null)
        {
            throw new Exception(_exceptions.UsuarioNaoLocalizadoNoBancoDeDados);
        }

        if (dado.DataAfastamento.HasValue)
            throw new Exception($"O usuário {dado.Codigo} - {dado.Nome} não está ativo.");

        return new UsuarioRetornaOutModel(dado);
    }

    /// <inheritdoc />
    public async Task CriticarUsuarioNaoExistenteAsync(int codUsuario)
    {
        var dado = await _unitOfWork.UsuarioRepository.FirstAsync(u => u.Codigo == codUsuario);

        if (dado == null)
        {
            throw new Exception(_exceptions.UsuarioNaoLocalizadoNoBancoDeDados);
        }
    }

    /// <inheritdoc />
    public async Task CriticaUsuariosEspeciais(int codUsuario)
    {
        var usuario = await _unitOfWork.UsuarioRepository.FirstAsync(u => u.Codigo == codUsuario);
        if (usuario == null)
            throw new Exception(_exceptions.UsuarioNaoLocalizadoNoBancoDeDados);

        var tiposUsuariosEspeciais = EnumOperations.ToIntArray<TipoUsuarioService.TipoUsuarioEdicaoBloqueada>();
        if (tiposUsuariosEspeciais.Contains(usuario.CodTipoUsuario))
            throw new Exception(_exceptions.NaoEPermitidoAlterarEsteUsuario);
    }
}