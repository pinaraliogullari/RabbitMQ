using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://hivihrwt:4yeSDzme81G9-i1hzgFDl_TBE7ctvqjg@sparrow.rmq.cloudamqp.com/hivihrwt");

using IConnection connection= factory.CreateConnection();
using IModel channel= connection.CreateModel();

//Fanout exchange te bu exchange e bind olmuş tüm kuyruklara ilgili mesajlar ulaşır. Mesaj belli bir routing keye değil de tüm kuyruklara gideceği için routing keyi boş verdim.
channel.ExchangeDeclare(exchange: "fanout-exchange-example", type: ExchangeType.Fanout);

for (int i = 0; i < 100; i++)
{
	await Task.Delay(200);
	byte[] message= Encoding.UTF8.GetBytes($"Merhaba {i}");
	channel.BasicPublish(exchange: "fanout-exchange-example", routingKey: String.Empty, body: message);
}
Console.Read();
