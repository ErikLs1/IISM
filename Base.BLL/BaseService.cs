using Base.BLL.Contracts;
using Base.Contracts;
using Base.DAL.Contracts;

namespace Base.BLL;


public class BaseService<TBllEntity, TDalEntity, TDalRepository> : BaseService<TBllEntity, TDalEntity, TDalRepository, Guid>, IBaseService<TBllEntity>
    where TDalEntity : class, IDomainId
    where TBllEntity : class, IDomainId
    where TDalRepository : class, IBaseRepository<TDalEntity>
{
    public BaseService(IBaseUow serviceUow, TDalRepository serviceRepository, IBllMapper<TBllEntity, TDalEntity, Guid> bllMapper) : base(serviceUow, serviceRepository, bllMapper)
    {
    }
}

public class BaseService<TBllEntity, TDalEntity, TDalRepository, TKey> : IBaseService<TBllEntity, TKey>
    where TDalEntity : class, IDomainId<TKey>
    where TBllEntity : class, IDomainId<TKey>
    where TDalRepository: class, IBaseRepository<TDalEntity, TKey>
    where TKey : IEquatable<TKey>
{
    protected IBaseUow ServiceUow;
    protected TDalRepository ServiceRepository;
    protected IBllMapper<TBllEntity, TDalEntity, TKey> BllMapper;


    public BaseService(IBaseUow serviceUow, TDalRepository serviceRepository, IBllMapper<TBllEntity, TDalEntity, TKey> bllMapper)
    {
        ServiceUow = serviceUow;
        ServiceRepository = serviceRepository;
        BllMapper = bllMapper;
    }

    public virtual IEnumerable<TBllEntity> All(TKey? userId = default)
    {
        var entities = ServiceRepository.All(userId);
        return entities.Select(e => BllMapper.Map(e)!).ToList();
    }

    public virtual async Task<IEnumerable<TBllEntity>> AllAsync(TKey? userId = default)
    {
        var entities = await ServiceRepository.AllAsync(userId);
        return entities.Select(e => BllMapper.Map(e)!).ToList();
    }

    public virtual TBllEntity? Find(TKey id, TKey? userId = default)
    {
        var entities = ServiceRepository.Find(id, userId);
        return BllMapper.Map(entities);
    }

    public virtual async Task<TBllEntity?> FindAsync(TKey id, TKey? userId = default)
    {
        var entities = await ServiceRepository.FindAsync(id, userId);
        return BllMapper.Map(entities);
    }

    public virtual void Add(TBllEntity entity, TKey? userId = default)
    {
        var dalEntity = BllMapper.Map(entity);
        ServiceRepository.Add(dalEntity!, userId);
    }

    public virtual TBllEntity Update(TBllEntity entity)
    {
        var dalEntity = BllMapper.Map(entity);
        var updateEntity = ServiceRepository.Update(dalEntity!);
        return BllMapper.Map(updateEntity)!;
    }

    public virtual void Remove(TBllEntity entity, TKey? userId = default)
    {
        Remove(entity.Id, userId);
    }

    public virtual void Remove(TKey id, TKey? userId = default)
    {
        var entity = ServiceRepository.Find(id, userId);
        if (entity != null)
        {
            ServiceRepository.Remove(entity, userId);
        }
    }

    public virtual async Task RemoveAsync(TKey id, TKey? userId = default)
    {
        var entity = await ServiceRepository.FindAsync(id, userId);
        if (entity != null)
        {
            await ServiceRepository.RemoveAsync(id, userId);
        }
    }

    public virtual bool Exists(TKey id, TKey? userId = default)
    {
        var entity = ServiceRepository.Find(id, userId);
        return entity != null;
    }

    public virtual async Task<bool> ExistsAsync(TKey id, TKey? userId = default)
    {
        var entity = await ServiceRepository.FindAsync(id, userId);
        return entity != null;
    }
}
