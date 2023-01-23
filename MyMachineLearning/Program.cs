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
            int numEntradas = 4;
            int numNeuronasOcultas = 6;
            int numNeuronasSalida = 1;

            // declarar variables del archivo json de pesos que se usa más abajo
            string json;

            // ruta del archivo json de pesos con el mismo nombre que usaremos más abajo
            string path = @"C:\Users\Tutto\source\repos\MyMachineLearning\xorpesos.json";

            Func<double, double> funcionActivacion = Sigmoid;
            RedNeuronal red = new RedNeuronal(numEntradas, numNeuronasOcultas, numNeuronasSalida, funcionActivacion);

            // Entrenar la red XOR un conjunto de datos de entrenamiento    
            /*
                La operación lógica XOR (Exclusive OR) compara dos bits y devuelve 1 si solo uno de los bits es 1, de lo contrario devuelve 0. Entonces, para resolver la incógnita para cada entrada en la matriz que proporcionaste, debemos aplicar la operación XOR para cada par de bits en la entrada y verificar si el resultado es 1 o 0.

                // Matriz de 4 bits XOR
                // Entrada 1: 0, 0, 0, 0 - Salida: 0
                // Entrada 2: 0, 0, 0, 1 - Salida: 1
                // Entrada 3: 0, 0, 1, 0 - Salida: 1
                // Entrada 4: 0, 0, 1, 1 - Salida: 0
                // Entrada 5: 0, 1, 0, 0 - Salida: 1
                // Entrada 6: 0, 1, 0, 1 - Salida: 0
                // Entrada 7: 0, 1, 1, 0 - Salida: 1
                // Entrada 8: 0, 1, 1, 1 - Salida: 0
                // Entrada 9: 1, 0, 0, 0 - Salida: 1
                // Entrada 10: 1, 0, 0, 1 - Salida: 0
                // Entrada 11: 1, 0, 1, 0 - Salida: 1
                // Entrada 12: 1, 0, 1, 1 - Salida: 0
                // Entrada 13: 1, 1, 0, 0 - Salida: 1
                // Entrada 14: 1, 1, 0, 1 - Salida: 0
                // Entrada 15: 1, 1, 1, 0 - Salida: 1
                // Entrada 16: 1, 1, 1, 1 - Salida: 0             
            */

            // El arreglo entradas es una matriz de 16 filas y 4 columnas que contiene los datos de entrada para el 
            // conjunto de datos XOR de 4 entradas. Cada fila representa una entrada diferente, con cada columna 
            // representando uno de los cuatro valores de entrada.
            double[][] entradas = new double[16][]{
                new double[] { 0, 0, 0, 0 },
                new double[] { 0, 0, 0, 1 },
                new double[] { 0, 0, 1, 0 },
                new double[] { 0, 0, 1, 1 },
                new double[] { 0, 1, 0, 0 },
                new double[] { 0, 1, 0, 1 },
                new double[] { 0, 1, 1, 0 },
                new double[] { 0, 1, 1, 1 },
                new double[] { 1, 0, 0, 0 },
                new double[] { 1, 0, 0, 1 },
                new double[] { 1, 0, 1, 0 },
                new double[] { 1, 0, 1, 1 },
                new double[] { 1, 1, 0, 0 },
                new double[] { 1, 1, 0, 1 },
                new double[] { 1, 1, 1, 0 },
                new double[] { 1, 1, 1, 1 }
            };

            // El arreglo salidasEsperadas es una matriz de 16 filas y 1 columna que contiene las salidas esperadas
            // para el conjunto de datos XOR de 4 entradas. Cada fila representa la salida esperada para la entrada
            // correspondiente en el arreglo entradas.
            double[][] salidasEsperadas = new double[16][] {
                new double[] { 0 },
                new double[] { 1 },
                new double[] { 1 },
                new double[] { 0 },
                new double[] { 1 },
                new double[] { 0 },
                new double[] { 1 },
                new double[] { 0 },
                new double[] { 1 },
                new double[] { 0 },
                new double[] { 1 },
                new double[] { 0 },
                new double[] { 1 },
                new double[] { 0 },
                new double[] { 1 },
                new double[] { 0 }
            };

            int numEpocas = 10000000;
            double tasaAprendizaje = 0.0001;

            // Timer que recoja cuanto tarda en entrenar la red
            var watch = System.Diagnostics.Stopwatch.StartNew();

            // no entrenar la red si ya se ha entrenado y se ha guardado el archivo de datos de entrenamiento
            if (!System.IO.File.Exists(path))
            {
                red.Entrenar(entradas, salidasEsperadas, numEpocas, tasaAprendizaje);
            }
            else
            {
                // leer el archivo de datos de entrenamiento
                json = System.IO.File.ReadAllText(path);
                var datosEntrenadosArray = JsonConvert.DeserializeObject<double[]>(json);
                // comprobar si datosEntrenadosArray es null o no tiene datos para evitar errores de ejecución en la red neuronal
                if (datosEntrenadosArray != null && datosEntrenadosArray.Any())
                {
                    red.SetearPesos(datosEntrenadosArray);
                }
            }

            // entrenar la red si no ha sido datosEntrenadosArray es null o no tiene datos
            if (red.Entrenada() == false)
            {
                red.Entrenar(entradas, salidasEsperadas, numEpocas, tasaAprendizaje);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Tiempo de entrenamiento: " + elapsedMs + " ms");

            // crear un conjunto de resultados de datos entrenados para evitat el entrenamiento de la red cada vez que se ejecuta el programa
            // el archivo se encuentra en la carpeta de salida del proyecto
            var datosEntrenados = red.ObtenerPesos();
            json = JsonConvert.SerializeObject(datosEntrenados);
            System.IO.File.WriteAllText(path, json);


            // -------------------------------------------------------------
            // Entrenar la red XOR un conjunto de datos de entrenamiento    
            /*              
                # Matriz XOR de 4 bits
                # Cada fila representa una combinación de bits de entrada, y la salida es el resultado de la operación XOR
                # La operación XOR compara cada bit de entrada y devuelve 1 si solo uno de los bits es 1, o 0 si ambos bits son 0 o 1

                // Matriz de 4 bits XOR
                // Entrada 1: 0, 0, 0, 0 - Salida: 0
                // Entrada 2: 0, 0, 0, 1 - Salida: 1
                // Entrada 3: 0, 0, 1, 0 - Salida: 1
                // Entrada 4: 0, 0, 1, 1 - Salida: 0
                // Entrada 5: 0, 1, 0, 0 - Salida: 1
                // Entrada 6: 0, 1, 0, 1 - Salida: 0
                // Entrada 7: 0, 1, 1, 0 - Salida: 1
                // Entrada 8: 0, 1, 1, 1 - Salida: 0
                // Entrada 9: 1, 0, 0, 0 - Salida: 1
                // Entrada 10: 1, 0, 0, 1 - Salida: 0
                // Entrada 11: 1, 0, 1, 0 - Salida: 1
                // Entrada 12: 1, 0, 1, 1 - Salida: 0
                // Entrada 13: 1, 1, 0, 0 - Salida: 1
                // Entrada 14: 1, 1, 0, 1 - Salida: 0
                // Entrada 15: 1, 1, 1, 0 - Salida: 1
                // Entrada 16: 1, 1, 1, 1 - Salida: 0         
            */
            // -------------------------------------------------------------

            // Propagar una entrada de 4 valores a través de la red y mostrar la salida
            bool entrada1 = true;
            bool entrada2 = true;
            bool entrada3 = true;
            bool entrada4 = true;

            int entrada1Int = (entrada1 == true) ? 1 : 0;
            int entrada2Int = (entrada2 == true) ? 1 : 0;
            int entrada3Int = (entrada3 == true) ? 1 : 0;
            int entrada4Int = (entrada4 == true) ? 1 : 0;

            // double[] itemEntrada = new double[] { 0, 0, 0, 0 }; // Salida: 0
            // double[] itemEntrada = new double[] { 0, 0, 0, 1 }; // Salida: 1
            // double[] itemEntrada = new double[] { 0, 0, 1, 0 }; // Salida: 1
            // double[] itemEntrada = new double[] { 0, 0, 1, 1 }; // Salida: 0
            // double[] itemEntrada = new double[] { 0, 1, 0, 0 }; // Salida: 1
            // double[] itemEntrada = new double[] { 0, 1, 0, 1 }; // Salida: 0
            // double[] itemEntrada = new double[] { 0, 1, 1, 0 }; // Salida: 1
            double[] itemEntrada = new double[] { 0, 1, 1, 1 }; // Salida: 0
            // double[] itemEntrada = new double[] { 1, 0, 0, 0 }; // Salida: 1
            // double[] itemEntrada = new double[] { 1, 0, 0, 1 }; // Salida: 0
            // double[] itemEntrada = new double[] { 1, 0, 1, 0 }; // Salida: 1
            // double[] itemEntrada = new double[] { 1, 0, 1, 1 }; // Salida: 0
            // double[] itemEntrada = new double[] { 1, 1, 0, 0 }; // Salida: 0
            // double[] itemEntrada = new double[] { 1, 1, 0, 1 }; // Salida: 1
            // double[] itemEntrada = new double[] { 1, 1, 1, 0 }; // Salida: 1
            // double[] itemEntrada = new double[] { 1, 1, 1, 1 }; // Salida: 0


            double[] entrada = new double[] { itemEntrada[0], itemEntrada[1], itemEntrada[2], itemEntrada[3] };
            double[] salida = red.PropagarEntrada(entrada);
            bool resultado = (salida[0] > 0.5) ? true : false;
            Console.WriteLine($"Salida para la entrada [{itemEntrada[0]},{itemEntrada[1]},{itemEntrada[2]},{itemEntrada[3]}] es: {salida[0]} RESULTADO: {resultado}");

            // Serializar el objeto pesos a una variable de tipo string
            string pesosSerializados = Newtonsoft.Json.JsonConvert.SerializeObject(datosEntrenados);

            // escribir el string en la consola
            Console.WriteLine(pesosSerializados);


            // espera del usuario para finalizar
            // Console.ReadKey();
        }

        public static double[][] GenerarDatosXOR(int numEntradas)
        {

            // Generar datos de entrenamiento para la red XOR de n entradas y 1 salida
            int numEjemplos = (int)Math.Pow(2, numEntradas);
            double[][] entradas = new double[numEjemplos][];
            double[][] salidasEsperadas = new double[numEjemplos][];

            for (int i = 0; i < numEjemplos; i++)
            {
                // Generar una entrada binaria de n bits
                string binario = Convert.ToString(i, 2).PadLeft(numEntradas, '0');

                // Crear el vector de entrada para la red neuronal con los valores binarios convertidos a valores reales entre 0 y 1
                entradas[i] = new double[numEntradas];

                for (int j = 0; j < numEntradas; j++)
                    entradas[i][j] = (binario[j] == '0') ? 0.0 : 1.0;

                // Calcular la salida esperada para cada ejemplo de entrada como XOR de todas las entradas 
                salidasEsperadas[i] = new double[] { CalcularXOR(entradas[i]) };
            }

            return salidasEsperadas;
        }

        // Función que calcula el XOR de un vector de n bits 
        public static int CalcularXOR(double[] vector)
        {
            int resultado = 0;

            foreach (double bit in vector)                                         // Iterar sobre cada bit del vector y calcular el XOR acumulativo  
                resultado ^= Convert.ToInt32(bit);                                  // Realizar el XOR acumulativo con el bit actual

            return resultado;                                                       // Devolver el resultado final del XOR acumulativo  
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