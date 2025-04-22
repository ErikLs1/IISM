using Base.DAL.Contracts;
using Base.Domain;

namespace Base.DAL.EF;

public class BaseRepository<TEntity> : BaseRepository<TEntity, Guid>, IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    
}

public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
    where TEntity : BaseEntity
    where TKey : IEquatable<TKey>
{
    
    
    
    public IEnumerable<TEntity> All(TKey? userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TEntity>> AllAsync(TKey? userId)
    {
        throw new NotImplementedException();
    }

    public TEntity Find(TKey id, TKey? userId)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> FindAsync(TKey id, TKey? userId)
    {
        throw new NotImplementedException();
    }

    public void Add(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public TEntity Update(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(TEntity entity, TKey? userId)
    {
        throw new NotImplementedException();
    }

    public void Remove(TKey id, TKey? userId)
    {
        throw new NotImplementedException();
    }
}
