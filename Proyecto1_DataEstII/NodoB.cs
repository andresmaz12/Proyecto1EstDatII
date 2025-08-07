using System;
using System.Collections.Generic;
class NodoB
{
    public int[] Clave;
    public int Grado;
    public NodoB[] hijos;
    public int n;
    public bool EsHoja;
    public NodoB(int grado, bool esHoja)
    {
        Grado = grado;
        EsHoja = esHoja;
        this.Clave = new int[Grado - 1];
        this.hijos = new NodoB[Grado];
        this.n = 0;
    }

    public void Traverse()
    {
        for (int i = 0; i < n; i++)
        {
            if (!EsHoja)
                hijos[i].Traverse();
            Console.Write($"{Clave[i]} ");
        }

        if (!EsHoja)
            hijos[n].Traverse();
    }
    public NodoB BUsqueda(int clave)
    {
        int i = 0;
        while (i < n && clave > Clave[i])
            i++;

        if (i < n && Clave[i] == clave)
            return this;

        if (EsHoja)
            return null;

        return hijos[i].BUsqueda(clave);
    }
    public void InsertarDatos(int clave)
    {
        int i = n - 1;

        if (EsHoja)
        {
            while (i >= 0 && Clave[i] > clave)
            {
                Clave[i + 1] = Clave[i];
                i--;
            }

            Clave[i + 1] = clave;
            n++;
        }
        else
        {
            while (i >= 0 && Clave[i] > clave)
                i--;

            if (hijos[i + 1].n == 2 * Grado - 1)
            {
                DividirNodos(i + 1, hijos[i + 1]);

                if (clave > Clave[i + 1])
                    i++;
            }

            hijos[i + 1].InsertarDatos(clave);
        }
    }
    public void DividirNodos(int i, NodoB y)
    {
        NodoB z = new NodoB(y.Grado, y.EsHoja);
        z.n = Grado - 1;

        for (int j = 0; j < Grado - 1; j++)
            z.Clave[j] = y.Clave[j + Grado];

        if (!y.EsHoja)
        {
            for (int j = 0; j < Grado; j++)
                z.hijos[j] = y.hijos[j + Grado];
        }

        y.n = Grado - 1;

        for (int j = n; j >= i + 1; j--)
            hijos[j + 1] = hijos[j];

        hijos[i + 1] = z;

        for (int j = n - 1; j >= i; j--)
            Clave[j + 1] = Clave[j];

        Clave[i] = y.Clave[Grado - 1];
        n++;
    }
}