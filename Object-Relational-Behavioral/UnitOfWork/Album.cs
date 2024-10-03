namespace UnitOfWork;

public class Album : DomainObject
{
    private string title = string.Empty;

    public static Album Create(string name)
    {
        var album = new Album()
        {
            // Id = IdGenerator.NextId(),
            Title = name,
        };
        album.MarkAsNew();
        return album;
    }

    public string Title
    {
        get {
            return title;
        }

        set {
            title = value;
            MarkAsDirty();
        }
    }
}