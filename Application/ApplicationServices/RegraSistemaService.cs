using Domain.DomainServicesInterfaces;
using Domain.Dtos.RegraSistema.AdicionarRegra.Input;
using Domain.Dtos.RegraSistema.AdicionarRegraPerfil.Input;
using Domain.Dtos.RegraSistema.AdicionarRegraPerfil.Output;
using Domain.Dtos.RegraSistema.AdicionarTodas.Input;
using Domain.Dtos.RegraSistema.AdicionarTodasPerfil.Input;
using Domain.Dtos.RegraSistema.AdicionarTodasPerfil.Output;
using Domain.Dtos.RegraSistema.ListaRegraAusente.Input;
using Domain.Dtos.RegraSistema.ListaRegraAusente.Output;
using Domain.Dtos.RegraSistema.ListarRegraExedente.Input;
using Domain.Dtos.RegraSistema.ListarRegraExedente.Output;
using Domain.Dtos.RegraSistema.RedefinirPadraoUsuario.Input;
using Domain.Dtos.RegraSistema.RemoverPermissaoPerfil.Input;
using Domain.Dtos.RegraSistema.RemoverPermissaoPerfil.Output;
using Domain.Dtos.RegraSistema.RemoverRegra.Input;
using Domain.Dtos.RegraSistema.RemoverTodas.Input;
using Domain.Dtos.RegraSistema.RemoverTodasPerfil.Input;
using Domain.Dtos.RegraSistema.RemoverTodasPerfil.Output;
using Domain.Dtos.RegraSistema.Retorna.Input;
using Domain.Dtos.RegraSistema.Retorna.Output;
using Domain.Dtos.RegraSistema.RetornaRegrasNegadasTipoUsuario.Input;
using Domain.Dtos.RegraSistema.RetornaRegrasNegadasTipoUsuario.Output;
using Domain.Dtos.RegraSistema.RetornaRegrasNegadasUsuario.Input;
using Domain.Dtos.RegraSistema.RetornaRegrasNegadasUsuario.Output;
using Domain.Dtos.RegraSistema.RetornaRegrasTipoUsuario.Input;
using Domain.Dtos.RegraSistema.RetornaRegrasTipoUsuario.Output;
using Domain.Dtos.RegraSistema.RetornaRegrasUsuario.Input;
using Domain.Dtos.RegraSistema.RetornaRegrasUsuario.Output;
using Domain.Dtos.RegraSistema.UpdateDiretivasPerfilTipoUsuario.Input;
using Domain.Dtos.RegraSistema.UpdateDiretivasPerfilTipoUsuario.Output;
using Domain.Dtos.RegraSistema.UsuarioRegraCustomizada.Input;
using Domain.Dtos.TipoUsuario.Retorna.Input;
using Domain.Dtos.Usuario.Retorna.Input;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Exception;
using Domain.Interfaces.Repository;
using UtilService.Util;

namespace ApplicationServices;

/// <inheritdoc />
public class RegraSistemaService : IRegraSistemaService
{
    private readonly ITipoUsuarioCheckerService _tipoUsuarioCheckerService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsuarioCheckerService _usuarioCheckerService;
    private readonly IExceptions _exceptions;

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="tipoUsuarioCheckerService"></param>
    /// <param name="unitOfWork"></param>
    /// <param name="usuarioCheckerService"></param>
    /// <param name="exceptions"></param>
    public RegraSistemaService(
        ITipoUsuarioCheckerService tipoUsuarioCheckerService,
        IUnitOfWork unitOfWork,
        IUsuarioCheckerService usuarioCheckerService,
        IExceptions exceptions)
    {
        _tipoUsuarioCheckerService = tipoUsuarioCheckerService;
        _unitOfWork = unitOfWork;
        _usuarioCheckerService = usuarioCheckerService;
        _exceptions = exceptions;
    }

    #region Usuário

