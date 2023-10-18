using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using InventarioMobile.ViewModels;
using ItemmApp.Models.Response;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using ItemmApp.Interfaces;

namespace ItemmApp.ViewModel;
[QueryProperty(nameof(Student), nameof(Student))]
public partial class PersonalDepartmentAssessmentViewModel : BaseViewModel
{
    private readonly IAssessmentRepository _assessmentRepository;
    public ObservableCollection<AssessmentResponse> Assessments { get; set; }
        = new();

    [ObservableProperty]
    public AssessmentResponse? selectedAssessment;

    public StudentResponse Student { get; set; }


    public PersonalDepartmentAssessmentViewModel(IAssessmentRepository assessmentRepository)
    {
        _assessmentRepository = assessmentRepository;
    }

    [ObservableProperty] string level = "";
    [ObservableProperty] string module = "";
    [ObservableProperty] DateTime assessmentDate = DateTime.Today;

    internal async Task UpdateAssessmentsList()
    {
        var assessments = await _assessmentRepository.GetAssessmentsAsync(Student.Cpf);
        Assessments.Clear();

        foreach (var assessment in assessments)
            Assessments.Add(assessment);
    }

    internal async Task InitAsync()
    {
        IsBusy = true;

        await UpdateAssessmentsList();

        IsBusy = false;
    }

    [ICommand]
    async Task MoveToAddAssessment()
    {
        var navigationParams = new Dictionary<string, object>
        {
            {"Cpf", Student.Cpf },
            {"Level", Level},
            {"Module", Module},
            {"AssessmentDate", AssessmentDate},
            {"Student", Student}
        };

        await Shell.Current.GoToAsync(nameof(PersonalDepartmentAddAssessmentPage), navigationParams);
    }

    [RelayCommand]
    public async Task DeleteAssessment()
    {
        if (selectedAssessment == null)
        {
            await Shell.Current.DisplayAlert("Atenção", "Nenhuma avaliação foi selecionada, impossivel prosseguir com deleção", "OK");
            return;
        }

        bool answer = await Shell.Current.DisplayAlert("Atenção", $"Deseja mesmo excluir essa avaliação?", "Sim", "Não");

        if (answer)
        {
            bool sucess = await _assessmentRepository.DeleteAsync(selectedAssessment.Id.ToString());

            if (sucess)
            {
                IsBusy = true;

                selectedAssessment = null;

                await UpdateAssessmentsList();

                IsBusy = false;

                await Shell.Current.DisplayAlert("Sucesso", "Avaliação foi deletada com sucesso", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Atenção", "Houve um erro ao tentar deletar avaliação, tente novamente!", "OK");
            }
        }
    }
}