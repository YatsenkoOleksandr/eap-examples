namespace IdentityMap;

public class IdentityMap
{
    private readonly Dictionary<int, Person> people = [];

    public void AddPerson(Person person)
    {
        if (person is null)
        {
            return;
        }

        people.TryAdd(person.Id, person);
    }

    public Person? GetPerson(int id)
    {
        return people.GetValueOrDefault(id);
    }
}