    /// <inheritdoc />
    public async Task AdicionarTodasRegrasSistemaUsuarioAsync(RegraSistemaAdicionarTodasRegrasSistemaUsuarioInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();

        await _usuarioCheckerService.CriticaUsuariosEspeciais(model.CodUsuario);
        foreach (var obj in (await RetornaRegrasSistemaUsuarioAsync(new RegraSistemaRetornaRegrasSistemaUsuarioInModel { CodUsuario = model.CodUsuario })).ListaRegras)
        {
            await RemoverRegraSistemaUsuarioAsync(new RegraSistemaRemoverRegraSistemaUsuarioInModel
            {
                CodRegraSistema = obj.Codigo,
                CodUsuarioAltecao = model.CodUsuarioAlteracao,
                CodUsuario = model.CodUsuario
            });
        }

        var listToIngnore = new List<int>
        {
            TipoRegraSistema.CadastroUsuario.GetIntValue(),
            TipoRegraSistema.CadastroTipoUsuario.GetIntValue()
        };

        var regras = await _unitOfWork.RegraSistemaRepository.AllAsync();
        foreach (var obj in regras)
        {
            if (!listToIngnore.Contains(obj.Codigo))
            {
                await RegraSistemaAdicionarUsuarioAsync(new RegraSistemaAdicionarUsuarioInModel
                {
                    CodUsuario = model.CodUsuario,
                    CodUsuarioInclusao = model.CodUsuarioAlteracao,
                    CodRegraSistema = obj.Codigo
                });
            }
        }
    }

    /// <inheritdoc />
    public async Task RegrasSistemaRemoverTodasUsuarioAsync(RegraSistemaRemoverTodasUsuarioInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();
        await _usuarioCheckerService.CriticaUsuariosEspeciais(model.CodUsuario);

        foreach (var obj in (await RetornaRegrasSistemaUsuarioAsync(new RegraSistemaRetornaRegrasSistemaUsuarioInModel { CodUsuario = model.CodUsuario })).ListaRegras)
        {
            await RemoverRegraSistemaUsuarioAsync(new RegraSistemaRemoverRegraSistemaUsuarioInModel
            {
                CodRegraSistema = obj.Codigo,
                CodUsuario = model.CodUsuario,
                CodUsuarioAltecao = model.CodUsuarioAlteracao,
                ValidaUsuario = model.ValidaUsuario
            });
        }
    }
            
    /// <inheritdoc />
    public async Task RegraSistemaAdicionarUsuarioAsync(RegraSistemaAdicionarUsuarioInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();
        await _usuarioCheckerService.CriticarUsuarioNaoExistenteAsync(model.CodUsuario);
        await _usuarioCheckerService.CriticaUsuariosEspeciais(model.CodUsuario);
        var regraSistema = await RetornarAsync(new RegraSistemaRetornarInModel { Codigo = model.CodRegraSistema });

        var regrasSistemaUsuario = await RetornaRegrasSistemaUsuarioAsync(new RegraSistemaRetornaRegrasSistemaUsuarioInModel { CodUsuario = model.CodUsuario });

        if (regrasSistemaUsuario.ListaRegras.Any(o => o.Codigo == regraSistema.Codigo))
        {
            throw new Exception(_exceptions.NaoEPermitidoCadastrarARegraDeSistemaRegraDescricaoParaEsteUsuarioPoisEleJaTemARegraCadastrada(regraSistema.RegraSistemaDescricao));
        }

        var newUsuarioRegraSistema = new UsuarioRegraSistema
        {
            CodRegraSistema = model.CodRegraSistema,
            CodUsuario = model.CodUsuario,
            CodUsuarioInclusao = model.CodUsuarioInclusao,
            DataHoraInclusao = DateTime.UtcNow
        };            

        await _unitOfWork.UsuarioRegraSistemaRepository.AddAsync(newUsuarioRegraSistema);
        await _unitOfWork.SaveAsync();

        var newLogRegra = new LogUsuarioRegraSistema
        {
            CodRegraSistema = model.CodRegraSistema,
            CodUsuario = model.CodUsuario,
            CodUsuarioAcao = model.CodUsuarioInclusao,
            DataHora = newUsuarioRegraSistema.DataHoraInclusao,
            Inclusao = true
        };          
            
        await _unitOfWork.LogUsuarioRegraSistemaRepository.AddAsync(newLogRegra);
        await _unitOfWork.SaveAsync();
    }

