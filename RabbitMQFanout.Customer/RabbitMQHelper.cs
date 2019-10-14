using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQFanout.Customer
{
    class RabbitMQHelper
    {
        ConnectionFactory factory = null;

        /// <summary>
        /// RabbitMQ 构造函数
        /// </summary>
        /// <param name="hostName"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="port"></param>
        public RabbitMQHelper(string hostName, string userName, string password, int port = 5672)
        {
            factory = new ConnectionFactory()
            {
                HostName = hostName,
                UserName = userName,
                Password = password,
                Port = port,
                Protocol = Protocols.DefaultProtocol,
                AutomaticRecoveryEnabled = true,
            };
        }

        /// <summary>
        /// 生产者-广播消息
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="message"></param>
        public void Producer(string message, string exchange, string type = "topic")
        {
            using (var connection = factory.CreateConnection())
            {
                // 创建通道
                using (var channel = connection.CreateModel())
                {
                    // 创建名为test的exchange,类型为topic，持久性设为true
                    channel.ExchangeDeclare(exchange, type, true, false, null);
                    /*
                    这里不创建队列
                    */
                    // 创建两个队列，持久性均设为true
                    //channel.QueueDeclare("a", true, false, false, null);
                    //channel.QueueDeclare("b", true, false, false, null);
                    // 队列与exchange进行捆绑
                    //channel.QueueBind("a", exchangeName, "", null);
                    //channel.QueueBind("b", exchangeName, "", null);
                    // 发布消息
                    channel.BasicPublish(exchange, "", null, Encoding.UTF8.GetBytes(message));
                }
            }
        }

        /// <summary>
        /// 消费者-接收消息
        /// </summary>
        /// <param name="exchange"></param>
        public void Customer(string exchange, string type = "topic")
        {
            // 建立连接
            IConnection connection = factory.CreateConnection();
            // 创建通道
            IModel channel = connection.CreateModel();
            // 声明exchange，exchange名与刚才发布的
            channel.ExchangeDeclare(exchange, type, durable: true);
            // 读取exchange下的队列
            string queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queueName, exchange, "");
            // 创建消费者对象
            var consumer = new EventingBasicConsumer(channel);
            // 订阅消息，实时获取消息
            consumer.Received += Consumer_Received;
            channel.BasicConsume(queue: queueName,
                                 noAck: true,
                                 consumer: consumer);
        }


        public delegate void dTopic_Customer_Message(string message);
        public event dTopic_Customer_Message Topic_Customer_Message;
        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            var message = Encoding.UTF8.GetString(body);
            Topic_Customer_Message?.Invoke(message);
        }
    }
}
