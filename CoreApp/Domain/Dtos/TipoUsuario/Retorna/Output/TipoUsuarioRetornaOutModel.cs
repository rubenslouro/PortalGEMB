

namespace Domain.Dtos.TipoUsuario.Retorna.Output;

public class TipoUsuarioRetornaOutModel
{
    public TipoUsuarioRetornaOutModel(Entities.TipoUsuario tipoUsuario)            
    {
        Codigo = tipoUsuario.Codigo;
        Descricao = tipoUsuario.Descricao;
    }

    /// <summary>
    /// Código do tipo de usuário/perfil
    /// </summary>
    public int Codigo { get; private set; }

    /// <summary>
    /// Descrição do tipo d usuário
    /// </summary>
    public string Descricao { get; private set; } 
}