    /// <inheritdoc />
    public async Task RemoverRegraSistemaUsuarioAsync(RegraSistemaRemoverRegraSistemaUsuarioInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();

        await _usuarioCheckerService.CriticarUsuarioNaoExistenteAsync(model.CodUsuario);
        await _usuarioCheckerService.CriticaUsuariosEspeciais(model.CodUsuario);
        var regraSistema = await RetornarAsync(new RegraSistemaRetornarInModel { Codigo = model.CodRegraSistema });

        var regrasUsuario = await RetornaRegrasSistemaUsuarioAsync(new RegraSistemaRetornaRegrasSistemaUsuarioInModel { CodUsuario = model.CodUsuario });
        if (!regrasUsuario.ListaRegras.Exists(o => o.Codigo == regraSistema.Codigo))
        {
            throw new Exception(_exceptions.ARegraQueVoceEstaTentandoRemoverRegraSistemaDescricaoNuncaExistiuParaOUsuarioOuJaFoiRemovidaAnteriormente(regraSistema.RegraSistemaDescricao));
        }
        var regraEscolhida = await _unitOfWork.UsuarioRegraSistemaRepository.FirstAsync(o => o.CodUsuario == model.CodUsuario &&
            o.CodRegraSistema == regraSistema.Codigo);
        if (regraEscolhida == null) return;

        _unitOfWork.UsuarioRegraSistemaRepository.Delete(regraEscolhida);

        var newLog = new LogUsuarioRegraSistema
        {
            CodRegraSistema = model.CodRegraSistema,
            CodUsuario = model.CodUsuario,
            CodUsuarioAcao = model.CodUsuarioAltecao,
            DataHora = DateTime.UtcNow,
            Inclusao = false
        };
    

        await _unitOfWork.LogUsuarioRegraSistemaRepository.AddAsync(newLog);
        await _unitOfWork.SaveAsync();
    }

    /// <inheritdoc />
    public async Task RedefinirRegrasSistemaPadraoUsuarioAsync(RegraSistemaRedefinirRegrasSistemaPadraoUsuarioInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();
        var usuarioUpdated = await _usuarioCheckerService.RetornaAtivoAsync(new UsuarioRetornarInModel { CodUsuario = model.CodUsuario });

        await RegrasSistemaRemoverTodasUsuarioAsync(new RegraSistemaRemoverTodasUsuarioInModel
        {
            CodUsuario = usuarioUpdated.Codigo,
            CodUsuarioAlteracao = model.CodUsuarioAlteracao
        });

        foreach (var regra in await RetornaRegrasSistemaTipoUsuarioEntidadeAsync(usuarioUpdated.CodTipoUsuario))
        {
            await RegraSistemaAdicionarUsuarioAsync(new RegraSistemaAdicionarUsuarioInModel
            {
                CodRegraSistema = regra.Codigo,
                CodUsuario = usuarioUpdated.Codigo,
                CodUsuarioInclusao = model.CodUsuarioAlteracao,
                ValidaUsuario = false
            });
        }
    }

