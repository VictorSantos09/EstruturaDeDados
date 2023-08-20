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
    5. As palavras static, public, private, internal e void não é necessário o significado para entender a lista
 */
int opcao = Convert.ToInt32(Console.ReadLine());
if (opcao == 1)
{
    Console.WriteLine("aguarde...");
    await PessoaService.BuildRandomPeople(ListaSample.Pessoas);
    Console.WriteLine($"sua lista já contém {ListaSample.Pessoas.Count} pessoas");
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