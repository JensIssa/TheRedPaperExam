using Domain.Entities;

namespace Application.InterfaceRepos;

public interface IConditionRepository
{
    List<Condition> GetConditions();
}