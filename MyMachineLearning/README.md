# Red neuronal feedforward en C#

Este código proporciona un ejemplo básico de cómo crear una red neuronal feedforward en C#. La red neuronal está compuesta por dos capas: una capa oculta y una capa de salida. Cada capa está compuesta por un conjunto de neuronas, cada una con sus propios pesos y bias. La red neuronal es entrenada mediante el algoritmo de retropropagación del error y se utiliza una función de activación sigmoid.

## Uso
Para usar este código, debes seguir estos pasos:

1. Crear una nueva instancia de la clase `RedNeuronal` especificando el número de entradas, el número de neuronas en la capa oculta y en la capa de salida, y la función de activación a utilizar.
2. Entrenar la red neuronal con un conjunto de datos de entrenamiento específico utilizando el método `Entrenar` de la clase `RedNeuronal`.
3. Utilizar el método `PropagarEntrada` para propagar una entrada a través de la red y obtener la salida.

## Clases
- `RedNeuronal`: esta clase representa la red neuronal en sí. Contiene los métodos para entrenar y propagar entradas a través de la red.
- `Neurona`: esta clase representa una neurona individual en la red. Contiene los pesos y bias de la neurona y un método para calcular la salida de la neurona dada una entrada.

# Datos de Entrada y Salida

los datos de entrada y salida en una red neuronal son los conjuntos de datos que se utilizan para entrenar y evaluar la red.

## Datos de Entrada

Los datos de entrada son los valores que se utilizan para "alimentar" la red neuronal. Estos valores se propagan a través de las diferentes capas de la red y son utilizados por las neuronas para calcular sus salidas. Los datos de entrada pueden ser cualquier tipo de valores numéricos o vectores que sean relevantes para el problema en cuestión. Por ejemplo, si se está trabajando con imágenes, los datos de entrada podrían ser los valores de los pixels de cada imagen.

## Datos de Salida

Los datos de salida son los valores que se esperan obtener de la red neuronal. Estos valores se utilizan para comparar con las salidas reales de la red y evaluar su desempeño. Al igual que los datos de entrada, los datos de salida pueden ser cualquier tipo de valores numéricos o vectores relevantes para el problema. Por ejemplo, si se está trabajando con imágenes, los datos de salida podrían ser las etiquetas de clase asociadas a cada imagen.

### Resumen

```En resumen, los datos de entrada son los valores que "alimentan" la red neuronal y los datos de salida son los valores esperados como resultado. Los datos de entrada y salida son utilizados para entrenar y evaluar la red neuronal, permitiendo a la red aprender a realizar una tarea específica a partir de los datos de entrada y a producir un resultado esperado en los datos de salida.```

# DIAGRAMAS

## Clases

classDiagram
  class Neurona{
    +double[] pesos
    +double bias
    +Func<double, double> funcionActivacion
    +CalcularSalida(double[] entrada)
  }
  class RedNeuronal{
    +Neurona[] capaOculta
    +Neurona[] capaSalida
    +Func<double, double> funcionActivacion
    +RedNeuronal(int numEntradas, int numNeuronasOcultas, int numNeuronasSalida, Func<double, double> funcionActivacion)
    +Entrenar(double[][] entradas, double[][] salidasEsperadas, int numEpocas, double tasaAprendizaje)
    +PropagarEntrada(double[] entrada)
  }
  Neurona --> "*" Func<double, double>
  RedNeuronal --> "*" Neurona
  RedNeuronal --> Func<double, double>

## Secuencia 

sequenceDiagram
  participant Neurona as Neurona
  participant RedNeuronal as RedNeuronal
  RedNeuronal->>Neurona: CalcularSalida(entrada)
  Neurona-->>RedNeuronal: salida

## Estructura

![class diagram RedNeuronal](https://user-images.githubusercontent.com/1908966/213896215-f0a02e91-79ee-495b-a8e3-d3cb535adac5.png)

```
class RedNeuronal {
  -entradas: int
  -neuronasOcultas: int
  -neuronasSalida: int
  -funcionActivacion: Func<double, double>
  +Entrenar(entradas: double[][], salidasEsperadas: double[][], numEpocas: int, tasaAprendizaje: double) : void
  +PropagarEntrada(entrada: double[]) : double[]
}
```
![class diagram Neuronal](https://user-images.githubusercontent.com/1908966/213896223-dc904a91-9b21-487a-8d23-f2db1640a7af.png)

```
class Neurona {
  -pesos: double[]
  -bias: double
  -funcionActivacion: Func<double, double>
  +CalcularSalida(entrada: double[]) : double
}
```

## Comportamiento

sequenceDiagram
participant RedNeuronal
participant Neurona
RedNeuronal->>Neurona: CalcularSalida(entrada)
Neurona-->>RedNeuronal: salida


## Paquetes

packagDiagram
package "Red Neuronal" {
  class RedNeuronal
  class Neurona
}

## Estado

stateDiagram
[*] --> Entrenando
Entrenando --> [*]: Termina el entrenamiento
