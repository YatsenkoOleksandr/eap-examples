using System.Data;
using Microsoft.Data.SqlClient;

namespace TransactionScript
{
    /*
      Table Data Gateway which returns ADO.NET DataReader instead of original ResultSet
   */
    internal class TableDataGateway
    {
        private readonly SqlConnection db;

        public TableDataGateway(SqlConnection db)
        {
            this.db = db;
        }

        public IDataReader FindRecognitionsFor(long contractNumber, DateTime asOf)
        {
            var findRecognitionsQuery = @"
                SELECT amount
                FROM RevenueRecognitions
                WHERE contract = @contractNumber AND recognizedOn <= @asOf;";
            var command = new SqlCommand(findRecognitionsQuery, db);
            command.Parameters.AddWithValue("@contractNumber", contractNumber);
            command.Parameters.AddWithValue("@asOf", asOf);
            return command.ExecuteReader();
        }

        public IDataReader FindContract(long contractNumber)
        {
            var findContractQuery = @"
                SELECT *
                FROM contracts c, products p
                WHERE ID = @contractNumber AND c.product = p.ID;";
            var command = new SqlCommand(findContractQuery, db);
            command.Parameters.AddWithValue("@contractNumber", contractNumber);
            return command.ExecuteReader();
        }

        public long InsertRecognition(long contractNumber, decimal amount, DateTime date)
        {
            var insertQuery = "INSERT INTO RevenueRecognitions VALUES (?, ?, ?);";
            var command = new SqlCommand(insertQuery, db);
            command.Parameters.AddWithValue("@contractNumber", contractNumber);
            command.Parameters.AddWithValue("@amount", amount);
            command.Parameters.AddWithValue("@date", date);
            return command.ExecuteNonQuery();
        }
    }
}
