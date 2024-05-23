using Domain.DomainServicesInterfaces;
using Domain.Dtos.LogGenerico.Output;
using Domain.Dtos.LogGenerico.Input;
using Domain.Dtos.Permissao.Input;
using Domain.Dtos.RegraSistema.RedefinirPadraoUsuario.Input;
using Domain.Dtos.RegraSistema.RetornaRegrasNegadasUsuario.Input;
using Domain.Dtos.RegraSistema.RetornaRegrasUsuario.Input;
using Domain.Dtos.TipoUsuario.Retorna.Input;
using Domain.Dtos.Usuario.Afastar.Input;
using Domain.Dtos.Usuario.AlterarSenha.Input;
using Domain.Dtos.Usuario.Criar.Input;
using Domain.Dtos.Usuario.Criar.Output;
using Domain.Dtos.Usuario.Editar.Input;
using Domain.Dtos.Usuario.Editar.Output;
using Domain.Dtos.Usuario.EsqueciSenha.Input;
using Domain.Dtos.Usuario.EsqueciSenha.Output;
using Domain.Dtos.Usuario.Listar.Input;
using Domain.Dtos.Usuario.Listar.Output;
using Domain.Dtos.Usuario.ListarLog.Input;
using Domain.Dtos.Usuario.Readmitir.Input;
using Domain.Dtos.Usuario.Retorna.Input;
using Domain.Dtos.Usuario.Retorna.Output;
using Domain.Dtos.Usuario.RetornaDetalhado.Input;
using Domain.Dtos.Usuario.RetornaDetalhado.Output;
using Domain.Dtos.Usuario.RetornaParaEdicao.Input;
using Domain.Dtos.Usuario.RetornaParaEdicao.Output;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Exception;
using Domain.Interfaces.Repository;
using UtilService.Util;

namespace ApplicationServices;

