namespace QLSV.Core
{
    public interface ICmparable<T>
    {
        int CompareTo(T t, ISpecification<T> specification);
    }
}