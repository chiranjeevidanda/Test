namespace NEC.Fulf3PL.Core.Entities.Persistence;

public interface IAuditableEntity
{
    public DateTime LoggedOn { get; set; }

    public DateTime ModifiedDate { get; set; }
}
