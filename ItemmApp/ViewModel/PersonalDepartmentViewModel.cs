using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using InventarioMobile.ViewModels;
using ItemmApp.Models.Response;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ItemmApp.Interfaces;

namespace ItemmApp.ViewModel;

public partial class PersonalDepartmentViewModel : BaseViewModel
{
    public ObservableCollection<StudentResponse> Students { get; set; }
        = new();

    public ObservableCollection<ClassResponse> Classes { get; set; }
        = new();

    private readonly IStudentRepository _studentRepository;
    private readonly IClassRepository _classRepository;

    public PersonalDepartmentViewModel(IStudentRepository studentRepository, IClassRepository classRepository)
    {
        _studentRepository = studentRepository;
        _classRepository = classRepository;
    }

    internal async Task InitAsync()
    {
        IsBusy = true;

        var students = await _studentRepository.GetStudentsAsync();
        var classes = await _classRepository.GetClassesAsync();

        Students.Clear();
        Classes.Clear();

        foreach (var student in students)
            Students.Add(student);

        foreach (var @class in classes)
            Classes.Add(@class);

        IsBusy = false;
    }

    [ICommand]
    async Task MoveToCadasterScreen() => await Shell.Current.GoToAsync(nameof(PersonalDepartmentCadasterPage));

    [ICommand]
    async Task MoveToUpdateCadasterScreen() => await Shell.Current.GoToAsync(nameof(PersonalDepartmentUpdateCadasterPage));

    [ICommand]
    async Task MoveToAttendanceScreen() => await Shell.Current.GoToAsync(nameof(PersonalDepartmentAttendancePage));

    [ICommand]
    async Task MoveToAssessmentScreen() => await Shell.Current.GoToAsync(nameof(PersonalDepartmentAssessmentPage));
}