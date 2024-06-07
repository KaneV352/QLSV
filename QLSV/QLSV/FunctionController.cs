using QLSV.List;
using QLSV.Core;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using QLSV.Sort;
using System.Linq;

namespace QLSV
{
    public class FunctionController 
    {
        private IMyList<Student> _list;
        private static Lazy<SortController<Student>> _sortController;
        private static System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();

        public FunctionController(IMyList<Student> list) 
        {
            _list = list;
            _sortController = new Lazy<SortController<Student>>();
        }

        public void ChooseFunction()
        {
            while (true)
            {
                DisplayOption();

                int choice = -1;
                while (choice > 7 || choice < 1)
                {
                    Console.Write("Your choice: ");
                    try
                    {
                        choice = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        if (choice > 6 || choice < 1)
                            throw new Exception();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid choice. Please try again.");
                    }
                }

                if (choice == 7) break;

                switch (choice)
                {
                    case 1:
                        FindStudents(_list);
                        break;
                    case 2:
                        AddStudent(_list);
                        break;
                    case 3:
                        RemoveStudent(_list);
                        break;
                    case 4:
                        DisplayTopScoreStudents(_list);
                        break;
                    case 5:
                        DisplayBottomScoreStudents(_list);
                        break;
                    case 6:
                        SortStudents(_list);
                        break;
                }
            }
        }

