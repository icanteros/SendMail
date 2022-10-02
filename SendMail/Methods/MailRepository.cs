using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMail.Methods
{
    public class MailRepository
    {
        public void SendPreloadMail(string mailType, int port)
        {

            Console.WriteLine("Destination Mail");
            var destinationEmail = Console.ReadLine();

            //here put you subject
            var subjectEmail = "your subject";

            var messageEmail = "your maessage";

            MimeMessage message = new MimeMessage();

            //change for you email and name/Bussines Name 
            message.From.Add(new MailboxAddress("Denis", "deni@typemail.com"));

            message.To.Add(MailboxAddress.Parse($"{destinationEmail}"));

            message.Subject = subjectEmail;

            message.Body = new TextPart("plain")
            {
                Text = messageEmail
            };

            //change for you email
            string emailAddress = "deni@typemail.com";

            //here put your password
            string password = "your password (if you use gmail you need the application pasword)";

            SmtpClient client = new SmtpClient();
            ExceptionHandling(message, emailAddress, password, client, mailType, port);
        }
        public void SendPersonalizedMail(string mailType, int port)
        {
            Console.WriteLine("Destination Mail");
            var destinationEmail = Console.ReadLine();

            Console.WriteLine("Subject Mail");
            var subjectEmail = Console.ReadLine();

            Console.WriteLine("Message Mail");
            var messageEmail = Console.ReadLine();

            Console.WriteLine("Display name for the Mail");
            var displayName = Console.ReadLine();

            Console.WriteLine("Source Mail");
            var surceMail = Console.ReadLine();

            Console.WriteLine("Password of Mail (if you use gmail you need the application pasword)");
            var password = Console.ReadLine();


            MimeMessage message = new MimeMessage();

            message.From.Add(new MailboxAddress($"{displayName}", $"{surceMail}"));

            message.To.Add(MailboxAddress.Parse($"{destinationEmail}"));

            message.Subject = subjectEmail;

            message.Body = new TextPart("plain")
            {
                Text = messageEmail
            };

            SmtpClient client = new SmtpClient();
            ExceptionHandling(message, surceMail, password, client, mailType, port);
        }
        private static void ExceptionHandling(MimeMessage message, string emailAddress, string password, SmtpClient client, string mailType, int port)
        {
            try
            {
                client.Connect($"{mailType}", port, true);
                client.Authenticate(emailAddress, password);
                client.Send(message);

                Console.WriteLine("Email sent");
                Console.ReadLine();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Email wasn't send beacuse {ex.Message}");
                Console.ReadLine();
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }

        public static string CollerctorType(int mailTyper)
        {
            string clientConnect;
            if (mailTyper == 1)
            {
                clientConnect = "smtp.gmail.com";
            }
            else if (mailTyper == 2)
            {
                clientConnect = "smtp-mail.outlook.com";
            }
            else
            {
                Console.WriteLine("We need to know how is de client connection, please search how is the client connection for <Client Type Mail>");
                Console.WriteLine("Please tipe your mail type");
                clientConnect = Console.ReadLine();
            }
            return clientConnect;
        }
        public static int CollerctorPort(int mailTyper)
        {
            int port;
            if (mailTyper == 1)
            {
                port = 465;
            }
            else if (mailTyper == 2)
            {
                port = 587;
            }
            else
            {
                Console.WriteLine("Please tipe your default port");
                port = Convert.ToInt32(Console.ReadLine());
            }
            return port;
        }
        public static void SecondSelection(string mailType, int port)
        {
            Console.WriteLine("Choose a opction\n1) Send a email preload\n2) Send a email personalized");
            var firstMenu = Convert.ToInt32(Console.ReadLine());
            if (firstMenu == 1)
            {
                Console.WriteLine("How many emails are you going to send?");
                var secondMenu = Convert.ToInt32(Console.ReadLine());

                for (int i = 1; i <= secondMenu; i++)
                {
                    var emailPreload = new MailRepository();
                    emailPreload.SendPreloadMail(mailType, port);
                }
            }
            else if (firstMenu == 2)
            {
                var emailPersonalized = new MailRepository();
                emailPersonalized.SendPersonalizedMail(mailType, port);
            }
        }
        public static void FirstSelection()
        {
            Console.WriteLine("Choose a opction\n1) Gmail type\n2) Hotmail Type\n3) Another");
            var selectType = Convert.ToInt32(Console.ReadLine());
            var type = CollerctorType(selectType);
            var port = CollerctorPort(selectType);
            SecondSelection(type, port);

        }
    }
}
