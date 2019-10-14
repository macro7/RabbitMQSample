using System;

namespace RabbitMQFanout.Customer
{
    class Program
    {
        static void Main(string[] args)
        {
            // 构造MQ，建立连接
            RabbitMQHelper mQHelper = new RabbitMQHelper(
                hostName: "39.98.252.59",
                userName: "admin",
                password: "jOVZlgJU0Xn_81FNFUa4",
                port: 5672);

            mQHelper.Customer("callpaient_topic_exchange");
            // 委托绑定接收消息
            mQHelper.Topic_Customer_Message += S_Topic_Customer_Message;
        }

        /// <summary>
        /// 接收到消息 ，在这里做处理 
        /// </summary>
        /// <param name="message"></param>
        private static void S_Topic_Customer_Message(string message)
        {
            Console.WriteLine(" [x] {0}", message);
        }

        ////RabbitMQHelper mQHelper = new RabbitMQHelper(
        ////   hostName: "39.98.252.59",
        ////   userName: "admin",
        ////   password: "jOVZlgJU0Xn_81FNFUa4",
        ////   port: 5672);
        ////mQHelper.Topic_Customer(exchange: "callpaient_topic_exchange");
        ////    mQHelper.Topic_Customer_Message += MQHelper_Topic_Customer_Message;
        ////    Console.ReadKey();
        //private static void Consumer_Received1(object sender, BasicDeliverEventArgs e)
        //{
        //    var body = e.Body;
        //    var message = Encoding.UTF8.GetString(body);
        //    Console.WriteLine(" [x] {0}", message);
        //}

        //static void Main111(string[] args)
        //{
        //    string hostName = "39.98.252.59";
        //    string userName = "admin";
        //    string password = "jOVZlgJU0Xn_81FNFUa4";
        //    int port = 5672;
        //    string queue = "callpaient_queue";
        //    var factory = new ConnectionFactory()
        //    {
        //        HostName = hostName,
        //        UserName = userName,
        //        Password = password,
        //        Port = port,
        //        //Uri = hostName,
        //        VirtualHost = "/"
        //    };
        //    using (var connection = factory.CreateConnection())
        //    {
        //        using (var channel = connection.CreateModel())
        //        {
        //            #region EventingBasicConsumer
        //            //定义一个EventingBasicConsumer消费者                                    
        //            var consumer = new EventingBasicConsumer(channel);
        //            //接收到消息时触发的事件
        //            consumer.Received += (model, ea) =>
        //            {

        //                Console.WriteLine(Encoding.UTF8.GetString(ea.Body));
        //            };
        //            Console.WriteLine("消费者准备就绪....");
        //            //调用消费方法 queue指定消费的队列，autoAck指定是否自动确认，consumer就是消费者对象
        //            channel.BasicConsume(queue: queue,
        //                                   noAck: false,
        //                                   consumer: consumer);
        //            Console.ReadKey();
        //            #endregion
        //        }
        //    }
        //}

        //private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
