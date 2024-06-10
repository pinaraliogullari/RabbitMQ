using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://ctpgaytw:kMPeUt-BVWJT4h_zrgcNESx5-Eyp4bAa@sparrow.rmq.cloudamqp.com/ctpgaytw");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

#region P2P Tasarımı
//string queueName = "example-p2p-queue";
//channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false);

//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

//consumer.Received += (sender, e) =>
//{
//	Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//};
#endregion
#region Pub/Sub Tasarımı
//string exchangeName = "example-pub-sub-exchange";
//channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);

//string queuName = channel.QueueDeclare().QueueName;
//channel.QueueBind(queue: queuName, exchange: exchangeName, routingKey: string.Empty);

//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(queue: queuName, autoAck: false, consumer: consumer);
//consumer.Received += (sender, e) =>
//{
//	Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//};
#endregion
#region Work Queue Tasarımı
//string queueName = "example-work-queue";
//channel.QueueDeclare(queue: queueName, exclusive: false, durable: false, autoDelete: false);

//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(queue:queueName,autoAck:true,consumer:consumer);
////tüm consumerlar aynı iş yüküne ve görev dağılımına sahip olsun diye aşağıdaki konfigürasyon;
//channel.BasicQos(prefetchSize: 1, prefetchCount: 0, global: false);

//consumer.Received += (sender, e) =>
//{
//	Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//};
#endregion
#region Request/Response Tasarımı
string requestQueueName = "example-request-response-queue";
channel.QueueDeclare(queue: requestQueueName, durable: false, exclusive: false, autoDelete: false);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue:requestQueueName,autoAck:true,consumer:consumer);

consumer.Received += (sender, e) =>
{
	string message=Encoding.UTF8.GetString(e.Body.Span);
	Console.WriteLine(message);
	//......
	byte[] responseMessage= Encoding.UTF8.GetBytes($"İşlem tamamlandı: {message}");

	IBasicProperties properties=channel.CreateBasicProperties();
	properties.CorrelationId= e.BasicProperties.CorrelationId;
	channel.BasicPublish(exchange:string.Empty,routingKey:e.BasicProperties.ReplyTo,basicProperties:properties,body:responseMessage);
};
#endregion
Console.Read();