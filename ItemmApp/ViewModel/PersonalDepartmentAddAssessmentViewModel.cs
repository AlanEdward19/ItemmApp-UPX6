using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventarioMobile.ViewModels;
using ItemmApp.Interfaces;
using ItemmApp.Models.Request;
using ItemmApp.Models.Response;
using Microsoft.Toolkit.Mvvm.Input;

namespace ItemmApp.ViewModel;
[QueryProperty(nameof(Cpf), nameof(Cpf))]
[QueryProperty(nameof(Level), nameof(Level))]
[QueryProperty(nameof(Module), nameof(Module))]
[QueryProperty(nameof(AssessmentDate), nameof(AssessmentDate))]
[QueryProperty(nameof(Student), nameof(Student))]

public partial class PersonalDepartmentAddAssessmentViewModel : BaseViewModel
{
    private  readonly IAssessmentRepository _assessmentRepository;

    public StudentResponse Student { get; set; }
    public string Cpf { get; set; }
    public string Level { get; set; }
    public string Module { get; set; }
    public DateTime AssessmentDate { get; set; }

    [ObservableProperty] int skillTechnique = 1;
    [ObservableProperty] int participation = 1;
    [ObservableProperty] int interPersonalRelationship = 1;
    [ObservableProperty] int goalFulfillment = 1;

    public PersonalDepartmentAddAssessmentViewModel(IAssessmentRepository assessmentRepository)
    {
        _assessmentRepository = assessmentRepository;
    }

    [RelayCommand]
    public async Task AddAssessment()
    {
        var request = new InsertAssessmentRequest(Cpf, Level, Module, SkillTechnique, Participation,
            InterPersonalRelationship, GoalFulfillment, AssessmentDate);

        bool sucess = await _assessmentRepository.AddAsync(request);

        if (sucess)
        {
            await Shell.Current.DisplayAlert("Sucesso", "Avaliação foi inserida com sucesso", "OK");

            var navigationParams = new Dictionary<string, object>
            {
                {"Student", Student}
            };

            await Shell.Current.GoToAsync(nameof(PersonalDepartmentAssessmentPage), navigationParams);
            return;
        }

        await Shell.Current.DisplayAlert("Atenção", "Houve um erro ao tentar inserir avaliação, tente novamente!", "OK");
    }
}