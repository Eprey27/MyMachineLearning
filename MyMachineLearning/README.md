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

<svg aria-labelledby="chart-title-mermaid-1674349635709 chart-desc-mermaid-1674349635709" role="img" viewBox="0.000030517578125 0 1221.3907470703125 207.33331298828125" style="max-width: 1221.39px; background-color: rgb(255, 255, 255);" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns="http://www.w3.org/2000/svg" width="100%" id="mermaid-1674349635709"><title id="chart-title-mermaid-1674349635709"/><desc id="chart-desc-mermaid-1674349635709"/><style>#mermaid-1674349635709 {font-family:"trebuchet ms",verdana,arial,sans-serif;font-size:16px;fill:#333;}#mermaid-1674349635709 .error-icon{fill:#552222;}#mermaid-1674349635709 .error-text{fill:#552222;stroke:#552222;}#mermaid-1674349635709 .edge-thickness-normal{stroke-width:2px;}#mermaid-1674349635709 .edge-thickness-thick{stroke-width:3.5px;}#mermaid-1674349635709 .edge-pattern-solid{stroke-dasharray:0;}#mermaid-1674349635709 .edge-pattern-dashed{stroke-dasharray:3;}#mermaid-1674349635709 .edge-pattern-dotted{stroke-dasharray:2;}#mermaid-1674349635709 .marker{fill:#333333;stroke:#333333;}#mermaid-1674349635709 .marker.cross{stroke:#333333;}#mermaid-1674349635709 svg{font-family:"trebuchet ms",verdana,arial,sans-serif;font-size:16px;}#mermaid-1674349635709 g.classGroup text{fill:#9370DB;fill:#131300;stroke:none;font-family:"trebuchet ms",verdana,arial,sans-serif;font-size:10px;}#mermaid-1674349635709 g.classGroup text .title{font-weight:bolder;}#mermaid-1674349635709 .nodeLabel,#mermaid-1674349635709 .edgeLabel{color:#131300;}#mermaid-1674349635709 .edgeLabel .label rect{fill:#ECECFF;}#mermaid-1674349635709 .label text{fill:#131300;}#mermaid-1674349635709 .edgeLabel .label span{background:#ECECFF;}#mermaid-1674349635709 .classTitle{font-weight:bolder;}#mermaid-1674349635709 .node rect,#mermaid-1674349635709 .node circle,#mermaid-1674349635709 .node ellipse,#mermaid-1674349635709 .node polygon,#mermaid-1674349635709 .node path{fill:#ECECFF;stroke:#9370DB;stroke-width:1px;}#mermaid-1674349635709 .divider{stroke:#9370DB;stroke:1;}#mermaid-1674349635709 g.clickable{cursor:pointer;}#mermaid-1674349635709 g.classGroup rect{fill:#ECECFF;stroke:#9370DB;}#mermaid-1674349635709 g.classGroup line{stroke:#9370DB;stroke-width:1;}#mermaid-1674349635709 .classLabel .box{stroke:none;stroke-width:0;fill:#ECECFF;opacity:0.5;}#mermaid-1674349635709 .classLabel .label{fill:#9370DB;font-size:10px;}#mermaid-1674349635709 .relation{stroke:#333333;stroke-width:1;fill:none;}#mermaid-1674349635709 .dashed-line{stroke-dasharray:3;}#mermaid-1674349635709 #compositionStart,#mermaid-1674349635709 .composition{fill:#333333!important;stroke:#333333!important;stroke-width:1;}#mermaid-1674349635709 #compositionEnd,#mermaid-1674349635709 .composition{fill:#333333!important;stroke:#333333!important;stroke-width:1;}#mermaid-1674349635709 #dependencyStart,#mermaid-1674349635709 .dependency{fill:#333333!important;stroke:#333333!important;stroke-width:1;}#mermaid-1674349635709 #dependencyStart,#mermaid-1674349635709 .dependency{fill:#333333!important;stroke:#333333!important;stroke-width:1;}#mermaid-1674349635709 #extensionStart,#mermaid-1674349635709 .extension{fill:#333333!important;stroke:#333333!important;stroke-width:1;}#mermaid-1674349635709 #extensionEnd,#mermaid-1674349635709 .extension{fill:#333333!important;stroke:#333333!important;stroke-width:1;}#mermaid-1674349635709 #aggregationStart,#mermaid-1674349635709 .aggregation{fill:#ECECFF!important;stroke:#333333!important;stroke-width:1;}#mermaid-1674349635709 #aggregationEnd,#mermaid-1674349635709 .aggregation{fill:#ECECFF!important;stroke:#333333!important;stroke-width:1;}#mermaid-1674349635709 #lollipopStart,#mermaid-1674349635709 .lollipop{fill:#ECECFF!important;stroke:#333333!important;stroke-width:1;}#mermaid-1674349635709 #lollipopEnd,#mermaid-1674349635709 .lollipop{fill:#ECECFF!important;stroke:#333333!important;stroke-width:1;}#mermaid-1674349635709 .edgeTerminals{font-size:11px;}#mermaid-1674349635709 :root{--mermaid-font-family:"trebuchet ms",verdana,arial,sans-serif;}</style><g><defs><marker orient="auto" markerHeight="240" markerWidth="190" refY="7" refX="0" class="marker aggregation classDiagram" id="classDiagram-aggregationStart"><path d="M 18,7 L9,13 L1,7 L9,1 Z"/></marker></defs><defs><marker orient="auto" markerHeight="28" markerWidth="20" refY="7" refX="19" class="marker aggregation classDiagram" id="classDiagram-aggregationEnd"><path d="M 18,7 L9,13 L1,7 L9,1 Z"/></marker></defs><defs><marker orient="auto" markerHeight="240" markerWidth="190" refY="7" refX="0" class="marker extension classDiagram" id="classDiagram-extensionStart"><path d="M 1,7 L18,13 V 1 Z"/></marker></defs><defs><marker orient="auto" markerHeight="28" markerWidth="20" refY="7" refX="19" class="marker extension classDiagram" id="classDiagram-extensionEnd"><path d="M 1,1 V 13 L18,7 Z"/></marker></defs><defs><marker orient="auto" markerHeight="240" markerWidth="190" refY="7" refX="0" class="marker composition classDiagram" id="classDiagram-compositionStart"><path d="M 18,7 L9,13 L1,7 L9,1 Z"/></marker></defs><defs><marker orient="auto" markerHeight="28" markerWidth="20" refY="7" refX="19" class="marker composition classDiagram" id="classDiagram-compositionEnd"><path d="M 18,7 L9,13 L1,7 L9,1 Z"/></marker></defs><defs><marker orient="auto" markerHeight="240" markerWidth="190" refY="7" refX="0" class="marker dependency classDiagram" id="classDiagram-dependencyStart"><path d="M 5,7 L9,13 L1,7 L9,1 Z"/></marker></defs><defs><marker orient="auto" markerHeight="28" markerWidth="20" refY="7" refX="19" class="marker dependency classDiagram" id="classDiagram-dependencyEnd"><path d="M 18,7 L9,13 L14,7 L9,1 Z"/></marker></defs><defs><marker orient="auto" markerHeight="240" markerWidth="190" refY="7" refX="0" class="marker lollipop classDiagram" id="classDiagram-lollipopStart"><circle r="6" cy="7" cx="6" fill="white" stroke="black"/></marker></defs><g class="root"><g class="clusters"/><g class="edgePaths"/><g class="edgeLabels"/><g class="nodes"><g transform="translate(416.6133117675781, 103.66665649414062)" id="classid-RedNeuronal-10" class="node default"><rect height="191.33332443237305" width="817.2266235351562" y="-95.66666221618652" x="-408.6133117675781" class="outer title-state"/><line y2="-65.33333015441895" y1="-65.33333015441895" x2="408.6133117675781" x1="-408.6133117675781" class="divider"/><line y2="39.99999809265137" y1="39.99999809265137" x2="408.6133117675781" x1="-408.6133117675781" class="divider"/><g class="label"><foreignObject height="0" width="0"><div style="display: inline-block; white-space: nowrap;" xmlns="http://www.w3.org/1999/xhtml"><span class="nodeLabel"></span></div></foreignObject><foreignObject transform="translate( -48.0859375, -88.16666221618652)" height="18.333332061767578" width="96.171875" class="classTitle"><div style="display: inline-block; white-space: nowrap;" xmlns="http://www.w3.org/1999/xhtml"><span class="nodeLabel">RedNeuronal</span></div></foreignObject><foreignObject transform="translate( -401.1133117675781, -53.833330154418945)" height="18.333332061767578" width="98.46353912353516"><div style="display: inline-block; white-space: nowrap;" xmlns="http://www.w3.org/1999/xhtml"><span class="nodeLabel">-entradas: int</span></div></foreignObject><foreignObject transform="translate( -401.1133117675781, -31.499998092651367)" height="18.333332061767578" width="154.2578125"><div style="display: inline-block; white-space: nowrap;" xmlns="http://www.w3.org/1999/xhtml"><span class="nodeLabel">-neuronasOcultas: int</span></div></foreignObject><foreignObject transform="translate( -401.1133117675781, -9.166666030883789)" height="18.333332061767578" width="143.5677032470703"><div style="display: inline-block; white-space: nowrap;" xmlns="http://www.w3.org/1999/xhtml"><span class="nodeLabel">-neuronasSalida: int</span></div></foreignObject><foreignObject transform="translate( -401.1133117675781, 13.166666030883789)" height="18.333332061767578" width="177.91665649414062"><div style="display: inline-block; white-space: nowrap;" xmlns="http://www.w3.org/1999/xhtml"><span class="nodeLabel">-funcionActivacion: Func</span></div></foreignObject><foreignObject transform="translate( -401.1133117675781, 47.49999809265137)" height="18.333332061767578" width="802.2266235351562"><div style="display: inline-block; white-space: nowrap;" xmlns="http://www.w3.org/1999/xhtml"><span class="nodeLabel">+Entrenar(entradas: double[][], salidasEsperadas: double[][], numEpocas: int, tasaAprendizaje: double) : : void</span></div></foreignObject><foreignObject transform="translate( -401.1133117675781, 69.83333015441895)" height="18.333332061767578" width="352.0703430175781"><div style="display: inline-block; white-space: nowrap;" xmlns="http://www.w3.org/1999/xhtml"><span class="nodeLabel">+PropagarEntrada(entrada: double[]) : : double[]</span></div></foreignObject></g></g><g transform="translate(1044.3086700439453, 103.66665649414062)" id="classid-Neurona-11" class="node default"><rect height="146.6666603088379" width="338.1640930175781" y="-73.33333015441895" x="-169.08204650878906" class="outer title-state"/><line y2="-42.99999809265137" y1="-42.99999809265137" x2="169.08204650878906" x1="-169.08204650878906" class="divider"/><line y2="39.99999809265137" y1="39.99999809265137" x2="169.08204650878906" x1="-169.08204650878906" class="divider"/><g class="label"><foreignObject height="0" width="0"><div style="display: inline-block; white-space: nowrap;" xmlns="http://www.w3.org/1999/xhtml"><span class="nodeLabel"></span></div></foreignObject><foreignObject transform="translate( -31.595050811767578, -65.83333015441895)" height="18.333332061767578" width="63.190101623535156" class="classTitle"><div style="display: inline-block; white-space: nowrap;" xmlns="http://www.w3.org/1999/xhtml"><span class="nodeLabel">Neurona</span></div></foreignObject><foreignObject transform="translate( -161.58204650878906, -31.499998092651367)" height="18.333332061767578" width="116.10676574707031"><div style="display: inline-block; white-space: nowrap;" xmlns="http://www.w3.org/1999/xhtml"><span class="nodeLabel">-pesos: double[]</span></div></foreignObject><foreignObject transform="translate( -161.58204650878906, -9.166666030883789)" height="18.333332061767578" width="93.54166412353516"><div style="display: inline-block; white-space: nowrap;" xmlns="http://www.w3.org/1999/xhtml"><span class="nodeLabel">-bias: double</span></div></foreignObject><foreignObject transform="translate( -161.58204650878906, 13.166666030883789)" height="18.333332061767578" width="177.91665649414062"><div style="display: inline-block; white-space: nowrap;" xmlns="http://www.w3.org/1999/xhtml"><span class="nodeLabel">-funcionActivacion: Func</span></div></foreignObject><foreignObject transform="translate( -161.58204650878906, 47.49999809265137)" height="18.333332061767578" width="323.1640930175781"><div style="display: inline-block; white-space: nowrap;" xmlns="http://www.w3.org/1999/xhtml"><span class="nodeLabel">+CalcularSalida(entrada: double[]) : : double</span></div></foreignObject></g></g></g></g></g></svg>

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