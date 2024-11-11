
public class StudentNode
{
    public int StudentId { get; set; }  // Mã số sinh viên
    public string FullName { get; set; }  // Họ và tên
    public string PhoneNumber { get; set; }  // Số điện thoại
    public double GPA { get; set; }  // Điểm trung bình
    public StudentNode Left { get; set; }  // Con trái
    public StudentNode Right { get; set; }  // Con phải

    public StudentNode(int studentId, string fullName, string phoneNumber, double gpa)
    {
        StudentId = studentId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        GPA = gpa;
        Left = Right = null;
    }
}

public class LinkedList
{
    public class ListNode
    {
        public StudentNode Student { get; set; }  // Sinh viên
        public ListNode Next { get; set; }  // Liên kết tới phần tử tiếp theo

        public ListNode(StudentNode student)
        {
            Student = student;
            Next = null;
        }
    }

    public ListNode Top { get; set; }  // Phần tử đầu tiên của danh sách

    public void Push(StudentNode student)  // Thêm sinh viên vào đầu danh sách
    {
        var newNode = new ListNode(student);
        newNode.Next = Top;
        Top = newNode;
    }

    public void Print()  // In danh sách sinh viên
    {
        var current = Top;
        while (current != null)
        {
            Console.WriteLine($"Mã số SV: {current.Student.StudentId}, Họ tên: {current.Student.FullName}, SĐT: {current.Student.PhoneNumber}, Điểm TB: {current.Student.GPA}");
            current = current.Next;
        }
    }

    public void SortByGPA()  // Sắp xếp danh sách theo điểm trung bình
    {
        if (Top == null || Top.Next == null)
            return;

        for (var current = Top; current != null; current = current.Next)
        {
            for (var index = current.Next; index != null; index = index.Next)
            {
                if (current.Student.GPA > index.Student.GPA)
                {
                    var temp = current.Student;
                    current.Student = index.Student;
                    index.Student = temp;
                }
            }
        }
    }
}

public class BinarySearchTree
{
    private StudentNode root;  // Gốc của cây

    public void Insert(int studentId, string fullName, string phoneNumber, double gpa)  // Thêm sinh viên vào cây
    {
        root = InsertRecursive(root, studentId, fullName, phoneNumber, gpa);
    }

    private StudentNode InsertRecursive(StudentNode node, int studentId, string fullName, string phoneNumber, double gpa)
    {
        if (node == null)
            return new StudentNode(studentId, fullName, phoneNumber, gpa);

        if (studentId < node.StudentId)
            node.Left = InsertRecursive(node.Left, studentId, fullName, phoneNumber, gpa);
        else if (studentId > node.StudentId)
            node.Right = InsertRecursive(node.Right, studentId, fullName, phoneNumber, gpa);
        else
            Console.WriteLine("Mã số sinh viên đã tồn tại!");

        return node;
    }

    public void InOrderTraversal(LinkedList stack)  // Duyệt cây theo thứ tự LNR
    {
        InOrderTraversalRecursive(root, stack);
    }

    private void InOrderTraversalRecursive(StudentNode node, LinkedList stack)
    {
        if (node != null)
        {
            InOrderTraversalRecursive(node.Left, stack);  // Duyệt nhánh trái
            stack.Push(node);  // Thêm sinh viên vào danh sách
            InOrderTraversalRecursive(node.Right, stack);  // Duyệt nhánh phải
        }
    }
}

public class StudentManager
{
    private BinarySearchTree tree;  // Cây nhị phân tìm kiếm

    public StudentManager()
    {
        tree = new BinarySearchTree();
    }

    public void AddStudent()  // Thêm sinh viên mới
    {
        Console.Write("Nhập mã số SV: ");
        int studentId = int.Parse(Console.ReadLine());

        Console.Write("Nhập họ tên SV: ");
        string fullName = Console.ReadLine();

        Console.Write("Nhập số điện thoại SV: ");
        string phoneNumber = Console.ReadLine();

        Console.Write("Nhập điểm trung bình: ");
        double gpa = double.Parse(Console.ReadLine());

        tree.Insert(studentId, fullName, phoneNumber, gpa);
        Console.WriteLine("Thêm sinh viên thành công!");
    }

    public void ListStudents()  // Liệt kê danh sách sinh viên
    {
        LinkedList stack = new LinkedList();
        tree.InOrderTraversal(stack);

        Console.WriteLine("Danh sách sinh viên (Duyệt LNR):");
        stack.Print();

        Console.WriteLine("Sắp xếp theo điểm trung bình...");
        stack.SortByGPA();
        Console.WriteLine("Danh sách sau khi sắp xếp:");
        stack.Print();
    }

    public void ShowMenu()  // Hiển thị menu
    {
        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Thêm sinh viên mới");
            Console.WriteLine("2. Liệt kê sinh viên (Duyệt LNR & Sắp xếp theo GPA)");
            Console.WriteLine("3. Thoát");
            Console.Write("Chọn chức năng: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddStudent();
                    break;
                case 2:
                    ListStudents();
                    break;
                case 3:
                    return;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng thử lại.");
                    break;
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        Console.InputEncoding = System.Text.Encoding.Unicode;

        StudentManager manager = new StudentManager();
        manager.ShowMenu();
    }
}