        private void SortStudents(IMyList<Student> list)
        {
            Console.WriteLine("Choose sort algorithm: ");
            Console.WriteLine("1. Bubble sort:");
            Console.WriteLine("2. Quick sort:");
            Console.WriteLine("3. Merge sort:");
            Console.WriteLine("4. Selection sort:");
            Console.WriteLine("5. Insertion sort:");
            Console.WriteLine("6. Heap sort:");

            int choice = -1;
            while (choice > 6 || choice < 1)
            {
                Console.Write("Your choice: ");
                try
                {
                    choice = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    if (choice > 6 || choice < 1)
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
                    watch.Restart();
                    _sortController.Value.BubbleSort(list, Specification());
                    watch.Stop();
                    break;
                case 2:
                    watch.Restart();
                    _sortController.Value.QuickSort(list, Specification());
                    watch.Stop();
                    break;
                case 3:
                    watch.Restart();
                    _sortController.Value.MergeSort(list, Specification());
                    watch.Stop();
                    break;
                case 4:
                    watch.Restart();
                    _sortController.Value.SelectionSort(list, Specification());
                    watch.Stop();
                    break;
                case 5:
                    watch.Restart();
                    _sortController.Value.InsertionSort(list, Specification());
                    watch.Stop();
                    break;
                case 6:
                    watch.Restart();
                    _sortController.Value.HeapSort(list, Specification());
                    watch.Stop();
                    break;
            }

            Console.WriteLine("Do you want to print the result?");
            Console.WriteLine("1. Yes.");
            Console.WriteLine("2. No.");
            while (choice > 2 || choice < 1)
            {
                try
                {
                    choice = int.Parse(Console.ReadLine());
                    if (choice > 6 || choice < 1)
                        throw new Exception();
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
            if (choice == 1)
            {
                var students = list.PrintList();
                foreach (var student in students)
                {
                    PrintStudent(student, false, false, null);
                }
            }

            Console.WriteLine("Time to sort: " + watch.ElapsedMilliseconds + " milliseconds");
        }

        private void DisplayTopScoreStudents(IMyList<Student> list)
        {
            Console.Write("Enter number of students: ");
            int n;
            while (true)
            {
                try
                {
                    n = int.Parse(Console.ReadLine());
                    if (n < 0)
                        throw new Exception();
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
            _sortController.Value.MergeSort(list, new StudentScoreSpecification());

            Console.WriteLine("Your result: ");
            for (int i = list.Count - 1; i > list.Count - n - 1; i--)
            {
                PrintStudent(list.GetIndex(i), false, false, null);
            }
        }

        private void DisplayBottomScoreStudents(IMyList<Student> list)
        {
            Console.Write("Enter number of students: ");
            int n;
            while (true)
            {
                try
                {
                    n = int.Parse(Console.ReadLine());
                    if (n < 0)
                        throw new Exception();
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
            _sortController.Value.MergeSort(list, new StudentScoreSpecification());
            Console.WriteLine("Your result: ");
            for (int i = 0; i < n; i++)
            {
                PrintStudent(list.GetIndex(i), false, false, null);
            }
        }

        private void RemoveStudent(IMyList<Student> list)
        {
            Console.Write("Enter ID: ");
            string id;
            while (true)
            {
                id = Console.ReadLine();
                if (IsIdValid(id))
                    break;
                Console.WriteLine("Invalid ID. The example ID should be N22DCPT046");
            }
            list.Remove(new Student(id, "", "", "", 0), new StudentIdSpecification());
        }

        private void AddStudent(IMyList<Student> list)
        {
            Console.Write("Enter ID: ");
            string id;
            while (true)
            {
                id = Console.ReadLine();
                if (IsIdValid(id))
                    break;
                Console.WriteLine("Invalid ID. The example ID should be N22DCPT046");
            }
            Console.Write("Enter last middle name: ");
            string lastMiddleName = Console.ReadLine();
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter class name: ");
            string className;
            while (true)
            {
                className = Console.ReadLine();
                if (IsClassValid(className))
                    break;
                Console.WriteLine("Invalid class name. The example class name should be D22CQPT01-N");
            }
            Console.Write("Enter score: ");
            float score = float.Parse(Console.ReadLine());

            Student student = new Student(id, lastMiddleName, firstName, className, score);
            list.Add(student);
        }

        private void FindStudents(IMyList<Student> list)
        {
            Console.WriteLine("Choose a specification:");
            Console.WriteLine("1. Find students with an ID:");
            Console.WriteLine("2. Find students with a last name:");
            Console.WriteLine("3. Find students with a first name:");
            Console.WriteLine("4. Find students with a class name:");
            Console.WriteLine("5. Find students with a score:");

            int choice = -1;
            while (choice > 5 || choice < 1)
            {
                Console.Write("Your choice: ");
                try
                {
                    choice = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    if (choice > 5 || choice < 1)
                        throw new Exception();
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Enter ID: ");
                    string id;
                    bool isReverse;
                    while (true)
                    {
                        id = Console.ReadLine();
                        if (IsIdValid(id))
                            break;
                        Console.WriteLine("Invalid Id. Please try again");
                    }

                    watch.Restart();

                    ISpecification<Student> specification = new StudentIdSpecification();
                    var result1 = list.Find(new Student(id, "", "", "", 0), specification, _sortController.Value.MergeSort).ToList();

                    watch.Stop();

                    if (result1.Count() > 0)
                    {
                        isReverse = IsReversed();
                        Console.WriteLine("Your result: ");
                        foreach (var student in result1)
                        {
                            PrintStudent(student, isReverse, true, specification);
                        }
                        Console.WriteLine("Time to find: " + watch.ElapsedMilliseconds + " milliseconds");
                    }
                    else
                    {
                        Console.WriteLine("No student found.");
                    }
                    break;
                case 2:
                    Console.Write("Enter last name: ");
                    string lastName = Console.ReadLine();

                    watch.Restart();

                    specification = new StudentLastNameSpecification();
                    var result2 = list.Find(new Student("", lastName, "", "", 0), specification, _sortController.Value.MergeSort).ToList();

                    watch.Stop();

                    if (result2.Count() > 0)
                    {
                        isReverse = IsReversed();

                        Console.WriteLine("Your result: ");
                        foreach (var student in result2)
                        {
                            PrintStudent(student, isReverse, true, specification);
                        }
                        Console.WriteLine("Time to find: " + watch.ElapsedMilliseconds + " milliseconds");
                    }
                    else
                    {
                        Console.WriteLine("No student found.");
                    }
                    break;
                case 3:
                    Console.Write("Enter first name: ");
                    string firstName = Console.ReadLine();

                    watch.Restart();

                    specification = new StudentFirstNameSpecification();
                    var result3 = list.Find(new Student("", "", firstName, "", 0), specification, _sortController.Value.MergeSort).ToList();

                    watch.Stop();

                    if (result3.Count() > 0)
                    {
                        isReverse = IsReversed();

                        Console.WriteLine("Your result: ");
                        foreach (var student in result3)
                        {
                            PrintStudent(student, isReverse, true, specification);
                        }
                        Console.WriteLine("Time to find: " + watch.ElapsedMilliseconds + " milliseconds");
                    }
                    else
                    {
                        Console.WriteLine("No student found.");
                    }
                    break;
                case 4:
                    Console.Write("Enter class name: ");
                    string className;
                    while (true)
                    {
                        className = Console.ReadLine();
                        if (IsClassValid(className))
                            break;
                        Console.WriteLine("Invalid class name. The example class name should be D22CQPT01-N");
                    }

                    watch.Restart();

                    specification = new StudentClassNameSpecification();
                    var result4 = list.Find(new Student("", "", "", className, 0), specification, _sortController.Value.MergeSort).ToList();

                    watch.Stop();

                    Console.WriteLine("Your result: ");
                    if (result4.Count() > 0)
                    {
                        isReverse = IsReversed();
                        foreach (var student in result4)
                        {
                            PrintStudent(student, isReverse, true, specification);
                        }
                        Console.WriteLine("Time to find: " + watch.ElapsedMilliseconds + " milliseconds");
                    }
                    else
                    {
                        Console.WriteLine("No student found.");
                    }
                    break;
                case 5:
                    Console.Write("Enter score: ");
                    float score = float.Parse(Console.ReadLine());

                    watch.Restart();

                    specification = new StudentScoreSpecification();
                    var result5 = list.Find(new Student("", "", "", "", score), specification, _sortController.Value.MergeSort);

                    watch.Stop();

                    if (result5.Count() > 0)
                    {
                        isReverse = IsReversed();

                        Console.WriteLine("Your result: ");
                        foreach (var student in result5)
                        {
                            PrintStudent(student, isReverse, true, specification);
                        }
                        Console.WriteLine("Time to find: " + watch.ElapsedMilliseconds + " milliseconds");
                    }
                    else
                    {
                        Console.WriteLine("No student found.");
                    }
                    break;
            }
        }

        private bool IsReversed()
        {
            Console.WriteLine("Do you want to reverse name?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            int choice = -1;
            while (true)
            {
                try
                {
                    Console.Write("Your choice: ");
                    choice = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    if (choice > 2 || choice < 1)
                        throw new Exception();
                    if (choice == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid choice. Try again!");
                }
            }
        }

        private void PrintStudent(Student student, bool isReverse, bool isBold, ISpecification<Student> specification)
        {
            if (!isBold)
            {
                if (student == null)
                {
                    return;
                }

                Console.Write(student.id + " ");
                if (!isReverse)
                {
                    Console.Write(student.lastName + " ");
                    Console.Write(student.middleName + " ");
                    Console.Write(student.firstName + " ");
                }
                else
                {
                    Stack<char> stack = new Stack<char>();
                    for (int i = 0; i < student.lastName.Length; i++)
                    {
                        stack.Push(student.lastName[i]);
                    }
                    stack.Push(' ');

                    for (int i = 0; i < student.middleName.Length; i++)
                    {
                        stack.Push(student.middleName[i]);
                    }
                    stack.Push(' ');

                    for (int i = 0; i < student.firstName.Length; i++)
                    {
                        stack.Push(student.firstName[i]);
                    }

                    while (stack.Count > 0)
                    {
                        Console.Write(stack.Pop().ToString());
                    }
                    Console.Write(" ");
                }

                Console.Write(student.className + " ");
                Console.WriteLine(student.score);
            }
            else
            {
                if (student == null)
                {
                    var exception = new NullReferenceException();
                    Console.WriteLine(exception.Message);
                    throw exception;
                }

                if (specification == null)
                {
                    return;
                }

                if (specification is StudentIdSpecification)
                {
                    Console.Write("\u001b[1m" + student.id + "\u001b[22m" + " ");
                }
                else
                {
                    Console.Write(student.id + " ");
                }
                if (!isReverse)
                {
                    if (specification is StudentLastNameSpecification)
                    {
                        Console.Write("\u001b[1m" + student.lastName + "\u001b[22m" + " ");
                    }
                    else
                    {
                        Console.Write(student.lastName + " ");
                    }
                    Console.Write(student.middleName + " ");
                    if (specification is StudentFirstNameSpecification)
                    {
                        Console.Write("\u001b[1m" + student.firstName + "\u001b[22m" + " ");
                    }
                    else
                    {
                        Console.Write(student.firstName + " ");
                    }
                }
                else
                {
                    Stack<char> stack = new Stack<char>();
                    for (int i = 0; i < student.lastName.Length; i++)
                    {
                        stack.Push(student.lastName[i]);
                    }
                    stack.Push(' ');

                    for (int i = 0; i < student.middleName.Length; i++)
                    {
                        stack.Push(student.middleName[i]);
                    }
                    stack.Push(' ');

                    for (int i = 0; i < student.firstName.Length; i++)
                    {
                        stack.Push(student.firstName[i]);
                    }

                    List<string> words = new List<string>();
                    string currentWord = "";

                    while (stack.Count > 0)
                    {
                        if (stack.Peek() == ' ')
                        {
                            stack.Pop();
                            words.Add(currentWord);
                            currentWord = "";
                        }
                        else
                        {
                            currentWord = currentWord + stack.Pop();
                        }
                    }

                    if (currentWord != "")
                    {
                        words.Add(currentWord);
                    }

                    for (int i = 0; i < words.Count; i++)
                    {
                        if (i == 0 && specification is StudentFirstNameSpecification)
                        {
                            Console.Write("\u001b[1m" + words[i] + "\u001b[22m" + " ");
                        }

                        else if (i == words.Count - 1 && specification is StudentLastNameSpecification)
                        {
                            Console.Write("\u001b[1m" + words[i] + "\u001b[22m" + " ");
                        }
                        else
                        {
                            Console.Write(words[i] + " ");
                        }
                    }
                }

                if (specification is StudentClassNameSpecification)
                {
                    Console.Write("\u001b[1m" + student.className + "\u001b[22m" + " ");
                }
                else
                {
                    Console.Write(student.className + " ");
                }
                if (specification is StudentScoreSpecification)
                {
                    Console.WriteLine("\u001b[1m" + student.score + "\u001b[22m" + " ");
                }
                else
                {
                    Console.WriteLine(student.score + " ");
                }
            }
        }

        private bool IsIdValid(string id)
        {
            string pattern = @"N[0-9]{2}[a-zA-Z]{4}[0-9]{3}";
            return Regex.IsMatch(id, pattern);
        }

        private bool IsClassValid(string id)
        {
            string pattern = @"D[0-9]{2}[A-Z]{4}[0-9]{2}-N";
            return Regex.IsMatch(id, pattern);
        }

        private void DisplayOption()
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Choose function: ");
            Console.WriteLine("1. Find students.");
            Console.WriteLine("2. Add student.");
            Console.WriteLine("3. Remove student.");
            Console.WriteLine("4. Display top score students.");
            Console.WriteLine("5. Display bottom score students.");
            Console.WriteLine("6. Sort students.");
            Console.WriteLine("7. Exit!");
        }

        private ISpecification<Student> Specification()
        {
            Console.WriteLine("Choose a specification to sort: ");
            Console.WriteLine("1. Id.");
            Console.WriteLine("2. Name.");
            Console.WriteLine("3. Score.");
            Console.Write("Your choice: ");

            int choice = -1;

            while (true)
            {
                choice = int.Parse(Console.ReadLine());
                Console.WriteLine();

                if (choice > 0 && choice <= 3)
                {
                    break;
                }
                Console.WriteLine("Invalid choice. Please try again!");
            }

            switch (choice)
            {
                case 1:
                    return new StudentIdSpecification();
                case 2:
                    return new StudentFirstNameSpecification();
                case 3:
                    return new StudentScoreSpecification();
                default:
                    return new StudentIdSpecification();
            }
        }


    }
}
