using Application.InterfaceServices;

namespace Infrastructure.Repositories;

public class RebuildRepository : IRebuildRepository
{
    public RepositoryDBContext _repositoryDbContext;

    public RebuildRepository(RepositoryDBContext repositoryDbContext)
    {
        _repositoryDbContext = repositoryDbContext;
    }

    public void RebuildDB()
    {
        _repositoryDbContext.Database.EnsureDeleted();
        _repositoryDbContext.Database.EnsureCreated();
    }
}