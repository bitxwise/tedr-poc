using System.Linq;
using Risly.Cqrs;
using StudyApi.Models;

namespace StudyApi.Queries
{
    public class StudyQueryHandlers
    {
        private readonly IRepository<Study> _repository;
        
        /// <summary>
        /// Initializes a new instance of the StudyApi.Queries.StudyQueryHandlers class
        /// with the specified study repository.
        /// </summary>
        /// <param name="repository">Repository for studies.</param>
        public StudyQueryHandlers(IRepository<Study> repository)
        {
            _repository = repository;
        }
        
        /// <summary>
        /// Handles the specified get all studies queries.
        /// </summary>
        /// <param name="query">The get all studies query to handle.</param>
        public IQueryable Handle(GetAllStudiesQuery query)
        {
            var allStudyIds = _repository.GetAllIds();
            var studies = allStudyIds.Select(id => _repository.GetById(id));
            return studies.AsQueryable();
        }
    }
}