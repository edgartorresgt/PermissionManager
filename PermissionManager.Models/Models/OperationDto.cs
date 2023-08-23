namespace PermissionManager.Models.Models;

public class OperationDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string NameOperation { get; set; }
}