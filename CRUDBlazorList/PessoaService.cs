using Aula1.Model;
using System.Net.Http.Json;

namespace CRUDBlazorList;
internal class PessoaService
{
    private static readonly HttpClient _http = new();
    public static Random Random { get; set; } = new();

    public static async Task BuildRandomPeople(List<Pessoa> people)
    {
        HttpResponseMessage peopleResponse = await _http.GetAsync("https://random-data-api.com/api/v2/users?size=10");
        var peopleContent = await peopleResponse.Content.ReadFromJsonAsync<IEnumerable<PessoaModelApi>>();

        foreach (var p in peopleContent)
        {
            Pessoa pessoa = ConvertToPersonModel(p);
            await BuildAddress(pessoa);
            people.Add(pessoa);
        }
    }

    private static Pessoa ConvertToPersonModel(PessoaModelApi model)
    {
        var pessoaId = Guid.NewGuid();
        Pessoa output = new Pessoa
        {
            Id = pessoaId,
            DataNac = DateTime.Parse(model.date_of_birth),
            Nome = $"{model.first_name} {model.last_name}",
            CPF = GenerateCPF()
        };
        return output;
    }

    private static Endereco ConvertToEnderecoModel(PessoaAddressModel model, Pessoa pessoa)
    {
        Endereco output = new()
        {
            Id = Guid.NewGuid(),
            IdClient = pessoa.Id,
            Numero = Random.Next(34, 985).ToString(),
            Logradouro = model.street_address
        };
        return output;
    }

    private static async Task BuildAddress(Pessoa person)
    {
        HttpResponseMessage addressResponse = await _http.GetAsync($"https://random-data-api.com/api/v2/addresses?size={Random.Next(2, 9)}");
        var addressContent = await addressResponse.Content.ReadFromJsonAsync<IEnumerable<PessoaAddressModel>>();
        foreach (var a in addressContent)
        {
            var end = ConvertToEnderecoModel(a, person);
            person.Enderecos.Add(end);
        }
    }

    private static string GenerateCPF()
    {
        string output = "";
        for (int i = 0; i < 11; i++)
        {
            output += Random.Next(10);
        }
        return output;
    }
}

internal class PessoaModelApi
{
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string date_of_birth { get; set; }
    public string username { get; set; }
    public string phone_number { get; set; }
}

internal class PessoaAddressModel
{
    public string street_address { get; set; }
}