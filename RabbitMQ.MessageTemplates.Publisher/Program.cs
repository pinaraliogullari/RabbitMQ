
using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://ctpgaytw:kMPeUt-BVWJT4h_zrgcNESx5-Eyp4bAa@sparrow.rmq.cloudamqp.com/ctpgaytw");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

#region P2P Tasarımı
string queuName = "example-p2p-queue";
channel.QueueDeclare(queue:queuName,durable:false, exclusive:false,autoDelete:false);

byte[] message = Encoding.UTF8.GetBytes("merhaba");

channel.BasicPublish(exchange:string.Empty,routingKey:queuName,body:message);


#endregion

Console.Read();