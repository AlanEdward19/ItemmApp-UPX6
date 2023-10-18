using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventarioMobile.ViewModels;
using ItemmApp.Enums;
using ItemmApp.Interfaces;
using ItemmApp.Models.Request;
using ItemmApp.Models.Response;
using Microsoft.Toolkit.Mvvm.Input;

namespace ItemmApp.ViewModel;
[QueryProperty(nameof(Student), nameof(Student))]
public partial class PersonalDepartmentAttendanceViewModel : BaseViewModel
{
    public StudentResponse Student { get; set; }
    private readonly IAttendanceRepository _attendanceRepository;

    public PersonalDepartmentAttendanceViewModel(IAttendanceRepository attendanceRepository)
    {
        _attendanceRepository = attendanceRepository;
    }

    [ObservableProperty] DateTime attendanceDate = DateTime.Today;
    [ObservableProperty] EAttendance tipo;
    [ObservableProperty] string level = "";
    [ObservableProperty] string module = "";

    [RelayCommand]
    public async Task AddAttendance()
    {
        var request = new InsertAttendanceRequest(Student.Cpf, Level, Module, Tipo, AttendanceDate);

        bool sucess = await _attendanceRepository.AddAsync(request);

        if (sucess)
        {
            await Shell.Current.DisplayAlert("Sucesso", "Chamada feita com sucesso", "OK");

            await Shell.Current.GoToAsync(nameof(PersonalDepartmentPage));
            return;
        }

        await Shell.Current.DisplayAlert("Atenção", "Houve um erro ao tentar fazer chamada, tente novamente!", "OK");
    }
}