## Sistema de Consola de Alarma Contra Incendios

Realizado por:

<div align="center">

| **Nombre** | **Codigo** | **Funcion** |
|:----------:|:----------:|:-----------:|
| Daniel Enrique Jara Alva | N00243181 | Lider | 
| Quiroz Cabanillas Franco Yaren   | N00500542 | Desarrollador 1 | 
| Diaz Tello Pablo  | N00483823 | Desarrollador 2 | 
| Pompa Culqui Jhordan Jesús  | N00490707 | Desarrollador 3| 

</div>


Este proyecto primero se hizo en 5 reuniones desde el 31 de octubre del 2025 donde se discutio y se quedo que crearamos un panel de control y un visor donde se ingresara todo los datos en c# y limites de temperatura y se quedo que primero se desarrollara en
Visual Studio 2022, posteriormente el Prototipo de Consola duro casi medio mes acabando el 20 de noviembre del 2025 usando diferentes codigos, sintaxis, arreglos y base de datos para lograr una buena secuencia de opciones para un panel de control.
Se logro crear un el sistema desarrollado en C# que permite registrar, almacenar, evaluar y monitorear temperaturas relacionadas con un posible incendio.
El sistema se divide en tres bibliotecas especializadas y el Menu Principal donde se ejecuta todo:

- BibliotecaContraIncendios
Maneja toda la lógica del sistema como registrar, evaluar, reemplazar, eliminar, buscar.

- BibliotecaDatosIncendios
Se encarga del manejo de archivos temperaturas.txt para guardar y cargar el historial.

- BibliotecaLimiteTemperatura
Permite cargar y guardar los límites.txt de alerta y peligro desde un archivo externo.

- Y finalmente el proyecto de Consola Principal que es el corazón del sistema, aquí se cargan los datos principales y la lógica de operaciones.

Todo esto llevo mucho tiempo pero se creo el corazon del proyecto despues se hizo otra reunion donde el lider propuso crear un panel donde se subieran todo los datos a una pagina web y controlarlo en tiempo 
real visualizando mejor el ingreso de datos.
El líder del proyecto asumió la responsabilidad de integrar todo el código desarrollado por los cuatro compañeros encargados de las bibliotecas y de la consola en C#. Para ello, recopiló cada módulo, revisó la 
compatibilidad entre funciones, depuró errores y unificó todas las partes en un solo sistema funcional. Posteriormente, migró el proyecto a JetBrains Rider, herramienta que permitió realizar la ejecución, 
pruebas y ajustes finales directamente en el entorno del VPS, asegurando que la consola funcionara de forma estable fuera del entorno local.
Paralelamente, el líder desarrolló e integró la versión web del sistema utilizando WebStorm, donde organizó y estructuró los archivos HTML, CSS y JavaScript para crear un panel visual que mostrara estados, 
alertas y datos del sistema de alarma. Tras completar la interfaz, configuró el servidor NGINX y Cloudflare, vinculó el dominio, y desplegó la web en el VPS para que estuviera disponible públicamente mediante 
un acceso seguro y encriptado.
Una vez finalizada toda la integración técnica, el líder realizó una explicación detallada al equipo sobre el funcionamiento del sistema completo: el proceso de carga del código, la relación entre la consola 
en Rider y la web en WebStorm, la configuración de puertos, DNS, NGINX y el despliegue final en el servidor. Finalmente, con todos los módulos funcionando y documentados, el equipo concluyó la elaboración del 
informe final del proyecto.
