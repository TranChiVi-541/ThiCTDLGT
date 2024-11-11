public class Node
{
    public int CustomerId { get; set; }  // Mã số khách hàng
    public string FullName { get; set; }  // Họ tên khách hàng
    public string PhoneNumber { get; set; }  // Số điện thoại
    public double Points { get; set; }  // Điểm thành viên
    public double TotalAmount { get; set; }  // Tổng tiền mua hàng
    public Node Left { get; set; }
    public Node Right { get; set; }

    public Node(int customerId, string fullName, string phoneNumber, double amount)
    {
        CustomerId = customerId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Points = 0;  // Điểm ban đầu là 0
        TotalAmount = amount;
        Left = Right = null;
    }

    // Cập nhật thông tin điểm và tổng tiền khi khách hàng đã tồn tại
    public void UpdateCustomer(double amount)
    {
        Points += amount * 0.1;  // Tích lũy 10% số tiền mua hàng vào điểm
        TotalAmount += amount;  // Cộng số tiền vào tổng tiền của khách hàng
    }
}
public class LinkedList
{
    public class ListNode
    {
        public Node Customer { get; set; }
        public ListNode Next { get; set; }

        public ListNode(Node customer)
        {
            Customer = customer;
            Next = null;
        }
    }

    public ListNode Top { get; set; }

    // Thêm khách hàng vào đầu danh sách (stack)
    public void Push(Node customer)
    {
        var newNode = new ListNode(customer);
        newNode.Next = Top;
        Top = newNode;
    }

    // In danh sách khách hàng
    public void Print()
    {
        var current = Top;
        while (current != null)
        {
            Console.WriteLine($"Customer ID: {current.Customer.CustomerId}, Name: {current.Customer.FullName}, Phone: {current.Customer.PhoneNumber}, Points: {current.Customer.Points}, Total Amount: {current.Customer.TotalAmount}");
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

    // Thêm hoặc cập nhật khách hàng vào cây
    public void Insert(int customerId, string fullName, string phoneNumber, double amount)
    {
        root = InsertRecursive(root, customerId, fullName, phoneNumber, amount);
    }

    private Node InsertRecursive(Node node, int customerId, string fullName, string phoneNumber, double amount)
    {
        if (node == null)
        {
            return new Node(customerId, fullName, phoneNumber, amount);
        }

        if (customerId < node.CustomerId)
        {
            node.Left = InsertRecursive(node.Left, customerId, fullName, phoneNumber, amount);
        }
        else if (customerId > node.CustomerId)
        {
            node.Right = InsertRecursive(node.Right, customerId, fullName, phoneNumber, amount);
        }
        else
        {
            node.UpdateCustomer(amount);  // Nếu khách hàng đã tồn tại, chỉ cập nhật thông tin
        }

        return node;
    }

    // Duyệt cây theo thứ tự LRN (Left - Right - Node) và lưu vào stack
    public void PostOrderTraversal(LinkedList stack)
    {
        PostOrderTraversalRecursive(root, stack);
    }

    private void PostOrderTraversalRecursive(Node node, LinkedList stack)
    {
        if (node != null)
        {
            PostOrderTraversalRecursive(node.Left, stack);  // Duyệt nhánh trái
            PostOrderTraversalRecursive(node.Right, stack);  // Duyệt nhánh phải
            stack.Push(node);  // Thêm khách hàng vào stack
        }
    }
}
public class CustomerManager
{
    private BinarySearchTree tree;

    public CustomerManager()
    {
        tree = new BinarySearchTree();
    }

    // Thêm khách hàng mới hoặc cập nhật thông tin nếu khách hàng đã tồn tại
    public void AddCustomer()
    {
        Console.Write("Nhập mã số khách hàng : ");
        int customerId = int.Parse(Console.ReadLine());

        Console.Write("Nhập tên khách hàng : ");
        string fullName = Console.ReadLine();

        Console.Write("Nhập số điện thoại khách hàng : ");
        string phoneNumber = Console.ReadLine();

        Console.Write("Nhập số tiền mua hàng: ");
        double amount = double.Parse(Console.ReadLine());

        tree.Insert(customerId, fullName, phoneNumber, amount);
        Console.WriteLine("Thêm khách hàng thành công");
    }

    // Duyệt cây theo thứ tự LRN và lưu vào danh sách liên kết đơn theo kiểu stack
    public void ListCustomers()
    {
        LinkedList stack = new LinkedList();
        tree.PostOrderTraversal(stack);
        Console.WriteLine("Danh sách khách hàng");
        stack.Print();
    }

    // Hiển thị menu và xử lý lựa chọn
    public void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Add or update customer");
            Console.WriteLine("2. List customers in post-order");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddCustomer();
                    break;
                case 2:
                    ListCustomers();
                    break;
                case 3:
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
        CustomerManager manager = new CustomerManager();
        manager.ShowMenu();
    }
}
