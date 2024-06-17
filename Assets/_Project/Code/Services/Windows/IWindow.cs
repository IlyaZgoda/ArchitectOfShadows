namespace Code.Services.Windows
{
    public interface IWindow
    {
        void Destroy();
        bool IsStillExist();
    }
}