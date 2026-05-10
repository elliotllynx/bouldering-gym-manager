using BoulderSetManager.Views;

namespace BoulderSetManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("MainView", typeof(MainView));
        }
    }
}