    /// <inheritdoc />
    public async Task<RegrasSistemaNegadasUsuarioOutModel> RegrasSistemaNegadasUsuarioAsync(RegrasSistemaNegadasUsuarioInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();
        await _usuarioCheckerService.CriticarUsuarioNaoExistenteAsync(model.CodUsuario);
        var dadosIgnorados = (await RetornaRegrasSistemaUsuarioAsync(new RegraSistemaRetornaRegrasSistemaUsuarioInModel
        {
            CodUsuario = model.CodUsuario
        })).ListaRegras.Select(o => o.Codigo).ToList();

        var dados = await _unitOfWork.RegraSistemaRepository.FindAsync(o => !dadosIgnorados.Contains(o.Codigo));

        var ret = new RegrasSistemaNegadasUsuarioOutModel
        {
            ListaRegras = dados.Select(o => new RegraSistemaRetornaOutModel(o)).ToList()
        };

        return ret;
    }

    /// <inheritdoc />
    public async Task<List<RegraSistemaRetornaOutModel>> ListaTodasRegrasSistemaAsync()
    {
        return (await _unitOfWork.RegraSistemaRepository.AllAsync()).Select(o => new RegraSistemaRetornaOutModel(o)).ToList();
    }

    /// <inheritdoc />
    public async Task<RegraSistemaRetornaRegrasSistemaUsuarioOutModel> RetornaRegrasSistemaUsuarioAsync(RegraSistemaRetornaRegrasSistemaUsuarioInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();
        await _usuarioCheckerService.CriticarUsuarioNaoExistenteAsync(model.CodUsuario);

        var dados = (await _unitOfWork.UsuarioRegraSistemaRepository.FindAsync(o => o.CodUsuario == model.CodUsuario)).Select(o => o.CodRegraSistemaNavigation).ToList();

        var ret = new RegraSistemaRetornaRegrasSistemaUsuarioOutModel
        {
            ListaRegras = dados.Select(o => new RegraSistemaRetornaOutModel(o)).ToList()
        };

        return ret;
    }

    /// <inheritdoc />
    public async Task<RegraSistemaRegrasSistemaExcedenteOutModel> RegrasSistemaExcedenteAsync(RegraSistemaRegrasSistemaExcedenteInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();
        var usuario = await _usuarioCheckerService.RetornarAsync(new UsuarioRetornarInModel { CodUsuario = model.CodUsuario });
        var regrasTipoUsuario = (await RetornaRegrasSistemaTipoUsuarioEntidadeAsync(usuario.CodTipoUsuario)).Select(o => o.Codigo).ToList();
        var regrasUsuario = (await RetornaRegrasSistemaUsuarioAsync(new RegraSistemaRetornaRegrasSistemaUsuarioInModel { CodUsuario = usuario.Codigo })).ListaRegras.Select(o => o.Codigo).ToList();
        var regrasSistemaExcedentes = regrasUsuario.Where(o => !regrasTipoUsuario.Contains(o)).Select(x => x).ToList();
        var dados = await _unitOfWork.RegraSistemaRepository.FindAsync(o => regrasSistemaExcedentes.Contains(o.Codigo));

        var ret = new RegraSistemaRegrasSistemaExcedenteOutModel
        {
            ListaRegras = dados.Select(o => new RegraSistemaRetornaOutModel(o)).ToList()
        };

        return ret;
    }

    /// <inheritdoc />
    public async Task<RegraSistemaListaRegrasSistemaAusentesOutModel> ListaRegrasSistemaAusentesAsync(RegraSistemaListaRegrasSistemaAusentesInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();
        var usuario = await _usuarioCheckerService.RetornarAsync(new UsuarioRetornarInModel
        {
            CodUsuario = model.CodUsuario
        });
        var regrasTipoUsuario = (await RetornaRegrasSistemaTipoUsuarioEntidadeAsync(usuario.CodTipoUsuario)).Select(o => o.Codigo).ToList();
        var regrasUsuario = (await RetornaRegrasSistemaUsuarioAsync(new RegraSistemaRetornaRegrasSistemaUsuarioInModel { CodUsuario = usuario.Codigo })).ListaRegras.Select(o => o.Codigo).ToList();
        var regrasSistemaAusentes = regrasTipoUsuario.Where(o => !regrasUsuario.Contains(o)).Select(x => x).ToList();
        var dados = await _unitOfWork.RegraSistemaRepository.FindAsync(o => regrasSistemaAusentes.Contains(o.Codigo));

        var ret = new RegraSistemaListaRegrasSistemaAusentesOutModel
        {
            ListaRegras = dados.Select(o => new RegraSistemaRetornaOutModel(o)).ToList()
        };

        return ret;
    }

