using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MSF.Util.Cache
{
    public class MemoryCacheDemo
    {
        // Instalar System.Runtime.Caching 7.0.0
        // Em projetos Web API, adicionar o serviço na classe Program.cs (builder.Services.AddMemoryCache();)
        // Caso a variável armazenada em cache possa vir a ser alterada durante o processamento, lembrar de atualizar o cache após a sensibilização.

        private IMemoryCache _memoryCache;

        public MemoryCacheDemo(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void GetClientes()
        {
            if (!_memoryCache.TryGetValue(key: "todosClientes", out List<Cliente> clientes))
            {
                clientes = ClientesModel.Clientes();
                _memoryCache.Set(key: "todosClientes", clientes, DateTimeOffset.Now.AddDays(1));
            }

            if (clientes.Any()) { Debug.WriteLine("Clientes OK"); }
        }
    }
}
