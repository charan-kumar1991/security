using Ardalis.Specification;
using Core.Entities;
using System.Linq;

namespace Core.Specifications
{
    public class FetchUserByUsernameOrEmailSpecification : Specification<User>
    {
        public FetchUserByUsernameOrEmailSpecification(string login)
        {
            Query.Where(c => c.Username == login || c.Email == login);
        }
    }
}
