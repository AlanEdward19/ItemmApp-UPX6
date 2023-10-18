using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using InventarioMobile.ViewModels;
using ItemmApp.Models.Response;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.Input;
using ItemmApp.Helpers;
using ItemmApp.Interfaces;

namespace ItemmApp.ViewModel;

public partial class PersonalDepartmentViewModel : BaseViewModel
{
    public ObservableCollection<StudentResponse> Students { get; set; }
        = new();

    public ObservableCollection<StudentResponse> FilteredStudents { get; set; }
        = new();

    [ObservableProperty] 
    public StudentResponse? selectedStudent;

    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            SetProperty(ref _name, value);
            FilterStudents();
        }
    }

    private ClassResponse _selectedClass;
    public ClassResponse SelectedClass
    {
        get => _selectedClass;
        set
        {
            if (SetProperty(ref _selectedClass, value))
            {
                FilterStudents();
            }
        }
    }

    public ObservableCollection<ClassResponse> Classes { get; set; }
        = new();

    private readonly IStudentRepository _studentRepository;
    private readonly IClassRepository _classRepository;

    public PersonalDepartmentViewModel(IStudentRepository studentRepository, IClassRepository classRepository)
    {
        _studentRepository = studentRepository;
        _classRepository = classRepository;
    }

    internal async Task UpdateClassesList()
    {
        var classes = await _classRepository.GetClassesAsync();
        Classes.Clear();

        foreach (var @class in classes)
            Classes.Add(@class);
    }

    internal async Task UpdateStudentsList()
    {
        var students = await _studentRepository.GetStudentsAsync();

        Students.Clear();

        foreach (var student in students)
            Students.Add(student);
    }

    internal async Task InitAsync()
    {
        IsBusy = true;

        await UpdateStudentsList();
        await UpdateClassesList();

        FilterStudents();

        IsBusy = false;
    }

    [ICommand]
    async Task MoveToCadasterScreen() => await Shell.Current.GoToAsync(nameof(PersonalDepartmentCadasterPage));

    [ICommand]
    async Task MoveBackToMain() => await Shell.Current.GoToAsync(nameof(MainPage));

    [ICommand]
    async Task MoveToUpdateCadasterScreen()
    {
        if (selectedStudent == null)
        {
            await Shell.Current.DisplayAlert("Atenção", "Nenhum aluno foi selecionado, impossivel prosseguir com deleção", "OK");
            return;
        }

        var navigationParams = new Dictionary<string, object>
        {
            {"Student", selectedStudent }
        };

        await Shell.Current.GoToAsync(nameof(PersonalDepartmentUpdateCadasterPage), navigationParams);
    }

    [ICommand]
    async Task MoveToAttendanceScreen(StudentResponse student)
    {
        var navigationParams = new Dictionary<string, object>
        {
            {"Student", student }
        };

        await Shell.Current.GoToAsync(nameof(PersonalDepartmentAttendancePage), navigationParams);
    }

    [ICommand]
    async Task MoveToAssessmentScreen(StudentResponse student)
    {
        var navigationParams = new Dictionary<string, object>
        {
            {"Student", student }
        };

        await Shell.Current.GoToAsync(nameof(PersonalDepartmentAssessmentPage), navigationParams);
    }

    [RelayCommand]
    public async Task GenerateCertificate()
    {
        //await Shell.Current.DisplayAlert("Atenção", "Presença do aluno inferior a 75%.", "OK");
        try
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string docxTemplatePath = Path.Combine(basePath, "../../../../../Files", "CertificadoIttem.docx");

            var resultado = await FolderPicker.PickAsync(new CancellationToken());
            string docxOutputPath = Path.Combine(resultado.Folder.Path, $"{selectedStudent.Name}-Certificate.docx");

            // Dicionário de substituição
            Dictionary<string, string> changes = new Dictionary<string, string>
            {

                {"rg do aluno", Converter.FormatCpf(selectedStudent.Cpf) },
                {"nome da empresa", selectedStudent.CompanyName},
                {"função do aluno", selectedStudent.FunctionName},
                {"data1", selectedStudent.AdmissionDate.ToString("dd/MM/yyyy")},
                {"data2", selectedStudent.EndDate.ToString("dd/MM/yyyy")}
            };

            if (resultado != null)
            {
                Converter.FillDOCX(docxTemplatePath, docxOutputPath, changes);

                // Informe ao usuário que o arquivo foi salvo
                await Shell.Current.DisplayAlert("Sucesso", "Arquivo salvo com sucesso!", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Erro", $"Erro: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    public async Task DeleteStudent()
    {
        if (selectedStudent == null)
        {
            await Shell.Current.DisplayAlert("Atenção", "Nenhum aluno foi selecionado, impossivel prosseguir com deleção", "OK");
            return;
        }

        bool answer = await Shell.Current.DisplayAlert("Atenção", $"Deseja mesmo excluir o Aluno: {selectedStudent.Name}?", "Sim", "Não");

        if (answer)
        {
            bool sucess = await _studentRepository.DeleteAsync(selectedStudent.Cpf);

            if (sucess)
            {
                IsBusy = true;

                selectedStudent = null;

                await UpdateStudentsList();

                IsBusy = false;

                await Shell.Current.DisplayAlert("Sucesso", "Aluno foi deletado com sucesso", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Atenção", "Houve um erro ao tentar deletar aluno, tente novamente!", "OK");
            }
        }
        
    }

    private void FilterStudents()
    {
        // Lógica para filtrar alunos com base nos critérios (Nome e Turma)
        var filteredStudents = Students;

        if (!string.IsNullOrEmpty(Name))
        {
            filteredStudents = new ObservableCollection<StudentResponse>(filteredStudents.Where(a => a.Name.Contains(Name, StringComparison.InvariantCultureIgnoreCase)));
        }

        if (SelectedClass != null)
        {
            filteredStudents = new ObservableCollection<StudentResponse>(filteredStudents.Where(a => a.ClassName == SelectedClass.Name));
        }

        FilteredStudents.Clear();

        foreach (var value in filteredStudents)
            FilteredStudents.Add(value);
    }

}