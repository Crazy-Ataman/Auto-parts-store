using VintageParts.Database;
using VintageParts.Models;
using VintageParts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VintageParts.Services
{
    public static class EmailSenderService
    {
        public static async Task SendCodeRefactor(string UserMail, int code, string subject, string body)
        {
            try
            {
                MailAddress from = new MailAddress("vintageparts2022@gmail.com", "VintageParts");
                MailAddress to = new MailAddress(UserMail);
                MailMessage message = new MailMessage(from, to);
                message.Subject = subject;
                message.Body = body + code.ToString();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("vintageparts2022@gmail.com", "akzqkexukogculsf");
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public static async Task SendTicket(string UserMail, string subject, string body)
        {
            try
            {
                MailAddress from = new MailAddress("vintageparts2022@gmail.com", "VintageParts");
                MailAddress to = new MailAddress(UserMail);
                MailMessage message = new MailMessage(from, to);
                message.Subject = subject;
                message.Body = body;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("vintageparts2022@gmail.com", "akzqkexukogculsf");
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public static string GenerateTicket(Order order)
        {
            using (PartShopDbContext db = new PartShopDbContext()) 
            {
                double sum = 0;
                var orderParts = from o in db.Orders
                                 join op in db.OrderedParts
                                 on o.Order_id equals op.Order_id
                                 join p in db.Parts
                                 on op.Part_id equals p.Part_id
                                 where o.Order_id == order.Order_id
                                 select new
                                 {
                                     Name = p.Name,
                                     Amount = op.Amount,
                                     Price = p.Price,
                                     Brand = p.Brand.Name
                                 };
                StringBuilder body = new StringBuilder();
                switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "ru-RU":
                        {
                            body.Append($"Чек заказа {order.Order_id} из магазина автозапастей VintageParts");
                            body.Append("\n" + order.OrderDate);
                            body.Append("\n-----------------------------------");
                            foreach (var p in orderParts)
                            {
                                body.Append($"\nЗапчасть: {p.Name}, Количество: {p.Amount}, Цена: {p.Price}, Бренд: {p.Brand}");
                                sum += p.Price * p.Amount;
                            }
                            body.Append("\n-----------------------------------");
                            body.Append($"\nДоставка: {order.Delivery.Name}");
                            body.Append($"\nИтоговая сумма: {sum + order.Delivery.Price}");
                            body.Append("\n-----------------------------------");
                            body.Append("\nСпасибо за заказ в нашем магазине!");
                            return body.ToString();
                        }
                    default:
                        {
                            body.Append($"Order receipt {order.Order_id} from VintageParts auto parts store.");
                            body.Append("\n" + order.OrderDate);
                            body.Append("\n-----------------------------------");
                            foreach (var p in orderParts)
                            {
                                body.Append($"\nPart: {p.Name}, Quantity: {p.Amount}, Price: {p.Price}, Brand: {p.Brand}");
                                sum += p.Price * p.Amount;
                            }
                            body.Append("\n-----------------------------------");
                            body.Append($"\nDelivery: {order.Delivery.Name}");
                            body.Append($"\nTotal amount: {sum + order.Delivery.Price}");
                            body.Append("\n-----------------------------------");
                            body.Append("\nThank you for ordering in our store!");
                            return body.ToString();
                        }
                }
            }
        }
    }
}
