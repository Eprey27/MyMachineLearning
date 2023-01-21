namespace MyMachineLearning;

internal class Program
{
    static void Main(string[] args)
    {
        // Crear una nueva instancia de la clase RedNeuronal
        int numEntradas = 2;
        int numNeuronasOcultas = 3;
        int numNeuronasSalida = 1;
        Func<double, double> funcionActivacion = Sigmoid;
        var red = new RedNeuronal(numEntradas, numNeuronasOcultas, numNeuronasSalida, funcionActivacion);

        // Entrenar la red con un conjunto de datos de entrenamiento
        double[][] entradas = new double[4][] { new double[] { 0, 0 }, new double[] { 0, 1 }, new double[] { 1, 0 }, new double[] { 1, 1 } };
        double[][] salidasEsperadas = new double[4][] { new double[] { 0 }, new double[] { 1 }, new double[] { 1 }, new double[] { 0 } };
        int numEpocas = 10000;
        double tasaAprendizaje = 0.1;
        red.Entrenar(entradas, salidasEsperadas, numEpocas, tasaAprendizaje);

        // Propagar una entrada a través de la red y mostrar la salida
        double[] entrada = new double[] { 1, 1 };
        double[] salida = red.PropagarEntrada(entrada);
        Console.WriteLine("Salida para la entrada [1, 1]: " + salida[0]);

        // espera del usuario para finalizar
        Console.ReadKey();
    }

    private static double Sigmoid(double x)
    {
        return 1 / (1 + Math.Exp(-x));
    }
}


/// <summary>
/// En este ejemplo, la clase Neurona tiene tres atributos: un arreglo de pesos, un bias y una función de activación. El constructor de la clase inicializa los pesos y el bias con valores aleatorios y asigna la función de activación dada. También tiene un método llamado CalcularSalida que toma un arreglo de entradas y devuelve la salida de la neurona. 
/// En este ejemplo se utiliza una función de activación genérica, pero es importante mencionar que existen diferentes funciones de activación y cada una tiene sus propias características y se utilizan en diferentes contextos.
/// </summary>
public class Neurona
{
    // Atributos de la neurona
    public double[] pesos;
    private double bias;
    private Func<double, double> funcionActivacion;

    // Constructor
    public Neurona(int numEntradas, Func<double, double> funcionActivacion)
    {
        // Inicializar pesos y bias con valores aleatorios
        var random = new Random();
        pesos = new double[numEntradas];
        for (int i = 0; i < numEntradas; i++)
        {
            pesos[i] = random.NextDouble();
        }
        bias = random.NextDouble();

        // Asignar la función de activación
        this.funcionActivacion = funcionActivacion;
    }

    // Método para calcular la salida de la neurona dada una entrada
    public double CalcularSalida(double[] entrada)
    {
        // Validar que la cantidad de entradas sea correcta
        if (entrada.Length != pesos.Length)
        {
            throw new ArgumentException("La cantidad de entradas debe ser igual a la cantidad de pesos");
        }

        // Calcular la suma ponderada de las entradas
        double suma = 0;
        for (int i = 0; i < entrada.Length; i++)
        {
            suma += entrada[i] * pesos[i];
        }

        // Agregar el bias y aplicar la función de activación
        double salida = funcionActivacion(suma + bias);

        return salida;
    }

    /*
    En este ejemplo, se agregó un método llamado ActualizarPesos que toma como argumentos el arreglo de entradas, el error y la tasa de aprendizaje. El método utiliza el algoritmo de retropropagación para calcular el cambio de peso para cada entrada y el bias, y luego actualiza los pesos y el bias.
    Es importante mencionar que la tasa de aprendizaje es un hiper-parámetro que controla la velocidad de aprendizaje de la red neuronal, una tasa de aprendizaje muy grande puede hacer que la red no converja, mientras que una tasa de aprendizaje muy pequeña puede hacer que la red tardé mucho en converger. Es recomendable experimentar con diferentes valores para encontrar el mejor hiper-parámetro para cada problema en particular.
     */

