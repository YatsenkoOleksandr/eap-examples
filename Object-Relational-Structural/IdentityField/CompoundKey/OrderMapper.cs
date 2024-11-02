using System.Data;
using Microsoft.Data.SqlClient;

namespace IdentityField.CompoundKey;

public class OrderMapper : AbstractMapper
{
    public Order Find(Key key)
    {
        return (Order) AbstractFind(key);
    }

    public Order Find(long id)
    {
        return Find(new Key(id));
    }

    protected override string GetFindQuery()
    {
        return $"SELECT id, customer FROM Orders WHERE id = @id";
    }

    protected override DomainObject DoLoad(Key key, IDataReader reader)
    {
        string customer = reader.GetString(reader.GetOrdinal("customer"));
        var order = new Order()
        {
            Key = key,
            Customer = customer,
        };
        // MapperRegistry.lineItem().loadAllLineItemsFor(result);
        return order;
    }

    protected override SqlCommand CreateInsertCommand()
    {
        throw new NotImplementedException();
    }

    protected override Key FindNextDatabaseKeyObject()
    {
        throw new NotImplementedException();
    }

    protected override void InsertData(DomainObject domainObject, SqlCommand sqlCommand)
    {
        var order = domainObject as Order;
        sqlCommand.Parameters.AddWithValue("@customer", order.Customer);
    }

    protected override SqlCommand CreateUpdateCommand()
    {
        throw new NotImplementedException();
    }

    protected override void LoadUpdateCommand(DomainObject domainObject, SqlCommand command)
    {
        var order = domainObject as Order;
        command.Parameters.AddWithValue("@customer", order.Customer);
        command.Parameters.AddWithValue("@id", order.Id);
    }

    protected override SqlCommand CreateDeleteCommand()
    {
        throw new NotImplementedException();
    }

    protected override void LoadDeleteCommand(DomainObject domainObject, SqlCommand sqlCommand)
    {
        sqlCommand.Parameters.AddWithValue("@id", domainObject.Key.GetAsLongValue());
    }
}