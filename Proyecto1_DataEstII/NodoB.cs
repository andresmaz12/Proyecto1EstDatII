using System;
using System.Collections.Generic;
class NodoB
{
    public List<int> Claves = new List<int>();
    public List<Proveedor> Datos = new List<Proveedor>();
    public List<NodoB> Hijos = new List<NodoB>();
    public bool EsHoja = true;
}