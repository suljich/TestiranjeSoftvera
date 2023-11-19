using Test;

[TestFixture]
public class AgencijaTests
{
    [Test]
    public void DodajStan_DodajeStan_UListiStanova()
    {
        Agencija agencija = new Agencija();
        Stan stan = new NenamjestenStan(50, Lokacija.Gradsko, true);

        agencija.DodajStan(stan);

        Assert.That(agencija.stanovi, Does.Contain(stan));
    }

    [Test]
    public void DodajOsoblje_DodajeOsoblje_UListiOsoblja()
    {
        Agencija agencija = new();
        Osoba osoba = new Batler("John", "Doe", DateTime.Now, 5);

        agencija.DodajOsoblje(osoba);

        Assert.That(agencija.osoblje, Does.Contain(osoba));
    }

    [Test]
    public void IspisiStanove_VracaSortiraneStanove()
    {
        Agencija agencija = new();
        Stan stan1 = new NenamjestenStan(50, Lokacija.Gradsko, true);
        Stan stan2 = new NamjestenStan(40, Lokacija.Prigradsko, true, 2000, 2);
        agencija.DodajStan(stan1);
        agencija.DodajStan(stan2);

        string result = CaptureConsoleOutput(() => agencija.IspisiStanove());

        var lines = result.Split('\n').Select(line => line.Trim()).ToList();

        Assert.That(lines.IndexOf("Namjesten"), Is.LessThan(lines.IndexOf("Nenamjesten")));
    }


    [Test]
    public void IspisiOsoblje_VracaSortiranoOsoblje()
    {
        Agencija agencija = new();
        Osoba osoba1 = new Kuhar("Alice", "Smith", DateTime.Now, 1500, new List<string> { "Pasta", "Steak" });
        Osoba osoba2 = new Batler("John", "Doe", DateTime.Now, 5);
        agencija.DodajOsoblje(osoba1);
        agencija.DodajOsoblje(osoba2);

        string result = CaptureConsoleOutput(() => agencija.IspisiOsoblje());

        var lines = result.Split('\n').Select(line => line.Trim()).ToList();

        Assert.That(lines.IndexOf("Kuhar"), Is.LessThan(lines.IndexOf("Batler")));
    }


    private static string CaptureConsoleOutput(Action action)
    {
        using System.IO.StringWriter sw = new();
        Console.SetOut(sw);
        action.Invoke();
        return sw.ToString().Trim();
    }
}
