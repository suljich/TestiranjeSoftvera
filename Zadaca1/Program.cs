using System;
using System.Collections.Generic;

namespace Test;
public class Test
{
    static void Main()
    {
        Agencija agencija = new Agencija();

        agencija.DodajStan(new NenamjestenStan(50, Lokacija.Gradsko, true));
        agencija.DodajStan(new NenamjestenStan(80, Lokacija.Prigradsko, true));
        agencija.DodajStan(new NamjestenStan(40, Lokacija.Prigradsko, true, 2000, 2));
        agencija.DodajStan(new NamjestenStan(80, Lokacija.Gradsko, false, 3000, 6));

        agencija.DodajOsoblje(new Batler("John", "Doe", new DateTime(2022, 1, 1), 5));
        agencija.DodajOsoblje(new Kuhar("Alice", "Smith", new DateTime(2022, 2, 1), 1500, new List<string> { "Pasta", "Steak" }));
        agencija.DodajOsoblje(new Vrtlar("Bob", "Johnson", new DateTime(2022, 3, 1), 1200, new NenamjestenStan(60, Lokacija.Gradsko, true)));

        Console.WriteLine("Apartments:");
        agencija.IspisiStanove();
        Console.WriteLine("\nStaff:");
        agencija.IspisiOsoblje();
    }
}

public enum Lokacija
{
    Gradsko,
    Prigradsko,
}

public abstract class Stan
{
    public int BrojKvadrata { get; set; }
    public Lokacija Lokacija { get; set; }
    public bool Internet { get; set; }

    virtual public void Ispisi()
    {
        Console.Write($"\n{BrojKvadrata} ");
        Console.Write(Lokacija == Lokacija.Gradsko ? "Gradsko " : "Prigradsko ");
    }

    abstract public double ObracunajCijenuNajma();
}

public class NenamjestenStan : Stan
{
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

public class NamjestenStan : Stan
{
    public double VrijednostNamjestaja { get; set; }
    public int BrojAparata { get; set; }

    public NamjestenStan(int povrsina, Lokacija lokacija, bool internet, double vrijednostNamjestaja, int brojAparata)
    {
        BrojKvadrata = povrsina;
        Lokacija = lokacija;
        Internet = internet;
        VrijednostNamjestaja = vrijednostNamjestaja;
        BrojAparata = brojAparata;
    }

    override public void Ispisi()
    {
        base.Ispisi();
        Console.Write("Namjesten ");
        Console.Write(Internet ? "Da " : "Ne ");
        Console.Write($"{VrijednostNamjestaja} ");
        Console.Write($"{BrojAparata}");
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
        if (BrojAparata < 3)
        {
            value += VrijednostNamjestaja * 0.01;
        }
        else
        {
            value += VrijednostNamjestaja * 0.02;
        }
        return value;
    }
}

public class Osoba
{
    public string Ime { get; set; }
    public string Prezime { get; set; }
    public DateTime DatumUposlenja { get; set; }

    public Osoba(string ime, string prezime, DateTime datumUposlenja)
    {
        Ime = ime;
        Prezime = prezime;
        DatumUposlenja = datumUposlenja;
    }

    virtual public void Ispisi()
    {
        Console.WriteLine($"{Ime} {Prezime} {DatumUposlenja.ToShortDateString()}");
    }
}

public class Batler : Osoba
{
    public int GodineIskustva { get; set; }

    public Batler(string ime, string prezime, DateTime datumUposlenja, int godineIskustva)
        : base(ime, prezime, datumUposlenja)
    {
        GodineIskustva = godineIskustva;
    }

    override public void Ispisi()
    {
        base.Ispisi();
        Console.Write($"{GodineIskustva} godina iskustva\n");
    }
}

public class Kuhar : Osoba
{
    public double MjesecnaPlata { get; set; }
    public List<string> Jela { get; set; }

    public Kuhar(string ime, string prezime, DateTime datumUposlenja, double mjesecnaPlata, List<string> jela)
        : base(ime, prezime, datumUposlenja)
    {
        MjesecnaPlata = mjesecnaPlata;
        Jela = jela;
    }

    override public void Ispisi()
    {
        base.Ispisi();
        Console.Write($"{MjesecnaPlata} KM\n");
    }
}

public class Vrtlar : Osoba
{
    public double MjesecnaPlata { get; set; }
    public NenamjestenStan Stan { get; set; }

    public Vrtlar(string ime, string prezime, DateTime datumUposlenja, double mjesecnaPlata, NenamjestenStan stan)
        : base(ime, prezime, datumUposlenja)
    {
        MjesecnaPlata = mjesecnaPlata;
        Stan = stan;
    }

    override public void Ispisi()
    {
        base.Ispisi();
        Console.Write($"{MjesecnaPlata} KM\n");
    }
}

public class Agencija
{
    public readonly List<Stan> stanovi = new List<Stan>();
    public readonly List<Osoba> osoblje = new List<Osoba>();

    public void DodajStan(Stan stan)
    {
        stanovi.Add(stan);
    }

    public void DodajOsoblje(Osoba osoba)
    {
        osoblje.Add(osoba);
    }

    public void IspisiStanove()
    {
        stanovi.Sort((s1, s2) => s1.ObracunajCijenuNajma().CompareTo(s2.ObracunajCijenuNajma()));

        foreach (Stan stan in stanovi)
        {
            stan.Ispisi();
            Console.WriteLine($"Ukupna cijena najma stana je {stan.ObracunajCijenuNajma():F2}.");
        }
    }

    public void IspisiOsoblje()
    {
        osoblje.Sort((o1, o2) =>
        {
            if (o1 is not null && o2 is not null)
            {
                return ((Osoba)o1).DatumUposlenja.CompareTo(((Osoba)o2).DatumUposlenja);
            }
            return 0;
        });

        foreach (Osoba osoba in osoblje)
        {
            osoba.Ispisi();
        }
    }
}
