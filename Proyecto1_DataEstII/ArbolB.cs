class BTree
{
    private NodoB root;
    private int t;

    public BTree(int t)
    {
        this.root = null;
        this.t = t;
    }

    public void Traverse()
    {
        if (root != null)
            root.Traverse();
    }

    public NodoB Search(int key)
    {
        return root == null ? null : root.BUsqueda(key);
    }

    public void Insert(int key)
    {
        if (root == null)
        {
            root = new NodoB(t, true);
            root.Clave[0] = key;
            root.n = 1;
        }
        else
        {
            if (root.n == 2 * t - 1)
            {
                NodoB s = new NodoB(t, false);
                s.hijos[0] = root;
                s.DividirNodos(0, root);

                int i = 0;
                if (s.Clave[0] < key)
                    i++;
                s.hijos[i].InsertarDatos(key);

                root = s;
            }
            else
            {
                root.InsertarDatos(key);
            }
        }
    }
}