namespace DemoApi.Models;

public class Dev
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // avoid null warnings
    public string Level { get; set; } = string.Empty;
}
