﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace ItemmApp.ViewModel;

public partial class PersonalDepartmentUpdateCadasterViewModel : ObservableObject
{
    [ICommand]
    async Task MoveBack() => await Shell.Current.GoToAsync("..");
}