namespace Sample.Estruturas;
internal class StackSample
{
    private static readonly Stack<string> _history = new();

    public static void Search()
    {
        Console.WriteLine("Digite sua busca na web");
        Console.Write("Digite: ");
        string path = Console.ReadLine();
        _history.Push(path);
    }

    public static void History()
    {
        if (_history.Count <= 0)
            Console.WriteLine("Histórico vazio");

        else
        {
            Console.WriteLine("HISTÓRICO");
            foreach (var item in _history)
            {
                Console.WriteLine(item);
            }
        }
    }

    public static void PreviousSearch()
    {
        string page = _history.Pop();
        Console.WriteLine($"removida a busca de '{page}'");
    }

    public static void LastSearch()
    {
        string page = _history.Peek();
        Console.WriteLine($"Sua ultima pesquisa foi '{page}'");
    }

    public static bool Executar()
    {
        Console.WriteLine("0 - Sair");
        Console.WriteLine("1 - Pesquisar");
        Console.WriteLine("2 - Voltar");
        Console.WriteLine("3 - Ver histórico");
        Console.Write("Digite: ");

        switch (Console.ReadLine())
        {
            case "0":
                return false;

            case "1":
                Search();
                break;

            case "2":
                PreviousSearch();
                break;

            case "3":
                History();
                break;
            default:
                Console.WriteLine("Opção inválida, tente novamente");
                break;
        }
        return true;
    }
}