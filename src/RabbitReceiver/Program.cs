using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

#region Default Configuration

    var factory = new ConnectionFactory();
    factory.Uri = new Uri(uriString: "amqp://admin:123456@localhost:5672/");
    factory.ClientProvidedName = "Rabbit MQ Receiver App";

    IConnection connection = factory.CreateConnection();
    IModel channel = connection.CreateModel();
    var exchangeName = "RabbitExchange";
    var queueName = "RabbitQueue";

    var routingKey = "rabbit-routing-key";

    channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
    channel.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, null);
    channel.QueueBind(queueName, exchangeName, routingKey, null);

#endregion

// PrefetchSize: Dont care how big the message is
// PrefetchCount: Number of messages to receive at once
// Global: False apply just to this instance
channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, args) =>
{
    Task.Delay(TimeSpan.FromSeconds(3)).Wait();

    var body = args.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Message received: '{message}'");

    // Tells if the message was delivered properly, basic acknowledgement
    // Is just when the BasicAck is processed that the message will come out of the queue
    // So, in case of error or exception, dont run that
    channel.BasicAck(args.DeliveryTag, false);
};

// Tag of the overall consume system, that can be canceled
var consumerTag = channel.BasicConsume(queueName, false, consumer);

Console.ReadLine();

channel.BasicCancel(consumerTag);
channel.Close();
connection.Close();
