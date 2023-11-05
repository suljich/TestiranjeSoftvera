using System;

namespace Zadatak;
class Test
{
    static void Main()
    {
        Stan[] stanovi = new Stan[4];
        stanovi[0] = new NenamjestenStan(50, Lokacija.Gradsko, true);
        stanovi[1] = new NenamjestenStan(80, Lokacija.Prigradsko, true);
        stanovi[2] = new NamjestenStan(40, Lokacija.Prigradsko, true, 2000, 2);
        stanovi[3] = new NamjestenStan(80, Lokacija.Gradsko, false, 3000, 6);
        Console.WriteLine("Površina Lokacija Namješten Internet Vrijednost namještaja Broj aparata");
        foreach (Stan stan in stanovi)
        {
            stan.Ispisi();
        }
        int minPovrsina = 0;
        int maxPovrsina = 0;
        Console.WriteLine("Unesite minimalnu zeljenu povrsinu");
        while (!Int32.TryParse(Console.ReadLine(), out minPovrsina) || minPovrsina < 0)
        {
            Console.WriteLine("Unos nije ispravan");
        }
        Console.WriteLine("Unesite maksimalnu zeljenu povrsinu");
        while (!Int32.TryParse(Console.ReadLine(), out maxPovrsina) || minPovrsina < 0)
        {
            Console.WriteLine("Unos nije ispravan");
        }
        foreach (Stan stan in stanovi)
        {
            if (stan.BrojKvadrata >= minPovrsina && stan.BrojKvadrata <= maxPovrsina)
            {
                stan.Ispisi();
                Console.WriteLine("Ukupna cijena najma stana je {0:F2}.", stan.ObracunajCijenuNajma());
            }
        }
        Console.ReadLine();
    }
}

enum Lokacija
{
    Gradsko,
    Prigradsko,
}

abstract class Stan
{
    int _brojKvadrata;
    Lokacija _lokacija;
    bool _internet;

    public int BrojKvadrata
    {
        get => _brojKvadrata;
        set => _brojKvadrata = value;
    }

    public Lokacija Lokacija
    {
        get => _lokacija;
        set => _lokacija = value;
    }

    public bool Internet
    {
        get => _internet;
        set => _internet = value;
    }

    virtual public void Ispisi()
    {
        Console.Write("\n"+BrojKvadrata+" ");
        Console.Write(Lokacija==Lokacija.Gradsko?"Gradsko ":"Prigradsko ");
    }

    abstract public double ObracunajCijenuNajma();
}

class NenamjestenStan : Stan {

   public NenamjestenStan(int povrsina, Lokacija lokacija, bool internet)
    {
        BrojKvadrata = povrsina;
        Lokacija = lokacija;
        Internet = internet;
    }

    override public void Ispisi()
    {
        base.Ispisi();
        Console.Write("Nenamjesten ");
        Console.Write(Internet ? "Da " : "Ne ");
        Console.Write("\n");
    }

    override public double ObracunajCijenuNajma()
    {
        double value = Lokacija == Lokacija.Gradsko ? 200 : 150;
        value += BrojKvadrata;
        if (Internet)
        {
            value *= 1.02;
        }
        return value;

    }

}

class NamjestenStan : Stan {
   public double vrijednostNamjestaja;
   public int brojAparata;

   public NamjestenStan(int povrsina, Lokacija lokacija, bool internet, double vrijednostNamjestaja, int brojAparata)
    {
        BrojKvadrata = povrsina;
        Lokacija = lokacija;
        Internet = internet;
        this.vrijednostNamjestaja = vrijednostNamjestaja;
        this.brojAparata = brojAparata;
    }

    override public void Ispisi()
    {
        base.Ispisi();
        Console.Write("Namjesten ");
        Console.Write(Internet ? "Da " : "Ne ");
        Console.Write(vrijednostNamjestaja+" ");
        Console.Write(brojAparata);
        Console.Write("\n");
    }

    override public double ObracunajCijenuNajma()
    {
        double value = Lokacija == Lokacija.Gradsko ? 200 : 150;
        value += BrojKvadrata;
        if (Internet)
        {
            value *= 1.01;
        }
        if (brojAparata<3)
        {
            value += vrijednostNamjestaja * 0.01;
        } else
        {
            value += vrijednostNamjestaja * 0.02;
        }
        return value;

    }
}