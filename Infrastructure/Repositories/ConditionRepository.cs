using Application.InterfaceRepos;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class ConditionRepository : IConditionRepository
{
    private  RepositoryDBContext _context;

    public ConditionRepository(RepositoryDBContext context)
    {
        _context = context;
    }
    public List<Condition> getConditions()
    {
        return _context.ConditionTable.ToList();
    }
}