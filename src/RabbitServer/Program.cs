using RabbitMQ.Client;
using System.Text;

#region Default Configuration

    var factory = new ConnectionFactory();
    factory.Uri = new Uri(uriString: "amqp://admin:123456@localhost:5672/");
    factory.ClientProvidedName = "Rabbit MQ Sender App";

    IConnection connection = factory.CreateConnection();
    IModel channel = connection.CreateModel();
    var exchangeName = "RabbitExchange";
    var queueName = "RabbitQueue";

    var routingKey = "rabbit-routing-key";

    channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
    channel.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, null);
    channel.QueueBind(queueName, exchangeName, routingKey, null);

#endregion

    for (int i = 0; i < 60; i++)
    {
        Console.WriteLine($"Sending Message {i}...");
        byte[] msgBodyBytes = Encoding.UTF8.GetBytes($"Message {i}");
        channel.BasicPublish(exchangeName, routingKey, null, msgBodyBytes);
        Thread.Sleep(1000);
    }

    channel.Close();
    connection.Close();