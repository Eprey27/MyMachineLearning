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
        /// Punto de entrada de la aplicación. Esta es una función Main que crea una nueva instancia
        /// de la clase RedNeuronal con parámetros específicos. Luego, entrena la red con un conjunto
        /// de datos de entrenamiento, propagando una entrada a través de la red y mostrando la salida.
        /// Finalmente, se espera que el usuario presione una tecla para finalizar.
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
}