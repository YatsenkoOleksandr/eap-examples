using System.Collections;
using System.Data;

namespace ForeignKeyMapping.CollectionOfReferences;

public class DataSetHolder
{
    private Hashtable dataAdapters = new Hashtable();

    public readonly DataSet Data = new DataSet();
}