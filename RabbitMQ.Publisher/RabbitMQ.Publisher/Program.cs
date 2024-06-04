using RabbitMQ.Client;
using System.Text;

//Bağlantı oluşturma
ConnectionFactory factory = new();
factory.Uri = new("amqps://ctpgaytw:kMPeUt-BVWJT4h_zrgcNESx5-Eyp4bAa@sparrow.rmq.cloudamqp.com/ctpgaytw");

//Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection =factory.CreateConnection();
using IModel channel=connection.CreateModel();

//Queue(mesajın gönderileceği kuyruk) oluşturma 
//Bir kuyruk exclusive:true olarak ayarlanıyorsa, o kuyruk o bağlantıya özel oluşturulur ve daha sonra imha edilir.Yani consumer kuyruğa erişemez.
channel.QueueDeclare(queue: "example-queue", exclusive: false);

//Kuyruğa mesaj gönderme
//RabbitMQ kuyruğa atacağı mesajları byte türüenden kabul eder. Haliyle gönderilecek mesajları byte türüne dönüştürmemiz gerekir.
byte [] message = Encoding.UTF8.GetBytes("Merhaba");
channel.BasicPublish(exchange:"",routingKey: "example-queue", body:message);

Console.Read();