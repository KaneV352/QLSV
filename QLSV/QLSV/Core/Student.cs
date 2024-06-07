using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Text;

namespace QLSV.Core
{
    public class Student : ICmparable<Student>
    {
        private static readonly Regex IdPattern = new Regex(@"N[0-9]{2}[A-Z]{4}[0-9]{3}", RegexOptions.Compiled);
        private static readonly Regex ClassPattern = new Regex(@"D[0-9]{2}[A-Z]{4}[0-9]{3}", RegexOptions.Compiled);

        public string id { get; }
        public string lastName { get; }
        public string middleName { get; }
        public string firstName { get; }
        public string className { get; }
        public float score { get; }

        public Student(string id, string lastMiddleName, string firstName, string className, float score)
        {
            if (IdPattern.IsMatch(id))
            {
                this.id = id;
            }

            var names = lastMiddleName.Split(' ');
            lastName = names[0];
            if (names.Length > 1)
            {
                for (int i = 1; i < names.Length; i++)
                {
                    middleName += names[i];
                    if (i + 1 == names.Length)
                        break;
                    middleName += " ";
                }
            }
            else
            {
                middleName = "";
            }

            this.firstName = firstName;
            this.className = className;
            this.score = score;
        }

        // ...

        public int CompareTo(Student other, ISpecification<Student> specification)
        {
            try
            {
                float thisStudentScore = (float)specification.Value(this);
                float otherStudentScore = (float)specification.Value(other);

                return thisStudentScore.CompareTo(otherStudentScore);
            }
            catch (InvalidCastException)
            {
                string thisString = RemoveSignForVietnameseString((string)specification.Value(this));
                string otherString = RemoveSignForVietnameseString((string)specification.Value(other));

                return String.Compare(thisString, otherString, StringComparison.CurrentCultureIgnoreCase);
            }
        }

        private static readonly string[] VietnameseSigns = new string[]
        {

            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"
        };

        private string RemoveSignForVietnameseString(string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }
    }
}
