using AutoMapper;
using Domain.DomainServicesInterfaces;
using Domain.Dtos.LogGenerico.Input;
using Domain.Dtos.Atendimento.Input;
using Domain.Dtos.Atendimento.Output;
using Domain.Entities;
using Domain.Interfaces.Repository;
using UtilService;
using UtilService.Util;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.Dtos.RegraSistema.RetornaRegrasUsuario.Input;
using Domain.Dtos.LogGenerico.Output;
using System.Reflection;
using Domain.Dtos.AtendimentoTipoAtendimento.Output;
using System.Linq;
using Domain.Dtos.Assistido.Output;
using System;
using Domain.Dtos.Assistido.Listar.Output;

namespace ApplicationServices;

/// <inheritdoc />
public class AtendimentoService : IAtendimentoService
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
    public AtendimentoService(
        IUnitOfWork unitOfWork,
        ILogGenericoService logGenericoService,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logGenericoService = logGenericoService;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<List<Atendimento>> ListarAsync()
    {
        return (await _unitOfWork.AtendimentoRepository.AllAsync()).ToList();
    }

    /// <inheritdoc />
    public async Task<LogGenericoListarLogOutModel> ListarLogAsync(AtendimentoListarLogInModel model)
    {
        model.ValidateModel();

        var retorno = await _logGenericoService.ListarAsync(new LogGenericoListarInModel
        {
            Referencia = model.CodUsuarioSolicitacaoLog.ToString(),
            Tabela = "GEMB_Atendimento"
        });

        return retorno;
    }

    /// <inheritdoc />
    public async Task<AtendimentoRetornarBasicoOutModel> RetornarDadosBasicosAsync(int codAtendimento)
    {
        var retorno = await _unitOfWork.AtendimentoRepository.RetornaPorCodigoAsync(codAtendimento);

        if (retorno == null)
            throw new Exception("Atendimento não localizado no banco de dados");

        return new AtendimentoRetornarBasicoOutModel(retorno);
    }

    /// <inheritdoc />
    public async Task<Atendimento> RetornarAsync(AtendimentoRetornarInModel model)
    {
        model.ValidateModel();

        var retorno = await _unitOfWork.AtendimentoRepository.RetornaPorCodigoAsync(model.codAtendimento);
        if (retorno == null)
            throw new Exception("Atendimento não localizado no banco de dados");

        return retorno;
    }

    /// <inheritdoc />
    public async Task<Atendimento> AdicionarAsync(AtendimentoAdicionarInModel model)
    {
        model.TrimStringProperties();
        model.ValidateModel();

        int codCurso = 0;

        // SALVAR ATENDIMENTO
        var atendimento = _mapper.Map<Atendimento>(model);

        await _unitOfWork.AtendimentoRepository.AddAsync(atendimento);
        await _unitOfWork.SaveAsync();

        // LEVANTAR E SALVAR O RELACIONAMENTO DOS TIPOS DE ATENDIMENTO
        foreach (CheckBoxList item in model.ChechBoxList)
        {
            if (item.IsChecked)
            {
                var relacionar_TipoAtendimento = new Atendimento_TipoAtendimento
                {
                    AtTA_ID_Atendimento = atendimento.Aten_ID_Atendimento,
                    AtTA_ID_TipoAtendimento = item.Value
                };

                await _unitOfWork.Atendimento_TipoAtendimentoRepository.AddAsync(relacionar_TipoAtendimento);

                // VERIFICAR SE O TIPO DE ATENDIMENTO AO ASSISTIDO É PARA UM CURSO
                var rettipoaten = await _unitOfWork.TipoAtendimentoRepository.FirstAsync(o => o.TpAt_ID_TipoAtendimento == item.Value);
                codCurso = rettipoaten.TpAt_ID_Disciplina != null ? Convert.ToInt32(rettipoaten.TpAt_ID_Disciplina) : codCurso;
            }
        }
        await _unitOfWork.SaveAsync();

        // SALVAR O RELACIONAMENTO ALUNO TURMA
        if (codCurso > 0)
        {
            var retturma = await _unitOfWork.TurmaRepository.FirstAsync(o => o.Turm_ID_Disciplina == codCurso && o.Turm_IN_AbertaMatrícula == "S");

            var relacionar_TurmaAluno = new TurmaAluno
            {
                TuAl_ID_Assistido = model.Aten_ID_Assistido,
                TuAl_ID_Turma = retturma.Turm_ID_Turma
            };

            await _unitOfWork.TurmaAlunoRepository.AddAsync(relacionar_TurmaAluno);
            await _unitOfWork.SaveAsync();
        }

        return atendimento;
    }

    /// <inheritdoc />
    public async Task<Atendimento> EditarAsync(AtendimentoEditarInModel model)
    {
        model.TrimStringProperties();
        model.ValidateModel();

        var atendimentoAntesAlteracao = await _unitOfWork.AtendimentoRepository.RetornaPorCodigoAsync(model.Aten_ID_Atendimento);

        if (atendimentoAntesAlteracao == null)
            throw new Exception("Atendimento não localizada no banco de dados.");

        var atendimentoAlterado = await _unitOfWork.AtendimentoRepository.RetornaPorCodigoAsync(model.Aten_ID_Atendimento);

        //atendimentoAlterado.Aten_ID_TipoAtendimento = model.Aten_ID_TipoAtendimento;
        atendimentoAlterado.Aten_TX_Observacao = model.Aten_TX_Observacao;

        await _unitOfWork.SaveAsync();

        await _logGenericoService.AdicionarAsync(
            atendimentoAntesAlteracao,
            atendimentoAlterado,
            atendimentoAlterado.Aten_ID_Atendimento.ToString(),
            model.IDUsuarioCadastro);

        return atendimentoAntesAlteracao;
    }


    public async Task<Atendimento> QtdAtendimento_SexoAsync()
    {
        var ret = new Atendimento();

        var lista = await _unitOfWork.AtendimentoRepository.FirstAsync(o => o.Aten_DT_Cadastro >= DateTime.Today.AddDays(-7));


        return (ret);
    }

    public async Task<List<Atendimento>> RetornarQtdAtendimentoAsync()
    {
        var retlista = (await _unitOfWork.AtendimentoRepository.FindAsync(o => o.Aten_DT_Cadastro >= DateTime.Today.AddDays(-7))).ToList();
        var retlistaAssitidos = (await _unitOfWork.AssistidoRepository.FindAsync(o => o.Assi_ID_Assistido == retlista[0].Aten_ID_Assistido)).ToList();

        //var retlistaAssitidos2 = (await _unitOfWork.AssistidoRepository.FindAsync(o => o.Assi_ID_Assistido == new AtendimentoRetornarBasicoOutModel(retlista))).ToList();
        var retlista2 = (await _unitOfWork.AtendimentoRepository.FindAsync(o => o.Aten_DT_Cadastro >= DateTime.Today.AddDays(-7))).Select(o => new AssistidoItemOutModel { Codigo = o.Aten_ID_Assistido }).ToList();

        var ret = (await _unitOfWork.AssistidoRepository.AllAsync()).ToList();

        //var retlista2 = (await _unitOfWork.AtendimentoRepository.FindAsync(o => o.Aten_DT_Cadastro >= DateTime.Today.AddDays(-7))).Select(o => new AssistidoRetornarItemOutModel(o)).ToList();

        //var retlista = (await _unitOfWork.AtendimentoRepository.FindAsync(o => o.Aten_DT_Cadastro >= DateTime.Today.AddDays(-7))).ToList();

        if (retlista == null)
        {
            throw new Exception("Nenhum atendimento foi localizado.");
        }

        return retlista;
    }
}

