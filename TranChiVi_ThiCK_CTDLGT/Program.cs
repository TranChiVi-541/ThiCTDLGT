
public class Node
{
    public string EnglishWord { get; set; }
    public LinkedList Meanings { get; set; } // Danh sách nghĩa của từ
    public Node Left { get; set; }
    public Node Right { get; set; }

    public Node(string englishWord)
    {
        EnglishWord = englishWord;
        Meanings = new LinkedList();
        Left = Right = null;
    }
}

public class LinkedList
{
    public class ListNode
    {
        public string Meaning { get; set; }
        public ListNode Next { get; set; }

        public ListNode(string meaning)
        {
            Meaning = meaning;
            Next = null;
        }
    }

    public ListNode Head { get; set; }

    public void Add(string meaning)
    {
        var newNode = new ListNode(meaning);
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
            Console.WriteLine(current.Meaning);
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

    public void Insert(string word, string meaning)
    {
        root = InsertRecursive(root, word, meaning);
    }

    private Node InsertRecursive(Node node, string word, string meaning)
    {
        if (node == null)
        {
            var newNode = new Node(word);
            newNode.Meanings.Add(meaning);
            return newNode;
        }

        if (string.Compare(word, node.EnglishWord) < 0)
        {
            node.Left = InsertRecursive(node.Left, word, meaning);
        }
        else if (string.Compare(word, node.EnglishWord) > 0)
        {
            node.Right = InsertRecursive(node.Right, word, meaning);
        }
        else
        {
            // Nếu từ đã tồn tại trong từ điển, chỉ thêm nghĩa mới
            node.Meanings.Add(meaning);
        }

        return node;
    }

    public bool Search(string word, out LinkedList meanings)
    {
        var node = SearchRecursive(root, word);
        if (node != null)
        {
            meanings = node.Meanings;
            return true;
        }
        else
        {
            meanings = null;
            return false;
        }
    }

    private Node SearchRecursive(Node node, string word)
    {
        if (node == null)
        {
            return null;
        }

        if (string.Compare(word, node.EnglishWord) < 0)
        {
            return SearchRecursive(node.Left, word);
        }
        else if (string.Compare(word, node.EnglishWord) > 0)
        {
            return SearchRecursive(node.Right, word);
        }
        else
        {
            return node;
        }
    }

    public void PrintInOrder()
    {
        PrintInOrderRecursive(root);
    }

    private void PrintInOrderRecursive(Node node)
    {
        if (node != null)
        {
            PrintInOrderRecursive(node.Left);
            Console.WriteLine($"Word: {node.EnglishWord}");
            node.Meanings.Print();
            PrintInOrderRecursive(node.Right);
        }
    }
}
public class DictionaryManager
{
    private BinarySearchTree dictionary;

    public DictionaryManager()
    {
        dictionary = new BinarySearchTree();
    }

    public void AddWord()
    {
        Console.Write("Mời nhập từ vựng mới:");
        string word = Console.ReadLine();
        Console.Write("Mời nhập nghĩa của từ vựng:");
        string meaning = Console.ReadLine();

        dictionary.Insert(word, meaning);
        Console.WriteLine("Từ đã được thêm thành công");
    }

    public void SearchWord()
    {
        Console.Write("Nhập từ vựng cần kiếm:");
        string word = Console.ReadLine();
        if (dictionary.Search(word, out var meanings))
        {
            Console.WriteLine($"Nghĩa của từ vựng: {word}:");
            meanings.Print();
        }
        else
        {
            Console.WriteLine("Không tìm thấy từ vựng !");
        }
    }

    public void PrintAllWords()
    {
        Console.WriteLine("Tất cả từ vựng:");
        dictionary.PrintInOrder();
    }

    public void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1.Mời bạn nhập từ mới:");
            Console.WriteLine("2.Từ bạn cần tìm:");
            Console.WriteLine("3. In ra tất cả từ vựng:");
            Console.WriteLine("4.Thoát chương trình");
            Console.Write("Chọn một chức năng:");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddWord();
                    break;
                case 2:
                    SearchWord();
                    break;
                case 3:
                    PrintAllWords();
                    break;
                case 4:
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
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
        DictionaryManager manager = new DictionaryManager();
        manager.ShowMenu();

    }
}



