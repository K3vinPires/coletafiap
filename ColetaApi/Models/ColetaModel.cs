namespace ColetaApi.Models
{
    public class ColetaModel
    {
        public int Pk_id { get; set; }
        public string Ds_coleta { get; set; }
        public string Ds_tipocoleta { get; set; }
        public DateTime Dt_coleta { get; set; }

        public ColetaModel() { }

        public ColetaModel(int pk_id, string ds_coleta, string ds_tipocoleta, DateTime dt_coleta)
        {
            Pk_id = pk_id;
            Ds_coleta = ds_coleta;
            Ds_tipocoleta = ds_tipocoleta;
            Dt_coleta = dt_coleta;
        }
    }
}
