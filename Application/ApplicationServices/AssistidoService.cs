using AutoMapper;
using Domain.DomainServicesInterfaces;
using Domain.Dtos.LogGenerico.Input;
using Domain.Dtos.Assistido.Input;
using Domain.Dtos.Assistido.Output;
using Domain.Entities;
using Domain.Interfaces.Repository;
using UtilService;
using UtilService.Util;
using Domain.Dtos.LogGenerico.Output;

namespace ApplicationServices;

/// <inheritdoc />
public class AssistidoService : IAssistidoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogGenericoService _logGenericoService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="unitOfWork"></param>
    /// <param name="logGenericoService"></param>
    /// <param name="mapper"></param>
    public AssistidoService(
        IUnitOfWork unitOfWork,
        ILogGenericoService logGenericoService,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logGenericoService = logGenericoService;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<List<Assistido>> ListarAsync()
    {
        return (await _unitOfWork.AssistidoRepository.AllAsync()).ToList();
    }

    /// <inheritdoc />
    public async Task<Assistido> AdicionarAsync(AssistidoAdicionarInModel model)
    {
        model.TrimStringProperties();
        model.ValidateModel();

        if (model.Assi_MM_Imagem != null) { model.Assi_MM_Imagem = model.Assi_MM_Imagem.Replace(ImageOperation.PrefixBase64Jpeg, string.Empty); }
        model.Assi_NM_Nome = model.Assi_NM_Nome.ApenasLetras();
        model.Assi_NR_RG = model.Assi_NR_RG.ApenasNumeros();
        model.Assi_NR_CPF = model.Assi_NR_CPF.ApenasNumeros();
        model.Assi_NR_Telefone = model.Assi_NR_Telefone.ApenasNumeros();

        var assistido = _mapper.Map<Assistido>(model);

        await _unitOfWork.AssistidoRepository.AddAsync(assistido);
        await _unitOfWork.SaveAsync();

        return assistido;
    }

    /// <inheritdoc />
    public async Task<Assistido> EditarAsync(AssistidoEditarInModel model)
    {
        model.TrimStringProperties();
        model.ValidateModel();

        var assistidoAntesAlteracao = await _unitOfWork.AssistidoRepository.RetornaPorCodigoAsync(model.Assi_ID_Assistido);

        if (assistidoAntesAlteracao == null)
            throw new Exception("Assistido não localizada no banco de dados.");

        model.Assi_NM_Nome = model.Assi_NM_Nome.ApenasLetras();
        model.Assi_NR_RG = model.Assi_NR_RG.ApenasNumeros();
        model.Assi_NR_CPF = model.Assi_NR_CPF.ApenasNumeros();
        model.Assi_NR_Telefone = model.Assi_NR_Telefone.ApenasNumeros();

        var assistidoAlterado = await _unitOfWork.AssistidoRepository.RetornaPorCodigoAsync(model.Assi_ID_Assistido);

        if (model.Assi_MM_Imagem != null) { assistidoAlterado.Assi_MM_Imagem = model.Assi_MM_Imagem.Replace(ImageOperation.PrefixBase64Jpeg, string.Empty); }
        assistidoAlterado.Assi_NM_Nome = model.Assi_NM_Nome;
        assistidoAlterado.Assi_CD_Sexo = model.Assi_CD_Sexo;
        assistidoAlterado.Assi_DT_Nascimento = Convert.ToDateTime(model.Assi_DT_Nascimento);
        assistidoAlterado.Assi_NR_Idade = Convert.ToInt32(model.Assi_NR_Idade);
        assistidoAlterado.Assi_NR_RG = model.Assi_NR_RG;
        assistidoAlterado.Assi_NR_CPF = model.Assi_NR_CPF;
        assistidoAlterado.Assi_NR_Telefone = model.Assi_NR_Telefone;
        assistidoAlterado.Assi_NM_Mae = model.Assi_NM_Mae;
        assistidoAlterado.Assi_NM_Endereco = model.Assi_NM_Endereco;

        assistidoAlterado.Assi_NM_Profissao = model.Assi_NM_Profissao;
        assistidoAlterado.Assi_ID_Moradia = Convert.ToInt32(model.Assi_ID_Moradia);
        assistidoAlterado.Assi_ID_Escolaridade = Convert.ToInt32(model.Assi_ID_Escolaridade);
        assistidoAlterado.Assi_ID_EstadoCivil = Convert.ToInt32(model.Assi_ID_EstadoCivil);
        assistidoAlterado.Assi_CD_DeficienteFisico = model.Assi_CD_DeficienteFisico;
        assistidoAlterado.Assi_CD_DeficienteMental = model.Assi_CD_DeficienteMental;
        assistidoAlterado.Assi_ID_Dependente = Convert.ToInt32(model.Assi_ID_Dependente);
        assistidoAlterado.Assi_CD_ImpossibilidadeTrabalho = model.Assi_CD_ImpossibilidadeTrabalho;
        assistidoAlterado.Assi_ID_AtividadeRemunerada = Convert.ToInt32(model.Assi_ID_AtividadeRemunerada);
        assistidoAlterado.Assi_CD_Score = model.Assi_CD_Score;
        assistidoAlterado.Assi_NR_Score = Convert.ToInt32(model.Assi_NR_Score);
        assistidoAlterado.Assi_TX_Observacao = model.Assi_TX_Observacao;

        await _unitOfWork.SaveAsync();

        await _logGenericoService.AdicionarAsync(
            assistidoAntesAlteracao,
            assistidoAlterado,
            assistidoAlterado.Assi_ID_Assistido.ToString(),
            model.IDUsuarioCadastro);

        return assistidoAntesAlteracao;
    }

    /// <inheritdoc />
    public async Task<LogGenericoListarLogOutModel> ListarLogAsync(AssistidoListarLogInModel model)
    {
        model.ValidateModel();

        var retorno = await _logGenericoService.ListarAsync(new LogGenericoListarInModel
        {
            Referencia = model.CodUsuarioSolicitacaoLog.ToString(),
            Tabela = "GEMB_Assistido"
        });

        return retorno;
    }

    /// <inheritdoc />
    public async Task<AssistidoRetornarBasicoOutModel> RetornarDadosBasicosAsync(int codAssistido)
    {
        var retorno = await _unitOfWork.AssistidoRepository.RetornaPorCodigoAsync(codAssistido);

        if (retorno == null)
            throw new Exception("Assistido não localizado no banco de dados");

        return new AssistidoRetornarBasicoOutModel(retorno);
    }

    /// <inheritdoc />
    public async Task<Assistido> RetornarAsync(AssistidoRetornarInModel model)
    {
        model.ValidateModel();

        var retorno = await _unitOfWork.AssistidoRepository.RetornaPorCodigoAsync(model.Assi_ID_Assistido);
        if (retorno == null)
            throw new Exception("Assistido não localizado no banco de dados");

        retorno.Assi_MM_Imagem = $"{ImageOperation.PrefixBase64Jpeg}{retorno.Assi_MM_Imagem}";
        return retorno;
    }


}

