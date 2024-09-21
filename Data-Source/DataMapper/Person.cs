namespace DataMapper;

internal class Person : DomainObject
{
  public string LastName { get; set; } = string.Empty;

  public string FirstName { get; set; } = string.Empty;

  public int NumberOfDependents { get; set; }
}
