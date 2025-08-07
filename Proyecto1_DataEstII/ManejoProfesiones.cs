class ManejoProfesiones
{
    string ID { get; set; }
    string NombreTrabajador { get; set; }
    string TipoServicio { get; set; }
    int Calificacion { get; set; }

    public void Registro(string iD, string nombreTrabajador, string tipoServicio, int calificacion)
    {
        ID = iD;
        NombreTrabajador = nombreTrabajador;
        TipoServicio = tipoServicio;
        Calificacion = calificacion;
    }
    public void RetornoDatos()
    {

    }
}