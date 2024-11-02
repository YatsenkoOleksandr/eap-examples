namespace IdentityField.CompoundKey;

/// <summary>
/// Represents compound key
/// </summary>
public class Key
{
    private readonly object[] keys;

    public bool IsSingleKey { get { return keys.Length == 1; } }

    public Key(params object[] keys)
    {
        ValidateKeys(keys);
        this.keys = keys;
    }

#region Convenience constructors to use with common types

    public Key(long key): this([key])
    {
    }

    public Key(int key): this([key])
    {
    }

    public Key(Guid key): this([key])
    {
    }

    public Key(string key): this([key])
    {
    }

#endregion

#region Equality check

    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not Key)
        {
            return false;
        }

        Key otherKey = obj as Key;
        if (keys.Length != otherKey.keys.Length)
        {
            return false;
        }

        for (int i = 0; i < keys.Length; i++)
        {
            if (!keys[i].Equals(otherKey.keys[i]))
            {
                return false;
            }
        }

        return true;
    }

    public override int GetHashCode()
    {
        if (keys.Length == 0)
        {
            return 0;
        }

        return HashCode.Combine(keys);
    }
#endregion

    public object GetValue(int index)
    {
        return keys[index];
    }

#region Convenience methods to get already casted key value

    public object GetValue()
    {
        ValidateIsSingleKey();
        return keys.First();
    }

    public long GetAsLongValue()
    {
        return (long) GetValue();
    }

    public long GetAsLongValue(int index)
    {
        return (long) GetValue(index);
    }

    public int GetAsIntValue()
    {
        return (int) GetValue();
    }

    public long GetAsIntValue(int index)
    {
        return (int) GetValue(index);
    }

    public string GetAsStringValue()
    {
        return (string) GetValue();
    }

    public string GetAsStringValue(int index)
    {
        return (string) GetValue(index);
    }

    public Guid GetAsGuidValue()
    {
        return (Guid) GetValue();
    }

    public Guid GetAsGuidValue(int index)
    {
        return (Guid) GetValue(index);
    }
#endregion

    private void ValidateKeys(object[] keys)
    {
        if (keys is null)
        {
            throw new ArgumentNullException(nameof(keys), "Cannot have null keys");
        }

        foreach (var key in keys)
        {
            if (key is null)
            {
                throw new ArgumentException("Cannot have null key");
            }
        }
    }

    private void ValidateIsSingleKey()
    {
        if (!IsSingleKey)
        {
            throw new FieldAccessException("Cannot take value on composite key");
        }
    }
}