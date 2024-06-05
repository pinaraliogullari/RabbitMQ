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
channel.QueueDeclare(queue: "example-queue", exclusive: false,durable:true); //durable:true ile kuyruğu kalıcı yaptım.

//Kuyruğa mesaj gönderme
//RabbitMQ kuyruğa atacağı mesajları byte türünden kabul eder. Haliyle gönderilecek mesajları byte türüne dönüştürmemiz gerekir.
IBasicProperties properties = channel.CreateBasicProperties();
properties.Persistent = true;
byte [] message = Encoding.UTF8.GetBytes("Merhaba");
channel.BasicPublish(exchange:"",routingKey: "example-queue", body:message, basicProperties: properties); //basicProperties: properties ile mesajı kalıcı yaptım 

Console.Read();