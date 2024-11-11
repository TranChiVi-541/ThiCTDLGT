public class Node
{
    public int BookId { get; set; }  // Mã số sách
    public string BookName { get; set; }  // Tên sách
    public int Quantity { get; set; }  // Số lượng sách
    public Node Left { get; set; }
    public Node Right { get; set; }

    public Node(int bookId, string bookName, int quantity)
    {
        BookId = bookId;
        BookName = bookName;
        Quantity = quantity;
        Left = Right = null;
    }
}
public class LinkedList
{
    public class ListNode
    {
        public Node Book { get; set; }
        public ListNode Next { get; set; }

        public ListNode(Node book)
        {
            Book = book;
            Next = null;
        }
    }

    public ListNode Top { get; set; }

    // Thêm sách vào đầu danh sách (stack)
    public void Push(Node book)
    {
        var newNode = new ListNode(book);
        newNode.Next = Top;
        Top = newNode;
    }

    // In tất cả sách trong stack
    public void Print()
    {
        var current = Top;
        while (current != null)
        {
            Console.WriteLine($"ID: {current.Book.BookId}, Name: {current.Book.BookName}, Quantity: {current.Book.Quantity}");
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

    // Thêm sách mới vào cây
    public void Insert(int bookId, string bookName, int quantity)
    {
        root = InsertRecursive(root, bookId, bookName, quantity);
    }

    private Node InsertRecursive(Node node, int bookId, string bookName, int quantity)
    {
        if (node == null)
        {
            return new Node(bookId, bookName, quantity);
        }

        if (bookId < node.BookId)
        {
            node.Left = InsertRecursive(node.Left, bookId, bookName, quantity);
        }
        else if (bookId > node.BookId)
        {
            node.Right = InsertRecursive(node.Right, bookId, bookName, quantity);
        }

        return node;  // Nếu mã số đã tồn tại thì không thêm
    }

    // Tìm kiếm sách theo mã số
    public Node Search(int bookId)
    {
        return SearchRecursive(root, bookId);
    }

    private Node SearchRecursive(Node node, int bookId)
    {
        if (node == null || node.BookId == bookId)
        {
            return node;
        }

        if (bookId < node.BookId)
        {
            return SearchRecursive(node.Left, bookId);
        }
        else
        {
            return SearchRecursive(node.Right, bookId);
        }
    }

    // Duyệt cây theo thứ tự NLR và lưu vào stack
    public void PreOrderTraversal(LinkedList stack)
    {
        PreOrderTraversalRecursive(root, stack);
    }

    private void PreOrderTraversalRecursive(Node node, LinkedList stack)
    {
        if (node != null)
        {
            stack.Push(node);  // Thêm sách vào stack
            PreOrderTraversalRecursive(node.Left, stack);  // Duyệt nhánh trái
            PreOrderTraversalRecursive(node.Right, stack);  // Duyệt nhánh phải
        }
    }
}
public class LibraryManager
{
    private BinarySearchTree tree;

    public LibraryManager()
    {
        tree = new BinarySearchTree();
    }

    // Thêm một đầu sách mới
    public void AddBook()
    {
        Console.Write("Mời nhập mã số của sách: ");
        int bookId = int.Parse(Console.ReadLine());
        Console.Write("Mời nhập tên của sách:");
        string bookName = Console.ReadLine();
        Console.Write("Mời nhập số lượng: ");
        int quantity = int.Parse(Console.ReadLine());

        tree.Insert(bookId, bookName, quantity);
        Console.WriteLine("Thêm sách thành công ");
    }

    // Tìm kiếm một sách theo mã số
    public void SearchBook()
    {
        Console.Write("Mời nhập mã số sách cần tìm: ");
        int bookId = int.Parse(Console.ReadLine());

        var bookNode = tree.Search(bookId);
        if (bookNode != null)
        {
            Console.WriteLine($"Sách được tìm thấy: Mã số: {bookNode.BookId}, Tên: {bookNode.BookName}, Số lượng: {bookNode.Quantity}");
        }
        else
        {
            Console.WriteLine("Không tìm thấy sách ");
        }
    }

    // Duyệt cây theo thứ tự NLR và lưu vào danh sách liên kết đơn theo kiểu stack
    public void ListBooks()
    {
        LinkedList stack = new LinkedList();
        tree.PreOrderTraversal(stack);
        Console.WriteLine("Danh sách các đầu sách là:");
        stack.Print();
    }

    // Hiển thị menu và xử lý lựa chọn
    public void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Thêm một đầu sách mới");
            Console.WriteLine("2. Tìm kiếm một sách theo mã số");
            Console.WriteLine("3. Danh sách các đầu sách");
            Console.WriteLine("4. Thoát");
            Console.Write("Chọn một chức năng: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddBook();
                    break;
                case 2:
                    SearchBook();
                    break;
                case 3:
                    ListBooks();
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
        LibraryManager manager = new LibraryManager();
        manager.ShowMenu();
    }
}
