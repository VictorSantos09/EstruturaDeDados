using Aula1.Model;
using System.Reflection.PortableExecutable;
using System.Threading.Channels;

namespace Sample.Estruturas;
internal class ListaSample
{
    public static List<Pessoa> Pessoas = new();

    public static bool Executar() // executa o programa contendo todas as opção fornecidas para demonstração
    {
        Console.WriteLine("0 - Sair");
        Console.WriteLine("1 - Ver Pessoas Registradas");
        Console.WriteLine("2 - Registrar Pessoas");
        Console.WriteLine("3 - Atualizar Pessoas");
        Console.WriteLine("4 - Deletar Pessoa");
        Console.WriteLine("5 - Filtrar Pessoa");
        int opcao = Convert.ToInt32(Console.ReadLine());

        switch (opcao)
        {
            case 0:
                return false;
            case 1:
                VerPessoas();
                break;

            case 2:
                RegistrarPessoa();
                break;

            case 3:
                AtualizarPessoa();
                break;

            case 4:
                DeletarPessoa();
                break;

            case 5:
                FiltrarPessoa();
                break;

            default:
                Console.WriteLine("Inválido, tente novamente");
                break;
        }

        return true;
    }

    private static void VerPessoas() // printa as pessoas na lista
    {
        int contador = 0;
        foreach (Pessoa p in Pessoas)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nPessoa Nº {++contador}\n");
            Console.ForegroundColor = ConsoleColor.White;
            PrintarPessoa(p);
        }
    }

    private static void PrintarEndereco(Endereco endereco) // printa o endereço
    {
        Console.WriteLine($"NUMERO: {endereco.Numero}");
        Console.WriteLine($"LOGRADOURO: {endereco.Logradouro}");
        Console.WriteLine($"ID: {endereco.Id}");
        Console.WriteLine($"ID DA PESSOA: {endereco.IdClient}");
    }

    private static void RegistrarPessoa() // adiciona uma nova pessoa na lista
    {
        Pessoa pessoa = new(); // cria uma nova instância de pessoa
        Console.Write("NOME: ");
        pessoa.Nome = Console.ReadLine();
        Console.Write("DATA DE NASCIMENTO: ");
        pessoa.DataNac = DateTime.Parse(Console.ReadLine());
        Console.Write("CPF: ");
        pessoa.CPF = Console.ReadLine();

        Console.Write("----------PRINCIPAL ENDEREÇO----------\n");
        RegistrarEndereco(pessoa.PrincipalEndereco); // chama o metodo que registra o endereço, passando como parâmetro o Principal Endereço da pessoa
        pessoa.PrincipalEndereco.IdClient = pessoa.Id;

        while (true)
        {
            Console.Write("Deseja adicionar outro endereço? 1 - SIM | 2 - NÃO\n");
            Console.Write("Digite: ");
            int opcao = Convert.ToInt32(Console.ReadLine());

            if (opcao == 1)
            {
                Endereco endereco = new(); // cria uma nova instância de endereço, para adicionar na lista de endereços da mesma pessoa sendo registrada
                RegistrarEndereco(endereco); // registra o outro novo endereço
                pessoa.Enderecos.Add(endereco); // adiciona o outro novo endereço na lista de endereços da pessoa
            }
            else
                break;
        }
        Pessoas.Add(pessoa); // adiciona efetivamente a nova pessoa na lista com seu endereço principal e a lista de outro endereços
    }

    private static void RegistrarEndereco(Endereco endereco) // registra as informações necessárias para o endereço
    {
        Console.Write("NÚMERO: ");
        endereco.Numero = Console.ReadLine();
        Console.Write("LOGRADOURO: ");
        endereco.Logradouro = Console.ReadLine();
    }

    private static void AtualizarPessoa() // atualiza as informações de uma pessoa selecionada na lista
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("AVISO: AS INFORMAÇÕES SERÃO TROCADAS MESMO QUE NÃO INFORME, SE NÃO DESEJA ALTERAR, ESCREVA NOVAMENTE");
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine("Digite o número da pessoa que deseja atualizar");
        VerPessoas(); // apresenta novamente as pessoas existentes na lista
        Console.Write("Digite: ");
        int opcao = Convert.ToInt32(Console.ReadLine());

        Pessoa pessoaParaAtualizar = Pessoas.ElementAt(--opcao); // pega a pessoa na posição do index fornecida (o -- é devido a lista ser um index abaixo do que é visível na tela)
        Console.Write($"Novo nome (atual: {pessoaParaAtualizar.Nome}) Digite: ");
        pessoaParaAtualizar.Nome = Console.ReadLine();

        Console.Write($"Nova data de nascimento (atual: {pessoaParaAtualizar.DataNac}) Digite: ");
        pessoaParaAtualizar.DataNac = DateTime.Parse(Console.ReadLine());

        Console.Write($"Novo CPF (atual: {pessoaParaAtualizar.CPF}) Digite: ");
        pessoaParaAtualizar.CPF = Console.ReadLine();

        Console.Write($"Novo numero endereço principal (atual: {pessoaParaAtualizar.PrincipalEndereco.Numero}) Digite: ");
        pessoaParaAtualizar.PrincipalEndereco.Numero = Console.ReadLine();

        Console.Write($"Novo logradouro endereço principal (atual: {pessoaParaAtualizar.PrincipalEndereco.Logradouro}) Digite: ");
        pessoaParaAtualizar.PrincipalEndereco.Logradouro = Console.ReadLine();

        Pessoas.Remove(pessoaParaAtualizar); // remove a pessoa existente da lista
        Pessoas.Insert(opcao, pessoaParaAtualizar); // insere ela novamente na lista no index de "opcao" (onde ela já estava)

        Console.WriteLine($"Pessoa atualizada");
    }

    private static void DeletarPessoa() // deleta uma pessoa na lista
    {
        Console.WriteLine("Digite o Nº da pessoa que deseja deletar");
        Console.Write("Digite: ");
        VerPessoas();

        int opcao = Convert.ToInt32(Console.ReadLine());
        Pessoa pessoaParaDeletar = Pessoas.ElementAt(--opcao); // pega a pessoa na posição do index fornecida (o -- é devido a lista ser um index abaixo do que é visível na tela)

        Pessoas.Remove(pessoaParaDeletar); // remove a pessoa da lista
        Console.WriteLine("Pessoa deletada");
    }

    private static void FiltrarPessoa() // filtra uma pessoa na lista por condições
    {
        Console.WriteLine("Selecione a opção de filtro");
        Console.WriteLine("1 - ID");
        Console.WriteLine("2 - CPF");
        Console.WriteLine("3 - NOME");
        Console.WriteLine("4 - DATA DE NASCIMENTO");
        int opcao = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Digite o texto para filtrar");
        string filtro = Console.ReadLine();

        Pessoa pessoa;
        switch (opcao)
        {
            case 1:
                pessoa = Pessoas.Find(x => x.Id == Guid.Parse(filtro)); // busca uma pessoa na lista com o id fornecido em filtro
                PrintarPessoa(pessoa);
                break;

            case 2:
                pessoa = Pessoas.Where(x => x.CPF == filtro).First(); // busca e converte na lista onde for encontrado na lista uma pessoa com o mesmo CPF de filtro
                PrintarPessoa(pessoa);
                break;

            case 3:
                pessoa = Pessoas.Find(x => x.Nome == filtro); // busca uma pessoa na lista com o mesmo nome de filtro
                PrintarPessoa(pessoa);
                break;

            case 4:
                pessoa = Pessoas.Find(x => x.DataNac.ToShortDateString() == DateTime.Parse(filtro).ToShortDateString());
                PrintarPessoa(pessoa);
                break;
            default:
                Console.WriteLine("Inválido, tente novamente");
                break;
        }

        //Pessoas.RemoveAll() remove todos os elementos que atendam a condição ex: Pessoas.RemoveAll(x => x.Nome == "Ana")
        //Pessoas.Clear() limpa a lista removendo todos
        //Pessoas.AddRange() adiciona outra lista de pessoa na lista
        //Pessoas.Order() ordena a lista
        //Pessoas.OrderBy() ordena a lista por condição ex: Pessoas.OrderBy(x => x.Nome)
        //Pessoas.Exists() verifica se existe um elemento na lista
        //Pessoas.Contains() verifica se contem um elemento na lista
        //Pessoas.Count armazena a quantidade de elementos na lista
        //Pessoas.FindAll() busca todas as pessoas na lista que atendam a uma condição ex: Pessoas.FindAll(x => x.Nacionalidade == "brasileiro")
        //Pessoas.Reverse(); inverte a lista
        // entre outros...

        // aviso: a maioria deles tem parâmetros, apenas não coloquei pois tem sobrecargas diferentes
    }

    private static void PrintarPessoa(Pessoa pessoa) // printa uma pessoa na tela
    {
        if (pessoa == null)
            Console.WriteLine("Pessoa não existe");

        else
        {
            Console.WriteLine("----------PESSOA ENCONTRADA----------");
            Console.WriteLine($"ID: {pessoa.Id}");
            Console.WriteLine($"NOME: {pessoa.Nome}");
            Console.WriteLine($"DATA DE NASCIMENTO: {pessoa.DataNac}");
            Console.WriteLine($"CPF: {pessoa.CPF}");

            if (pessoa.PrincipalEndereco.IdClient != Guid.Empty)
            {
                Console.WriteLine("-----------PRINCIPAL ENDEREÇO-----------");
                PrintarEndereco(pessoa.PrincipalEndereco);
            }

            if (pessoa.Enderecos.Count > 0)
            {
                Console.WriteLine("-----------OUTROS ENDEREÇOS-----------");
                foreach (Endereco e in pessoa.Enderecos)
                {
                    PrintarEndereco(e);
                }
            }
        }
    }
}