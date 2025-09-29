namespace MetasApp.Models;
public class Meta
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataLimite { get; set; } = DateTime.Today;
    public bool IsCompleted { get; set; }
    public DateTime? DataConclusao { get; set; }
}