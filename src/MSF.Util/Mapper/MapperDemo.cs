using BenchmarkDotNet.Attributes;
using System;
using Mapster;

namespace MSF.Util.Mapper
{
    [MemoryDiagnoser]
    [RankColumn]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    public class MapperDemo
    {
        [Benchmark]
        public void Mapster()
        { 
            var payment = new Payment()
            {
                Amount = 120,
                Created = DateTime.Now,
                Id = Guid.NewGuid(),
                Status = "Created"
            };

            var paymentDTO = payment.Adapt<PaymentEntity>();
            Console.WriteLine($"{paymentDTO.Id}, {paymentDTO.Amount}, {paymentDTO.Status}, {paymentDTO.Created}");
        }

        [Benchmark]
        public void FastMaper()
        {
            var payment = new Payment()
            {
                Amount = 120,
                Created = DateTime.Now,
                Id = Guid.NewGuid(),
                Status = "Created"
            };
            
            var paymentDTO = FastMapper.TypeAdapter.Adapt<Payment, PaymentEntity>(payment);
            Console.WriteLine($"{paymentDTO.Id}, {paymentDTO.Amount}, {paymentDTO.Status}, {paymentDTO.Created}");
        }
    }

    internal class Payment
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public int Amount { get; set; }
        public DateTime Created { get; set; }
    }

    internal class PaymentEntity
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public int Amount { get; set; }
        public DateTime Created { get; set; }
    }
}
