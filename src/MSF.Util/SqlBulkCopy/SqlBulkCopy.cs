using Microsoft.Data.SqlClient;

namespace MSF.Util.SqlBulkCopyCl
{
    public static class SqlBulkCopyCl
    {
        public static void Connect()
        {
            using var connection = new SqlConnection("");
            using var bulkCopy = new SqlBulkCopy(connection);
            bulkCopy.DestinationTableName = "Table Name";

        }  
    }
}
