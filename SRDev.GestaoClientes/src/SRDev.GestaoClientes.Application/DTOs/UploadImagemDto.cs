public class UploadImagemDto
{
    public string NomeArquivo { get; set; }
    public byte[] Conteudo { get; set; }

    public UploadImagemDto(string nomeArquivo, byte[] conteudo)
    {
        NomeArquivo = nomeArquivo;
        Conteudo = conteudo;
    }
}
