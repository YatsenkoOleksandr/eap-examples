using System.Data;

namespace EmbeddedValue
{
    /// <summary>
    /// Active Record which maps cost using Embedded Value <see cref="Money" />
    /// </summary>
    public class ProductOffering
    {
        public int Id { get; set; }

        public Money? BaseCost { get; set; }

        public static ProductOffering Load(IDataReader dataReader)
        {
            int id = dataReader.GetInt32(dataReader.GetOrdinal("ID"));

            // Mapping
            decimal baseCostAmount = dataReader.GetDecimal(dataReader.GetOrdinal("base_cost_amount"));
            string baseCostCurrency = dataReader.GetString(dataReader.GetOrdinal("base_cost_currency"));

            return new ProductOffering()
            {
                Id = id,
                BaseCost = new Money()
                {
                    Amount = baseCostAmount,
                    Currency = baseCostCurrency,
                }
            };
        }

        public void Update()
        {
            const string updateQuery =
                @"UPDATE product_offerings
                    SET base_cost_amount = @baseCostAmount, base_cost_currency = @baseCostCurrency
                    WHERE id = @id";

            var baseCostAmount = BaseCost?.Amount;
            var baseCostCurrency = BaseCost?.Currency;

            // Execute UPDATE query and provide params
        }
    }
}
