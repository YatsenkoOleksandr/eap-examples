using FluentAssertions;

namespace UnitOfWork;

public class UnitOfWork
{
    private static readonly ThreadLocal<UnitOfWork> current = new(() => {
        return new UnitOfWork();
    });

    public static void SetCurrent(UnitOfWork unitOfWork)
    {
        current.Value = unitOfWork;
    }

    public static UnitOfWork GetCurrent()
    {
        return current.Value;
    }

    private readonly List<DomainObject> newObjects = [];

    private readonly List<DomainObject> dirtyObjects = [];

    private readonly List<DomainObject> removedObjects = [];

    public void RegisterNew(DomainObject domainObject)
    {
        domainObject.Should().NotBeNull();

        // Origin code checks Id to be not null
        // domainObject.Id.Should().NotBeNull();

        dirtyObjects.Contains(domainObject).Should().BeFalse("object not dirty");
        dirtyObjects.All(dirty => dirty.Id != domainObject.Id).Should().BeTrue("object not dirty");

        removedObjects.Contains(domainObject).Should().BeFalse("object not removed");
        removedObjects.All(removed => removed.Id != domainObject.Id).Should().BeTrue("object not removed");

        newObjects.Contains(domainObject).Should().BeFalse("object not already registered as new");
        newObjects.All(removed => removed.Id != domainObject.Id).Should().BeTrue("object not already registered as new");

        newObjects.Add(domainObject);
    }

    public void RegisterDirty(DomainObject domainObject)
    {
        domainObject.Should().NotBeNull();

        // Origin code checks Id to be not null
        // domainObject.Id.Should().NotBeNull();

        removedObjects.Contains(domainObject).Should().BeFalse("object not removed");
        removedObjects.All(removed => removed.Id != domainObject.Id).Should().BeTrue("object not removed");

        var isAlreadyDirty = dirtyObjects.Contains(domainObject) || dirtyObjects.Any(dirty => dirty.Id == domainObject.Id);
        var isNew = newObjects.Contains(domainObject) || newObjects.Any(newObject => newObject.Id == domainObject.Id);

        if (!isAlreadyDirty && !isNew)
        {
            dirtyObjects.Add(domainObject);
        }
    }

    public void RegisterRemoved(DomainObject domainObject)
    {
        domainObject.Should().NotBeNull();

        // Origin code checks Id to be not null
        // domainObject.Id.Should().NotBeNull();

        var newObject = newObjects.FirstOrDefault(obj => obj.Id == domainObject.Id);
        if (newObjects.Remove(domainObject) || (newObject is not null && newObjects.Remove(newObject)))
        {
            return;
        }

        var dirtyObject = dirtyObjects.FirstOrDefault(obj => obj.Id == domainObject.Id);
        if (dirtyObject is not null)
        {
            dirtyObjects.Remove(dirtyObject);
        }
        dirtyObjects.Remove(domainObject);


        var isAlreadyRemoved = removedObjects.Contains(domainObject) || removedObjects.Any(removed => removed.Id == domainObject.Id);
        if (!isAlreadyRemoved)
        {
            removedObjects.Add(domainObject);
        }
    }

    public void RegisterClean(DomainObject domainObject)
    {
        domainObject.Should().NotBeNull();

        // Origin code checks Id to be not null
        // domainObject.Id.Should().NotBeNull();

        /*
            Method does not do anything here.
            The common practice is to place an Identity Map within Unit Of Work:
            - RegisterClean would put the registered object in it
            - RegisterNew would put a new object in the map
            - RegisterRemoved would remove object from the map
        */
    }

    public void Commit()
    {
        InsertNew();
        UpdateDirty();
        DeleteRemoved();
    }

    private void InsertNew()
    {
        foreach (var newObject in newObjects)
        {
            MapperRegistry.GetMapper(newObject.GetType()).Insert(newObject);
        }
    }

    private void UpdateDirty()
    {
        foreach (var updated in dirtyObjects)
        {
            MapperRegistry.GetMapper(updated.GetType()).Update(updated);
        }
    }

    private void DeleteRemoved()
    {
        foreach (var removed in removedObjects)
        {
            MapperRegistry.GetMapper(removed.GetType()).Delete(removed);
        }
    }
}
