using CRUDBlazorList;
using Sample.Estruturas;

Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("Deseja iniciar com uma lista contendo valores? 1 - SIM | 2 - NÃO");
Console.Write("Digite: ");

/*
    1. A parte principal do projeto está em ListaSample.cs
    2. a Program.cs pode ignorar porque apenas tem questões para manter o programa rodando e popular a lista automaticamente se desejado para economizar tempo
    3. Deixei comentado algumas partes do código referentes as operações com lista para facilitar o entendimento
    4. Tentei deixar o mais simples que deu mas para não ficar com muitas linhas tive que utilizar métodos
    5. As palavras async, await, static, public, private, internal e void não é necessário o significado para entender a lista
 */

int opcao = Convert.ToInt32(Console.ReadLine());
if (opcao == 1)
{
    Console.WriteLine("Criando pessoas aleatórias");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Aviso: Essas pessoas não terão endereço principal, mas podem ter de 1-8 outros endereços");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Aguarde...");
    await PessoaService.BuildRandomPeople(ListaSample.Pessoas);
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"sua lista agora contém {ListaSample.Pessoas.Count} pessoas");
    Console.ForegroundColor = ConsoleColor.White;
}

while (true)
{
    try
    {
        bool rodar = ListaSample.Executar();
        if (rodar == false)
            break;
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