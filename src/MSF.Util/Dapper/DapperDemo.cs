using Dapper;
using System;
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

        public Customer Parameters()
        {
            var parameters = new DynamicParameters();
            parameters.Add("CustomerId", 1);

            using (var connection = new SqlConnection("data source=(localdb)\\MSSQLLocalDB;initial catalog=RestaurantDB;integrated security=True;MultipleActiveResultSets=True"))
            {
                connection.Open();

                var query = "SELECT * FROM TB_CUSTOMER WHERE Id = @CustomerId";
                var customer = connection.QueryFirstOrDefault<Customer>(query, parameters);

                return customer;
            }
        }

        public void Transactions()
        {
            var parameters = new DynamicParameters();
            parameters.Add("CustomerId", 1);

            using (var connection = new SqlConnection("connectionString"))
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        connection.Open();

                        var query = "INSERT INTO TB_CUSTOMER (Id, Name) Values (3, 'Name')";
                        connection.Execute(query);

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
