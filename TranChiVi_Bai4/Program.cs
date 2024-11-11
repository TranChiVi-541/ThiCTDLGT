public class Node
{
    public int DepartmentId { get; set; }  // Mã số khoa
    public string DepartmentName { get; set; }  // Tên khoa
    public int EstablishYear { get; set; }  // Năm thành lập
    public Node Left { get; set; }
    public Node Right { get; set; }

    public Node(int departmentId, string departmentName, int establishYear)
    {
        DepartmentId = departmentId;
        DepartmentName = departmentName;
        EstablishYear = establishYear;
        Left = Right = null;
    }
}
public class LinkedList
{
    public class ListNode
    {
        public Node Department { get; set; }
        public ListNode Next { get; set; }

        public ListNode(Node department)
        {
            Department = department;
            Next = null;
        }
    }

    public ListNode Head { get; set; }

    public void AddToEnd(Node department)
    {
        var newNode = new ListNode(department);
        if (Head == null)
        {
            Head = newNode;
        }
        else
        {
            var current = Head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
    }

    public void Print()
    {
        var current = Head;
        while (current != null)
        {
            Console.WriteLine($"ID: {current.Department.DepartmentId}, Name: {current.Department.DepartmentName}, Year: {current.Department.EstablishYear}");
            current = current.Next;
        }
    }
}
public class BinarySearchTree
{
    private Node root;

    public BinarySearchTree()
    {
        root = null;
    }

    // Thêm khoa mới vào cây
    public void Insert(int departmentId, string departmentName, int establishYear)
    {
        root = InsertRecursive(root, departmentId, departmentName, establishYear);
    }

    private Node InsertRecursive(Node node, int departmentId, string departmentName, int establishYear)
    {
        if (node == null)
        {
            return new Node(departmentId, departmentName, establishYear);
        }

        if (departmentId < node.DepartmentId)
        {
            node.Left = InsertRecursive(node.Left, departmentId, departmentName, establishYear);
        }
        else if (departmentId > node.DepartmentId)
        {
            node.Right = InsertRecursive(node.Right, departmentId, departmentName, establishYear);
        }

        return node; // Nếu mã số đã tồn tại thì không thêm
    }

    // Tìm kiếm khoa theo mã số
    public Node Search(int departmentId)
    {
        return SearchRecursive(root, departmentId);
    }

    private Node SearchRecursive(Node node, int departmentId)
    {
        if (node == null || node.DepartmentId == departmentId)
        {
            return node;
        }

        if (departmentId < node.DepartmentId)
        {
            return SearchRecursive(node.Left, departmentId);
        }
        else
        {
            return SearchRecursive(node.Right, departmentId);
        }
    }

    // Đếm số khoa được thành lập vào một năm
    public int CountDepartmentsByYear(int year)
    {
        return CountDepartmentsByYearRecursive(root, year);
    }

    private int CountDepartmentsByYearRecursive(Node node, int year)
    {
        if (node == null)
        {
            return 0;
        }

        int count = 0;
        if (node.EstablishYear == year)
        {
            count = 1;
        }

        return count + CountDepartmentsByYearRecursive(node.Left, year) + CountDepartmentsByYearRecursive(node.Right, year);
    }

    // Duyệt cây theo thứ tự LNR và thêm các khoa vào danh sách liên kết đơn
    public void InOrderTraversal(LinkedList list)
    {
        InOrderTraversalRecursive(root, list);
    }

    private void InOrderTraversalRecursive(Node node, LinkedList list)
    {
        if (node != null)
        {
            InOrderTraversalRecursive(node.Left, list);
            list.AddToEnd(node); // Thêm khoa vào danh sách
            InOrderTraversalRecursive(node.Right, list);
        }
    }
}
public class UniversityManager
{
    private BinarySearchTree tree;

    public UniversityManager()
    {
        tree = new BinarySearchTree();
    }

    // Thêm khoa mới
    public void AddDepartment()
    {
        Console.Write("Mời nhập mã số khoa: ");
        int departmentId = int.Parse(Console.ReadLine());
        Console.Write("Mời nhập tên khoa: ");
        string departmentName = Console.ReadLine();
        Console.Write("Mời nhập năm thành lập:");
        int establishYear = int.Parse(Console.ReadLine());

        tree.Insert(departmentId, departmentName, establishYear);
        Console.WriteLine("Thêm khoa thành công.");
    }

    // Đếm số lượng khoa được thành lập vào năm x
    public void CountDepartmentsByYear()
    {
        Console.Write("Mời nhập năm khoa được thành lập: ");
        int year = int.Parse(Console.ReadLine());

        int count = tree.CountDepartmentsByYear(year);
        Console.WriteLine($"Các khoa được thành lập vào năm  {year}: {count}");
    }

    // Duyệt cây theo thứ tự LNR và lưu vào danh sách liên kết đơn
    public void ListDepartments()
    {
        LinkedList list = new LinkedList();
        tree.InOrderTraversal(list);
        Console.WriteLine("Danh sách các khoa:");
        list.Print();
    }

    // Hiển thị menu và xử lý lựa chọn
    public void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Thêm khoa mới");
            Console.WriteLine("2.Đếm số lượng khoa được thành lập vào năm");
            Console.WriteLine("3. Duyệt cây theo thứ tự LNR và lưu vào danh sách liên kết đơn");
            Console.WriteLine("4. Thoát");
            Console.Write("Chọn một chức năng: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddDepartment();
                    break;
                case 2:
                    CountDepartmentsByYear();
                    break;
                case 3:
                    ListDepartments();
                    break;
                case 4:
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
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
        UniversityManager manager = new UniversityManager();
        manager.ShowMenu();
    }
}
