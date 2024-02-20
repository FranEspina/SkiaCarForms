# SkiaSharp + Windows Forms + Neuoronal Network 
Ejemplo de red neuronal que intenta aprender a conducir un coche.

Gráficos dibujados manualmente usando ``SkiaSharp``, tanto los vehículos como el visualizador de la red neuronal. ``SkiaSharp`` es una API multiplataforma para gráficos 2D para plataformas .NET y que está basada en [Skia](skia.org), librería gráfica de Google.

Basado en un proyecto original que usaba [Vanilla Javascript](https://github.com/gniziemazity/self-driving-car) y el canvas HTML5 para pintar, y realizado por [Radu Mariescu-Istodor](https://github.com/gniziemazity). 

A partir de información del proyecto original, se rehace desde cero, cambiando de tecnología, usando ``.NET`` en vez de Javascript, y ``SkiaSharp`` como sustituto del canvas del explorador. 

## Funcionalidad
- Conducción manual haciendo uso del teclado: Teclas WASD. Cada una de estas posibles teclas se corresponde a una de las neuronas de la capa de salida.
- Sensores de colisiones configurables. Determinan el número de neuronas de la capa de entrada.
- Conducción automatica a través de la red.
- Acercamiento al aprendizaje genético con selección manual de la especie que sobrevive.
- Mutaciones aleatorias.
- Representación visual de la red neuronal y de las activaciones.
- Posibilidad de modificar el número de neuronas por capa (excepto la capa de salida que determinan las instrucciones al vehículo).
- Simulación de tráfico aleatorio que actúan como obstáculos.
- Simulación de bordes de la carretera.
- Seguimiento del mejor coche (posibilidad de definir función de fitness) y posibilidad de persistir su red neuronal
- Importación de pesos de la red neuronal.

Es interesenta la representación gráfica de las capas de la red neuronal y de las activaciones entre capas, permite ver visualmente como el valor de los pesos afecta al resultado del movimiento del coche.

## Ejemplo 
Se muestra un ejemplo de 100 vehículos mutados a partir de una misma red en sus etapas iniciales. Cada vehículo, tras las mutaciones de la generación, toma sus desiciones en función de los estímulos de los sensores que recibe.
![Ejemplo Simulación](https://github.com/FranEspina/SkiaCarForms/assets/53045314/25e52f65-19ad-44f1-8e8e-f469da402737)
