class proveedor
{
    public int ID { get; set; }
    public string Nombre { get; set; }
    public string Servicio { get; set; }
    public int Calificacion { get; set; }

    public override string ToString()
    {
        return $"ID: {ID}, Nombre: {Nombre}, Servicio: {Servicio}, Calificación: {Calificacion}";
    }
}
