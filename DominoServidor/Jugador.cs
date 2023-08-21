namespace DominoServidor;

public class Jugador
{
    private List<Ficha> _mano = new List<Ficha>();

    public List<Ficha> Mano
    {
        get { return _mano; }
    }

    public void AgregarFichaAMano(Ficha ficha)
    {
        _mano.Add(ficha);
    }

    public void SacarFichaDeMano(Ficha ficha)
    {
        _mano.Remove(ficha);
    }

    public List<Jugada> ObtenerJugadasPosibles()
    {
        List<Jugada> jugadas = new List<Jugada>();
        foreach (var ficha in _mano)
        {
            jugadas.Add(new Jugada(ficha, Posicion.Inicio));
            jugadas.Add(new Jugada(ficha, Posicion.Fin));
        }

        return jugadas;
    }
    
    public bool TieneFichasEnMano()
    {
        return _mano.Any();
    }

    public bool TieneChancho6()
    {
        foreach (Ficha ficha in _mano)
            if (ficha.EsChancho6())
                return true;
        return false;
    }

    public int ObtenerPuntosEnMano()
    {
        int puntos = 0;
        foreach (Ficha ficha in _mano)
            puntos += ficha.ObtenerSumaValores();
        return puntos;
    }
}