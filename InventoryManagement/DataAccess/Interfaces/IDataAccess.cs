using Entities;

namespace DataAccess.Interfaces;

public interface IDataAccess<T>
{
    List<T> GetAll();
    void SaveAll(List<T> list);
    void Add(T item);
    T? GetFirstMatch(Predicate<T> predicate);
    List<T> GetMatchedItems(Predicate<T> predicate);
    void Delete(Predicate<T> predicate);
}
