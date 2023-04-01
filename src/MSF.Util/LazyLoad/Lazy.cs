using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MSF.Util.LazyLoad
{
    public class LazyDemo
    {
        public Lazy<PaymentEntity> objPaymentEntity { get; set; }
        private static bool Key { get; set; }

        public LazyDemo()
        {
            objPaymentEntity = new Lazy<PaymentEntity>();
            Key = false;
        }

        public void Loader(int valor)
        {
            if (!Key)
            {
                var objeto = objPaymentEntity.Value.CriarObjeto(valor);
                Console.WriteLine($"Objeto criado: {objeto.Id}");
            }
            else
                Console.WriteLine("Finalizado sem criação");
        }
    }

    public class PaymentEntity
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public int Amount { get; set; }
        public DateTime Created { get; set; }

        public PaymentEntity()
        {
            Thread.Sleep(5000);
        }

        public PaymentEntity CriarObjeto(int valor)
        {
            var payment = new PaymentEntity()
            {
                Amount = valor,
                Created = DateTime.Now,
                Id = Guid.NewGuid(),
                Status = "Created"
            };

            return payment;
        }
    }
}
