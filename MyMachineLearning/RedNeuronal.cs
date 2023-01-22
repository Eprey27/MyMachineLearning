using System;
using System.Linq;

namespace MyMachineLearning
{
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
}