/// <inheritdoc />
public class UsuarioService : IUsuarioService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPermissaoService _permissaoService;
    private readonly ILogGenericoService _logGenericoService;
    private readonly IRegraSistemaService _regraSistemaService;
    private readonly ITipoUsuarioCheckerService _tipoUsuarioCheckerService;
    private readonly ITipoUsuarioService _tipoUsuarioService;
    private readonly IUsuarioCheckerService _usuarioCheckerService;
    private readonly IConfiguracaoGeralService _configuracaoGeralService;
    private readonly IExceptions _exceptions;
    private readonly ICriptografiaService _criptografiaService;

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="unitOfWork"></param>
    /// <param name="permissaoService"></param>
    /// <param name="logGenericoService"></param>
    /// <param name="regraSistemaService"></param>
    /// <param name="tipoUsuarioCheckerService"></param>
    /// <param name="tipoUsuarioService"></param>
    /// <param name="usuarioCheckerService"></param>
    /// <param name="configuracaoGeralService"></param>
    /// <param name="exceptions"></param>
    /// <param name="criptografiaService"></param>
    public UsuarioService(
        IUnitOfWork unitOfWork,
        IPermissaoService permissaoService,
        ILogGenericoService logGenericoService,
        IRegraSistemaService regraSistemaService,
        ITipoUsuarioCheckerService tipoUsuarioCheckerService,
        ITipoUsuarioService tipoUsuarioService,
        IUsuarioCheckerService usuarioCheckerService,
        IConfiguracaoGeralService configuracaoGeralService,
        IExceptions exceptions,
        ICriptografiaService criptografiaService)
    {
        _unitOfWork = unitOfWork;
        _permissaoService = permissaoService;
        _logGenericoService = logGenericoService;
        _regraSistemaService = regraSistemaService;
        _tipoUsuarioCheckerService = tipoUsuarioCheckerService;
        _tipoUsuarioService = tipoUsuarioService;
        _usuarioCheckerService = usuarioCheckerService;
        _configuracaoGeralService = configuracaoGeralService;
        _exceptions = exceptions;
        _criptografiaService = criptografiaService;
    }

    /// <inheritdoc />
    public async Task<LogGenericoListarLogOutModel> ListarLogAsync(UsuarioListarLogLogInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();

        var dados = await _logGenericoService.ListarAsync(new LogGenericoListarInModel
        {
            Referencia = model.CodUsuario.ToString(),
            Tabela = "Usuario"
        });

        return dados;
    }

    /// <inheritdoc />
    public async Task<UsuarioAdicionarOutModel> AdicionarAsync(UsuarioAdicionarInModel model)
    {
        model.TrimStringProperties();
        model.ValidateModel();

        model.Senha = _criptografiaService.EncryptString(model.Senha);
        model.SenhaConfirmacao = _criptografiaService.EncryptString(model.SenhaConfirmacao);

        model.Nome = model.Nome.ApenasLetrasNumeros();
        model.Email = model.Email.ToTrimLower();

        var tipoUsuatio = await _tipoUsuarioCheckerService.RetornarAsync(new TipoUsuarioTipoUsuarioRetornarInModel { Codigo = model.CodTipoUsuario });

        if (tipoUsuatio.Codigo == TipoUsuarioService.TipoUsuarioEdicaoBloqueada.Master.GetIntValue())
        {
            throw new Exception(_exceptions.NaoEPermitidoDefinirUsuariosComOTipoMaster);
        }

        if (await _unitOfWork.UsuarioRepository.AnyAsync(o => o.Email == model.Email))
        {
            throw new Exception(_exceptions.JaExisteUmUsuarioComOEmailInformado);
        }

        var newUsuario = new Usuario
        {
            CodTipoUsuario = model.CodTipoUsuario,
            CodUsuarioCadastro = model.CodUsuarioCadastro,
            DataAfastamento = null,
            DataCadastro = DateTime.UtcNow,
            Email = model.Email,
            Nome = model.Nome,
            Senha = model.Senha
        };

        await _unitOfWork.UsuarioRepository.AddAsync(newUsuario);
        await _unitOfWork.SaveAsync();

        if (!_tipoUsuarioService.ListaTiposUsuarioEdicaoBloqueada().Contains(tipoUsuatio.Codigo))
            await _regraSistemaService.RedefinirRegrasSistemaPadraoUsuarioAsync(new RegraSistemaRedefinirRegrasSistemaPadraoUsuarioInModel { CodUsuario = newUsuario.Codigo, CodUsuarioAlteracao = model.CodUsuarioCadastro });

        return new UsuarioAdicionarOutModel(newUsuario);
    }

    /// <inheritdoc />
    public async Task<UsuarioEditarOutModel> EditarAsync(UsuarioEditarInModel model)
    {
        model.TrimStringProperties();
        model.ValidateModel();

        model.TrimStringProperties();
        model.CheckIfModelIsValid();

        model.Nome = model.Nome.ApenasLetrasNumeros();
        model.Email = model.Email.ToTrimLower();

        var usuario = await _usuarioCheckerService.RetornaAtivoAsync(new UsuarioRetornarInModel { CodUsuario = model.Codigo });

        var tipoUsuario = await _tipoUsuarioCheckerService.RetornarAsync(new TipoUsuarioTipoUsuarioRetornarInModel { Codigo = model.CodTipoUsuario });

        if (usuario.CodTipoUsuario == TipoUsuarioService.TipoUsuarioEdicaoBloqueada.Master.GetIntValue())
        {
            throw new Exception(_exceptions.NaoEPermitidoAlterarEsteUsuarioPisElePertenceAoGrupoDosUsuariosQueNaoTemSuporteAEdicaoDeCadastro);
        }

        if (tipoUsuario.Codigo == TipoUsuarioService.TipoUsuarioEdicaoBloqueada.Master.GetIntValue())
        {
            throw new Exception(_exceptions.NaoEPermitidoDefinirUsuariosComOTipoMaster);
        }

        if (await _unitOfWork.UsuarioRepository.AnyAsync(u => u.Email == model.Email && u.Codigo != model.Codigo))
        {
            throw new Exception(_exceptions.JaExisteUmUsuarioComOEmailInformado);
        }

        var usuarioObjAntesAlteracao = await _unitOfWork.UsuarioRepository.FirstAsync(u => u.Codigo == model.Codigo, true);
        var usuarioObjAlterado = await _unitOfWork.UsuarioRepository.FirstAsync(u => u.Codigo == model.Codigo);

        if (usuarioObjAntesAlteracao == null || usuarioObjAlterado == null)
            throw new Exception(_exceptions.ErroDesconhecido);

        usuarioObjAlterado.Email = model.Email;
        usuarioObjAlterado.Nome = model.Nome;
        usuarioObjAlterado.CodTipoUsuario = tipoUsuario.Codigo;

        await _unitOfWork.SaveAsync();

        await _logGenericoService.AdicionarAsync(
            usuarioObjAntesAlteracao,
            usuarioObjAlterado,
            usuarioObjAlterado.Codigo.ToString(),
            model.CodUsuarioAcao);

        var usuRet = new UsuarioEditarOutModel(usuarioObjAlterado);

        return usuRet;
    }

    /// <inheritdoc />
    public async Task<UsuarioEsqueciSenhaOutModel> EsqueciSenhaAsync(UsuarioEsqueciSenhaInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();

        model.Email = model.Email.Trim().ToLower();
        var usuario = await _usuarioCheckerService.RetornaAtivoAsync(new UsuarioRetornarInModel { Email = model.Email });

        if (usuario.CodTipoUsuario == TipoUsuarioService.TipoUsuarioEdicaoBloqueada.Master.GetIntValue())
        {
            throw new Exception(_exceptions.UsuarioNaoLocalizadoNoBancoDeDados);
        }

        var configSite = await _configuracaoGeralService.RetornarAsync();

        if (configSite == null)
            throw new Exception("Erro ao consultar configurações gerais do site.");


        var urlSite = configSite.UrlSite;

        var menssagem = "Olá " + usuario.Nome + ".";
        menssagem += "\n\nAcreditamos que você esqueceu sua senha de acesso ao nosso sistema e acionou a opção lembrar senha em seu e-mail. Informamos que os dados de seu cadastro são:";
        menssagem += "\n\nNome: " + usuario.Nome;
        menssagem += "\n\nE-mail: " + usuario.Email;
        menssagem += "\n\nSenha: ";
        menssagem += $"\n\nAcesse nosso site {urlSite} para realizar sua autenticação.";
        menssagem += $"\n\nObrigado.";

        var usuarioEsqueciSenhaOutModel = new UsuarioEsqueciSenhaOutModel
        {
            Assunto = "Esqueci Minha Senha",
            De = urlSite,
            ParaEmail = usuario.Email,
            ParaNome = usuario.Nome,
            Mensagem = menssagem
        };
        return usuarioEsqueciSenhaOutModel;
    }

    /// <inheritdoc />
    public async Task<UsuarioListarOutModel> ListarAsync(UsuarioListarInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();

        _unitOfWork.EnableLazyLoader(true);

        return new UsuarioListarOutModel
        {
            Usuarios = (await _unitOfWork.UsuarioRepository.AllAsync(new[] { "CodUsuarioCadastroNavigation" })).Select(u => new UsuarioItemOutModel
            {
                Codigo = u.Codigo,
                DataAfastamento = u.DataAfastamento,
                DataCadastro = u.DataCadastro,
                Email = u.Email,
                Nome = u.Nome,
                UsuarioCadastro = $"{u.CodUsuarioCadastroNavigation?.Codigo} - {u.CodUsuarioCadastroNavigation?.Nome}"
            }).ToList()
        };
    }

    /// <inheritdoc />
    public async Task<UsuarioRetornaParaEdicaoOutModel> RetornaParaEdicaoAsync(UsuarioRetornaParaEdicaoInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();

        var usuario = await _usuarioCheckerService.RetornaAtivoAsync(new UsuarioRetornarInModel { CodUsuario = model.CodUsuario });
        if (usuario.CodTipoUsuario == TipoUsuarioService.TipoUsuarioEdicaoBloqueada.Master.GetIntValue())
            throw new Exception(_exceptions.NaoEPermitidoDefinirUsuariosComOTipoMaster);

        var usuarioEdicao = new UsuarioRetornaParaEdicaoOutModel(usuario);

        return usuarioEdicao;
    }

    /// <inheritdoc />
    public async Task<UsuarioRetornaDetalhadoOutModel> RetornaDetalhadoAsync(UsuarioRetornaDetalhadoInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();

        var usuario = await _unitOfWork.UsuarioRepository.FirstAsync(u => u.Codigo == model.CodUsuario) ?? throw new Exception(_exceptions.UsuarioNaoLocalizadoNoBancoDeDados);

        UsuarioRetornaOutModel? usuarioAfastamento = null;

        //Levantamento de dados de afastamento
        if (usuario.DataAfastamento.HasValue)
        {
            var dadosLog = (await ListarLogAsync(new UsuarioListarLogLogInModel
            {
                CodUsuario = model.CodUsuario,
                CodUsuarioSolicitacaoLog = model.CodUsuarioSolicitacao
            })).ListaLogGenerico;

            var dadoLog = dadosLog.FirstOrDefault(o => o.Campo == "DataAfastamento" &&
                                                       o.ValorAnterior == string.Empty &&
                                                       o.ValorAlterado == usuario.DataAfastamento.Value.ToShortDateString());
            if (dadoLog != null)
            {
                usuarioAfastamento = await _usuarioCheckerService.RetornarAsync(new UsuarioRetornarInModel
                {
                    CodUsuario = dadoLog.CodUsuarioAcao
                });
            }
        }

        var usuarioSolicitacao = await _usuarioCheckerService.RetornarAsync(new UsuarioRetornarInModel
        {
            CodUsuario = model.CodUsuarioSolicitacao
        });

        var permiteLog = await _permissaoService.AvaliarNivelAsync(
            new PermissaoAvaliarNivelInModel
            {
                CodUsuario = model.CodUsuarioSolicitacao,
                CodRegraSistema = TipoRegraSistema.LogUsuario.GetIntValue()
            });

        var log = permiteLog ? await ListarLogAsync(new UsuarioListarLogLogInModel
        {
            CodUsuario = model.CodUsuario,
            CodUsuarioSolicitacaoLog = model.CodUsuarioSolicitacao
        }) : null;

        var regrasAtribuidas =
            (await _regraSistemaService.RetornaRegrasSistemaUsuarioAsync(new RegraSistemaRetornaRegrasSistemaUsuarioInModel
            {
                CodUsuario = usuario.Codigo
            })).ListaRegras;

        var regrasNaoAtribuidas =
            (await _regraSistemaService.RegrasSistemaNegadasUsuarioAsync(
                new RegrasSistemaNegadasUsuarioInModel
                {
                    CodUsuario = usuario.Codigo
                })).ListaRegras;

        var ret = new UsuarioRetornaDetalhadoOutModel(usuario, usuarioAfastamento, permiteLog, log, regrasAtribuidas, regrasNaoAtribuidas, usuarioSolicitacao, _tipoUsuarioService.ListaTiposUsuarioEdicaoBloqueada());

        return ret;
    }

    /// <inheritdoc />
    public async Task<Usuario?> ReturnByEmailAsync(string email)
    {
        if (email.IsNullOrWhiteSpace())
            throw new Exception(_exceptions.OEmailInformadoEInvalido);

        var dado = await _unitOfWork.UsuarioRepository.FirstAsync(u => u.Email == email.ToLower());
        return dado;
    }

    /// <inheritdoc />
    public async Task AlterarSenhaAsync(UsuarioAlterarSenhaInModel model)
    {
        model.CheckIfModelIsValid();
        //Verifica se o usuário existe no banco de dados
        var usu = await _usuarioCheckerService.RetornaAtivoAsync(new UsuarioRetornarInModel { CodUsuario = model.CodUsuario });

        //Bloqueia o cadastro do tipo de usuário master
        if (usu.CodTipoUsuario == TipoUsuarioService.TipoUsuarioEdicaoBloqueada.Master.GetIntValue())
        {
            throw new Exception(_exceptions.NaoEPermitidoALterarASenhaDoUsuarioMaster);
        }

        var usuario = await _unitOfWork.UsuarioRepository.FirstAsync(o => o.Codigo == usu.Codigo);

        if (usuario!.Senha != model.SenhaAntiga)
            throw new Exception(_exceptions.ASenhaAntigaEInvalida);

        usuario.Senha = model.SenhaNova;

        await _unitOfWork.SaveAsync();
    }

    /// <inheritdoc />
    public async Task AfastarAsync(UsuarioAfastarInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();

        var usuarioAfastado = await _usuarioCheckerService.RetornaAtivoAsync(new UsuarioRetornarInModel { CodUsuario = model.CodUsuario });

        if (usuarioAfastado.CodTipoUsuario == TipoUsuarioService.TipoUsuarioEdicaoBloqueada.Master.GetIntValue())
        {
            throw new Exception(_exceptions.NaoEPermitidoAlterarUsuariosDoTipoMaster);
        }

        if (usuarioAfastado.Codigo == model.CodUsuarioResponsavel)
        {
            throw new Exception(_exceptions.NaoEPermitidoAfastarASiMesmoNoSistema);
        }
        var usuarioAntesAlteracao = await _unitOfWork.UsuarioRepository.FirstAsync(o => o.Codigo == usuarioAfastado.Codigo, true);
        var usuarioDepoisAlteracao = await _unitOfWork.UsuarioRepository.FirstAsync(o => o.Codigo == usuarioAfastado.Codigo);

        if (usuarioDepoisAlteracao == null)
            return;

        usuarioDepoisAlteracao.DataAfastamento = DateTime.UtcNow;
        await _unitOfWork.SaveAsync();

        await _logGenericoService.AdicionarAsync(
            usuarioAntesAlteracao!,
            usuarioDepoisAlteracao,
            usuarioDepoisAlteracao.Codigo.ToString(),
            model.CodUsuarioResponsavel);
    }

    /// <inheritdoc />
    public async Task ReativarAsync(UsuarioReativarInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();

        var usuarioASerReadmitido = await RetornaDetalhadoAsync(new UsuarioRetornaDetalhadoInModel { CodUsuario = model.CodUsuario, CodUsuarioSolicitacao = model.CodUsuarioResponsavel });

        if (!usuarioASerReadmitido.DataAfastamento.HasValue)
            throw new Exception(_exceptions.NaoEPermitidoReadmitirUmUsuarioQueJaEstaAtivoNoSistema);

        var usuarioAntesAlteracao = await _unitOfWork.UsuarioRepository.FirstAsync(o => o.Codigo == usuarioASerReadmitido.Codigo, true);
        var usuarioDepoisAlteracao = await _unitOfWork.UsuarioRepository.FirstAsync(o => o.Codigo == usuarioASerReadmitido.Codigo);

        if (usuarioDepoisAlteracao == null)
            return;

        usuarioDepoisAlteracao.DataAfastamento = null;
        await _unitOfWork.SaveAsync();

        await _logGenericoService.AdicionarAsync(
            usuarioAntesAlteracao!,
            usuarioDepoisAlteracao,
            usuarioDepoisAlteracao.Codigo.ToString(),
            model.CodUsuarioResponsavel);
    }

    /// <inheritdoc />
    public async Task<bool> ExisteUsuarioCadastradoAsync()
    {
        return await _unitOfWork.UsuarioRepository.AnyAsync();
    }

    /// <inheritdoc/>
    public async Task CriarUsuarioMasterAsync(Usuario? usuario)
    {
        if (usuario != null)
        {
            await _unitOfWork.UsuarioRepository.AddAsync(usuario);
            await _unitOfWork.SaveAsync();
        }
    }

}