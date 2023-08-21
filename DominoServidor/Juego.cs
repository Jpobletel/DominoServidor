namespace DominoServidor;

public class Juego
{
    private const int IdUsuario = 0;
    private const int NumJugadores = 4;
    private const int CantidadInicialFichas = 7;

    private Jugadores _jugadores;
    private int _idJugadorTurno;
    private FichasEnMesa _fichasEnMesa;
    //private Vista _vista = new VistaConsola();
    private Vista _vista = new VistaSocket();

    public Juego()
    {
        PonerMesaVacia();
        CrearJugadores();
        RepartirFichas();
        DecidirQuienParte();
    }

    private void PonerMesaVacia() => _fichasEnMesa = new FichasEnMesa();
    private void CrearJugadores() => _jugadores = new Jugadores(NumJugadores);
    private void RepartirFichas() => _jugadores.RepartirFichas(CantidadInicialFichas);
    private void DecidirQuienParte() => _idJugadorTurno = _jugadores.ObtenerIdJugadorConChancho6();
    
        
    public void Jugar()
    {
        while (!EsFinJuego())
        {
            JugarTurno();
            _vista.Pausar();
        }
        FelicitarGanadores();
        _vista.Cerrar();
    }

    private bool EsFinJuego()
    {
        bool hayGanador = _jugadores.ExisteJugadorSinFichas();
        bool sePuedenJugarMasFichas = _jugadores.AlguienTieneFichasJugables(_fichasEnMesa);
        return hayGanador || !sePuedenJugarMasFichas;
    }

    private void JugarTurno()
    {
        _vista.MostrarInfoJugador(_idJugadorTurno);
        _vista.MostrarMesa(_fichasEnMesa);
        if (EsTurnoUsuario())
            JugarTurnoUsuario();
        else
            JugarTurnoPC();
        _vista.MostrarMesa(_fichasEnMesa);
        AvanzarTurno();
    }
    private bool EsTurnoUsuario() => _idJugadorTurno == IdUsuario;

    private void JugarTurnoUsuario()
    {
        Jugador usuario = _jugadores.ObtenerJugador(IdUsuario);
        _vista.MostrarMano(usuario);
        List<Jugada> jugadasValidas = _jugadores.ObtenerJugadasValidas(_fichasEnMesa, IdUsuario);
        if (jugadasValidas.Any())
        {
            int idJugada = _vista.PedirJugada(jugadasValidas);
            BajarFicha(jugadasValidas[idJugada]);
        }
        else
            _vista.IndicarQueNoHayFichasJugables();
    }

    private void BajarFicha(Jugada jugada)
    {
        Jugador jugador = _jugadores.ObtenerJugador(_idJugadorTurno);
        jugador.SacarFichaDeMano(jugada.FichaAJugar);
        _fichasEnMesa.BajarFicha(jugada);
        _vista.MostrarJugada(jugada);
    }

    private void JugarTurnoPC()
    {
        List<Jugada> jugadasValidas = _jugadores.ObtenerJugadasValidas(_fichasEnMesa, _idJugadorTurno);
        if (jugadasValidas.Any())
            BajarFicha(jugadasValidas[0]);
        else
            _vista.IndicarQuePaso();
    }

    private void AvanzarTurno() => _idJugadorTurno = (_idJugadorTurno+1) % NumJugadores;
    
    private void FelicitarGanadores()
    {
        (List<int> idGanadores, int puntos) = _jugadores.ObtenerIdGanadores();
        bool tieneFichasElGanador = _jugadores.ObtenerJugador(idGanadores[0]).TieneFichasEnMano();
        _vista.MostrarMensajeFelicitandoGanadores(idGanadores, tieneFichasElGanador, puntos);        
    }
}