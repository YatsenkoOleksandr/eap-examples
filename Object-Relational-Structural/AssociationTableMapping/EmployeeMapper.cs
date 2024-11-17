using System.Data;

namespace AssociationTableMapping;

/// <summary>
/// Data Mapper for <see cref="Employee"/> which loads related <see cref="Skill"/> using association table
/// </summary>
public class EmployeeMapper : AbstractMapper<Employee>
{
    protected override string TableName => "Employees";

    protected override Employee CreateDomainObject()
    {
        return new Employee();
    }

    protected override void DoLoad(Employee employee, DataRow row)
    {
        employee.Name = (string)row["name"];

        LoadSkills(employee);
    }

    /// <summary>
    /// Method to query related <see cref="Skill"/>
    /// </summary>
    /// <param name="employee"></param>
    /// <returns></returns>
    protected ICollection<Skill> LoadSkills(Employee employee)
    {
        // 1: Query from association table
        var rows = SkillLinkRows(employee);
        var result = new List<Skill>(rows.Length);

        foreach (var row in rows)
        {
            long skillId = (long)row["skillId"];
            // 2: Query from skills table
            // employee.AddSkill(MapperRegistry.Skill.Find(skillId));
        }
        return result;
    }

    /// <summary>
    /// Method to query related <see cref="Skill"/> from association table
    /// </summary>
    /// <param name="employee"></param>
    /// <returns></returns>
    protected DataRow[] SkillLinkRows(Employee employee)
    {
        var filter = $"employeeId = {employee.Id}";
        return SkillLinkTable.Select(filter);
    }

    /// <summary>
    /// Method to save <see cref="Employee"/> to the database
    /// </summary>
    /// <param name="employee"></param>
    /// <param name="row"></param>
    protected override void Save(Employee employee, DataRow row)
    {
        row["name"] = employee.Name;
        SaveSkills(employee);
    }

    protected DataTable SkillLinkTable
    {
        get { return _dsh.Data.Tables["skillEmployees"]; }
    }

    /// <summary>
    /// Method to save <see cref="Employee"/>-<see cref="Skill"/> association to the database.
    /// </summary>
    /// <param name="employee"></param>
    protected void SaveSkills(Employee employee)
    {
        // 1. Delete existing association
        DeleteSkills(employee);

        foreach (var skill in employee.Skills)
        {
            // 2. Create new association
            DataRow newSkillLinkRow = SkillLinkTable.NewRow();
            newSkillLinkRow["employeeId"] = employee.Id;
            newSkillLinkRow["skillId"] = skill.Id;

            SkillLinkTable.Rows.Add(newSkillLinkRow);
        }
    }

    protected void DeleteSkills(Employee employee)
    {
        var skillRows = SkillLinkRows(employee);
        foreach (var row in skillRows)
        {
            row.Delete();
        }
    }
}