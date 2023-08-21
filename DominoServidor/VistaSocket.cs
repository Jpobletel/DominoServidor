using System.Net;
using System.Net.Sockets;

namespace DominoServidor;

public class VistaSocket : Vista
{
    private TcpListener _listener;
    private TcpClient _client;
    private StreamReader _reader;
    private StreamWriter _writer;
    
    public VistaSocket()
    {
        _listener = new TcpListener(IPAddress.Loopback, 8001);
        _listener.Start();
        _client = _listener.AcceptTcpClient();
        _reader = new StreamReader(_client.GetStream());
        _writer = new StreamWriter(_client.GetStream());
    }


    protected override void Escribir(string mensaje)
    {
        _writer.Write(mensaje);
        _writer.Flush();
    }

    protected override string LeerLinea()
    {
        EscribirLinea("[INGRESE INPUT]");
        return _reader.ReadLine();
    }

    public override void Cerrar()
    {
        EscribirLinea("[FIN JUEGO]");
        _client.Close();
        _listener.Stop();
    }
}