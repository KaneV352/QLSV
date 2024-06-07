namespace QLSV.Core
{
    public interface ISpecification<T>
    {
        object Value(T t);
    }

    public class StudentScoreSpecification : ISpecification<Student>
    {
        public object Value(Student student)
        {
            return student.score;
        }
    }

    public class StudentIdSpecification : ISpecification<Student>
    {
        public object Value(Student student)
        {
            return student.id;
        }
    }

    public class StudentFirstNameSpecification : ISpecification<Student>
    {
        public object Value(Student student)
        {
            return student.firstName;
        }
    }

    public class StudentClassNameSpecification : ISpecification<Student>
    {
        public object Value(Student student)
        {
            return student.className;
        }
    }

    public class StudentLastNameSpecification : ISpecification<Student>
    {
        public object Value(Student student)
        {
            return student.lastName;
        }
    }


}