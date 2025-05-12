namespace App.BLL.DTO;

public class WarehouseFiltersBllDto
{
    public IEnumerable<string> Streets { get; set; } = Array.Empty<string>();
    public IEnumerable<string> Cities { get; set; } = Array.Empty<string>();
    public IEnumerable<string> States { get; set; } = Array.Empty<string>();
    public IEnumerable<string> Countries { get; set; } = Array.Empty<string>();
}