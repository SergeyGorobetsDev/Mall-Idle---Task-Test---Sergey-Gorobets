namespace Assets.Project.Code.Runtime.Gameplay.Common.WindowsSystem
{
    public interface IWindowsNavigator
    {
        void CleanUp();
        void Dispose();
        Window GetActiveWindow();
        void Initialize();
        void Pop();
        void Show<T>() where T : Window;
        T ShowAndGetPopUp<T>() where T : PopUpWindow;
        void ShowPopUp<T>() where T : PopUpWindow;
    }
}