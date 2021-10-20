using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTree
{
    bool flag;

    HashSet<Prism> m;

    QuadTree[] st;
    Vector2 origin;
    float r;


    public QuadTree(int d, Vector2 p1, float r1)
    {
        origin = p1;
        r = r1;

        if (d <= 0)
        {
            m = new HashSet<Prism>();
            flag = true;
            return;
        }

        st = new QuadTree[4];
        st[0] = new QuadTree(d - 1, new Vector2(p1.x - r / 2, p1.y - r / 2), r / 2);
        st[1] = new QuadTree(d - 1, new Vector2(p1.x - r / 2, p1.y + r / 2), r / 2);
        st[2] = new QuadTree(d - 1, new Vector2(p1.x + r / 2, p1.y - r / 2), r / 2);
        st[3] = new QuadTree(d - 1, new Vector2(p1.x + r / 2, p1.y + r / 2), r / 2);
    }

    public List<Prism[]> register(Prism p)
    {

        List<Prism[]> col = new List<Prism[]>();

        if (!flag)
        {


            if (p.bounds[0].x < origin.x && p.bounds[0].y < origin.y)
            {
                col.AddRange(st[0].register(p));
            }

            if (p.bounds[0].x < origin.x && p.bounds[1].y > origin.y)
            {
                col.AddRange(st[1].register(p));
            }

            if (p.bounds[1].x > origin.x && p.bounds[0].y < origin.y)
            {
                col.AddRange(st[2].register(p));
            }

            if (p.bounds[1].x > origin.x && p.bounds[1].y > origin.y)
            {
                col.AddRange(st[3].register(p));
            }

            return col;
        }


        if (m.Contains(p))
        {

            return col;
        }
        foreach (Prism member in m)
        {
            Prism[] collision = new Prism[2];
            collision[0] = member;
            collision[1] = p;
            col.Add(collision);
        }

        m.Add(p);
        return col;
    }

    public void draw()
    {
        if (flag)
        {
            return;
        }

        Vector3 l = new Vector3(origin.x - r, 0, origin.y);
        Vector3 ri = new Vector3(origin.x + r, 0, origin.y);
        Vector3 up = new Vector3(origin.x, 0, origin.y + r);
        Vector3 down = new Vector3(origin.x, 0, origin.y - r);

        var clr = Color.magenta;
        Debug.DrawLine(l, ri, clr);
        Debug.DrawLine(up, down, clr);

        foreach (QuadTree qt in st)
        {
            qt.draw();
        }
    }

    public void print(string s)
    {
        MonoBehaviour.print(s);
    }


}
