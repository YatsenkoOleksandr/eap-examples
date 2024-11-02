namespace IdentityField.CompoundKey;

public class DomainObject
{
    public Key? Key { get; set; }

    protected DomainObject(Key Id)
    {
        Key = Id;
    }

    protected DomainObject()
    {
    }
}