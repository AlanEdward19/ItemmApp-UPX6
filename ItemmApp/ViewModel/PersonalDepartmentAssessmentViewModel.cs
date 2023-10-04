using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace ItemmApp.ViewModel;

public partial class PersonalDepartmentAssessmentViewModel : ObservableObject
{
    [ICommand]
    async Task MoveBack() => await Shell.Current.GoToAsync("..");

    [ICommand]
    async Task MoveToAddAttendance() => await Shell.Current.GoToAsync(nameof(PersonalDepartmentAddAssessmentPage));
}