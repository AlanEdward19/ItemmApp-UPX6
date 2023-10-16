using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using InventarioMobile.ViewModels;
using Microsoft.Toolkit.Mvvm.Input;

namespace ItemmApp.ViewModel;

public partial class PersonalDepartmentAssessmentViewModel : BaseViewModel
{

    [ICommand]
    async Task MoveToAddAssessment() => await Shell.Current.GoToAsync(nameof(PersonalDepartmentAddAssessmentPage));
}