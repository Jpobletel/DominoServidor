namespace DominoServidor;

public class Ficha
{
    private int _valor1;
    private int _valor2;
    public int valor1
    {
        get { return _valor1; }
    }
    public int valor2
    {
        get { return _valor2; }
    }

    public Ficha(int valor1, int valor2)
    {
        _valor1 = valor1;
        _valor2 = valor2;
    }

    public void Rotar()
    {
        (_valor1, _valor2) = (_valor2, _valor1);
    }

    public int ObtenerSumaValores() => _valor1 + _valor2;
    
    
    public bool EsChancho6() => _valor1 == 6 && _valor2 == 6;
    

    public override string ToString() => "[" + _valor1 + "," + _valor2 + "]";
    
}