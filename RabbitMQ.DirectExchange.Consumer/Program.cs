
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://hivihrwt:4yeSDzme81G9-i1hzgFDl_TBE7ctvqjg@sparrow.rmq.cloudamqp.com/hivihrwt");

using IConnection connection= factory.CreateConnection();
using IModel channel= connection.CreateModel();

//1.Adım: Publisherdaki exchane ile birebir aynı isim ve tipe sahip bir exchange tanımlanmalıdır.
channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

//2.Adım: Publisher tarafından routing keyde bulunan değerdeki kuyruğa gönderilen mesajları, kendi oluşturduğumuz kuyruğa yönlendirerek tüketmemiz gerekmektedir. Bunun için öncelikle bir kuyruk oluşturulmalıdır.
string queueName = channel.QueueDeclare().QueueName;

//3.Adım: Binding işlemi
channel.QueueBind(queue: queueName, exchange: "direct-exchange-example", routingKey: "direct-queue-example");


//4. Adım: Mesajı okuma işlemi
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
	queue:queueName, autoAck:true,consumer:consumer);
consumer.Received += (sender, e) =>
{
	string message = Encoding.UTF8.GetString(e.Body.Span);
	Console.WriteLine(message);
};
Console.Read();