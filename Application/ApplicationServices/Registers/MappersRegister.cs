using AutoMapper;
using Domain.Dtos.Atendimento.Input;
using Domain.Dtos.Assistido.Input;
using Domain.Dtos.Usuario.Editar.Input;
using Domain.Dtos.Usuario.RetornaParaEdicao.Output;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Domain.Dtos.ConfiguracaoGeral.Input;
using Domain.Dtos.ConfiguracaoGeral.Output;

namespace ApplicationServices.Registers;

/// <summary>
/// Innjetor do IMapper
/// </summary>
public static class MappersRegister
{
    /// <summary>
    /// Método para injeção dos Mappers
    /// </summary>
    /// <param name="services"></param>
    public static void AddMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(map =>
            {
                MappersConfiguracaoGeral(map);
                MappersUsuario(map);
                MappersAtendimento(map);
                MappersAssistido(map);
            }
        );
    }

    #region Métodos privados

    private static void MappersAssistido(IMapperConfigurationExpression map)
    {
        map.CreateMap<AssistidoAdicionarInModel, Assistido>()
           .ForMember(dest => dest.Assi_ID_UsuarioCadastro, opt => opt.MapFrom(src => src.IDUsuarioCadastro))
           .ForMember(dest => dest.Assi_DT_Cadastro, opt => opt.MapFrom(src => DateTime.Now));
    }

    private static void MappersAtendimento(IMapperConfigurationExpression map)
    {
        map.CreateMap<AtendimentoAdicionarInModel, Atendimento>()
           .ForMember(dest => dest.Aten_ID_UsuarioCadastro, opt => opt.MapFrom(src => src.IDUsuarioCadastro))
           .ForMember(dest => dest.Aten_DT_Cadastro, opt => opt.MapFrom(src => DateTime.Now));
    }

    private static void MappersConfiguracaoGeral(IMapperConfigurationExpression map)
    {
        map.CreateMap<ConfiguracaoGeralRetornaOutModel, ConfiguracaoGeralEditarConfiguracaoInModel>();

    }

    private static void MappersUsuario(IMapperConfigurationExpression map)
    {
        map.CreateMap<UsuarioRetornaParaEdicaoOutModel, UsuarioEditarInModel>();

    }

    #endregion Métodos privados   
}