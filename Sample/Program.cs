using Sample.Estruturas;

Console.ForegroundColor = ConsoleColor.White;
/*
    1. A partes principais do projeto está na pasta Estruturas
    2. a Program.cs pode ignorar porque apenas tem questões para manter o programa rodando
    3. Deixei comentado algumas partes do código referentes as operações com lista para facilitar o entendimento
    4. Tentei deixar o mais simples que deu mas para não ficar com muitas linhas tive que utilizar métodos
    5. As palavras async, await, static, public, private, internal e void não é necessário o significado para entender a lista
 */

bool rodar = true;
while (rodar)
{
    try
    {
        Console.WriteLine("Escolha o exemplo a executar");
        Console.WriteLine("1 - Lista");
        Console.WriteLine("2 - Fila");
        Console.WriteLine("3 - Pilha");
        Console.Write("Digite: ");

        switch (Console.ReadLine())
        {
            case "1":
                rodar = ListaSample.Executar();
                break;

            case "2":
                rodar = QueueSample.Executar(); // FIFO
                break;

            case "3":
                rodar = StackSample.Executar(); // LIFO
                break;

            default:
                Console.WriteLine("opção inválida");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Um erro ocorreu...");
        Console.WriteLine($"Erro: {ex.Message}\n");
        Console.WriteLine($"StackTrace: {ex.StackTrace}");
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine("\nPressione ESC para encerrar ou qualquer outra para continuar");
        var key = Console.ReadKey().Key;
        if (key == ConsoleKey.Escape)
            break;
    }
}