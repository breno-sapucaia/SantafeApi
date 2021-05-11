using System;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SantafeApi.Services
{
	public class MailService
	{
		private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
			_configuration = configuration;
        }

		public void SendEmail(string to, string token)
		{
            var link = $"http://localhost:3000/recuperar/{token}";
			var text = $"<h1>Santafe</h1>" +
				$"<small>INSPEÇÕES, MANUTENÇÃO E TREINAMENTOS. A SERVIÇO DA SEGURANÇA</small>" +
				$"</br><p>Clique no <a href='{link}' target='blank'> aqui </a> para resetar a senha</p>" +
				$"<h2>Contato: </h2>" +
				$"<p><a href='tel:(11) 3105-2248'> (11) 3105-2248</a></p>"; 
				
				
				
				
					
				
			var message = new MimeMessage();

			message.From.Add(MailboxAddress.Parse(_configuration["email:user"]));
			message.To.Add(MailboxAddress.Parse(to));
			message.Subject = "Redefinir senha";
			message.Body = new TextPart("html")
			{
				Text = text
			};

            using var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate(_configuration["email:user"], _configuration["email:pass"]);
            client.Send(message);
            client.Disconnect(true);
        }
	}
}
