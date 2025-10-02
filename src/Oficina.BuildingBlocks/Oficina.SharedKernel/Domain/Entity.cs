namespace Oficina.SharedKernel.Domain;
public abstract class Entity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime Created_At { get; protected set; } = DateTime.UtcNow;
    public DateTime? Updated_At { get; protected set; }
    public void Touch() => Updated_At = DateTime.UtcNow;
}
