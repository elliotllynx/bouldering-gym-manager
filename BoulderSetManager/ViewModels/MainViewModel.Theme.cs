using CommunityToolkit.Mvvm.ComponentModel;

namespace BoulderSetManager.ViewModels
{
    public partial class MainViewModel
    {
        [ObservableProperty]
        public partial bool IsDarkMode { get; set; } = Application.Current.RequestedTheme == AppTheme.Dark;

        partial void OnIsDarkModeChanged(bool value)
        {
            Application.Current!.UserAppTheme = value ? AppTheme.Dark : AppTheme.Light;
        }
    }
}