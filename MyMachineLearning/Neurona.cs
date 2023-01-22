using System;
using System.Linq;

namespace MyMachineLearning
{    
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