//Bağlantı oluşturma
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://ctpgaytw:kMPeUt-BVWJT4h_zrgcNESx5-Eyp4bAa@sparrow.rmq.cloudamqp.com/ctpgaytw");

//Bağlantıyı açma ve kanal oluşturma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Queue oluşturma
//Consumerdaki kuyruk publisherdaki ile birebir aynı yapıda tanımlanmalıdır. 
channel.QueueDeclare(queue: "example-queue", exclusive: false);

//Queuedan mesaj okuma
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue", false, consumer);
consumer.Received += (sender, e) =>
{
	//kuyruğa gelen mesajın işlendiği yerdir.
	//e.Body: Kuyruktaki mesajın verisini bütünsel olarak getirecektir.
	//e.Body.Span veya e.Body.ToArray(): Kuyruktaki mesajın byte verisini getirecektir.
	Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();
