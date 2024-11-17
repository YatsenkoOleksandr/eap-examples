namespace AssociationTableMapping;

public class Employee: DomainObject
{
    private ICollection<Skill> _skills = [];

    public string Name { get; set; } = string.Empty;

    public ICollection<Skill> Skills
    {
        get { return _skills.ToArray().AsReadOnly(); }
        set { _skills = new List<Skill>(value); }
    }

    public void AddSkill(Skill skill)
    {
        _skills.Add(skill);
    }

    public void RemoveSkill(Skill skill)
    {
        _skills.Remove(skill);
    }
}