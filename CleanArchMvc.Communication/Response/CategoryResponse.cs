namespace CleanArchMvc.Communication.Response;

public class CategoryResponse
{
    public int Id { get; set; }
    public string Name { get; private set; } = string.Empty;
}