    /// <inheritdoc />
    public async Task<bool> RegrasSistemaCustomizadasUsuario(RegrasSistemaCustomizadasUsuarioInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();
        var usuario = await _usuarioCheckerService.RetornarAsync(new UsuarioRetornarInModel { CodUsuario = model.CodUsuario });

        var regrasTipoUsuario = (await RetornaRegrasSistemaTipoUsuarioEntidadeAsync(usuario.CodTipoUsuario)).Select(o => o.Codigo).ToList();
        var regrasUsuario = (await RetornaRegrasSistemaUsuarioAsync(new RegraSistemaRetornaRegrasSistemaUsuarioInModel { CodUsuario = usuario.Codigo }))
            .ListaRegras.Select(o => o.Codigo).ToList();

        if (usuario.DataAfastamento.HasValue)
        {
            return false;
        }

        if (regrasUsuario.Count != regrasTipoUsuario.Count)
        {
            return true;
        }

        if (regrasUsuario.Any(o => !regrasTipoUsuario.Contains(o)))
        {
            return true;
        }

        if (regrasTipoUsuario.Any(o => !regrasUsuario.Contains(o)))
        {
            return true;
        }

        return false;
    }

    #endregion

    #region Tipo Usuário

    /// <inheritdoc />
    public async Task<RegraSistemaAdicionarTodasRegraSistemaPerfilUsuarioOutModel> AdicionarTodasRegraSistemaPerfilUsuarioAsync(RegraSistemaAdicionarTodasRegraSistemaPerfilUsuarioInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();
        var tipoUsuario = await _tipoUsuarioCheckerService.RetornarAsync(new TipoUsuarioTipoUsuarioRetornarInModel { Codigo = model.CodTipoUsuario });
        await _tipoUsuarioCheckerService.CriticaUsuariosEspeciaisTipoUsuario(model.CodTipoUsuario);

        foreach (var obj in await RetornaRegrasSistemaTipoUsuarioEntidadeAsync(model.CodTipoUsuario))
        {
            await RemoverRegraSistemaTipoUsuarioAsync(
                new RegraSistemaRemoverRegraSistemaTipoUsuarioInModel
                {
                    CodRegraSistema = obj.Codigo,
                    CodUsuarioAlteracao = model.CodUsuarioAlteracao,
                    CodTipoUsuario = tipoUsuario.Codigo,
                    AplicaRegraRetroativa = model.AplicaRegraRetroativa
                });
        }

        var listaNegados = new List<int>
        {
            TipoRegraSistema.CadastroUsuario.GetIntValue(),
            TipoRegraSistema.CadastroTipoUsuario.GetIntValue()
        };

        var regras = await _unitOfWork.RegraSistemaRepository.AllAsync();
        var ret = new RegraSistemaAdicionarTodasRegraSistemaPerfilUsuarioOutModel();
        foreach (var obj in regras)
        {
            if (listaNegados.Contains(obj.Codigo)) continue;

            var regraSistemaAdicionarRegraPerfilOutModel = await AdicionarRegraSistemaPerfilUsuarioAsync(new RegraSistemaAdicionarRegraSistemaPerfilUsuarioInModel
            {
                CodRegraSistema = obj.Codigo,
                CodTipoUsuario = tipoUsuario.Codigo,
                AplicaRegraRetroativa = model.AplicaRegraRetroativa,
                CodUsuarioAlteracao = model.CodUsuarioAlteracao
            });
            ret.ListaCodigosUsuariosAfetados ??= regraSistemaAdicionarRegraPerfilOutModel.ListaCodigosUsuariosAfetados;
        }

        return ret;
    }

