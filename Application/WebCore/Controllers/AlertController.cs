using Microsoft.AspNetCore.Mvc;

namespace WebCore.Controllers;

/// <inheritdoc />
public class AlertController : Controller
{
    /// <summary>
    /// Partial para dialogs
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public IActionResult AlertDialogOkView()
    {
        return PartialView("~/Views/Partial/AlertDialogOKView.cshtml");
    }

    /// <summary>
    /// Partial para dialogs
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public IActionResult AlertDialogErroRedirectView()
    {
        return PartialView("~/Views/Partial/AlertDialogErroRedirectView.cshtml");
    }

    /// <summary>
    /// Partial para dialogs
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public IActionResult AlertDialogErroView()
    {
        return PartialView("~/Views/Partial/AlertDialogErroView.cshtml");
    }

    /// <summary>
    /// Partial para dialogs
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public IActionResult AlertDialogOkRedirectView()
    {
        return PartialView("~/Views/Partial/AlertDialogOKRedirectView.cshtml");
    }

    /// <summary>
    /// Partial para dialogs
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public IActionResult AlertDialogSending()
    {
        return PartialView("~/Views/Partial/AlertDialogSending.cshtml");
    }

    /// <summary>
    /// Partial para dialogs
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public IActionResult AlertDialogYesNoView()
    {
        return PartialView("~/Views/Partial/AlertDialogYesNoView.cshtml");
    }

    /// <summary>
    /// Partial para dialogs
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public IActionResult DialogVisualizadorDeImagensView()
    {
        return PartialView("~/Views/Partial/DialogVisualizadorDeImagensView.cshtml");
    }

}