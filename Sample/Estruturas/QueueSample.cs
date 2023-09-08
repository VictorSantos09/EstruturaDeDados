namespace Sample.Estruturas;
internal class QueueSample
{
    public static Queue<string> Tasks = new();

    public static void AddTask()
    {
        Console.WriteLine("Digite a tarefa a ser realizada");
        Console.Write("Digite: ");
        string task = Console.ReadLine();

        Tasks.Enqueue(task);
        Console.WriteLine($"Tarefa {task} adicionada");
    }

    public static void RemoveTask()
    {
        string task = Tasks.Dequeue();
        Console.WriteLine($"Tarefa {task} removida");
    }

    public static void View()
    {
        string task = Tasks.Peek();
        Console.WriteLine($"A tarefa no topo é {task}");
    }

    public static bool Executar()
    {
        Console.WriteLine("0 - Sair");
        Console.WriteLine("1 - Adicionar Tarefa");
        Console.WriteLine("2 - Remover Tarefa");
        Console.WriteLine("3 - Ver Tarefas");

        switch (Console.ReadLine())
        {
            case "0":
                return false;

            case "1":
                AddTask();
                break;
                
            case "2":
                RemoveTask();
                break;

            case "3":
                View();
                break;
            default:
                Console.WriteLine("opção inválida, tente novamente");
                break;
        }
        return true;
    }
}