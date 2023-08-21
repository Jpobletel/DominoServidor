namespace DominoServidor;

public abstract class Vista
{
    protected abstract void Escribir(string mensaje);
    protected abstract string LeerLinea();
    public virtual void Cerrar() {}

    protected void EscribirLinea(string mensaje) => Escribir(mensaje + "\n");
    protected void EscribirLinea() => EscribirLinea("");


    public void Pausar() => LeerLinea();

    public void MostrarMano(Jugador jugador)
    {
        Escribir("\nESTA ES TU MANO: ");
        foreach (var ficha in jugador.Mano)
            Escribir(ficha.ToString());
        EscribirLinea();
    }

    public void MostrarJugada(Jugada jugada) => EscribirLinea("Se jugó: " + jugada);

    public void MostrarInfoJugador(int idJugador) => EscribirLinea("Juega jugador " + idJugador + "---------------------------");
    

    public void MostrarMesa(FichasEnMesa fichasEnMesa) => EscribirLinea("MESA: " + fichasEnMesa);

    public int PedirJugada(List<Jugada> jugadas)
    {
        EscribirLinea("¿Cuál de las siguiente jugadas válidas quieres realizar?");
        for (int i = 0; i < jugadas.Count; i++)
            EscribirLinea(i + "- " + jugadas[i]);
        int idJugada = PedirNumeroValido(0, jugadas.Count - 1);

        return idJugada;
    }

    public void MostrarMensajeFelicitandoGanadores(List<int> idGanadores, bool tieneFichasElGanador, int puntos)
    { 
        EscribirLinea("Fin del juego!");
        if (tieneFichasElGanador)
            EscribirLinea($"Ya nadie puede poner más fichas por lo que gana quien tiene menos puntos ({puntos} punto(s) en este caso)");
        if(idGanadores.Count == 1)
            EscribirLinea("Ganó el jugador " + idGanadores[0]);
        else
            EscribirLinea("Hay un empate entre los jugadores: " + ConvertirListaAString(idGanadores));
        LeerLinea();
    }

    private string ConvertirListaAString(List<int> lista)
    {
        string retorno = "[" + lista[0];
        for (int i = 1; i < lista.Count; i++)
            retorno += ", " + lista[i];
        retorno += "]";
        return retorno;
    }

    public void IndicarQueNoHayFichasJugables() => EscribirLinea("No hay fichas que puedas jugar! :(");

    public void IndicarQuePaso() => EscribirLinea("Paso!");

    private int PedirNumeroValido(int minValue, int maxValue)
    {
        int numero;
        bool fuePosibleTransformarElString;
        do
        {
            string? inputUsuario = LeerLinea();
            fuePosibleTransformarElString = int.TryParse(inputUsuario, out numero);
        } while (!fuePosibleTransformarElString || numero < minValue || numero > maxValue);

        return numero;
    }

}