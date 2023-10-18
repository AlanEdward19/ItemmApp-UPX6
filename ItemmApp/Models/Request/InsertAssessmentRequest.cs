namespace ItemmApp.Models.Request;

public class InsertAssessmentRequest
{
    public string StudentCpf { get; private set; }
    public string Level { get; private set; }
    public string Module { get; private set; }
    public int SkillTechnique { get; private set; }
    public int Participation { get; private set; }
    public int InterPersonalRelationship { get; private set; }
    public int GoalFulfillment { get; private set; }
    public DateTime AssessmentDate { get; private set; }

    public InsertAssessmentRequest(string studentCpf, string level, string module, int skillTechnique, int participation, 
        int interPersonalRelationship, int goalFulfillment, DateTime assessmentDate)
    {
        StudentCpf = studentCpf;
        Level = level;
        Module = module;
        SkillTechnique = skillTechnique;
        Participation = participation;
        InterPersonalRelationship = interPersonalRelationship;
        GoalFulfillment = goalFulfillment;
        AssessmentDate = assessmentDate;
    }
}