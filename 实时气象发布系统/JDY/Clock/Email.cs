using System;
using System.Net.Mail;
using System.Windows.Forms;

namespace mainView.Clock
{
    internal class Email
    {
        /// <summary>
        /// 发送预警邮件
        /// </summary>
        /// <param name="address">收件人地址</param>
        /// <param name="content">消息内容</param>
        /// <returns ></returns>
        public static void SendEmail(string address, string content)
        {
            MailMessage mail = new MailMessage();
            mail.Subject = "RealTimeWeather预警提醒";//设置邮件的标题
            mail.From = new MailAddress("1421771020@qq.com", "RealTimeWeather管理员"); //设置邮件的发件人
            address = address.Replace(',', ';');
            string[] addressList = address.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (addressList.Length < 1)
            {
                MessageBox.Show("收件人地址为空,请仔细核对预警设置", "提示");
            }

            //设置邮件的收件人
            mail.To.Add(addressList[0]);
            //设置邮件的抄送收件人
            for (int i = 1; i < addressList.Length; i++)
            {
                mail.CC.Add(addressList[i]);
            }

            mail.Body = content;//设置邮件的内容
            mail.BodyEncoding = System.Text.Encoding.UTF8;//设置邮件的格式
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;//设置邮件的发送级别
                                                //mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess | DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.Delay;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.None;
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.qq.com";//设置用于 SMTP 事务的主机的名称
            client.Timeout = 1000 * 60 * 10;
            //设置用于 SMTP 事务的端口，默认的是 25
            client.Port = 25;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(@"1421771020@qq.com", @"medgztnfxybfigjh");//邮箱登陆名和密码
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(mail);
            MessageBox.Show("发送成功", "提示"); ;
        }
    }
}