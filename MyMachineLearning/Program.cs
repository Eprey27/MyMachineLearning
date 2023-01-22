using System;
using System.Linq;

namespace MyMachineLearning
{
    /// <summary>
    /// The main program class, containing the entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Punto de entrada de la aplicación
        /// </summary>
        /// <param name="args">Argumentos de línea de comando</param>
        static void Main(string[] args)
        {
            // Crear una nueva instancia de la clase RedNeuronal
            int numEntradas = 2;
            int numNeuronasOcultas = 3;
            int numNeuronasSalida = 1;
            Func<double, double> funcionActivacion = Sigmoid;
            RedNeuronal red = new RedNeuronal(numEntradas, numNeuronasOcultas, numNeuronasSalida, funcionActivacion);

            // Entrenar la red con un conjunto de datos de entrenamiento
            double[][] entradas = new double[4][] { new double[] { 0, 0 }, new double[] { 0, 1 }, new double[] { 1, 0 }, new double[] { 1, 1 } };
            double[][] salidasEsperadas = new double[4][] { new double[] { 0 }, new double[] { 1 }, new double[] { 1 }, new double[] { 0 } };
            int numEpocas = 30000;
            double tasaAprendizaje = 0.1;
            red.Entrenar(entradas, salidasEsperadas, numEpocas, tasaAprendizaje);

            // Propagar una entrada a través de la red y mostrar la salida
            double[] entrada = new double[] { 0, 1 };
            double[] salida = red.PropagarEntrada(entrada);
            Console.WriteLine("Salida para la entrada [1, 1]: " + salida[0]);

            // espera del usuario para finalizar
            Console.ReadKey();
        }

