using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://hivihrwt:4yeSDzme81G9-i1hzgFDl_TBE7ctvqjg@sparrow.rmq.cloudamqp.com/hivihrwt");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(
	exchange: "header-exchange-example",
	type: ExchangeType.Headers);

for (int i = 0; i < 100; i++)
{
	await Task.Delay(200);
	byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");
	Console.Write("Lütfen header value'sunu giriniz : ");
	string value = Console.ReadLine();

	IBasicProperties basicProperties = channel.CreateBasicProperties();
	basicProperties.Headers = new Dictionary<string, object>
	{
		["no"] = value
	};

	channel.BasicPublish(
		exchange: "header-exchange-example",
		routingKey: string.Empty,
		body: message,
		basicProperties: basicProperties
		);
}

Console.Read();