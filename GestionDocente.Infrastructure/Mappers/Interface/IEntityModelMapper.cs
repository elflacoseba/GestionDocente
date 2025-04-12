namespace GestionDocente.Infrastructure.Mappers.Interface
{
    public interface IEntityModelMapper<TEntity, TModel>
    {
        TModel ConvertToModel(TEntity entity);
        TEntity ConvertToEntity(TModel model);

    }
}
