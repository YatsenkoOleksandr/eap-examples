using System.Data;

namespace DependentMapping;

public class AlbumMapper: AbstractMapper<Album, long>
{
    /// <summary>
    /// Example SQL which loads owner (album) with dependents (track)
    /// </summary>
    protected string _findByIdQuery = @"
        SELECT a.Id, a.title, t.title as trackTitle
        FROM albums a, tracks t
        WHERE a.Id = @Id AND t.albumId = a.Id;
    ";

    /// <summary>
    /// Mapper which reads owner with dependents
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dataReader"></param>
    /// <returns></returns>
    protected override Album DoLoad(long id, IDataReader dataReader)
    {
        var title = dataReader["title"].ToString();
        var tracks = LoadTracks(dataReader);

        var album = new Album()
        {
            Id = id,
            Title = title,
            Tracks = tracks,
        };

        return album;
    }

    protected IEnumerable<Track> LoadTracks(IDataReader dataReader)
    {
        var tracks = new List<Track>();
        var track = LoadTrack(dataReader);
        if (track is null)
        {
            return tracks;
        }

        tracks.Add(track);
        while(dataReader.Read())
        {
            tracks.Add(LoadTrack(dataReader));
        }
        return tracks;
    }

    protected Track LoadTrack(IDataReader row)
    {
        var title = row["trackTitle"].ToString();
        if (string.IsNullOrEmpty(title))
        {
            return null;
        }

        return new Track()
        {
            Title = title,
        };
    }

    /// <summary>
    /// Updates Album only
    /// </summary>
    /// <param name="album"></param>
    public override void Update(Album album)
    {
        var updateQuery = @"
            UPDATE albums SET title = @title
            WHERE id = @id;
        ";

        // Execute update of Album table only
    }

    /// <summary>
    /// Updates Album tracks
    /// </summary>
    /// <param name="album"></param>
    public void UpdateAlbumTracks(Album album)
    {
        var deleteTracksQuery = @"
            DELETE FROM tracks
            WHERE albumId = @id
        ";

        // Execute delete of all album tracks

        // Insert new tracks
        foreach (var track in album.Tracks)
        {
            InsertTrack(track, album);
        }
    }

    /// <summary>
    /// Inserts track for given album
    /// </summary>
    /// <param name="track"></param>
    /// <param name="forAlbum"></param>
    public void InsertTrack(Track track, Album forAlbum)
    {
        var insertQuery = @"
            INSERT INTO tracks(albumId, title)
            VALUES (@albumId, @title)
        ";

        // Execute insert
    }
}