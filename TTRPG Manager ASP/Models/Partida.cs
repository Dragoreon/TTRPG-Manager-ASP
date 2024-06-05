namespace TTRPG_Manager_ASP.Models;

public partial class Partida
{
    public int Id { get; set; }

    public int NumPartida { get; set; }

    public virtual Entrada IdNavigation { get; set; } = null!;
}