        /// <summary>
        /// La función de activación utilizada por la red neuronal
        /// </summary>
        /// <param name="x">Entrada para la función</param>
        /// <returns>La salida de la función</returns>
        private static double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }
    }

    /// <summary>
    /// Clase que representa una red neuronal.
    /// </summary>
    public class RedNeuronal
    {
        /// <summary>
        /// Arreglo de neuronas que conforman la capa oculta de la red neuronal.
        /// </summary>
        public Neurona[] capaOculta { get; set; }
        /// <summary>
        /// Arreglo de neuronas que conforman la capa de salida de la red neuronal.
        /// </summary>
        public Neurona[] capaSalida { get; set; }

        /// <summary>
        /// Constructor de la clase RedNeuronal.
        /// Inicializa las capas oculta y de salida de acuerdo a los parámetros especificados.
        /// </summary>
        /// <param name="numEntradas">Número de entradas de la red neuronal.</param>
        /// <param name="numNeuronasOcultas">Número de neuronas en la capa oculta.</param>
        /// <param name="numNeuronasSalida">Número de neuronas en la capa de salida.</param>
        /// <param name="funcionActivacion">Función de activación utilizada por las neuronas de la red.</param>
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

        /// <summary>
        /// Entrena la red neuronal con un conjunto de datos de entrenamiento.
        /// </summary>
        /// <param name="entradas">Arreglo de entradas para el entrenamiento.</param>
        /// <param name="salidasEsperadas">Arreglo de salidas esperadas correspondientes a las entradas.</param>
        /// <param name="numEpocas">Número de épocas de entrenamiento.</param>
        /// <param name="tasaAprendizaje">Tasa de aprendizaje de la red neuronal.</param>
        public void Entrenar(double[][] entradas, double[][] salidasEsperadas, int numEpocas, double tasaAprendizaje)
        {
            // Iterar por cada época de entrenamiento
            for (int e = 0; e < numEpocas; e++)
            {
                // Iterar por cada conjunto de entrada
                for (int i = 0; i < entradas.Length; i++)
                {
                    // Propagar la entrada a través de la red
                    double[] salidaOculta = PropagarEntrada(entradas[i], capaOculta);
                    double[] salidaRed = PropagarEntrada(salidaOculta, capaSalida);

                    // Calcular el error en la capa de salida
                    double[] erroresSalida = new double[salidaRed.Length];
                    for (int j = 0; j < erroresSalida.Length; j++)
                    {
                        erroresSalida[j] = salidasEsperadas[i][j] - salidaRed[j];
                    }

                    // Retropropagar el error a través de la capa de salida
                    for (int j = 0; j < capaSalida.Length; j++)
                    {
                        capaSalida[j].ActualizarPesos(salidaOculta, erroresSalida[j], tasaAprendizaje);
                    }

                    // Calcular el error en la capa oculta
                    double[] erroresOcultos = new double[salidaOculta.Length];
                    for (int j = 0; j < erroresOcultos.Length; j++)
                    {
                        double error = 0;
                        for (int k = 0; k < erroresSalida.Length; k++)
                        {
                            error += erroresSalida[k] * capaSalida[k].pesos[j];
                        }
                        erroresOcultos[j] = error * DerivadaSigmoid(salidaOculta[j]);
                    }

                    // Retropropagar el error a través de la capa oculta
                    for (int j = 0; j < capaOculta.Length; j++)
                    {
                        capaOculta[j].ActualizarPesos(entradas[i], erroresOcultos[j], tasaAprendizaje);
                    }
                }
            }
        }

        /// <summary>
        /// Propaga una entrada a través de la red neuronal
        /// </summary>
        /// <param name="entrada">La entrada a propagar</param>
        /// <returns>La salida de la capa después de propagar la entrada</returns>
        public double[] PropagarEntrada(double[] entrada)
        {
            double[] salidaOculta = PropagarEntrada(entrada, capaOculta);
            double[] salidaRed = PropagarEntrada(salidaOculta, capaSalida);
            return salidaRed;
        }

        /// <summary>
        /// Propaga una entrada a través de la red neuronal
        /// </summary>
        /// <param name="entrada">La entrada a propagar</param>
        /// <param name="capa">La capa a través de la cual propagar la entrada</param>
        /// <returns>La salida de la capa después de propagar la entrada</returns>
        private double[] PropagarEntrada(double[] entrada, Neurona[] capa)
        {
            double[] salida = new double[capa.Length];
            for (int i = 0; i < capa.Length; i++)
            {
                salida[i] = capa[i].CalcularSalida(entrada);
            }
            return salida;
        }

        /// <summary>
        /// Calcula la derivada de la función sigmoide
        /// </summary>
        /// <param name="x">La entrada para la cual calcular la derivada</param>
        /// <returns>La derivada de la función sigmoide en x</returns>
        private double DerivadaSigmoid(double x)
        {
            return x * (1 - x);
        }
    }

    /// <summary>
    /// Clase que representa una neurona.
    /// </summary>
    public class Neurona
    {
        /// <summary>
        /// Los pesos de la neurona
        /// </summary>
        public double[] pesos;
        /// <summary>
        /// El sesgo de la neurona
        /// </summary>
        private double bias;
        /// <summary>
        /// La función de activación de la neurona
        /// </summary>
        private Func<double, double> funcionActivacion;

        /// <summary>
        /// Constructor de la clase Neurona.
        /// Inicializa los pesos y el bias de acuerdo a los parámetros especificados.
        /// </summary>
        /// <param name="numEntradas">Número de entradas de la neurona</param>
        /// <param name="funcionActivacion">Función de activación de la neurona</param>
        public Neurona(int numEntradas, Func<double, double> funcionActivacion)
        {
            var rand = new Random();
            pesos = Enumerable.Range(0, numEntradas).Select(_ => rand.NextDouble()).ToArray();
            bias = rand.NextDouble();
            this.funcionActivacion = funcionActivacion;
        }

        /// <summary>
        /// Calcula la salida de la neurona para una entrada dada
        /// </summary>
        /// <param name="entrada">La entrada para la neurona</param>
        /// <returns>La salida de la neurona</returns>
        public double CalcularSalida(double[] entrada)
        {
            if (entrada.Length != pesos.Length)
            {
                throw new ArgumentException("La cantidad de entradas debe ser igual a la cantidad de pesos");
            }

            double sumaPonderada = entrada.Zip(pesos, (entrada, peso) => entrada * peso).Sum() + bias;
            return funcionActivacion(sumaPonderada);
        }

        /// <summary>
        /// Actualiza los pesos de la neurona
        /// </summary>
        /// <param name="entrada">Vector de entradas para la neurona</param>
        /// <param name="delta">Delta de cambio en los pesos</param>
        /// <param name="tasaAprendizaje">Tasa de aprendizaje</param>
        public void ActualizarPesos(double[] entrada, double error, double tasaAprendizaje)
        {
            for (int i = 0; i < pesos.Length; i++)
            {
                pesos[i] += tasaAprendizaje * error * entrada[i];
            }
            bias += tasaAprendizaje * error;
        }
    }
}