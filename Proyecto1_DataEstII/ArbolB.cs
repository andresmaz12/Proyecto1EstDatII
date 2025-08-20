using System;
using System.Collections.Generic;

//AndresHueco
//Arriba los Pumas HDSPTMR
class BTree
{
    private NodoB raiz = new NodoB();  
    private int orden; // grado minimo  del arbo B

    //seccion de eliminacion
    public void Eliminar(int id)
    {
        EliminarRec(raiz, id);

        // Si  raíz queda sin claves y tiene hijos   raíz baja un nivel
        if (raiz.Claves.Count == 0 && !raiz.EsHoja)
        {
            raiz = raiz.Hijos[0];
        }
    }

    public BTree(int orden)
    {
        this.orden = orden; // se inicia el orden
    }

    // insertar un nuevo proveedor
    public void Insertar(Proveedor prov)
    {
        NodoB r = raiz;
        // Si la raíz está llena, se divide
        if (r.Claves.Count == (2 * orden - 1))
        {
            NodoB s = new NodoB();
            raiz = s;
            s.EsHoja = false;
            s.Hijos.Add(r);
            DividirHijo(s, 0, r); // division  de la raíz
            InsertarNoLleno(s, prov); /// insercion  en el nuevo nodo
        }
        else
        {
            InsertarNoLleno(r, prov); // Si no se  llena inserta directamente
        }
    }

    // insercion  en un nodo no lleno
    private void InsertarNoLleno(NodoB x, Proveedor prov)
    {
        int i = x.Claves.Count - 1;
        if (x.EsHoja)
        {
            // recorrido por Ids
            while (i >= 0 && prov.ID < x.Claves[i])
            {
                x.Claves.Add(0);
                x.Datos.Add(new Proveedor(0, "", "", 0));

                // mueve claves adelante
                x.Claves[i + 1] = x.Claves[i];
                x.Datos[i + 1] = x.Datos[i];
                i--;
            }

            // inserta la nueva clave 
            x.Claves.Insert(i + 1, prov.ID);
            x.Datos.Insert(i + 1, prov);
        }
        else
        {
            // buscar el hijo 
            while (i >= 0 && prov.ID < x.Claves[i])
            {
                i--;
            }
            i++;

            // se divide si el hijo está lleno
            if (x.Hijos[i].Claves.Count == (2 * orden - 1))
            {
                DividirHijo(x, i, x.Hijos[i]);
                if (prov.ID > x.Claves[i])
                {
                    i++;
                }
            }

            // inserta recursivamente en hijo
            InsertarNoLleno(x.Hijos[i], prov);
        }
    }

    // dividir un hijo cuando rebasa n
    private void DividirHijo(NodoB padre, int indice, NodoB hijo)
    {
        NodoB z = new NodoB(hijo.EsHoja); // nuevo nodo derecha
        int medio = orden - 1;

        // copia claves de la derecha al nuevo nodo
        for (int j = 0; j < orden - 1; j++)
        {
            z.Claves.Add(hijo.Claves[medio + 1 + j]);
            z.Datos.Add(hijo.Datos[medio + 1 + j]);
        }

        // copian los hijos si no es hoja
        if (!hijo.EsHoja)
        {
            for (int j = 0; j < orden; j++)
            {
                z.Hijos.Add(hijo.Hijos[medio + 1 + j]);
            }
        }

        // rebajar el hijo 
        hijo.Claves.RemoveRange(medio, hijo.Claves.Count - medio);
        hijo.Datos.RemoveRange(medio, hijo.Datos.Count - medio);

        if (!hijo.EsHoja)
        {
            hijo.Hijos.RemoveRange(medio + 1, hijo.Hijos.Count - (medio + 1));
        }

        // insertar el nuevo hijo en el padre
        padre.Hijos.Insert(indice + 1, z);

        // 
        padre.Claves.Insert(indice, hijo.Claves[medio]);
        padre.Datos.Insert(indice, hijo.Datos[medio]);

        hijo.Claves.RemoveAt(medio);
        hijo.Datos.RemoveAt(medio);
    }

    // Buscar proveedores por servicio
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

    // Mostrar en orden
    public void MostrarOrdenado()
    {
        MostrarOrdenadoRec(raiz);
    }

    private void MostrarOrdenadoRec(NodoB nodo)
    {
        for (int i = 0; i < nodo.Datos.Count; i++)
        {
            if (!nodo.EsHoja)
            {
                MostrarOrdenadoRec(nodo.Hijos[i]);
            }
            Console.WriteLine(nodo.Datos[i]);
        }
        if (!nodo.EsHoja)
        {
            MostrarOrdenadoRec(nodo.Hijos[nodo.Datos.Count]);
        }
    }

    // sección para eliminar en hoja

    public void EliminarEnHoja(int id)
    {
        EliminarEnHojaRec(raiz, id);
    }

    private void EliminarEnHojaRec(NodoB nodo, int id)
    {
        int idx = nodo.Claves.FindIndex(k => k == id);

        if (idx != -1 && nodo.EsHoja)
        {
            // si se encontró el ID en hoja se borra
            nodo.Claves.RemoveAt(idx);
            nodo.Datos.RemoveAt(idx);
            return;
        }

        // si no es hoja buscar en hijos
        if (!nodo.EsHoja)
        {
            foreach (var hijo in nodo.Hijos)
            {
                EliminarEnHojaRec(hijo, id);
            }
        }
    }

    private void EliminarRec(NodoB nodo, int id)
    {
        if (nodo == null) return;

        int idx = nodo.Claves.FindIndex(k => k == id);

        if (idx != -1 && nodo.EsHoja)
        {
            nodo.Claves.RemoveAt(idx);
            nodo.Datos.RemoveAt(idx);
            return;
        }

        if (!nodo.EsHoja)
        {
            foreach (var hijo in nodo.Hijos)
            {
                EliminarRec(hijo, id);
            }
        }
    }
}
