namespace Sistema.Entidades
{
    public class Articulo
    {
        public int Id { get; set; }
        public int IdCategoria { get; set; }
        public string Codigo { get; set; }
        public string Nomnbre { get; set; }
        public decimal Precioventa {  get; set; }
        public int Stock {  get; set; }
        public string Descripcion { get; set; }
        public string Image {  get; set; }
        public bool Estado { get; set; }

    }
}
