namespace OrderProcessing
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.messageQueueIn = new System.Messaging.MessageQueue();
            this.messageQueueNotifications = new System.Messaging.MessageQueue();
            this.messageQueueBackOffice = new System.Messaging.MessageQueue();
            this.SuspendLayout();
            // 
            // messageQueueIn
            // 
            this.messageQueueIn.Formatter = new System.Messaging.BinaryMessageFormatter(System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple, System.Runtime.Serialization.Formatters.FormatterTypeStyle.TypesAlways);
            this.messageQueueIn.MessageReadPropertyFilter.LookupId = true;
            this.messageQueueIn.Path = ".\\private$\\orderqueuein";
            this.messageQueueIn.SynchronizingObject = this;
            this.messageQueueIn.ReceiveCompleted += new System.Messaging.ReceiveCompletedEventHandler(this.messageQueueIn_ReceiveCompleted);
            // 
            // messageQueueNotifications
            // 
            this.messageQueueNotifications.Formatter = new System.Messaging.XmlMessageFormatter(new string[] {
            "ServiceLibrary.Order,ServiceLibrary"});
            this.messageQueueNotifications.MessageReadPropertyFilter.LookupId = true;
            this.messageQueueNotifications.Path = ".\\private$\\orderqueuenotifications";
            this.messageQueueNotifications.SynchronizingObject = this;
            // 
            // messageQueueBackOffice
            // 
            this.messageQueueBackOffice.Formatter = new System.Messaging.XmlMessageFormatter(new string[0]);
            this.messageQueueBackOffice.MessageReadPropertyFilter.LookupId = true;
            this.messageQueueBackOffice.Path = ".\\private$\\backoffice";
            this.messageQueueBackOffice.SynchronizingObject = this;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 259);
            this.Name = "MainForm";
            this.Text = "Order Processing";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Messaging.MessageQueue messageQueueIn;
        private System.Messaging.MessageQueue messageQueueNotifications;
        private System.Messaging.MessageQueue messageQueueBackOffice;
    }
}

