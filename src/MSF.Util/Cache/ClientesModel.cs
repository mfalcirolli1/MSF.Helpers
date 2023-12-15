using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Util.Cache
{
    public class ClientesModel
    {
        public static List<Cliente> Clientes()
        {
            return new List<Cliente>()
            {
                new Cliente() { Id = 1, Nome = "Cliente 1"},
                new Cliente() { Id = 2, Nome = "Cliente 2"},
                new Cliente() { Id = 3, Nome = "Cliente 3"}
            }; 
        }
    }

    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
