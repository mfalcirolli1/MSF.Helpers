using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace MSF.Util.LazyLoad
{
    public class LazyDemo
    {
        public Lazy<PaymentEntity> objPaymentEntity { get; set; }

        public LazyDemo()
        {
            objPaymentEntity = new Lazy<PaymentEntity>();
        }

        public void Loader(int valor, bool naoAtivarLazy)
        {
            if (naoAtivarLazy)
            {
                var objeto = objPaymentEntity.Value.CriarObjeto(valor);
                Debug.WriteLine($"Objeto criado: {objeto.Id}");
            }
            else
                Debug.WriteLine("Finalizado sem criação");
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
