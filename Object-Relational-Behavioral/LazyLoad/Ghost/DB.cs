using Microsoft.Data.SqlClient;

namespace LazyLoad.Ghost;

public static class DB
{
    public static SqlConnection Connection { get; set; }
}