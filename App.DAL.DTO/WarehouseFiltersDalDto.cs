namespace App.DAL.DTO;

public class WarehouseFiltersDalDto
{
    public IEnumerable<string> Streets { get; set; } = Array.Empty<string>();
    public IEnumerable<string> Cities { get; set; } = Array.Empty<string>();
    public IEnumerable<string> States { get; set; } = Array.Empty<string>();
    public IEnumerable<string> Countries { get; set; } = Array.Empty<string>();
}