    /// <inheritdoc />
    public async Task<RegraSistemaRemoverTodasRegrasSistemaPerfilUsuarioOutModel> RemoverTodasRegrasSistemaPerfilUsuarioAsync(RegraSistemaRemoverTodasRegrasSistemaPerfilUsuarioInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();
        var regras = await RetornaRegrasSistemaTipoUsuarioEntidadeAsync(model.CodTipoUsuario);

        var ret = new RegraSistemaRemoverTodasRegrasSistemaPerfilUsuarioOutModel();

        foreach (var obj in regras)
        {
            var regraSistemaRemoverRegraPerfilOutModel = await RemoverRegraSistemaTipoUsuarioAsync(new RegraSistemaRemoverRegraSistemaTipoUsuarioInModel
            {
                CodRegraSistema = obj.Codigo,
                CodUsuarioAlteracao = model.CodUsuarioAlteracao,
                AplicaRegraRetroativa = model.AplicaRegraRetroativa,
                CodTipoUsuario = model.CodTipoUsuario
            });

            ret.ListaCodigosUsuariosAfetados ??= regraSistemaRemoverRegraPerfilOutModel.ListaCodigosUsuariosAfetados;
        }

        return ret;
    }

    /// <inheritdoc />
    public async Task<RegraSistemaAdicionarRegraSistemaPerfilUsuarioOutModel> AdicionarRegraSistemaPerfilUsuarioAsync(RegraSistemaAdicionarRegraSistemaPerfilUsuarioInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();
        var regraSistema = await RetornarAsync(new RegraSistemaRetornarInModel { Codigo = model.CodRegraSistema });
        var tipoUsuario = await _tipoUsuarioCheckerService.RetornarAsync(new TipoUsuarioTipoUsuarioRetornarInModel { Codigo = model.CodTipoUsuario });

        await _tipoUsuarioCheckerService.CriticaUsuariosEspeciaisTipoUsuario(model.CodTipoUsuario);

        //verifica se a regra já foi adicionada
        if ((await RetornaRegrasSistemaTipoUsuarioEntidadeAsync(tipoUsuario.Codigo)).Exists(o => o.Codigo == regraSistema.Codigo))
        {
            throw new Exception(_exceptions.NaoEPermitidoCadastrarARegraDeSistemaRegraSistemaDescricaoParaEstePerfilTipoUsuatioEspecificoPoisEleJaTemEstaRegraCadastrada(regraSistema.RegraSistemaDescricao));
        }

        var newTipoUsuarioRegraSistema = new TipoUsuarioRegraSistema
        {
            CodRegraSistema = regraSistema.Codigo,
            CodTipoUsuario = tipoUsuario.Codigo,
            CodUsuarioInclusao = model.CodUsuarioAlteracao
        };

        await _unitOfWork.TipoUsuarioRegraSistemaRepository.AddAsync(newTipoUsuarioRegraSistema);
        await _unitOfWork.SaveAsync();

        var ret = new RegraSistemaAdicionarRegraSistemaPerfilUsuarioOutModel();

        if (!model.AplicaRegraRetroativa) return ret;

        var listaUsuarios = await RedefinirRegrasSistemaPadraoTipoUsuarioAsync(new RegraSistemaRedefinirRegrasSistemaPadraoTipoUsuarioInModel
        {
            CodTipoUsuario = tipoUsuario.Codigo,
            CodUsuarioAlteracao = model.CodUsuarioAlteracao
        });

        ret.ListaCodigosUsuariosAfetados = listaUsuarios.ListaCodigosUsuariosAfetados;

        return ret;
    }

