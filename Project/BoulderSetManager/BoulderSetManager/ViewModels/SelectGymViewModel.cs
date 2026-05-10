using BoulderSetManager.Models.Entities;
using BoulderSetManager.Models.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DAL;

namespace BoulderSetManager.ViewModels
{
    public partial class SelectGymViewModel : ObservableObject
    {
        private readonly GymService _gymService = new GymService();
        [ObservableProperty] public partial ObservableCollection<GymDTO> Gyms { get; set; }
        [ObservableProperty] public partial GymDTO SelectedGym { get; set; } = null;

        public SelectGymViewModel()
        {
            using var db = new GymDbContext();
            db.Database.EnsureCreated();
            LoadGyms();
        }
        private async Task LoadGyms()
        {
            Gyms = new ObservableCollection<GymDTO>(await _gymService.GetAllGyms());
        }

        [RelayCommand]
        public async Task SelectGym()
        {
            if (SelectedGym == null) return;
            await Shell.Current.GoToAsync("MainView");
        }
        [RelayCommand]
        public async Task DeleteGym()
        {
            if (SelectedGym == null) return;
            await _gymService.DeleteGym(SelectedGym.Id);
            Gyms.Remove(SelectedGym);
            SelectedGym = null;
        }

        [ObservableProperty] public partial bool IsFormVisible { get; set; }  = false;
        [ObservableProperty] public partial bool IsCreateFormVisible { get; set; } = false;
        [ObservableProperty] public partial bool IsModifyFormVisible { get; set; } = false;
        [ObservableProperty] public partial string NewGymName { get; set; } = string.Empty;
        [ObservableProperty] public partial string NewGymLocation { get; set; } = string.Empty;
        [ObservableProperty] public partial bool HasError { get; set; } = false;
        [ObservableProperty] public partial string ErrorMessage { get; set; } = string.Empty;


        [RelayCommand]
        private void ShowCreateForm()
        {
            IsFormVisible = true;
            IsCreateFormVisible = true;
        }

        [RelayCommand]
        private void ShowModifyForm()
        {
            IsFormVisible = true;
            IsModifyFormVisible = true;
        }

        [RelayCommand]
        private void HideForm()
        {
            IsFormVisible = false;
            IsCreateFormVisible = false;
            IsModifyFormVisible = false;
            NewGymName = string.Empty;
            NewGymLocation = string.Empty;
            HasError = false;
        }
        

        [RelayCommand]
        private async Task CreateGym()
        {
            HasError = false;
            if (string.IsNullOrWhiteSpace(NewGymName)
                || string.IsNullOrWhiteSpace(NewGymLocation))
            {
                ErrorMessage = "Both Name and Location is required.";
                HasError = true;
                return;
            }
            await _gymService.CreateGym(NewGymName, NewGymLocation);
            await LoadGyms();
            HideForm();
        }

        [RelayCommand]
        private async Task ModifyGym()
        {
            HasError = false;
            if (string.IsNullOrWhiteSpace(NewGymName)
                && string.IsNullOrWhiteSpace(NewGymLocation))
            {
                ErrorMessage = "New Name or Location is required.";
                HasError = true;
                return;
            }

            await _gymService.UpdateGym(SelectedGym.Id, NewGymName, NewGymLocation);
            await LoadGyms();
            HideForm();
        }
    }
}
