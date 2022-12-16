using Domain.Entities;

namespace Application.InterfaceRepos;

public interface IConditionRepository
{
    /// <summary>
    /// This method returns a list of all the conditions from the database
    /// </summary>
    /// <returns>A list of all the condition objects in the database</returns>
    List<Condition> GetConditions();
}