using CommunityToolkit.Mvvm.ComponentModel;
using InventarioMobile.ViewModels;
using ItemmApp.Enums;
using ItemmApp.Interfaces;
using ItemmApp.Models.Response;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using ItemmApp.Models.Request;

namespace ItemmApp.ViewModel;

[QueryProperty(nameof(Student), nameof(Student))]
public partial class PersonalDepartmentUpdateCadasterViewModel : BaseViewModel
{
    private readonly IStudentRepository _studentRepository;
    private readonly IClassRepository _classRepository;
    private readonly IPoloRepository _poloRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IFunctionRepository _functionRepository;

    [ObservableProperty]
    public List<string> reasonsForTermination = Enum.GetNames<EReasonForTermination>().ToList();

    [ObservableProperty]
    public List<string> models = Enum.GetNames<EModel>().ToList();


    public ObservableCollection<ClassResponse> Classes { get; set; }
        = new();

    public ObservableCollection<PoloResponse> Polos { get; set; }
        = new();

    public ObservableCollection<FunctionResponse> Functions { get; set; }
        = new();

    public ObservableCollection<CompanyResponse> Companies { get; set; }
        = new();

    [ObservableProperty] ClassResponse selectedClass;
    [ObservableProperty] PoloResponse selectedPolo;
    [ObservableProperty] CompanyResponse selectedCompany;
    [ObservableProperty] FunctionResponse selectedFunction;
    

    private StudentResponse _student;
    public StudentResponse Student
    {
        get => _student;
        set
        {
            SetProperty(ref _student, value);

            if (value == null) return;

            Name = value.Name;
            Cpf = value.Cpf;
            Polo = value.PoloName;
            City = value.City;
            ContractPeriod = value.ContractPeriod;
            CompanyName = value.CompanyName;
            ClassName = value.ClassName;
            PhoneNumber = value.PhoneNumber;
            FunctionName = value.FunctionName;
            FirstDayOfTrainingIntroduction = value.FirstDayOfTrainingIntroduction;
            FinalDayTrainingIntroduction = value.FinalDayTrainingIntroduction;
            FirstDayOfWeeklyTraining = value.FirstDayOfWeeklyTraining;
            DayOfTrainingWeek = value.DayOfTrainingWeek;
            AdmissionDate = value.AdmissionDate;
            EndDate = value.EndDate;
            BirthDate = value.BirthDate;
            ScheduleTrainingInitialeFinal = value.ScheduleTrainingInitialEFinal;
            PracticeHours = value.PracticeHours;
            TheoreticalHours = value.TheoreticalHours;
            Model = value.Model;
            ReasonForTermination = value.ReasonForTermination;
            Status = value.Status.Equals("Ativo", StringComparison.InvariantCultureIgnoreCase);
        }
    }

    public PersonalDepartmentUpdateCadasterViewModel(IStudentRepository studentRepository, IClassRepository classRepository, 
        IPoloRepository poloRepository, ICompanyRepository companyRepository, IFunctionRepository functionRepository)
    {
        _studentRepository = studentRepository;
        _classRepository = classRepository;
        _poloRepository = poloRepository;
        _companyRepository = companyRepository;
        _functionRepository = functionRepository;
    }

    internal async Task UpdateClassesList()
    {
        var classes = await _classRepository.GetClassesAsync();
        Classes.Clear();

        foreach (var @class in classes)
            Classes.Add(@class);

        SelectedClass = FindClassByName(ClassName);
    }

    internal async Task UpdateFunctionsList()
    {
        var functions = await _functionRepository.GetFunctionsAsync();
        Functions.Clear();

        foreach (var @function in functions)
            Functions.Add(@function);

        SelectedFunction = FindFunctionByName(FunctionName);
    }

    internal async Task UpdatePolosList()
    {
        var polos = await _poloRepository.GetPolosAsync();
        Polos.Clear();

        foreach (var @polo in polos)
            Polos.Add(@polo);

        SelectedPolo = FindPoloByName(Polo);
    }

    internal async Task UpdateCompaniesList()
    {
        var companies = await _companyRepository.GetCompaniesAsync();
        Classes.Clear();

        foreach (var @company in companies)
            Companies.Add(@company);

        SelectedCompany = FindCompanyByName(CompanyName);
    }

    internal async Task InitAsync()
    {
        IsBusy = true;

        await UpdatePolosList();
        await UpdateCompaniesList();
        await UpdateFunctionsList();
        await UpdateClassesList();

        IsBusy = false;
    }

    private ClassResponse FindClassByName(string name) => Classes.First(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    private PoloResponse FindPoloByName(string name) => Polos.First(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    private CompanyResponse FindCompanyByName(string name) => Companies.First(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    private FunctionResponse FindFunctionByName(string name) => Functions.First(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

    [ObservableProperty] string name;
    [ObservableProperty] string cpf;
    [ObservableProperty] string polo;
    [ObservableProperty] string city;
    [ObservableProperty] int contractPeriod;
    [ObservableProperty] string companyName;
    [ObservableProperty] string className;
    [ObservableProperty] string phoneNumber;
    [ObservableProperty] string functionName;
    [ObservableProperty] int firstDayOfTrainingIntroduction;
    [ObservableProperty] int finalDayTrainingIntroduction;
    [ObservableProperty] int firstDayOfWeeklyTraining;
    [ObservableProperty] string dayOfTrainingWeek;
    [ObservableProperty] DateTime admissionDate;
    [ObservableProperty] DateTime endDate;
    [ObservableProperty] DateTime birthDate;
    [ObservableProperty] string scheduleTrainingInitialeFinal;
    [ObservableProperty] int practiceHours;
    [ObservableProperty] int theoreticalHours;
    [ObservableProperty] string model;
    [ObservableProperty] string reasonForTermination;
    [ObservableProperty] bool status;

    [RelayCommand]
    public async Task UpdateStudent()
    {
        bool answer = await Shell.Current.DisplayAlert("Atenção", $"Deseja mesmo atualizar as informações do Aluno: {Name}?", "Sim", "Não");

        if (answer)
        {
            var request = new StudentRequest(Name, City, BirthDate, SelectedCompany.Cnpj, SelectedFunction.Id,
                SelectedClass.Id, PracticeHours, TheoreticalHours, Model, ReasonForTermination, ContractPeriod, Status,
                PhoneNumber, SelectedPolo.Id, EndDate, AdmissionDate, FirstDayOfTrainingIntroduction,
                finalDayTrainingIntroduction, firstDayOfWeeklyTraining, DayOfTrainingWeek,
                ScheduleTrainingInitialeFinal);

            bool sucess = await _studentRepository.UpdateAsync(request, Cpf);

            if (sucess)
            {
                await Shell.Current.DisplayAlert("Sucesso", "Aluno foi atualizado com sucesso", "OK");

                await Shell.Current.GoToAsync(nameof(PersonalDepartmentPage));
                return;
            }

            await Shell.Current.DisplayAlert("Atenção", "Houve um erro ao tentar atualizar aluno, tente novamente!", "OK");
        }
    }
}