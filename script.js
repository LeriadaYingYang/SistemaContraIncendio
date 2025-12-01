var limiteAlerta = 0;
var limitePeligro = 0;

// Control del sonido
var alarmaSonando = false;
var sonidoHabilitado = false;

function iniciarControlSonido() {
    var modal = document.getElementById("modalSonido");
    var btnSi = document.getElementById("btnSonidoSi");
    var btnNo = document.getElementById("btnSonidoNo");
    var audio = document.getElementById("sonidoAlarma");

    if (!modal || !btnSi || !btnNo || !audio) {
        console.log("Faltan elementos del modal o del audio.");
        return;
    }

    var preferencia = localStorage.getItem("alarma_sonido");

    if (preferencia === "si") {
        sonidoHabilitado = true;
        modal.style.display = "none";
        console.log("Sonido ya autorizado previamente.");
        return;
    }

    modal.style.display = "flex";

    btnSi.addEventListener("click", function () {
        audio.play().then(function () {
            audio.pause();
            audio.currentTime = 0;

            sonidoHabilitado = true;
            modal.style.display = "none";

            localStorage.setItem("alarma_sonido", "si");
        }).catch(function (error) {
            console.log("Error al activar sonido:", error);
        });
    });

    btnNo.addEventListener("click", function () {
        sonidoHabilitado = false;
        modal.style.display = "none";});
}

function cargarDatos() {
    fetch("limites.txt?cache=" + Date.now())
        .then(function (res1) {
            return res1.text();
        })
        .then(function (textoLimites) {
            procesarLimites(textoLimites);
            return fetch("temperaturas.txt?cache=" + Date.now())
        })
        .then(function (res2) {
            return res2.text();
        })
        .then(function (textoTemperaturas) {
            procesarTemperaturas(textoTemperaturas);
        })
        .catch(function (error) {
            console.log("Error:", error);
            document.getElementById("tablaHistorial").innerHTML =
                "<tr><td colspan='4'>No se pudieron cargar los archivos.</td></tr>";
        });
}


function procesarLimites(texto) {
    var lineas = texto.split("\n");
    var alertaOK = false;
    var peligroOK = false;

    for (var i = 0; i < lineas.length; i++) {
        var linea = lineas[i].trim();

        if (linea.indexOf("ALERTA=") === 0) {
            limiteAlerta = parseInt(linea.replace("ALERTA=", ""));
            alertaOK = !isNaN(limiteAlerta);
        }

        if (linea.indexOf("PELIGRO=") === 0) {
            limitePeligro = parseInt(linea.replace("PELIGRO=", ""));
            peligroOK = !isNaN(limitePeligro);
        }
    }

    var textoMostrar = "";

    if (alertaOK) {
        textoMostrar += "Alerta: " + limiteAlerta + "°C";
    } else {
        textoMostrar += "Alerta: no definido";
    }

    textoMostrar += "<br>";

    if (peligroOK) {
        textoMostrar += "Peligro: " + limitePeligro + "°C";
    } else {
        textoMostrar += "Peligro: no definido";
    }

    document.getElementById("textoLimites").innerHTML = textoMostrar;
}

function procesarTemperaturas(texto) {
    var lineas = texto.split("\n");
    var historial = [];

    for (var i = 0; i < lineas.length; i++) {
        var linea = lineas[i].trim();
        if (linea.length === 0) continue;

        var partes = linea.split(";");

        if (partes.length === 2) {
            var temp = parseInt(partes[0]);
            var fecha = partes[1];

            if (!isNaN(temp)) {
                historial.push({ temperatura: temp, fecha: fecha });
            }
        }
    }

    if (historial.length === 0) {
        document.getElementById("tablaHistorial").innerHTML =
            "<tr><td colspan='4'>No hay datos.</td></tr>";
        return;
    }

    var ultimo = historial[historial.length - 1];

    document.getElementById("ultimaTemp").innerHTML = ultimo.temperatura + "°C";
    document.getElementById("ultimaFecha").innerHTML = "Fecha y hora: " + ultimo.fecha;

    var estado = "NORMAL";
    var claseEstado = "normal";

    if (limitePeligro > 0 && ultimo.temperatura >= limitePeligro) {
        estado = "PELIGRO";
        claseEstado = "peligro";
    } else if (limiteAlerta > 0 && ultimo.temperatura >= limiteAlerta) {
        estado = "ALERTA";
        claseEstado = "alerta";
    }

    var elementoEstado = document.getElementById("estadoTemp");
    elementoEstado.innerHTML = estado;
    elementoEstado.className = "etiqueta " + claseEstado;

    var textoAlarma = "DESACTIVADA";
    var claseAlarma = "normal";

    if (estado === "PELIGRO") {
        textoAlarma = "ACTIVADA";
        claseAlarma = "peligro";
    }

    document.getElementById("estadoAlarma").innerHTML = textoAlarma;

    var audio = document.getElementById("sonidoAlarma");

    if (textoAlarma === "ACTIVADA") {
        if (audio && sonidoHabilitado && audio.paused) {
            audio.currentTime = 0;
            audio.play()
                .then(function () {
                    console.log("Alarma sonando...");
                    alarmaSonando = true;
                })
                .catch(function (err) {
                    console.log("No se pudo reproducir el sonido:", err);
                    alarmaSonando = false;
                });
        }
    } else {
        if (audio && !audio.paused) {
            audio.pause();
            audio.currentTime = 0;
            alarmaSonando = false;
            console.log("Alarma detenida.");
        }
    }

    var chip = document.getElementById("chipAlarma");
    chip.innerHTML = textoAlarma;
    chip.className = "etiqueta " + claseAlarma;
    var htmlTabla = "";
    var contador = 1;

    for (var j = historial.length - 1; j >= 0; j--) {
        var item = historial[j];

        var estadoItem = "NORMAL";
        var claseItem = "normal";

        if (limitePeligro > 0 && item.temperatura >= limitePeligro) {
            estadoItem = "PELIGRO";
            claseItem = "peligro";
        } else if (limiteAlerta > 0 && item.temperatura >= limiteAlerta) {
            estadoItem = "ALERTA";
            claseItem = "alerta";
        }

        htmlTabla += "<tr>";
        htmlTabla += "<td>" + contador + "</td>";
        htmlTabla += "<td>" + item.temperatura + "°C</td>";
        htmlTabla += "<td><span class='etiqueta " + claseItem + "'>" + estadoItem + "</span></td>";
        htmlTabla += "<td>" + item.fecha + "</td>";
        htmlTabla += "</tr>";

        contador++;
    }

    document.getElementById("tablaHistorial").innerHTML = htmlTabla;
}

window.addEventListener("load", function () {
    iniciarControlSonido();
    cargarDatos();
    setInterval(cargarDatos, 1000);
});
