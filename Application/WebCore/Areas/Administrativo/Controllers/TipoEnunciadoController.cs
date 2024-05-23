using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using WebCore.Areas.AreaManager;
using WebCore.Services.PopUp;
using Domain.DomainServicesInterfaces;
using WebCore.Services.LoginAuthentication;

namespace WebCore.Areas.Administrativo.Controllers;

/// <inheritdoc />
[CheckNivelPermissao]
[Area("Administrativo")]
public class TipoEnunciadoController : Controller
{
    private readonly ILoginAuthenticationService _loginService;
    private readonly ITipoAtendimentoService _tipoatendimentoService;
    private readonly ITipoAtividadeRemuneradaService _tipoatividaderemuneradaService;
    private readonly ITipoDependenteService _tipodependenteService;
    private readonly ITipoEscolaridadeService _tipoescolaridadeService;
    private readonly ITipoEstadoCivilService _tipoestadocivilService;
    private readonly ITipoMoradiaService _tipomoradiaService;

    /// <inheritdoc />
    public TipoEnunciadoController(
        ILoginAuthenticationService loginService,
        ITipoAtendimentoService tipoatendimentoService,
        ITipoAtividadeRemuneradaService tipoatividaderemuneradaService,
        ITipoDependenteService tipodependenteService,
        ITipoEscolaridadeService tipoescolaridadeService,
        ITipoEstadoCivilService tipoestadocivilService,
        ITipoMoradiaService tipomoradiaService)
    {
        _loginService = loginService;
        _tipoatendimentoService = tipoatendimentoService;
        _tipoatividaderemuneradaService = tipoatividaderemuneradaService;
        _tipodependenteService = tipodependenteService;
        _tipoescolaridadeService = tipoescolaridadeService;
        _tipoestadocivilService = tipoestadocivilService;
        _tipomoradiaService = tipomoradiaService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            TipoEnunciado listaEnunciado = new TipoEnunciado();

            var retatendimento = await _tipoatendimentoService.ListarAsync();
            listaEnunciado.TipoAtendimento = retatendimento;

            var retatividaderemunerada = await _tipoatividaderemuneradaService.ListarAsync();
            listaEnunciado.TipoAtividadeRemunerada = retatividaderemunerada;

            var retdependente = await _tipodependenteService.ListarAsync();
            listaEnunciado.TipoDependente = retdependente;

            var retescolaridade = await _tipoescolaridadeService.ListarAsync();
            listaEnunciado.TipoEscolaridade = retescolaridade;

            var retestadocivil = await _tipoestadocivilService.ListarAsync();
            listaEnunciado.TipoEstadoCivil = retestadocivil;

            var retmoradia = await _tipomoradiaService.ListarAsync();
            listaEnunciado.TipoMoradia = retmoradia;

            return View(listaEnunciado);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("Index", "Principal", new { Area = "Administrativo" }) ?? string.Empty);
        }

        return View(new List<Atendimento>());
    }
}

