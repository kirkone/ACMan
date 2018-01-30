namespace ACMan.Host.Services
{
    using System.Threading.Tasks;

    public interface IWebAppService
    {
        Task Start();
        Task Stop();
    }
}