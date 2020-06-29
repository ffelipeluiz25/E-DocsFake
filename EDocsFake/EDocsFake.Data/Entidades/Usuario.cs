namespace EDocsFake.Data.Entidades
{
    public class Usuario
    {
        public const string _tabela = "TB_USUARIO";
        public string Tabela { get { return _tabela; } }
        public int CodigoUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}