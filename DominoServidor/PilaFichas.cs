namespace DominoServidor;

public class PilaFichas
{
    private List<Ficha> _fichas;
    private Random _rnd = new Random();

    public PilaFichas()
    {
        GenerarFichas();
    }

    private void GenerarFichas()
    {
        _fichas = new List<Ficha>();
        for(int i = 0; i < 7; i++)
            for(int j = i; j < 7; j++)
                _fichas.Add(new Ficha(i, j));
    }

    public void DarFichas(Jugador jugador, int cantidadFichas)
    {
        for (int i = 0; i < cantidadFichas; i++)
            jugador.AgregarFichaAMano(SacarFichaAlAzar());
    }

    private Ficha SacarFichaAlAzar()
    {
        int idFicha = _rnd.Next(_fichas.Count);
        Ficha fichaSacada = _fichas[idFicha];
        _fichas.Remove(fichaSacada);
        return fichaSacada;
    }

    public override string ToString()
    {
        string s = "";
        foreach (var ficha in _fichas)
            s += ficha;
        return s;
    }
}