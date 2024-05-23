namespace Domain.Dtos.LogGenerico.Output;

public class LogGenericoListarLogOutModel
{
    /// <summary>
    /// Objeto de log genérico
    /// </summary>
    public List<LogGenericoListarItemOutModel> ListaLogGenerico { get; set; } = new List<LogGenericoListarItemOutModel>();

}