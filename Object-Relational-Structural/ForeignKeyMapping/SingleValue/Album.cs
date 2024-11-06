namespace ForeignKeyMapping.SingleValue;

public class Album: DomainObject
{
    public string Title { get; set; } = string.Empty;

    public Artist? Artist { get; set; } = null;
}