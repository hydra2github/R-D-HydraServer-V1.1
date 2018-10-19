namespace Ocuco.Hydra.WebMVC21.V2.Services
{
    public interface IMailService
    {
        void SendMessage(string to, string subject, string body);
    }
}