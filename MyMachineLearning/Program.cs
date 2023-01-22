using System;
using System.Linq;
using Newtonsoft.Json;

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
            int numEpocas = 1000000;
            double tasaAprendizaje = 0.001;

            // Timer que recoja cuanto tarda en entrenar la red
            var watch = System.Diagnostics.Stopwatch.StartNew();            
            red.Entrenar(entradas, salidasEsperadas, numEpocas, tasaAprendizaje);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Tiempo de entrenamiento: " + elapsedMs + " ms");            

            // Objeto con los valores de todos los pesos de la red
            var pesos = red.ObtenerPesos();

            // Propagar una entrada a través de la red y mostrar la salida
            bool entrada1 = false;
            bool entrada2 = true;

            int entrada1Int = (entrada1 == true) ? 1 : 0;
            int entrada2Int = (entrada2 == true) ? 1 : 0;

            double[] entrada = new double[] { entrada1Int, entrada2Int };
            double[] salida = red.PropagarEntrada(entrada);
            Console.WriteLine($"Salida para la entrada [{entrada1Int}, {entrada2Int}]: " + salida[0]);

            bool resultado = (salida[0] > 0.5) ? true : false;
            Console.WriteLine("Resultado: " + resultado);        

            // Serializar el objeto pesos a una variable de tipo string
            string pesosSerializados = Newtonsoft.Json.JsonConvert.SerializeObject(pesos);

            // escribir el string en la consola
            Console.WriteLine(pesosSerializados);


            // espera del usuario para finalizar
            // Console.ReadKey();
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