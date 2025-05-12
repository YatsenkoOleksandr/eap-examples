using Repository.DataMapping;
using Repository.Domain;
using System.Text;

namespace Repository.QueryObject
{
    /// <summary>
    /// Query Object for domain class that handles conjuction of elementary predicates.
    /// </summary>
    /// <typeparam name="T">Domain class</typeparam>
    public class QueryObject<T> where T : DomainObject
    {
        private List<Criteria> _criterias = [];
        private UnitOfWork _unitOfWork;


        public void AddCriteria(Criteria criteria)
        {
            _criterias.Add(criteria);
        }

        /// <summary>
        /// Entry point to execute a query.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        public IEnumerable<T> Execute(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            return unitOfWork.GetMapper<T>().FindObjectsWhere(GenerateWhereClause());
        }

        /// <summary>
        /// Method to combine criterias into SQL WHERE clause
        /// </summary>
        /// <returns></returns>
        private string GenerateWhereClause()
        {
            StringBuilder result = new StringBuilder();
            foreach (Criteria criteria in _criterias)
            {
                if (result.Length != 0)
                {
                    result.Append(" AND ");
                }
                result.Append(criteria.GenerateSql(_unitOfWork.GetMapper<T>().DataMap));
            }

            return result.ToString();
        }
    }
}
