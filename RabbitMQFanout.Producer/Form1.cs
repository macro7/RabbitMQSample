using System;
using System.Windows.Forms;

namespace RabbitMQFanout.Producer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var message = richTextBox1.Text.Trim();

            RabbitMQHelper mQHelper = new RabbitMQHelper(
                hostName: "39.98.252.59",
                userName: "admin",
                password: "jOVZlgJU0Xn_81FNFUa4",
                port: 5672);

            mQHelper.Producer(message, "callpaient_topic_exchange");
        }

    }
}
