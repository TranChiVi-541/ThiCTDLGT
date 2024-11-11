public class Node
{
    public int ProductId { get; set; }  // Mã số mặt hàng
    public string ProductName { get; set; }  // Tên mặt hàng
    public int UnitPrice { get; set; }  // Đơn giá mặt hàng
    public Node Left { get; set; }
    public Node Right { get; set; }

    public Node(int productId, string productName, int unitPrice)
    {
        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Left = Right = null;
    }
}
public class LinkedList
{
    public class ListNode
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int UnitPrice { get; set; }
        public ListNode Next { get; set; }

        public ListNode(int productId, string productName, int unitPrice)
        {
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Next = null;
        }
    }

    public ListNode Head { get; set; }

    public void AddToFront(int productId, string productName, int unitPrice)
    {
        var newNode = new ListNode(productId, productName, unitPrice);
        newNode.Next = Head;
        Head = newNode;
    }

    public void Print()
    {
        var current = Head;
        while (current != null)
        {
            Console.WriteLine($"ID: {current.ProductId}, Name: {current.ProductName}, Price: {current.UnitPrice}");
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

    // Chèn mặt hàng vào cây nhị phân
    public void Insert(int productId, string productName, int unitPrice)
    {
        root = InsertRecursive(root, productId, productName, unitPrice);
    }

    private Node InsertRecursive(Node node, int productId, string productName, int unitPrice)
    {
        if (node == null)
        {
            return new Node(productId, productName, unitPrice);
        }

        if (productId < node.ProductId)
        {
            node.Left = InsertRecursive(node.Left, productId, productName, unitPrice);
        }
        else if (productId > node.ProductId)
        {
            node.Right = InsertRecursive(node.Right, productId, productName, unitPrice);
        }

        return node;  // Nếu mã số đã tồn tại thì không làm gì
    }

    // Tìm mặt hàng theo mã số
    public Node Search(int productId)
    {
        return SearchRecursive(root, productId);
    }

    private Node SearchRecursive(Node node, int productId)
    {
        if (node == null || node.ProductId == productId)
        {
            return node;
        }

        if (productId < node.ProductId)
        {
            return SearchRecursive(node.Left, productId);
        }
        else
        {
            return SearchRecursive(node.Right, productId);
        }
    }

    // Duyệt cây theo thứ tự giảm dần và thêm vào danh sách liên kết
    public void InOrderDescending(LinkedList list)
    {
        InOrderDescendingRecursive(root, list);
    }

    private void InOrderDescendingRecursive(Node node, LinkedList list)
    {
        if (node != null)
        {
            InOrderDescendingRecursive(node.Right, list);
            list.AddToFront(node.ProductId, node.ProductName, node.UnitPrice);
            InOrderDescendingRecursive(node.Left, list);
        }
    }
}
public class InventoryManager
{
    private BinarySearchTree inventoryTree;

    public InventoryManager()
    {
        inventoryTree = new BinarySearchTree();
    }

    // Thêm mặt hàng mới
    public void AddProduct()
    {
        Console.Write("Mời nhập mã số sản phẩm:");
        int productId = int.Parse(Console.ReadLine());
        Console.Write("Mời nhập tên sản phẩm: ");
        string productName = Console.ReadLine();
        Console.Write("Mời nhập giá sản phẩm: ");
        int unitPrice = int.Parse(Console.ReadLine());

        inventoryTree.Insert(productId, productName, unitPrice);
        Console.WriteLine("Thêm sản phẩm thành công ");
    }

    // Tìm kiếm mặt hàng theo mã số
    public void SearchProduct()
    {
        Console.Write("Mời nhập mã số sản phẩm cần tìm: ");
        int productId = int.Parse(Console.ReadLine());

        var productNode = inventoryTree.Search(productId);
        if (productNode != null)
        {
            Console.WriteLine($"Sản phẩm được tìm thấy: Mã số: {productNode.ProductId}, Tên: {productNode.ProductName}, Giá: {productNode.UnitPrice}");
        }
        else
        {
            Console.WriteLine("Không tìm thấy sản phẩm.");
        }
    }

    // Lưu tất cả mặt hàng vào danh sách liên kết đơn theo thứ tự giảm dần của mã số
    public void ListProductsDescending()
    {
        LinkedList list = new LinkedList();
        inventoryTree.InOrderDescending(list);
        Console.WriteLine("Liệt kê tất cả các mặt hàng theo thứ tự giảm dần:");
        list.Print();
    }

    // Hiển thị menu và xử lý lựa chọn
    public void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Thêm sản phẩm mới:");
            Console.WriteLine("2. Tìm kiếm sản phẩm: ");
            Console.WriteLine("3.Liệt kê tất cả các mặt hàng theo thứ tự giảm dần:");
            Console.WriteLine("4. Thoát");
            Console.Write("Chọn một chức năng: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddProduct();
                    break;
                case 2:
                    SearchProduct();
                    break;
                case 3:
                    ListProductsDescending();
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
        InventoryManager manager = new InventoryManager();
        manager.ShowMenu();
    }
}

