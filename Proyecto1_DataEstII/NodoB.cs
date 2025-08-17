using System;
using System.Collections.Generic;

class NodoB
{
    public List<int> Claves = new List<int>();               // IDss de proveedores)
    public List<Proveedor> Datos = new List<Proveedor>();    // datos conectados a cada clave
    public List<NodoB> Hijos = new List<NodoB>();            //  hijos del nodo 
    public bool EsHoja = true;                               //indica  hoja

    //  Constructor
    public NodoB(bool esHoja = true)
    {
        this.EsHoja = esHoja;
    }
}
