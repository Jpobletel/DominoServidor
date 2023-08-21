namespace DominoServidor;

public class FichasEnMesa
{
    private List<Ficha> _fichasEnMesa = new List<Ficha>();
    
    
    public override string ToString()
    {
        string fichas = "";
        foreach (Ficha ficha in _fichasEnMesa)
            fichas += ficha;
        return fichas;
    }

    public void BajarFicha(Jugada jugada)
    {
        Ficha ficha = jugada.FichaAJugar;
        Posicion posicion = jugada.PosicionAJugar;
        if (posicion == Posicion.Fin)
            AgregarAlFinal(ficha);
        else
            AgregarAlInicio(ficha);
    }
    
    public bool EsJugable(Jugada jugada)
    {
        if (HayFichasEnMesa())
            return EsJugableMesaConFichas(jugada);
        return EsJugableMesaVacia(jugada);
    }

    private bool EsJugableMesaConFichas(Jugada jugada)
    {
        Ficha ficha = jugada.FichaAJugar;
        Posicion posicion = jugada.PosicionAJugar;
        int valorObjetivo;
        valorObjetivo = posicion == Posicion.Fin ? _fichasEnMesa[^1].valor2 : _fichasEnMesa[0].valor1;
        return ficha.valor1 == valorObjetivo || ficha.valor2 == valorObjetivo;
    }
    
    private bool EsJugableMesaVacia(Jugada jugada) => jugada.FichaAJugar.EsChancho6();
    
    
    private void AgregarAlInicio(Ficha ficha)
    {
        if (HayFichasEnMesa() && ficha.valor2 != _fichasEnMesa[0].valor1)
            ficha.Rotar();
        _fichasEnMesa.Insert(0, ficha);
    }
    
    private void AgregarAlFinal(Ficha ficha)
    {
        if (HayFichasEnMesa() && _fichasEnMesa[^1].valor2 != ficha.valor1)
            ficha.Rotar();
        _fichasEnMesa.Add(ficha);
    }

    private bool HayFichasEnMesa() => _fichasEnMesa.Any();
    
}