namespace Micro_api.Models
{

    //public class personas
    //{
    //    public bool HasError { get; set; }
    //    public personasData dataMami { get; set; }
    //    public List<errorItems> erroresP { get; set; }
    //}
    public class personas
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public int cantidadMatriculadas { get; set; }
        public int periodo { get; set; }
        public string email { get; set; }
        public Int64 phone { get; set; }
        public Int64 cuenta { get; set; }

    }
}
