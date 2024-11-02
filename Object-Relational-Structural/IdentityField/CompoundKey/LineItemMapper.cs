using System.Data;
using Microsoft.Data.SqlClient;

namespace IdentityField.CompoundKey;

public class LineItemMapper : AbstractMapper
{
    public LineItem Find(Key key)
    {
        return (LineItem) AbstractFind(key);
    }

    public LineItem Find(long orderId, long seq)
    {
        Key key = new Key(orderId, seq);
        return Find(key);
    }

    public IEnumerable<LineItem> FindAllLineItemsFor(Order order)
    {
        throw new NotImplementedException();
    }

    protected override string GetFindQuery()
    {
        return @"
            SELECT orderId, seq, amount, product
            FROM LineItems
            WHERE (orderId = @orderId) AND (seq = @seq);
        ";
    }

    protected override void AddKeyParameterToQuery(Key key, SqlCommand findCommand)
    {
        findCommand.Parameters.AddWithValue("@orderId", key.GetLineItemOrderId());
        findCommand.Parameters.AddWithValue("@seq", key.GetLineItemSeq());
    }

    protected override Key CreateKey(IDataReader reader)
    {
        var orderId = reader.GetInt64(reader.GetOrdinal("orderId"));
        var seq = reader.GetInt64(reader.GetOrdinal("seq"));

        return new Key(orderId, seq);
    }

    protected override DomainObject DoLoad(Key key, IDataReader reader)
    {
        // Order theOrder = MapperRegistry.order().find(orderID(key));

        var amount = reader.GetInt32(reader.GetOrdinal("amount"));
        var product = reader.GetString(reader.GetOrdinal("product"));

        var lineItem = new LineItem()
        {
            Key = key,
            Amount = amount,
            Product = product,
        };

        //order.addLineItem(result);//links to the order

        return lineItem;
    }

    protected override Key FindNextDatabaseKeyObject()
    {
        throw new NotImplementedException();
    }

    protected override SqlCommand CreateInsertCommand()
    {
        throw new NotImplementedException();
    }

    protected override void InsertKey(DomainObject domainObject, SqlCommand sqlCommand)
    {
        sqlCommand.Parameters.AddWithValue("@orderId", domainObject.Key.GetLineItemOrderId());
        sqlCommand.Parameters.AddWithValue("@seq", domainObject.Key.GetLineItemSeq());
    }

    protected override void InsertData(DomainObject domainObject, SqlCommand sqlCommand)
    {
        var lineItem = domainObject as LineItem;
        sqlCommand.Parameters.AddWithValue("@amount", lineItem.Amount);
        sqlCommand.Parameters.AddWithValue("@product", lineItem.Product);
    }

    /// <summary>
    /// DEPRECATED.
    /// To insert <see cref="LineItem"/> need a reference to <see cref="Order"/>,
    /// so the default method is not applicable and it is not supported
    /// </summary>
    /// <param name="domainObject"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    [Obsolete("Method is not supported since need an instance of Order to insert a Line Item")]
    public override Key Insert(DomainObject domainObject)
    {
        // This violates Liskov Substitution principle
        throw new NotSupportedException("Must supply an order when inserting a line item");
    }

    /// <summary>
    /// Use this method to insert a <see cref="LineItem"/> instance
    /// </summary>
    /// <param name="lineItem"></param>
    /// <param name="order"></param>
    /// <returns></returns>
    public Key Insert(LineItem lineItem, Order order)
    {
        Key key = new Key(order.Id, GetNextSequenceNumber(order));
        return PeformInsert(lineItem, key);
    }

    private long GetNextSequenceNumber(Order order)
    {
        throw new NotImplementedException();
    }

    protected override SqlCommand CreateUpdateCommand()
    {
        throw new NotImplementedException();
    }

    protected override void LoadUpdateCommand(DomainObject domainObject, SqlCommand command)
    {
        var lineItem = domainObject as LineItem;

        command.Parameters.AddWithValue("@amount", lineItem.Amount);
        command.Parameters.AddWithValue("@product", lineItem.Product);
        command.Parameters.AddWithValue("@orderId", lineItem.Key.GetLineItemOrderId());
        command.Parameters.AddWithValue("@seq", lineItem.Key.GetLineItemSeq());
    }

    protected override SqlCommand CreateDeleteCommand()
    {
        throw new NotImplementedException();
    }

    protected override void LoadDeleteCommand(DomainObject domainObject, SqlCommand sqlCommand)
    {
        sqlCommand.Parameters.AddWithValue("@orderId", domainObject.Key.GetLineItemOrderId());
        sqlCommand.Parameters.AddWithValue("@seq", domainObject.Key.GetLineItemSeq());
    }
}