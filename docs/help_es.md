# Fractal Tree Fondos de Escritorio - Guía de Usuario

> **Nota**: Este documento ha sido traducido por IA desde la versión original en chino. Si encuentra errores o expresiones poco claras, consulte el archivo original `help.md`.

## Introducción

`Fractal Tree Fondos de Escritorio` puede generar aleatoriamente un árbol hermoso. Gracias a su potente sistema de parámetros, puede simular **casi cualquier tipo** de árbol en el mundo, incluyendo aquellos que **existen en la naturaleza** y aquellos que **solo existen en tu imaginación**.

### ¿Qué es un `Árbol Fractal`?

Un `Árbol Fractal` es una estructura de árbol **generada recursivamente** — cada rama tiene múltiples sub-ramas, cada sub-rama contiene múltiples sub-ramas, y así sucesivamente, resultando en una estructura tipo árbol.  
En `Fractal Tree Fondos de Escritorio`, para reducir la computación de renderizado y mejorar la controlabilidad, solo usamos árboles binarios, lo que significa que cada rama tiene máximo dos sub-ramas.

Para lograr una estructura de árbol lo más realista posible, `Fractal Tree Fondos de Escritorio` proporciona numerosos parámetros para controlar la distancia y el ángulo entre nodos, el color de las líneas, etc.

---

## Introducción a los Parámetros

Si eres nuevo en `Fractal Tree Fondos de Escritorio`, no te intimides por la gran cantidad de parámetros — solo necesitas ajustar unos pocos parámetros clave para crear un árbol único. Intenta modificar el `Rango de Ángulo de Bifurcación` y el `Rango de Decaimiento de Longitud` para ver cómo cambia la forma del árbol; luego ajusta el **color** en `Configuración de Hojas` para agregar color a tu árbol. Una vez familiarizado con las operaciones básicas, puedes explorar más a fondo los **Grupos de Parámetros** y las **Curvas de Distribución** para hacer cada árbol una verdadera obra de arte.

A continuación se presenta un tutorial básico de parámetros ↓

### 1. Rango de Ángulo de Bifurcación

`Rango de Ángulo de Bifurcación` controla el rango del ángulo de desviación (en grados) de las sub-ramas respecto a su rama padre. La curva de distribución controla la probabilidad de los valores de ángulo dentro del rango (por supuesto, las curvas de distribución están fuera del alcance de este tutorial básico, así que mantén la distribución uniforme por defecto).

- Cuando el `Rango de Ángulo de Bifurcación` es **generalmente grande**, la copa del árbol aparece **dispersa y esponjosa**.
- Cuando el `Rango de Ángulo de Bifurcación` es **generalmente pequeño**, la copa del árbol aparece **densa y compacta**.
- Cuando el `Rango de Ángulo de Bifurcación` tiene un **intervalo amplio**, la copa del árbol aparece **caótica y desordenada**.
- Cuando el `Rango de Ángulo de Bifurcación` tiene un **intervalo estrecho**, la copa del árbol aparece **ordenada y uniforme**.

### 2. Rango de Decaimiento de Longitud

`Rango de Decaimiento de Longitud` controla el rango de la proporción de la longitud de la sub-rama respecto a la rama padre. La curva de distribución controla la probabilidad de los valores de proporción dentro del rango (de nuevo, las curvas de distribución están fuera del alcance de este tutorial básico, así que mantén la distribución uniforme por defecto).

Por ejemplo, si una bifurcación obtiene un valor aleatorio de **0.7** del rango preestablecido, la longitud de la sub-rama será el **70%** de la longitud de la rama padre.

- Cuando el `Rango de Decaimiento de Longitud` es **generalmente grande**, la copa del árbol aparece **enorme**.
- Cuando el `Rango de Decaimiento de Longitud` es **generalmente pequeño**, la copa del árbol aparece **pequeña y dispersa**.
- Cuando el `Rango de Decaimiento de Longitud` tiene un **intervalo amplio**, la copa del árbol aparece **variada y en capas**.
- Cuando el `Rango de Decaimiento de Longitud` tiene un **intervalo estrecho**, la copa del árbol aparece **uniforme y densa**.

> Ten en cuenta que a menos que tengas requisitos especiales, el valor máximo del `Rango de Decaimiento de Longitud` no debe exceder **0.95**, de lo contrario la copa del árbol será demasiado densa, o incluso crecerá infinitamente (limitado solo por la `Profundidad Máxima`), lo que no solo afecta el rendimiento de renderizado sino que también hace que la copa del árbol se vea extremadamente antinatural.

