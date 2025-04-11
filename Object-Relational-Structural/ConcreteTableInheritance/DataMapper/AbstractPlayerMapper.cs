using ConcreteTableInheritance.Domain;
using System.Data;

namespace ConcreteTableInheritance.DataMapper
{
    public abstract class AbstractPlayerMapper : Mapper
    {
        protected AbstractPlayerMapper(Gateway gateway) : base(gateway)
        {
        }

        /// <summary>
        /// Method to load the common props in superclass
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="row"></param>
        protected override void Load(DomainObject obj, DataRow row)
        {
            base.Load(obj, row);
            Player? player = obj as Player;
            player.Name = (string)row["name"];
        }

        /// <summary>
        /// Method to save the common props in superclass
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="row"></param>
        protected override void Save(DomainObject obj, DataRow row)
        {
            Player? player = obj as Player;
            row["name"] = player.Name;
        }
    }
}
