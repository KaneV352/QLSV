using QLSV.Core;
using QLSV.List;
using System;
using System.IO;

namespace QLSV
{
    class Program
    {
        private static ListController<Student> _listController;
        private static Lazy<FunctionController> _functionController;
        const string fileName = "DSSV.xlsx";
        private static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);


        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            MyExcel excel = new MyExcel(filePath, 1);
            _listController = new ListController<Student>();

            IMyList<Student> list = ChooseStoreMethod(excel);
            excel.Close();

            _functionController = new Lazy<FunctionController>(() => new FunctionController(list));
            _functionController.Value.ChooseFunction();
        }

        private static IMyList<Student> ChooseStoreMethod(MyExcel excel)
        {

            Console.WriteLine("Choose store method: ");
            Console.WriteLine("1. Store to Singly Linked List");
            Console.WriteLine("2. Store to Doubly Linked List");
            Console.WriteLine("3. Store to Circular Linked List");
            Console.WriteLine("4. Store to Array");

            int choice = -1;
            while (choice > 4 || choice < 1)
            {
                Console.Write("Your choice: ");
                try
                {
                    choice = int.Parse(Console.ReadLine());
                    if (choice > 4 || choice < 1)
                        throw new Exception();
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }

            switch (choice)
            {
                case 1:
                    return BySinglylist(excel);
                case 2:
                    return ByDoublylist(excel);
                case 3:
                    return ByCircularlist(excel);
                case 4:
                    return ByArrayList(excel);
                default:
                    return BySinglylist(excel);
            }
        }

        private static IMyList<Student> BySinglylist(MyExcel excel)
        {
            IMyList<Student> students = _listController.SinglyLinkedList();

            int rowCount = excel.GetRowCount();
            for (int i = 2; i <= rowCount; i++)
            {
                string id = excel.ReadCell(i, 2);
                string lastMiddleName = excel.ReadCell(i, 3);
                string name = excel.ReadCell(i, 4);
                string className = excel.ReadCell(i, 5);
                float score = float.Parse(excel.ReadCell(i, 6));

                Student student = new Student(id, lastMiddleName, name, className, score);
                students.Add(student);
            }
            return students;
        }

        private static IMyList<Student> ByDoublylist(MyExcel excel)
        {
            IMyList<Student> students = _listController.DoublyLinkedList();


            int rowCount = excel.GetRowCount();
            for (int i = 2; i <= rowCount; i++)
            {
                string id = excel.ReadCell(i, 2);
                string lastMiddleName = excel.ReadCell(i, 3);
                string name = excel.ReadCell(i, 4);
                string className = excel.ReadCell(i, 5);
                float score = float.Parse(excel.ReadCell(i, 6));

                Student student = new Student(id, lastMiddleName, name, className, score);
                students.Add(student);
            }
            return students;
        }

        private static IMyList<Student> ByCircularlist(MyExcel excel)
        {
            IMyList<Student> students = _listController.CircularLinkedList();

            int rowCount = excel.GetRowCount();
            for (int i = 2; i <= rowCount; i++)
            {
                string id = excel.ReadCell(i, 2);
                string lastMiddleName = excel.ReadCell(i, 3);
                string name = excel.ReadCell(i, 4);
                string className = excel.ReadCell(i, 5);
                float score = float.Parse(excel.ReadCell(i, 6));

                Student student = new Student(id, lastMiddleName, name, className, score);
                students.Add(student);
            }

            return students;
        }

        private static IMyList<Student> ByArrayList(MyExcel excel)
        {
            int rowCount = excel.GetRowCount();
            IMyList<Student> students = _listController.ArrayList();

            for (int i = 2; i <= rowCount; i++)
            {
                string id = excel.ReadCell(i, 2);
                string lastMiddleName = excel.ReadCell(i, 3);
                string name = excel.ReadCell(i, 4);
                string className = excel.ReadCell(i, 5);
                float score = float.Parse(excel.ReadCell(i, 6));

                Student student = new Student(id, lastMiddleName, name, className, score);
                students.Add(student);
            }

            return students;
        }
    }
}