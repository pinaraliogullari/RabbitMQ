﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://ctpgaytw:kMPeUt-BVWJT4h_zrgcNESx5-Eyp4bAa@sparrow.rmq.cloudamqp.com/ctpgaytw");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

#region P2P Tasarımı
string queueName = "example-p2p-queue";
channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

consumer.Received += (sender, e) =>
{
	Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};
#endregion


Console.Read();