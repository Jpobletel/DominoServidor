namespace DominoServidor;

public class Jugada
{
    private Ficha _fichaAJugar;
    private Posicion _posicionAJugar;

    public Ficha FichaAJugar
    {
        get { return _fichaAJugar; }
    }

    public Posicion PosicionAJugar
    {
        get { return _posicionAJugar; }
    }

    public Jugada(Ficha fichaAJugar, Posicion posicionAJugar)
    {
        _fichaAJugar = fichaAJugar;
        _posicionAJugar = posicionAJugar;
    }

    public override string ToString() => _fichaAJugar + " (" + _posicionAJugar + ")";
    
}