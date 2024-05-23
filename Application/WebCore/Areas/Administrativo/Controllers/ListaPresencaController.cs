using Microsoft.AspNetCore.Mvc;
using WebCore.Areas.AreaManager;

namespace WebCore.Areas.Administrativo.Controllers;

/// <inheritdoc />
[CheckNivelPermissao]
[Area("Administrativo")]
public class ListaPresencaController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

