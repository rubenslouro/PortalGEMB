using Domain.DomainServicesInterfaces;
using Domain.Dtos.Permissao.Input;
using Domain.Dtos.Usuario.Retorna.Input;
using Domain.Interfaces.Repository;
using UtilService.Util;

namespace ApplicationServices;

/// <inheritdoc />
public class PermissaoService : IPermissaoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsuarioCheckerService _usuarioCheckerService;

    /// <summary>
    /// Construtor
    /// </summary>
    public PermissaoService(
        IUnitOfWork unitOfWork,
        IUsuarioCheckerService usuarioCheckerService)
    {
        _unitOfWork = unitOfWork;
        _usuarioCheckerService = usuarioCheckerService;
    }

    /// <inheritdoc />
    public async Task CriticaNivelAcessoAsync(PermissaoCriticaNivelAcessoInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();
        await _usuarioCheckerService.CriticarUsuarioInativoAsync(model.CodUsuario);
        var usuario = await _usuarioCheckerService.RetornarAsync(new UsuarioRetornarInModel { CodUsuario = model.CodUsuario });
        if (EnumOperations.ToIntArray<TipoUsuarioService.TipoUsuarioEdicaoBloqueada>().Contains(usuario.CodTipoUsuario))
            return;

        if (await _unitOfWork.UsuarioRegraSistemaRepository.FirstAsync(o => o.CodUsuario == model.CodUsuario && o.CodRegraSistema == model.CodRegraSistema) == null)
        {
            var regraSistema = await GetRegraSistemaAsync(model.CodRegraSistema);
            throw new Exception("Você não tem permissão para executar esta ação. Para que você possa continuar é necessário" +
                                $" que você tenha a permissão [{regraSistema.Item1}] " +
                                $"que tem a seguinte funcionalidade: {regraSistema.Item2}.");
        }

    }

    /// <inheritdoc />
    public async Task<bool> AvaliarNivelAsync(PermissaoAvaliarNivelInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();
        await _usuarioCheckerService.CriticarUsuarioInativoAsync(model.CodUsuario);
        var usuario = await _usuarioCheckerService.RetornarAsync(new UsuarioRetornarInModel { CodUsuario = model.CodUsuario });
        if (EnumOperations.ToIntArray<TipoUsuarioService.TipoUsuarioEdicaoBloqueada>().Contains(usuario.CodTipoUsuario))
            return true;
        return await _unitOfWork.UsuarioRegraSistemaRepository.FirstAsync(o => o.CodUsuario == model.CodUsuario && o.CodRegraSistema == model.CodRegraSistema) != null;
    }

    #region Métodos privados
    /// <summary>
    /// Retorna dados bádicos de uma regra de sistema específica. Item1 será a descrição da regra de sistema e o Item2 será o detalhamento da regra de sistema. 
    /// </summary>
    /// <param name="codRegraSistema"></param>
    /// <returns></returns>
    private async Task<(string, string)> GetRegraSistemaAsync(int codRegraSistema)
    {
        var result = await _unitOfWork.RegraSistemaRepository.FirstAsync(r => r.Codigo == codRegraSistema);
        (string, string) objectRegrasistema = (result?.RegraSistemaDescricao, result?.Detalhamento)!;
        return objectRegrasistema;
    }

    #endregion Métodos privados
}