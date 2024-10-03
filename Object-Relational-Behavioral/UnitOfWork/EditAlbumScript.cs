namespace UnitOfWork;

public class EditAlbumScript
{
    public static void UpdateTitle(int albumId, string title)
    {
        // UnitOfWork.NewCurrent
        UnitOfWork.SetCurrent(new UnitOfWork()); // can be omitted
        Mapper mapper = MapperRegistry.GetMapper(typeof (Album));
        Album album = mapper.Find(albumId) as Album;
        album.Title = title;
        UnitOfWork.GetCurrent().Commit();
    }
}