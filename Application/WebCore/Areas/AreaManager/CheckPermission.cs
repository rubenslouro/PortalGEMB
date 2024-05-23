using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using Domain.DomainServicesInterfaces;
using Domain.Dtos.Permissao.Input;
using Domain.Enums;
using UtilService.Util;
using WebCore.Services.LoginAuthentication;

namespace WebCore.Areas.AreaManager;

/// <inheritdoc />
[Area("Administrativo")]
public class CheckNivelPermissao : ActionFilterAttribute 
{
    private readonly int? _codRegraSistema;
        
    /// <inheritdoc />
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
        try
        {
            var permissaoCore = (IPermissaoService) context.HttpContext.RequestServices.GetService(typeof(IPermissaoService));
            var loginAuthenticationService = (ILoginAuthenticationService)context.HttpContext.RequestServices.GetService(typeof(ILoginAuthenticationService));
            if (loginAuthenticationService == null || !loginAuthenticationService.LogadoAsync().Result)
            {
                throw new Exception();
            }
                
            if (_codRegraSistema != null)
            {
                permissaoCore?.CriticaNivelAcessoAsync(new PermissaoCriticaNivelAcessoInModel
                {
                    CodUsuario = loginAuthenticationService.CodUsuarioLogadoAsync().Result,
                    CodRegraSistema = _codRegraSistema.Value
                }).GetAwaiter().GetResult();
            }
        }
        catch (Exception)
        {
            context.Result = new RedirectToRouteResult(
                new RouteValueDictionary(new
                {
                    controller = "Principal",
                    action = "LoginAdm",
                    area = string.Empty
                }));
        }
    }

    /// <inheritdoc />
    public CheckNivelPermissao(TipoRegraSistema tipoRegraSistema)
    {
        _codRegraSistema = tipoRegraSistema.GetIntValue();
    }

    /// <inheritdoc />
    public CheckNivelPermissao()
    {
            
    }

}