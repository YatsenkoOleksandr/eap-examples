using System.Data;

namespace TableModule;

internal class Product: TableModule
{
  public Product(DataSet dataSet): base(dataSet, "Products")
  {
  }

  // indexer, which gets to a particular row in the data table by the primary key
  public DataRow this [long key]
  {
    get
    {
      string filter = $"ID = {key}";
      return table.Select(filter)[0];
    }
  }

  public ProductType GetProductType(long id)
  {
    /*
      GetProductType encapsulates the data in the data table. There’s an argument for
      doing this for all columns of data, as opposed to accessing them directly as I did
      with the amount on the contract. While encapsulation is generally a Good
      Thing, I don’t use it here because it doesn’t fit with the assumption of the environment
      that different parts of the system access the data set directly. There’s no
      encapsulation when the data set moves over to the UI, so column access functions
      only make sense when there’s some additional functionality to be done,
      such as converting a string to a product type.
      This is also a good time to mention that, although I’m using an untyped data
      set here because these are more common on different platforms, there’s a strong
      argument (page 509) for using .NET’s strongly typed data set.
    */

    string typeCode = (string)this[id]["type"];

    return (ProductType) Enum.Parse(typeof(ProductType), typeCode);
  }
}