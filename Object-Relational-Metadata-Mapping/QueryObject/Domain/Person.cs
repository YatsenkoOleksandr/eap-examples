namespace QueryObject.Domain
{
    public class Person : DomainObject
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int NumberOfDependents { get; set; }
    }
}
