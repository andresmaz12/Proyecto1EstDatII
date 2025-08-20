using System;
using System.Collections.Generic;
//AndresHueco
//Arriba los Pumas HDSPTMR

class BTree
{
    private NodoB raiz = new NodoB(); 
    private int orden; // grado minimo del ArbolB

    public BTree(int orden)
    {
        this.orden = orden; //se inicia el orden
    }

    //insertar unuevo provedor
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
            DividirHijo(s, 0, r); //division de la raiz
            InsertarNoLleno(s, prov); ///insercion en el nuevo nodo
        }
        else
        {
            InsertarNoLleno(r, prov); // Si no esta llena inserta directamente
        }
    }

    // Inserción en un nodo no lleno
    private void InsertarNoLleno(NodoB x, Proveedor prov)
    {
        int i = x.Claves.Count - 1;
        if (x.EsHoja) 
        {
            // recorrido por Ids
            while (i >= 0 && prov.ID < x.Claves[i])
            {
                x.Claves.Add(0);
                x.Datos.Add(null);

                // mueve claves a adelante
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

            //  se divide si el hijo esta lleno

            if (x.Hijos[i].Claves.Count == (2 * orden - 1))
            {
                DividirHijo(x, i, x.Hijos[i]);
                if (prov.ID > x.Claves[i])
                {
                    i++;
                }
            }

            // inserta recursivamente en  hijo
            InsertarNoLleno(x.Hijos[i], prov);
        }
    }

    // divide un hijo cuando rebasa n
    
    private void DividirHijo(NodoB padre, int indice, NodoB hijo)
    {
        NodoB z = new NodoB(hijo.EsHoja); //nuevo nodo derecha
        int medio = orden - 1;

        // copia claves de la derecha al nuevo nodo


        for (int j = 0; j < orden - 1; j++)
        {
            z.Claves.Add(hijo.Claves[medio + 1 + j]);
            z.Datos.Add(hijo.Datos[medio + 1 + j]);
        }

        //  copian los hijos si no es hoja

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

        // subir la clave del medio 
        padre.Claves.Insert(indice, hijo.Claves[medio]);

        padre.Datos.Insert(indice, hijo.Datos[medio]);

        // Eliminar la clave media 

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

    // busqueda recursiva por servicio
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

    // Mostrar en  orden
    public void MostrarOrdenado()
    {
        MostrarOrdenadoRec(raiz);
    }

    //recorrido arnol B

    private void MostrarOrdenadoRec(NodoB nodo)
    {
        for (int i = 0; i < nodo.Datos.Count; i++)
        {
            if (!nodo.EsHoja)
            {
                MostrarOrdenadoRec(nodo.Hijos[i]);
            }
<<<<<<< Updated upstream
            Console.WriteLine(nodo.Datos[i]);
=======
            Console.WriteLine("==================");
            Console.WriteLine(nodo.Datos[i]);
            Console.WriteLine("==================");
            Console.WriteLine();
>>>>>>> Stashed changes
        }
        if (!nodo.EsHoja)
        {
            MostrarOrdenadoRec(nodo.Hijos[nodo.Datos.Count]);
        }
    }
}