    // Método para actualizar los pesos de la neurona
    public void ActualizarPesos(double[] entrada, double error, double tasaAprendizaje)
    {
        // Calcular el cambio de peso para cada entrada
        for (int i = 0; i < entrada.Length; i++)
        {
            double deltaPeso = tasaAprendizaje * error * entrada[i];
            pesos[i] += deltaPeso;
        }

        // Actualizar el bias
        bias += tasaAprendizaje * error;
    }

}

/// <summary>
/// En este ejemplo, se creó una clase RedNeuronal que contiene dos arreglos de neuronas, uno para la capa oculta y otro para la capa de salida. El constructor de la clase inicializa estos arreglos con las neuronas necesarias, utilizando el número de entradas, el número de neuronas en cada capa y la función
/// </summary>
public class RedNeuronal
{
    private Neurona[] capaOculta;
    private Neurona[] capaSalida;

    // Constructor
    public RedNeuronal(int numEntradas, int numNeuronasOcultas, int numNeuronasSalida, Func<double, double> funcionActivacion)
    {
        // Inicializar capa oculta
        capaOculta = new Neurona[numNeuronasOcultas];
        for (int i = 0; i < numNeuronasOcultas; i++)
        {
            capaOculta[i] = new Neurona(numEntradas, funcionActivacion);
        }

        // Inicializar capa de salida
        capaSalida = new Neurona[numNeuronasSalida];
        for (int i = 0; i < numNeuronasSalida; i++)
        {
            capaSalida[i] = new Neurona(numNeuronasOcultas, funcionActivacion);
        }
    }

    // Método para propagar la entrada a través de la red
    public double[] PropagarEntrada(double[] entrada)
    {
        // Propagar la entrada a través de la capa oculta
        double[] salidaOculta = new double[capaOculta.Length];
        for (int i = 0; i < capaOculta.Length; i++)
        {
            salidaOculta[i] = capaOculta[i].CalcularSalida(entrada);
        }

        // Propagar la salida de la capa oculta a través de la capa de salida
        double[] salida = new double[capaSalida.Length];
        for (int i = 0; i < capaSalida.Length; i++)
        {
            salida[i] = capaSalida[i].CalcularSalida(salidaOculta);
        }

        return salida;
    }

    // Método para entrenar la red con un conjunto de datos de entrenamiento
    public void Entrenar(double[][] entradas, double[][] salidasEsperadas, int numEpocas, double tasaAprendizaje)
    {
        for (int epoca = 0; epoca < numEpocas; epoca++)
        {
            for (int i = 0; i < entradas.Length; i++)
            {
                // Propagar la entrada a través de la red
                double[] salidaOculta = new double[capaOculta.Length];
                for (int j = 0; j < capaOculta.Length; j++)
                {
                    salidaOculta[j] = capaOculta[j].CalcularSalida(entradas[i]);
                }
                double[] salida = PropagarEntrada(entradas[i]);

                // Calcular el error de la salida
                double[] error = new double[salida.Length];
                for (int j = 0; j < salida.Length; j++)
                {
                    error[j] = salidasEsperadas[i][j] - salida[j];
                }

                // Retropropagar el error a través de la capa de salida
                for (int j = 0; j < capaSalida.Length; j++)
                {
                    capaSalida[j].ActualizarPesos(salidaOculta, error[j], tasaAprendizaje);
                }

                // Calcular el error de la capa oculta
                double[] errorOculto = new double[salidaOculta.Length];
                for (int j = 0; j < salidaOculta.Length; j++)
                {
                    for (int k = 0; k < error.Length; k++)
                    {
                        errorOculto[j] += error[k] * capaSalida[k].pesos[j];
                    }
                }

                // Retropropagar el error a través de la capa oculta
                for (int j = 0; j < capaOculta.Length; j++)
                {
                    capaOculta[j].ActualizarPesos(entradas[i], errorOculto[j], tasaAprendizaje);
                }
            }
        }
    }
}