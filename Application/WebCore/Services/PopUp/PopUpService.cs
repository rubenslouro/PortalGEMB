using Microsoft.AspNetCore.Mvc;

namespace WebCore.Services.PopUp;

/// <summary>
/// Classe responsável por popups em aplicações MVC
/// </summary>
public static class PopUpService
{
    /// <summary>
    /// Popup de alerta
    /// </summary>
    /// <param name="ctrl"></param>
    /// <param name="mensagem"></param>
    public static void AlertaMsg(this Controller ctrl, string mensagem)
    {
        ctrl.ViewData["AlertDialogOKText"] = mensagem;
    }

    /// <summary>
    /// Popup utilizado para exibir erros e após click do botão redirecionar a um página
    /// </summary>
    /// <param name="ctrl"></param>
    /// <param name="mensagem"></param>
    /// <param name="urlRedirect"></param>
    public static void AlertaMsgErroRedirect(this Controller ctrl, string mensagem, string urlRedirect)
    {
        ctrl.ViewData["AlertDialogErroRedirectText"] = mensagem;
        ctrl.ViewData["AlertDialogErroRedirectUrl"] = urlRedirect;
    }

    /// <summary>
    /// Popup voltado para exibir mensagens de erro
    /// </summary>
    /// <param name="ctrl"></param>
    /// <param name="mensagem"></param>
    public static void AlertaMsgErro(this Controller ctrl, string mensagem)
    {
        ctrl.ViewData["AlertDialogErroText"] = mensagem;
    }

    /// <summary>
    /// Popup voltado a exibição de mensagens com redirecionamento para páginas
    /// </summary>
    /// <param name="ctrl"></param>
    /// <param name="mensagem"></param>
    /// <param name="urlRedirect"></param>
    public static void AlertaMsgRedirect(this Controller ctrl, string mensagem, string urlRedirect)
    {
        ctrl.ViewData["AlertDialogOKRedirectText"] = mensagem;
        ctrl.ViewData["AlertDialogOKRedirectUrl"] = urlRedirect;
    }

    //public static void AlertaMsgYesNo(this Controller ctrl, string mensagem, string urlRedirectYes, string urlRedirectNo)
    //{
    //    ctrl.ViewData["AlertDialogYesNoText"] = mensagem;
    //    ctrl.ViewData["AlertDialogYesNoLinkYes"] = urlRedirectYes;
    //    ctrl.ViewData["AlertDialogYesNoLinkNo"] = urlRedirectNo;
    //}

}