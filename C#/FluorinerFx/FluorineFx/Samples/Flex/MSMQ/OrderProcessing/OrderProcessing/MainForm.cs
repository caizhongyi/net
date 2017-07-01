using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Messaging;

using ServiceLibrary;

namespace OrderProcessing
{
    public partial class MainForm : Form
    {
        int _counter;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            messageQueueIn.BeginReceive(new TimeSpan(1, 0, 0), messageQueueIn);
        }

        private void messageQueueIn_ReceiveCompleted(object sender, System.Messaging.ReceiveCompletedEventArgs e)
        {
            System.Messaging.Message message = ((MessageQueue)e.AsyncResult.AsyncState).EndReceive(e.AsyncResult);
            Order order = message.Body as Order;
            order.Id = ++_counter;

            System.Messaging.Message notification =  new System.Messaging.Message();
            notification.Formatter = new System.Messaging.XmlMessageFormatter(new string[] { "ServiceLibrary.Order,ServiceLibrary" });
            notification.Body = order;
            messageQueueNotifications.Send(notification);
            messageQueueBackOffice.Send(notification);
            IAsyncResult asyncResult = ((MessageQueue)e.AsyncResult.AsyncState).BeginReceive(new TimeSpan(1, 0, 0), ((MessageQueue)e.AsyncResult.AsyncState));
        }
    }
}