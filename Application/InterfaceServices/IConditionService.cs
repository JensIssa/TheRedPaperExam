using Domain.Entities;

namespace Application.InterfaceServices;

public interface IConditionService
{
    List<Condition> GetAllConditions();
}