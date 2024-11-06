using System.Data;

namespace ForeignKeyMapping.SingleValue;

public class AlbumMapper: AbstractMapper
{
    /// <summary>
    /// Method updates <see cref="Album"/> and takes foreign key from connected <see cref="Album.Artist"/>
    /// </summary>
    /// <param name="domainObject"></param>
    /// <exception cref="NotImplementedException"></exception>
    public override void Update(DomainObject domainObject)
    {
        var album = domainObject as Album;

        var updateQuery = @"
            UPDATE album
            SET title = @title, artistId = @artistId
            WHERE id = @id;
        ";

        var title = album.Title;
        var artistId = album.Artist?.Id;
        var id = album.Id;

        throw new NotImplementedException();
    }


    /// <summary>
    /// Method reads <see cref="Album"/> from data reader
    /// </summary>
    /// <param name="id"></param>
    /// <param name="reader"></param>
    /// <returns></returns>
    protected override DomainObject DoLoad(int id, IDataReader reader)
    {
        var title = reader.GetString(reader.GetOrdinal("title"));

        // Make another call to get related artist
        var artistId = reader.GetInt32(reader.GetOrdinal("artistId"));
        var artist = GetArtist(artistId);

        var album = new Album()
        {
            Id = id,
            Title = title,
            Artist = artist
        };
        return album;
    }

    protected Artist GetArtist(int artistId)
    {
        // MapperRegistry.artist().find(artistID)
        throw new NotImplementedException();
    }
}