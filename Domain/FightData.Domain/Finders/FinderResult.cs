
namespace FightData.Domain.Finders
{
    public class FinderResult<T>
    {
        public FinderResult(T entity)
        {
            Result = entity;
        }

        public T Result { get; private set; }
        public bool IsFound()
        {
            return Result != null;
        }
    }
}
