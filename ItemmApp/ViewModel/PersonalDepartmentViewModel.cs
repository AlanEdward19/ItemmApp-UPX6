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
using System.Net;

namespace ItemmApp.ViewModel;

public partial class PersonalDepartmentViewModel : BaseViewModel
{
    private readonly IStudentRepository _studentRepository;
    private readonly IClassRepository _classRepository;
    private readonly ICertificateIssuanceRepository _certificateIssuanceRepository;

    public PersonalDepartmentViewModel(IStudentRepository studentRepository, IClassRepository classRepository, ICertificateIssuanceRepository certificateIssuanceRepository)
    {
        _studentRepository = studentRepository;
        _classRepository = classRepository;
        _certificateIssuanceRepository = certificateIssuanceRepository;
    }

    public List<StudentResponse> Students { get; set; }
        = new();

    public ObservableCollection<StudentResponse> FilteredStudents { get; set; }
        = new();

    public ObservableCollection<ClassResponse> Classes { get; set; }
        = new();

    [ObservableProperty]
    public StudentResponse? selectedStudent;


    private ClassResponse _selectedClass;
    public ClassResponse SelectedClass
    {
        get => _selectedClass;
        set
        {
            if (SetProperty(ref _selectedClass, value))
            {
                //FilterStudents();
            }
        }
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

        FilterStudents("");

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

    Stream GetTemplate(string url)
    {
        using WebClient webClient = new();
        return webClient.OpenRead(new Uri(url));
    }

    [RelayCommand]
    public async Task GenerateCertificate()
    {
        try
        {
            if (selectedStudent == null)
            {
                await Shell.Current.DisplayAlert("Atenção", "Nenhum aluno foi selecionado, impossivel prosseguir com deleção", "OK");
                return;
            }

            var response = await _certificateIssuanceRepository.GetCertificateIssuanceAsync(selectedStudent.Cpf);

            if (response.IsQualified)
            {
                string downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string docxOutputPath = Path.Combine(downloadsFolder, $"{selectedStudent.Name}-Certificate.docx");

                // Dicionário de substituição
                Dictionary<string, string> changes = new Dictionary<string, string>
                {

                    {"rg do aluno", Converter.FormatCpf(response.StudentCpf) },
                    {"nome da empresa", response.CompanyName},
                    {"função do aluno", response.StudentFunction},
                    {"data1", response.InitialDate.ToString("dd/MM/yyyy")},
                    {"data2", response.FinalDate.ToString("dd/MM/yyyy")}
                };

                string url = "https://cdn.discordapp.com/attachments/674738509457784852/1164302760930463765/CertificadoIttem.docx?ex=6542b871&is=65304371&hm=4cab46137774bca586fa0c80c32bed1b3cc0c91e8782d980ed53e32fce71b197&";
                Converter.FillDOCX(GetTemplate(url), docxOutputPath, changes);

                // Informe ao usuário que o arquivo foi salvo
                await Shell.Current.DisplayAlert("Sucesso", "Arquivo salvo com sucesso!", "OK");
            }

            else
            {
                await Shell.Current.DisplayAlert("Atenção", "Presença do aluno inferior a 75%.", "OK");
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

    [ICommand]
    private void FilterStudents(string name)
    {
        try
        {
            // Lógica para filtrar alunos com base nos critérios (Nome e Turma)
            List<StudentResponse> filteredStudents = Students;

            if (!string.IsNullOrEmpty(name))
            {
                filteredStudents = filteredStudents.Where(a => a.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            if (SelectedClass != null)
            {
                filteredStudents =filteredStudents.Where(a => a.ClassName == SelectedClass.Name).ToList();
            }

            FilteredStudents.Clear();

            foreach (var value in filteredStudents)
                FilteredStudents.Add(value);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

}