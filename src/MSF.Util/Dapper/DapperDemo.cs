using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace MSF.Util.Dapper
{
    public class DapperDemo
    {
        /*
            ORM: Object-Relational-Mapping
            Define como os dados serão mapeados entre os ambientes (BD e Código), como serão gravados e acessados
        */

        public IQueryable<Customer> DapperExemple()
        {
            using (var connection = new SqlConnection("connectionString"))
            {
                connection.Open();

                var query = "SELECT * FROM TB_CUSTOMER";
                var customers = connection.Query<Customer>(query).AsQueryable();

                return customers;
            }
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
