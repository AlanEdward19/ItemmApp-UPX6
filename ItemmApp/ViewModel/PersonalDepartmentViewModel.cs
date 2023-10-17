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

    [ObservableProperty] 
    StudentResponse selectedStudent;

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

    [RelayCommand]
    public async Task GenerateCertificate()
    {
        //await Shell.Current.DisplayAlert("Atenção", "Presença do aluno inferior a 75%.", "OK");

        try
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string docxTemplatePath = Path.Combine(basePath, "../../../../../Files", "CertificadoIttem.docx");
            string docxOutputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "saida.docx");

            // Dicionário de substituição
            Dictionary<string, string> changes = new Dictionary<string, string>
            {

                { "rg do aluno", Converter.FormatCpf(selectedStudent.Cpf) },
                {"nome da empresa", selectedStudent.CompanyName},
                {"função do aluno", selectedStudent.FunctionName},
                {"data1", selectedStudent.AdmissionDate.ToString("dd/MM/yyyy")},
                {"data2", selectedStudent.EndDate.ToString("dd/MM/yyyy")}
            };

            Converter.FillDOCX(docxTemplatePath, docxOutputPath, changes);


            // Agora, use o FilePicker para permitir que o usuário escolha o local de salvamento
            var opcoes = new PickOptions
            {
                PickerTitle = "Salvar Arquivo",
            };

            var resultado = await FolderPicker.PickAsync(new CancellationToken());
            if (resultado != null)
            {
                // Copie o arquivo para o local escolhido pelo usuário
                File.Copy(docxOutputPath, Path.Combine(resultado.Folder.Path, $"{selectedStudent.Name}-Certificate.docx"), true);

                // Informe ao usuário que o arquivo foi salvo
                await Shell.Current.DisplayAlert("Sucesso", "Arquivo salvo com sucesso!", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Erro", $"Erro: {ex.Message}", "OK");
        }
    }

}