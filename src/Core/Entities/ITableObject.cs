namespace SimpleRedirects.Core.Entities;

public interface ITableObject<T> where T: IEquatable<T>
{
    T Id { get; set; }
    void SetNewId();
}