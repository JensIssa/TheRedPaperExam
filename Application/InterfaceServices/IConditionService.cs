using Domain.Entities;

namespace Application.InterfaceServices;

public interface IConditionService
{
    /// <summary>
    /// Method gets a list of all the condition objects
    /// </summary>
    /// <returns>returns a list of all the conditions</returns>
    List<Condition> GetAllConditions();
}