using System.Threading.Tasks;

public interface ISendMailService {
    Task SendMail(MailContent mailContent);
}