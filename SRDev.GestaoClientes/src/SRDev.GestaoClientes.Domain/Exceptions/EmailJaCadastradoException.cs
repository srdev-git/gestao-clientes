namespace SRDev.GestaoClientes.Domain.Exceptions
{
    public class EmailJaCadastradoException : Exception
    {
        //TODO: Mascarar e-mail
        public EmailJaCadastradoException(string email)
            : base($"O e-mail '{email}' já está cadastrado para outro cliente.") { }
    }
}