### 3. Configuración de Hojas

`Configuración de Hojas` controla la apariencia de las hojas. Al ajustar el color, peso, densidad y ancho de línea de las hojas, puedes lograr diferentes efectos de hojas. (Para el tutorial básico, solo modifica los colores.)

Puedes comenzar agregando varios tonos de verde con alguna variación para ver el efecto.

### 4. Color de Fondo

Como su nombre indica, `Color de Fondo` controla el color de fondo del lienzo.  
Puedes usar un tono claro similar al de las hojas para un aspecto más natural.

Para sugerencias de color más profesionales, puedes consultar [Herramienta de Círculo de Color – Adobe Express](https://color.adobe.com/create/color-wheel).

---

## Parámetros Avanzados

Esta sección proporciona definiciones detalladas y el uso de cada parámetro.

Todos los parámetros en píxeles representan longitudes cuando la altura de la ventana es de 1080 píxeles estándar. El renderizado real escala proporcionalmente según la altura de la ventana.

---

### 1. Configuración Global

La configuración global no está incluida en ningún grupo de parámetros y afecta directamente el resultado.

#### 1.1 Nombre del Árbol

`Nombre del Árbol` se usa para nombrar tu árbol.  
Se usa para mostrar en la lista de la interfaz principal y como prefijo del nombre de archivo por defecto para las imágenes exportadas. Las cadenas vacías se corrigen automáticamente a **"Sin título"**.

#### 1.2 Color de Fondo

`Color de Fondo` controla el color de fondo del lienzo.  
Se usa para rellenar el color de fondo de la ventana y el color de fondo de la imagen de fondo de pantalla.

---

### 2. Parámetros de Forma Básica

Los siguientes parámetros controlan directamente la forma básica y las condiciones de terminación del árbol.

#### 2.1 Longitud del Tronco

`Longitud del Tronco` es la longitud inicial (en píxeles) de la primera rama que emana de la raíz del árbol. La raíz del árbol se posiciona al 10% arriba del fondo del lienzo.  
Se usa para determinar la escala base de todo el árbol, afectando directamente el tamaño final del árbol.

#### 2.2 Longitud Mínima de Rama

`Longitud Mínima de Rama` es una de las condiciones de terminación del árbol. Cuando la longitud de una rama cae por debajo de este umbral, no se generan más sub-ramas.  
Se usa para controlar la densidad de los extremos de la copa del árbol y el tamaño de la copa.

#### 2.3 Profundidad Máxima

`Profundidad Máxima` es una de las condiciones de terminación del árbol. Cuando el nivel de recursión de una rama (la raíz es nivel 0, las sub-ramas de la raíz son nivel 1, y así sucesivamente) alcanza este valor, la bifurcación se detiene.  
Se usa para evitar que la copa del árbol sea demasiado densa o crezca infinitamente, lo que afectaría el rendimiento o incluso congelaría la aplicación.

> Ten en cuenta que en las etapas iniciales de creación, se recomienda establecer la `Profundidad Máxima` en **10 ~ 30**, y ajustar más tarde según sea necesario para evitar congelaciones durante las pruebas debido a parámetros poco razonables.

#### 2.4 Umbral de Hoja

`Umbral de Hoja` determina si la rama actual debe renderizarse como una ramita o como una hoja. Basado en la longitud de la rama para determinar el rol visual. Cuando la longitud de la rama actual está por debajo de este umbral, la rama se renderiza como una hoja; por encima del umbral, se renderiza como un tronco.  
Se usa para controlar el efecto visual de las hojas, afectando la forma de la copa del árbol.

> Generalmente no se recomienda usar esto para ajustar directamente la densidad de las hojas, ya que esto está relacionado con la forma de las hojas. Se deben ajustar otros parámetros en su lugar.

#### 2.5 Inclinación Máxima de Rama

`Inclinación Máxima de Rama` es una de las condiciones de terminación del árbol. Controla el límite superior (en grados) del ángulo absoluto de las sub-ramas (el ángulo relativo a la dirección vertical hacia arriba). Cualquier sub-rama cuyo ángulo exceda este valor no se creará y terminará directamente.  
Se usa para evitar que las ramas caigan y mantener la forma del árbol.

---

### 3. Rango de Ángulo de Bifurcación

`Rango de Ángulo de Bifurcación` es el rango del intervalo del ángulo de desviación muestreado independientemente para cada sub-rama. Dos sub-ramas se desvían a cada lado de la rama padre respectivamente. El rango de muestreo es [Mínimo, Máximo] (grados).  
Se usa para controlar la forma de las ramas, afectando la uniformidad y el patrón de crecimiento de las ramas.

#### 3.1 Curva de Distribución de Probabilidad de Ángulo

Abre el editor de gráficos de barras a través del botón `Curva de Distribución`: haz clic en las barras para ajustar las alturas de las barras, con un rango de altura de `0.02 ~ 1.0`.

**Método de muestreo para cada sub-rama**: Divide el intervalo de ángulo en 8 sub-intervalos iguales, primero selecciona un sub-intervalo usando las alturas de las barras de la curva de distribución como pesos, luego toma un valor aleatorio uniforme dentro de ese sub-intervalo. Si no se especifica una curva de distribución, se toma un valor aleatorio uniforme del intervalo global.

En pocas palabras, cuanto más alta sea la barra, mayor será la probabilidad de que los valores cercanos sean seleccionados como ángulos de ramas.

#### 3.2 Ejemplo de Referencia

El árbol por defecto `Pino` tiene un rango de ángulo de bifurcación de `[0, 90]`, con una curva de distribución de probabilidad de ángulo aproximadamente como sigue:

<!-- BAR_CHART: [1.00, 0.02, 0.02, 0.02, 0.02, 0.02, 0.02, 0.20] | [0°, 12.86°, 25.71°, 38.57°, 51.43°, 64.29°, 77.14°, 90°] -->

Se puede ver que los ángulos de bifurcación del pino tienen más probabilidad de estar alrededor de 0 y 90 grados. Es decir, las ramas del pino tienen más probabilidad de ser casi paralelas o perpendiculares a la rama padre, en lugar de crecer diagonalmente.  
El efecto real es que los pinos mayormente crecen rectos, pero a veces hacen grandes giros, proporcionando una forma similar a la retorcida y vigorosa de los pinos reales.  
Entre ellos, es más probable que sean paralelos a la rama padre para mantener la forma básica de la copa del árbol sin volverse demasiado caótica.

---

### 4. Rango de Decaimiento de Longitud

`Rango de Decaimiento de Longitud` es el rango del intervalo de proporción de decaimiento de longitud muestreado independientemente para cada sub-rama. La longitud real de la sub-rama = longitud de la rama padre × proporción de decaimiento muestreada. El rango de muestreo es [Mínimo, Máximo] (proporción de decaimiento).

#### 4.1 Curva de Distribución de Probabilidad de Decaimiento

Similar a [3.1 Curva de Distribución de Probabilidad de Ángulo](#31-curva-de-distribución-de-probabilidad-de-ángulo).

Abre el editor de gráficos de barras a través del botón `Curva de Distribución`: haz clic en las barras para ajustar las alturas de las barras, con un rango de altura de `0.02 ~ 1.0`.

**Método de muestreo para cada sub-rama**: Divide el intervalo de ángulo en 8 sub-intervalos iguales, primero selecciona un sub-intervalo usando las alturas de las barras de la curva de distribución como pesos, luego toma un valor aleatorio uniforme dentro de ese sub-intervalo. Si no se especifica una curva de distribución, se toma un valor aleatorio uniforme del intervalo global.

En pocas palabras, cuanto más alta sea la barra, mayor será la probabilidad de que los valores cercanos sean seleccionados como proporciones de decaimiento.

#### 4.2 Ejemplo de Referencia

El árbol por defecto `Árbol Verde` tiene un rango de decaimiento de longitud de `[0.6, 0.94]`, con una curva de distribución de probabilidad de decaimiento aproximadamente como sigue:

<!-- BAR_CHART: [0.84, 0.02, 0.02, 0.02, 0.02, 0.02, 0.02, 0.70] | [0.60, 0.65, 0.70, 0.75, 0.79, 0.84, 0.89, 0.94] -->

Se puede ver que las proporciones de decaimiento del árbol verde tienen más probabilidad de estar alrededor de 0.60 y 0.94. Es decir, las ramas del árbol verde tienen casi la misma longitud que la rama padre o notablemente más cortas.  
El efecto real es que el árbol verde a veces tiene ramas extendidas y a veces hojas recogidas, proporcionando una forma en capas y variada.  
Vale la pena señalar que si el árbol verde elige una rama corta demasiado pronto, reducirá directamente la altura de todo el árbol, resultando en una mayor variación en la altura del árbol.

---

### 5. Configuración de Tronco/Hojas

`Configuración de Tronco/Hojas` es una colección de parámetros que determinan el efecto visual de troncos y hojas, sin afectar la estructura del árbol.

Los troncos y las hojas tienen listas de colores independientes. Cada entrada de color contiene tres atributos:

#### 5.1 Color

`Color` determina el color usado para renderizar troncos u hojas. Soporta establecer múltiples colores para troncos/hojas, seleccionados aleatoriamente.

#### 5.2 Peso

`Peso` determina la probabilidad de que este color sea seleccionado al dibujar un nuevo tronco u hoja. Cuanto mayor sea el valor, mayor será la proporción de este color en troncos/hojas.

#### 5.3 Proporción de Ancho de Línea

`Proporción de Ancho de Línea` determina el coeficiente de ancho de troncos u hojas. Cuanto mayor sea el valor, más grueso será el tronco/hoja. El grosor real también depende de la longitud de la rama.

**Comportamiento de Múltiples Grupos de Parámetros**: La configuración de tronco/hojas no participa en el promedio ponderado numérico de múltiples grupos de parámetros. Cuando existen múltiples grupos de parámetros, se usa directamente la lista de colores del grupo con el mayor peso.

---

### 6. Sistema de Hojas Caídas

`Sistema de Hojas Caídas` es una función para agregar hojas caídas en el suelo. Después de que se dibuja una hoja, hay una probabilidad de dibujar una hoja caída adicional con la misma forma en el área del suelo.  
Se usa para mejorar la atmósfera del árbol.

#### 6.1 Densidad de Hojas Caídas

`Densidad de Hojas Caídas` determina la probabilidad de que las hojas se conviertan en hojas caídas en el suelo. El rango de valores es `0.0 ~ 1.0`. Cuanto mayor sea el valor, más hojas caídas habrá.  
El número real de hojas caídas también depende de la densidad de la copa del árbol (el número de hojas en el árbol).

#### 6.2 Dispersión de Hojas Caídas

`Dispersión de Hojas Caídas` determina el rango de distribución de las hojas caídas, específicamente el rango de desplazamiento horizontal (en píxeles) relativo a la hoja original y el rango de desplazamiento vertical (en píxeles) relativo a la posición del suelo. Cuanto mayor sea el valor, mayor será el rango de distribución de las hojas caídas.

El desplazamiento vertical relativo a la posición del suelo usa una distribución triangular (promedio de dos valores aleatorios uniformes).  
El desplazamiento horizontal relativo a la hoja original usa una distribución uniforme, porque la aleatoriedad de una gran cantidad de hojas ya logra un efecto similar a una distribución normal.

---

### 7. Sistema de Grupos de Parámetros

`Grupo de Parámetros` es el concepto de abstracción central de `Fractal Tree Fondos de Escritorio`. Una configuración de árbol único puede contener uno o más grupos de parámetros, cada grupo con un conjunto independiente de parámetros.

#### 7.0 Por qué se Necesitan Grupos de Parámetros

Imagina que quieres diseñar un árbol con un tronco recto pero una copa extendida.  
Si solo usas un conjunto de parámetros, el tronco y la copa solo pueden tener la misma forma, lo que hace imposible lograr efectos complejos.

De hecho, este fue el problema real que encontré al diseñar `Arce`.  
Para esto, tuve dos ideas:

La primera idea era incorporar la coordenada y de la rama en "parámetros de parámetros", lo que podía implementar directamente parámetros que cambian con la altura. En otras palabras, los parámetros se podían escribir como funciones de y.  
Esto se alinea bien con el pensamiento del programador y puede lograr un control de forma más complejo y preciso. Sin embargo, es claramente poco intuitivo. En la práctica, requiere un pensamiento abstracto bastante complejo, y necesita calculadoras o incluso herramientas como Desmos para estudiar gráficos de funciones y ajustar parámetros repetidamente.

Por lo tanto, elegí la segunda idea: el sistema de grupos de parámetros.  
Al definir directamente múltiples conjuntos de parámetros y aplicar diferentes grupos de parámetros a diferentes alturas, se pueden distinguir las formas del tronco y la copa.

Durante el desarrollo real, para transiciones suaves, elegí aplicar una mezcla ponderada inversa de parámetros de diferentes grupos de parámetros.

#### 7.1 Operaciones Básicas

En `Control de Múltiples Grupos de Parámetros`:

- **Desplegable**: Selecciona un grupo de parámetros para editar. Haz clic para modificar el nombre del grupo de parámetros.
- **「+」**: Agrega un nuevo grupo de parámetros (copia los parámetros del grupo de parámetros actualmente seleccionado).
- **「✕」**: Elimina el grupo de parámetros actualmente seleccionado.

#### 7.2 Altura Efectiva Típica

`Altura Efectiva Típica` es la altura de anclaje (en píxeles) de este grupo de parámetros, usada para los cálculos de mezcla ponderada de múltiples grupos de parámetros.  
Se usa para controlar la posición aproximada donde el grupo de parámetros tiene efecto. Cuanto más cerca esté la rama de la altura efectiva típica desde el suelo, mayor será el peso del grupo de parámetros.

En pocas palabras, los parámetros tendrán efecto cerca de la `Altura Efectiva Típica`.

#### 7.3 Cálculo de Peso

Cuando una rama está a la altura $h$, el peso original del grupo $i$ es:

$$w_i = \min\left(\frac{1}{|h - t_i|},\ 128\right)$$

Donde $t_i$ es la altura típica del grupo $i$. El peso se fija en 128 cuando la distancia es 0 para evitar la división por cero. Después de normalizar todos los pesos:

- **Parámetros escalares ordinarios** (rango de ángulo, rango de decaimiento, rama mínima, profundidad máxima, umbral de hoja, parámetros de hojas caídas, longitud del tronco, inclinación de rama): Promedio ponderado.
- **`Configuración de Tronco/Hojas`**: Usar directamente la lista de colores del grupo de mayor peso.
- **`Curvas de Distribución`** (distribución de ángulo, distribución de decaimiento): Promedio ponderado por barra.

Cuando solo hay un grupo de parámetros, se omite el cálculo de mezcla y se usan directamente todos los parámetros de ese grupo.

#### 7.4 Ejemplo de Referencia

El árbol por defecto `Arce` tiene dos grupos de parámetros: "Tronco" y "Copa". La única diferencia entre ellos es el `Rango de Ángulo de Bifurcación`, el `Rango de Decaimiento de Longitud` y la `Altura Efectiva Típica`.

**Grupo de parámetros "Tronco"**:
- La altura efectiva típica es `0`, lo que significa que cuanto más cerca del suelo, mayor es el peso del grupo de parámetros.
- El rango de ángulo de bifurcación es `0 ~ 10` distribución uniforme, manteniendo la forma erguida del tronco.
- El rango de decaimiento de longitud es `0.8 ~ 0.95` distribución uniforme, asegurando que el tronco sea largo y reduciendo las ramas inferiores.

**Grupo de parámetros "Copa"**:
- La altura efectiva típica es `180`, básicamente cuanto más lejos del suelo, mayor es el peso del grupo de parámetros.
- El rango de ángulo de bifurcación es `0 ~ 60` con alta probabilidad en ambos extremos, manteniendo la forma esponjosa de la copa.
- El rango de decaimiento de longitud es `0.3 ~ 0.95` con alta probabilidad en el extremo grande y baja pero posible probabilidad en el extremo pequeño, manteniendo la forma en capas de la copa y aumentando la diversidad de la copa.

---

## Importar/Exportar Especies de Árboles

El programa guarda todas las especies de árboles personalizadas en archivos `<Nombre del Árbol>.json` en la carpeta `AppData\Local\Fractal Tree Fondos de Escritorio\trees`. Puedes hacer clic en el botón `Abrir Carpeta de Árboles` en la parte inferior izquierda de la ventana de configuración de parámetros para abrirlo rápidamente.

- Para importar una especie de árbol, simplemente coloca el archivo `<Nombre del Árbol>.json` que deseas importar en esa carpeta y reinicia el programa.
- Para exportar una especie de árbol, simplemente copia el archivo `<Nombre del Árbol>.json` que deseas exportar.

---

## Ventana de Renderizado

Haz clic en cualquier botón de especie de árbol en la interfaz principal o en Vista Previa en la interfaz de edición para abrir la ventana de renderizado del árbol fractal. Soporta arrastrar y hacer zoom.

- **Actualizar**: Reinicia la semilla aleatoria y vuelve a dibujar. Cada clic produce una forma diferente.
- **Guardar como Imagen**: Renderiza a PNG al tamaño de la ventana, guarda en el escritorio como `<Nombre del Árbol>_<Marca de Tiempo>.png`. La ventana permanece abierta después de completar.
- **Establecer como Fondo de Escritorio**: Renderiza a BMP a 2x la resolución de pantalla, llama a la API del sistema para establecer como fondo de escritorio de Windows. La ventana se cierra automáticamente después de completar.