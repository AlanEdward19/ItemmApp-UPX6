using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventarioMobile.ViewModels;
using ItemmApp.Enums;
using ItemmApp.Interfaces;
using ItemmApp.Models.Request;
using ItemmApp.Models.Response;

namespace ItemmApp.ViewModel;

public partial class PersonalDepartmentCadasterViewModel : BaseViewModel
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

    [ObservableProperty]
    public List<string> daysOfWeek = Enum.GetNames<EWeekDay>().ToList();

    public ObservableCollection<ClassResponse> Classes { get; set; }
        = new();

    public ObservableCollection<PoloResponse> Polos { get; set; }
        = new();

    public ObservableCollection<FunctionResponse> Functions { get; set; }
        = new();

    public ObservableCollection<CompanyResponse> Companies { get; set; }
        = new();

    [ObservableProperty] PoloResponse selectedPolo;
    [ObservableProperty] CompanyResponse selectedCompany;
    [ObservableProperty] FunctionResponse selectedFunction;
    [ObservableProperty] ClassResponse selectedClass;

    public PersonalDepartmentCadasterViewModel(IStudentRepository studentRepository, IClassRepository classRepository,
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
    }

    internal async Task UpdateFunctionsList()
    {
        var functions = await _functionRepository.GetFunctionsAsync();
        Functions.Clear();

        foreach (var @function in functions)
            Functions.Add(@function);
    }

    internal async Task UpdatePolosList()
    {
        var polos = await _poloRepository.GetPolosAsync();
        Polos.Clear();

        foreach (var @polo in polos)
            Polos.Add(@polo);
    }

    internal async Task UpdateCompaniesList()
    {
        var companies = await _companyRepository.GetCompaniesAsync();
        Classes.Clear();

        foreach (var @company in companies)
            Companies.Add(@company);
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
    public async Task AddStudent()
    {
        var request = new InsertStudentRequest(Name, City, BirthDate, SelectedCompany.Cnpj, SelectedFunction.Id,
            SelectedClass.Id, PracticeHours, TheoreticalHours, Model, ReasonForTermination, ContractPeriod, Status,
            PhoneNumber, SelectedPolo.Id, EndDate, AdmissionDate, FirstDayOfTrainingIntroduction,
            FinalDayTrainingIntroduction, FirstDayOfWeeklyTraining, DayOfTrainingWeek,
            ScheduleTrainingInitialeFinal, Cpf);

        bool sucess = await _studentRepository.AddAsync(request);

        if (sucess)
        {
            await Shell.Current.DisplayAlert("Sucesso", "Aluno foi inserido com sucesso", "OK");

            await Shell.Current.GoToAsync(nameof(PersonalDepartmentPage));
            return;
        }

        await Shell.Current.DisplayAlert("Atenção", "Houve um erro ao tentar inserir aluno, tente novamente!", "OK");
    }
}