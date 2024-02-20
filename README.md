# SkiaSharp + Windows Forms + Neuronal Network 
Ejemplo de red neuronal que intenta aprender a conducir un coche.

Gráficos dibujados manualmente usando ``SkiaSharp``, tanto los vehículos como el visualizador de la red neuronal. ``SkiaSharp`` es una API multiplataforma para gráficos 2D para plataformas .NET y que está basada en [Skia](skia.org), librería gráfica de Google.

Basado en un proyecto original que usaba [Vanilla Javascript](https://github.com/gniziemazity/self-driving-car) y el canvas HTML5 para pintar, y realizado por [Radu Mariescu-Istodor](https://github.com/gniziemazity). 

A partir de información del proyecto original, se rehace desde cero, cambiando de tecnología, usando ``.NET`` en vez de Javascript, y ``SkiaSharp`` como sustituto del canvas del explorador. 

## Funcionalidad
- Conducción manual haciendo uso del teclado: Teclas WASD. Cada una de estas posibles teclas se corresponde a una de las neuronas de la capa de salida.
- Conducción automática a través de la red. La red decide en función de los bordes de la carretera y del tráfico prensente que hacer: Girar izquierda o derecha, velocidad hacia delante, incluso posibilidad de ir marcha atrás.
- Sensores de colisiones configurables. Determinan el número de neuronas de la capa de entrada. Cada sensor calcula en tiempo real las colisiones con otros objetos y la distancia al primer punto de colisión.
- Acercamiento al aprendizaje genético con selección manual de la especie que sobrevive. Mutaciones aleatorias entre generación.
- Representación visual de la red neuronal y de las activaciones mediante un gráfico en el panel lateral.
- Posibilidad de modificar el número de neuronas por capa (excepto la capa de salida que determinan las instrucciones al vehículo).
- Simulación de tráfico aleatorio o programado. Los vehículos generados actúan como obstáculos a los vehículos que entrenamos.
- Simulación de carretera, lindes y carriles de circulación. Los vehículos que se salen de la carretera colisionan.
- Seguimiento del mejor coche (posibilidad de definir función de fitness) y posibilidad de persistir su red neuronal.
- Importación de pesos de la red neuronal.
- Implementación de un panel en el que se pueden mostrar datos en tiempo real de la simulación y que se puede ir ampliando para mostrar información de interés en el aprendizaje.

Es interesenta la representación gráfica de las capas de la red neuronal y de las activaciones entre capas, permite ver visualmente como el valor de los pesos afecta al resultado del movimiento del coche.

## Ejemplo 
Se muestra un GIF con un ejemplo de 100 vehículos mutados a partir de una misma red en sus etapas iniciales. Cada vehículo, tras las mutaciones de la generación, toma sus desiciones en función de los estímulos de los sensores que recibe.

![Ejemplo Simulación](https://github.com/FranEspina/SkiaCarForms/assets/53045314/25e52f65-19ad-44f1-8e8e-f469da402737)
