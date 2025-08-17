class Proveedor
{
    public int ID { get; set; }
    public string Nombre { get; set; }
    public string Servicio { get; set; }
    public int Calificacion { get; set; }

    public Proveedor(int id, string nombre, string servicio, int calificacion)
    {
        ID = id;
        Nombre = nombre;
        Servicio = servicio;
        Calificacion = calificacion;
    }
    
    public override string ToString()
    {
        return $"ID: {ID}, Nombre: {Nombre}, Servicio: {Servicio}, Calificación: {Calificacion}";
    }
}