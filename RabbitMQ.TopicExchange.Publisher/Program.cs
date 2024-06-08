
using RabbitMQ.Client;
using System.Text;
using System.Text.Unicode;

ConnectionFactory factory = new();
factory.Uri = new("amqps://hivihrwt:4yeSDzme81G9-i1hzgFDl_TBE7ctvqjg@sparrow.rmq.cloudamqp.com/hivihrwt");

using IConnection connection = factory.CreateConnection();
using IModel channel=connection.CreateModel();

channel.ExchangeDeclare(exchange: "topic-exchange-example", type: ExchangeType.Topic);

for (int i = 0; i < 100; i++)
{
	await Task.Delay(200);
	byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");
	Console.Write("Topic giriniz: ");
	string topic=Console.ReadLine();

	channel.BasicPublish(exchange:"topic-exchange-example",routingKey: topic,body:message);

	Console.Read();
}
