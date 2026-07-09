using BoulderSetManager.Models.Entities;
using BoulderSetManager.Models.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DAL.Enums;

namespace BoulderSetManager.ViewModels
{
    public partial class MainViewModel
    {

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPopUpVisible))]
        public partial bool IsAddBoulderVisible { get; set; } = false;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPopUpVisible))]
        public partial bool IsEditBoulderVisible { get; set; } = false;

        [ObservableProperty] public partial BoulderProblemDTO SelectedBoulder { get; set; }
        [ObservableProperty] public partial string NewGrade { get; set; } = string.Empty;
        [ObservableProperty] public partial BoulderStyle? NewStyle { get; set; } = null;
        [ObservableProperty] public partial string NewAuthor { get; set; } = string.Empty;
        [ObservableProperty] public partial DateTime NewBuiltDate { get; set; } = DateTime.Today;
        [ObservableProperty] public partial DateTime NewRetireDate { get; set; } = DateTime.Today.AddMonths(1);
        [ObservableProperty] public partial bool HasBoulderInputError { get; set; } = false;
        [ObservableProperty] public partial string BoulderInputErrorMessage { get; set; } = string.Empty;

        [RelayCommand]
        private void ShowAddBoulder(WallDTO wall)
        {
            NewBuiltDate = DateTime.Today;
            NewRetireDate = DateTime.Today.AddMonths(1);
            SelectedWall = wall;
            IsAddBoulderVisible = true;
        }

        [RelayCommand]
        private void ShowEditBoulder(BoulderProblemDTO boulder)
        {
            NewBuiltDate = boulder.BuiltDate;
            NewRetireDate = boulder.RetireDate;
            NewStyle = boulder.Style;
            SelectedBoulder = boulder;
            IsEditBoulderVisible = true;
        }

        [RelayCommand]
        private void HideBoulderForm()
        {
            IsAddBoulderVisible = false;
            IsEditBoulderVisible = false;
            NewGrade = string.Empty;
            NewStyle = null;
            NewAuthor = string.Empty;
            HasBoulderInputError = false;
            BoulderInputErrorMessage = string.Empty;
        }

        [RelayCommand]
        private async Task AddBoulder()
        {
            if (string.IsNullOrWhiteSpace(NewGrade)
                || NewStyle is null
                || string.IsNullOrWhiteSpace(NewAuthor))
            {
                HasBoulderInputError = true;
                BoulderInputErrorMessage = "All attributes must be filled in.";
                return;
            }
            if (!string.IsNullOrWhiteSpace(NewGrade) && !GradeHelper.IsValidGrade(NewGrade))
            {
                HasBoulderInputError = true;
                BoulderInputErrorMessage = "Grade must be in format e.g. 6A or 8B+";
                return;
            }
            if (NewBuiltDate >= NewRetireDate)
            {
                HasBoulderInputError = true;
                BoulderInputErrorMessage = "Built date cannot precede retire date.";
                return;
            }
            var boulder = new BoulderProblemDTO
            {
                Grade = NewGrade,
                Style = NewStyle.Value,
                Author = NewAuthor,
                BuiltDate = NewBuiltDate,
                RetireDate = NewRetireDate,
                WallId = SelectedWall.Id
            };

            SelectedWall.Boulders.Add(boulder);
            boulder.Id = await _boulderService.CreateBoulder(boulder);
            UpdateDynamicProperties();
            HideBoulderForm();
        }

        [RelayCommand]
        private async Task EditBoulder()
        {
            if (string.IsNullOrWhiteSpace(NewGrade)
                && NewStyle is null
                && string.IsNullOrWhiteSpace(NewAuthor)
                && NewBuiltDate == SelectedBoulder.BuiltDate
                && NewRetireDate == SelectedBoulder.RetireDate)
            {
                HasBoulderInputError = true;
                BoulderInputErrorMessage = "No changes were made.";
                return;
            }
            if (!string.IsNullOrWhiteSpace(NewGrade) && !GradeHelper.IsValidGrade(NewGrade))
            {
                HasBoulderInputError = true;
                BoulderInputErrorMessage = "Grade must be in format e.g. 6A or 8B+";
                return;
            }
            if (NewBuiltDate >= NewRetireDate)
            {
                HasBoulderInputError = true;
                BoulderInputErrorMessage = "Built date cannot precede retire date.";
                return;
            }
            if (!string.IsNullOrWhiteSpace(NewGrade)) SelectedBoulder.Grade = NewGrade;
            if (NewStyle != null) SelectedBoulder.Style = NewStyle.Value;
            if (!string.IsNullOrWhiteSpace(NewAuthor)) SelectedBoulder.Author = NewAuthor;
            SelectedBoulder.BuiltDate = NewBuiltDate;
            SelectedBoulder.RetireDate = NewRetireDate;
            await _boulderService.UpdateBoulder(SelectedBoulder);
            UpdateDynamicProperties();
            HideBoulderForm();
        }

        [RelayCommand]
        private async Task DeleteBoulder(BoulderProblemDTO boulder)
        {
            await _boulderService.DeleteBoulder(boulder.Id);
            var wall = Walls.FirstOrDefault(w => w.Id == boulder.WallId);
            wall?.Boulders.Remove(boulder);
            UpdateDynamicProperties();
        }
    }
}
