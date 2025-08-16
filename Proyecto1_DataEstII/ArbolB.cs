class BTree
{
    private NodoB raiz = new NodoB();
    private int orden;

    public ArbolB(int orden)
    {
        this.orden = orden;
    }

    public void Insertar(Proveedor prov)
    {
        NodoB r = raiz;
        if (r.Claves.Count == (2 * orden - 1))
        {
            NodoB s = new NodoB();
            raiz = s;
            s.EsHoja = false;
            s.Hijos.Add(r);
            DividirHijo(s, 0, r);
            InsertarNoLleno(s, prov);
        }
        else
        {
            InsertarNoLleno(r, prov);
        }
    }

    private void InsertarNoLleno(NodoB x, Proveedor prov)
    {
        int i = x.Claves.Count - 1;
        if (x.EsHoja)
        {
            x.Claves.Add(0);
            x.Datos.Add(null);
            while (i >= 0 && prov.ID < x.Claves[i])
            {
                x.Claves[i + 1] = x.Claves[i];
                x.Datos[i + 1] = x.Datos[i];
                i--;
            }
            x.Claves[i + 1] = prov.ID;
            x.Datos[i + 1] = prov;
        }
        else
        {
            while (i >= 0 && prov.ID < x.Claves[i])
            {
                i--;
            }
            i++;
            if (x.Hijos[i].Claves.Count == (2 * orden - 1))
            {
                DividirHijo(x, i, x.Hijos[i]);
                if (prov.ID > x.Claves[i])
                {
                    i++;
                }
            }
            InsertarNoLleno(x.Hijos[i], prov);
        }
    }

    private void DividirHijo(NodoB padre, int indice, NodoB hijo)
    {
        NodoB z = new NodoB();
        z.EsHoja = hijo.EsHoja;
        for (int j = 0; j < orden - 1; j++)
        {
            z.Claves.Add(hijo.Claves[orden + j]);
            z.Datos.Add(hijo.Datos[orden + j]);
        }
        if (!hijo.EsHoja)
        {
            for (int j = 0; j < orden; j++)
            {
                z.Hijos.Add(hijo.Hijos[orden + j]);
            }
        }
        hijo.Claves.RemoveRange(orden - 1, hijo.Claves.Count - (orden - 1));
        hijo.Datos.RemoveRange(orden - 1, hijo.Datos.Count - (orden - 1));
        if (!hijo.EsHoja)
        {
            hijo.Hijos.RemoveRange(orden, hijo.Hijos.Count - orden);
        }
        padre.Hijos.Insert(indice + 1, z);
        padre.Claves.Insert(indice, z.Claves[0]);
        padre.Datos.Insert(indice, z.Datos[0]);
    }

    public List<Proveedor> BuscarPorServicio(string servicio)
    {
        List<Proveedor> resultados = new List<Proveedor>();
        BuscarPorServicioRec(raiz, servicio, resultados);
        return resultados;
    }

    private void BuscarPorServicioRec(NodoB nodo, string servicio, List<Proveedor> resultados)
    {
        for (int i = 0; i < nodo.Claves.Count; i++)
        {
            if (nodo.Datos[i] != null && nodo.Datos[i].Servicio.Equals(servicio, StringComparison.OrdinalIgnoreCase))
            {
                resultados.Add(nodo.Datos[i]);
            }
        }
        if (!nodo.EsHoja)
        {
            foreach (var hijo in nodo.Hijos)
            {
                BuscarPorServicioRec(hijo, servicio, resultados);
            }
        }
    }

    public void MostrarOrdenado()
    {
        MostrarOrdenadoRec(raiz);
    }

    private void MostrarOrdenadoRec(NodoB nodo)
    {
        for (int i = 0; i < nodo.Claves.Count; i++)
        {
            if (!nodo.EsHoja)
            {
                MostrarOrdenadoRec(nodo.Hijos[i]);
            }
            Console.WriteLine(nodo.Datos[i]);
        }
        if (!nodo.EsHoja)
        {
            MostrarOrdenadoRec(nodo.Hijos[nodo.Claves.Count]);
        }
    }
}