    /// <inheritdoc />
    public async Task<RegraSistemaRemoverRegraSistemaTipoUsuarioOutModel> RemoverRegraSistemaTipoUsuarioAsync(RegraSistemaRemoverRegraSistemaTipoUsuarioInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();

        var regraSistema = await RetornarAsync(new RegraSistemaRetornarInModel { Codigo = model.CodRegraSistema });
        await _tipoUsuarioCheckerService.CriticaUsuariosEspeciaisTipoUsuario(model.CodTipoUsuario);
        var tipoUsuario = await _tipoUsuarioCheckerService.RetornarAsync(new TipoUsuarioTipoUsuarioRetornarInModel
        {
            Codigo = model.CodTipoUsuario
        });

        if (!(await RetornaRegrasSistemaTipoUsuarioEntidadeAsync(model.CodTipoUsuario)).Exists(o => o.Codigo == regraSistema.Codigo))
        {
            throw new Exception($"A regra de sistema que você está tentando remover [{regraSistema.RegraSistemaDescricao}] nunca existiu para o perfil/tipo usuário ou já foi removida anteriormente.");
        }

        var regraASerRemovida = await _unitOfWork.TipoUsuarioRegraSistemaRepository.FirstAsync(o => o.CodTipoUsuario == tipoUsuario.Codigo && o.CodRegraSistema == regraSistema.Codigo);

        if (regraASerRemovida != null)
        {
            _unitOfWork.TipoUsuarioRegraSistemaRepository.Delete(regraASerRemovida);
        }
        await _unitOfWork.SaveAsync();
        var ret = new RegraSistemaRemoverRegraSistemaTipoUsuarioOutModel();
        if (!model.AplicaRegraRetroativa) return ret;

        var regraSistemaUpdateDiretivasPerfilTipoUsuarioInModel = await RedefinirRegrasSistemaPadraoTipoUsuarioAsync(new RegraSistemaRedefinirRegrasSistemaPadraoTipoUsuarioInModel
        {
            CodTipoUsuario = tipoUsuario.Codigo,
            CodUsuarioAlteracao = model.CodUsuarioAlteracao
        });
        ret.ListaCodigosUsuariosAfetados = regraSistemaUpdateDiretivasPerfilTipoUsuarioInModel.ListaCodigosUsuariosAfetados;

        return ret;
    }

    /// <inheritdoc />
    public async Task<RegraSistemaRedefinirRegrasSistemaPadraoTipoUsuarioOutModel> RedefinirRegrasSistemaPadraoTipoUsuarioAsync(RegraSistemaRedefinirRegrasSistemaPadraoTipoUsuarioInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();
        var tipoUsuario = await _tipoUsuarioCheckerService.RetornarAsync(new TipoUsuarioTipoUsuarioRetornarInModel
        {
            Codigo = model.CodTipoUsuario
        });
        await _tipoUsuarioCheckerService.CriticaUsuariosEspeciaisTipoUsuario(model.CodTipoUsuario);

        var usuariosApenasDoTipo = (await _unitOfWork.UsuarioRepository.FindAsync(o => o.CodTipoUsuario == tipoUsuario.Codigo)).Select(o => o.Codigo).ToList();
        var dadosRegras = (await RetornaRegrasSistemaTipoUsuarioEntidadeAsync(model.CodTipoUsuario)).ToList();

        foreach (var codUsuario in usuariosApenasDoTipo)
        {
            await RegrasSistemaRemoverTodasUsuarioAsync(
                new RegraSistemaRemoverTodasUsuarioInModel
                {
                    CodUsuario = codUsuario,
                    CodUsuarioAlteracao = model.CodUsuarioAlteracao,
                    ValidaUsuario = false
                });

            foreach (var regra in dadosRegras)
            {
                await RegraSistemaAdicionarUsuarioAsync(new RegraSistemaAdicionarUsuarioInModel
                {
                    CodUsuario = codUsuario,
                    CodRegraSistema = regra.Codigo,
                    CodUsuarioInclusao = model.CodUsuarioAlteracao,
                    ValidaUsuario = model.ValidarUsuario
                });
            }
        }

        var ret = new RegraSistemaRedefinirRegrasSistemaPadraoTipoUsuarioOutModel
        {
            ListaCodigosUsuariosAfetados = usuariosApenasDoTipo
        };

        return ret;
    }

