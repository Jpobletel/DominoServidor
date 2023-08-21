namespace DominoServidor;

public class Jugadores
{
    private List<Jugador> _jugadores;

    public Jugadores(int numJugadores)
    {
        _jugadores = new List<Jugador>();
        for(int i = 0; i < numJugadores; i++)
            _jugadores.Add(new Jugador());
    }

    public Jugador ObtenerJugador(int idJugador) => _jugadores[idJugador];
    
    public void RepartirFichas(int cantidadInicialFichas)
    {
        PilaFichas pilaFichas = new PilaFichas();
        foreach (var jugador in _jugadores)
            pilaFichas.DarFichas(jugador, cantidadInicialFichas);
    }
    
    public int ObtenerIdJugadorConChancho6()
    {
        for(int idJugador = 0; idJugador < _jugadores.Count; idJugador++)
            if (_jugadores[idJugador].TieneChancho6())
                return idJugador;
        throw new InvalidOperationException("Nadie tiene el chancho 6");
    }

    public (List<int>, int) ObtenerIdGanadores()
    {
        int[] puntosJugadores = CalcularPuntosPorJugador();
        List<int> idGanadores = ObtenerIdsConMenorPuntaje(puntosJugadores);
        return (idGanadores, puntosJugadores.Min());
    }

    private List<int> ObtenerIdsConMenorPuntaje(int[] puntos)
    {
        List<int> idsMenorPuntaje = new List<int>();
        int puntajeMinimo = puntos.Min();
        for(int i = 0; i < puntos.Length; i++)
            if (puntos[i] == puntajeMinimo)
                idsMenorPuntaje.Add(i);
        return idsMenorPuntaje;
    }
    
    private int[] CalcularPuntosPorJugador()
    {
        int[] puntos = new int[_jugadores.Count];
        for (int i = 0; i < _jugadores.Count; i++)
            puntos[i] = _jugadores[i].ObtenerPuntosEnMano();
        return puntos;
    }

    
    public bool ExisteJugadorSinFichas()
    {
        foreach (var jugador in _jugadores)
            if (!jugador.TieneFichasEnMano())
                return true;
        return false;
    }
    
    public bool AlguienTieneFichasJugables(FichasEnMesa fichasEnMesa)
    {
        for (int idJugador = 0; idJugador < _jugadores.Count; idJugador++)
        {
            if (ObtenerJugadasValidas(fichasEnMesa, idJugador).Any())
                return true;
        }

        return false;
    }
    
    public List<Jugada> ObtenerJugadasValidas(FichasEnMesa fichasEnMesa, int idJugador)
    {
        List<Jugada> jugadasValidas = new List<Jugada>();
        foreach (Jugada jugada in _jugadores[idJugador].ObtenerJugadasPosibles())
            if(fichasEnMesa.EsJugable(jugada))
                jugadasValidas.Add(jugada);
        return jugadasValidas;
    }


}