namespace Aula1.Model;

public class Pessoa
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; }
    public DateTime DataNac { get; set; }
    public string CPF { get; set; }
    public Endereco PrincipalEndereco { get; set; } = new();
    public List<Endereco> Enderecos { get; set; } = new();
}

public class Endereco
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid IdClient { get; set; }
    public string Logradouro { get; set; }
    public string Numero { get; set; }
}