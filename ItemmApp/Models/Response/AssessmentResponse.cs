namespace ItemmApp.Models.Response;

public class AssessmentResponse
{
    public Guid Id { get; private set; }
    public string StudentName { get; private set; }
    public string StudentId { get; private set; }
    public string Level { get; private set; }
    public string Module { get; private set; }
    public int SkillTechnique { get; private set; }
    public int Participation { get; private set; }
    public int InterPersonalRelationship { get; private set; }
    public int GoalFulfillment { get; private set; }
    public DateTime AssessmentDate { get; private set; }

    public AssessmentResponse(Guid id, string studentName, string studentId, string level, string module, int skillTechnique, int participation, 
        int interPersonalRelationship, int goalFulfillment, DateTime assessmentDate)
    {
        Id = id;
        StudentName = studentName;
        StudentId = studentId;
        Level = level;
        Module = module;
        SkillTechnique = skillTechnique;
        Participation = participation;
        InterPersonalRelationship = interPersonalRelationship;
        GoalFulfillment = goalFulfillment;
        AssessmentDate = assessmentDate;
    }
}