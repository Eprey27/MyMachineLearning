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