    /// <inheritdoc />
    public async Task<RegrasSistemaNegadasTipoUsuarioOutModel> RegrasSistemaNegadasTipoUsuarioAsync(RegrasSistemaNegadasTipoUsuarioInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();
        var dadosIgnorados = (await RetornaRegrasSistemaTipoUsuarioEntidadeAsync(model.CodTipoUsuario)).Select(o => o.Codigo).ToList();
        var dados = await _unitOfWork.RegraSistemaRepository.FindAsync(o => !dadosIgnorados.Contains(o.Codigo));

        var ret = new RegrasSistemaNegadasTipoUsuarioOutModel
        {
            ListaRegras = dados.Select(o => new RegraSistemaRetornaOutModel(o)).ToList()
        };

        return ret;
    }

    /// <inheritdoc />
    public async Task<RegraSistemaRetornaRegrasSistemaTipoUsuarioOutModel> RetornaRegrasSistemaTipoUsuarioAsync(RegraSistemaRetornaRegrasSistemaTipoUsuarioInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();
        var tipoUsuario = await _tipoUsuarioCheckerService.RetornarAsync(new TipoUsuarioTipoUsuarioRetornarInModel { Codigo = model.CodTipoUsuario });
        var listaRegrasSelecionadas = await RetornaRegrasSistemaTipoUsuarioEntidadeAsync(tipoUsuario.Codigo);

        var ret = new RegraSistemaRetornaRegrasSistemaTipoUsuarioOutModel
        {
            ListaRegras = listaRegrasSelecionadas.Select(o => new RegraSistemaRetornaOutModel(o)).ToList()
        };

        return ret;
    }

    #endregion

    /// <inheritdoc />
    public async Task<RegraSistemaRetornaOutModel> RetornarAsync(RegraSistemaRetornarInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();
        var dado = await _unitOfWork.RegraSistemaRepository.FirstAsync(o => o.Codigo == model.Codigo);

        if (dado == null)
        {
            throw new Exception(_exceptions.RegraDeSistemaNaoLocalizadaNoBancoDeDados);
        }

        return new RegraSistemaRetornaOutModel(dado);
    }

    /// <inheritdoc />
    public async Task InstalarRegrasSistemaAsync()
    {
        foreach (TipoRegraSistema item in Enum.GetValues(typeof(TipoRegraSistema)))
        {
            if (await _unitOfWork.RegraSistemaRepository.FirstAsync(r => r.Codigo == item.GetIntValue()) == null)
            {
                var novaRegra = new RegraSistema
                {
                    Codigo = (int)item,
                    RegraSistemaDescricao = item.GetDefaultValue(),
                    Detalhamento = item.GetDescription()
                };
                await _unitOfWork.RegraSistemaRepository.AddAsync(novaRegra);
                await _unitOfWork.SaveAsync();
            }
        }
    }

    #region Métodos privados

    private async Task<List<RegraSistema>> RetornaRegrasSistemaTipoUsuarioEntidadeAsync(int codTipoUsuario)
    {
        await _tipoUsuarioCheckerService.CriticarTipoUsuarioNaoExistenteAsync(codTipoUsuario);
        var dadosPesq = await _unitOfWork.TipoUsuarioRegraSistemaRepository.FindAsync(o => o.CodTipoUsuario == codTipoUsuario);
        var dados = dadosPesq.Select(o => o.CodRegraSistemaNavigation).ToList();

        return dados;
    }

    #endregion Métodos privados
   
}