
using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://ctpgaytw:kMPeUt-BVWJT4h_zrgcNESx5-Eyp4bAa@sparrow.rmq.cloudamqp.com/ctpgaytw");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

#region P2P Tasarımı
//string queuName = "example-p2p-queue";
//channel.QueueDeclare(queue:queuName,durable:false, exclusive:false,autoDelete:false);

//byte[] message = Encoding.UTF8.GetBytes("merhaba");

//channel.BasicPublish(exchange:string.Empty,routingKey:queuName,body:message);


#endregion
#region Pub/Sub Tasarımı

//string exchangeName = "example-pub-sub-exchange";
//channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);
//for (int i = 0; i < 100; i++)
//{
//	await Task.Delay(200);
//	byte[] message = Encoding.UTF8.GetBytes($"merhaba {i}");
//	channel.BasicPublish(exchange: exchangeName, routingKey: string.Empty, body: message);
//}


#endregion
#region Work Queue Tasarımı
string queueName = "example-work-queue";
channel.QueueDeclare(queue: queueName, exclusive: false, durable: false, autoDelete: false);

for (int i = 0; i < 100; i++)
{
	await Task.Delay(200);
	byte[] message= Encoding.UTF8.GetBytes($"Merhaba {i}");
	channel.BasicPublish(exchange:string.Empty,routingKey:queueName,body:message);
}
#endregion

Console.Read();