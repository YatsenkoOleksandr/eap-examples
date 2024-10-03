namespace UnitOfWork;

public abstract class DomainObject
{
    public int Id { get; set; }

    protected void MarkAsNew()
    {
        UnitOfWork.GetCurrent().RegisterNew(this);
    }

    protected void MarkAsClean()
    {
        UnitOfWork.GetCurrent().RegisterClean(this);
    }

    protected void MarkAsDirty()
    {
        UnitOfWork.GetCurrent().RegisterDirty(this);
    }

    protected void MarkAsRemoved()
    {
        UnitOfWork.GetCurrent().RegisterRemoved(this);
    }
}