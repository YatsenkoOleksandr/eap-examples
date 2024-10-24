using System.Data;

namespace LazyLoad.Ghost;

/// <summary>
/// Data Mapper for <see cref="Employee"/>
/// </summary>
public class EmployeeMapper: Mapper
{
    public Employee Find(int key)
    {
        return (Employee) AbstractFind(key);
    }

    protected override DomainObject CreateGhost(int key)
    {
        return new Employee(key);
    }

    /// <summary>
    /// Sets all <see cref="Employee"/> data fields
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="obj"></param>
    protected override void DoLoad(IDataReader reader, DomainObject obj)
    {
        Employee employee = (Employee) obj;
        employee.Name = (string)reader["name"];

        /*
        DepartmentMapper depMapper =
            (DepartmentMapper) MapperRegistry.Mapper(typeof(Department));
        employee.Department = depMapper.Find((int) reader["departmentID"]);
        loadTimeRecords(employee);
        */
    }
}