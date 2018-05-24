using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace xDPx_Toolbox
{
    public partial class ErrorReporter : Form
    {
        public ErrorReporter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please wait while an Error Is Being Sent", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);

            try
            {


                //===========================================================================================
                //============================ Email Code ===================================================
                //===========================================================================================

                string imageurl = "http://" + "i.imgur.com/lUMHjfU.png" + "width=" + 50 + "%height=" + 50 + "%";
                var fromAddress = new MailAddress("pexploitcustom@gmail.com", "Error Reporting PeXploit");
                var toAddress = new MailAddress("pexploitcustom@gmail.com", textBox1.Text + "Error Report PeXploit V0.7+");
                const string fromPassword = "PeXploit082";
                string error = System.IO.File.ReadAllText(Application.StartupPath + @"\errorlog.txt");
                string subject = "xDPx Toolbox Error Report";
                string body =
                    @"<html>" + "<body>" + "<table>" + "<tr>" + "<thead><img src= " + imageurl + "/></thead>" + "<td>User Name : </td><td>" + textBox1.Text + "</td>" + "</tr>" +
                    "<tr>" + "<td> Error : </td><td>" + error + "</td>" + "<tfoot>Additional Information " + textBox2.Text + "OS: " + comboBox1.Text + "</tfoot></tr>" + "</table>" + "</body>" + "</html>";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = Convert.ToInt16("587"),
                    EnableSsl = true,
                    Timeout = 40000,
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                //============================================================================
                //================================== End Email Code ==========================
                //============================================================================

                MessageBox.Show("Report Sent");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't Send The Report");
            }
        }
    }
}
