function mostrarMensajeCorrecto() {
    document.getElementById("mensaje").innerHTML = "¡Correcto!";
    document.getElementById("btnSiguiente").style.display = "inline-block";
}
function mostrarMensajeIncorrecto() {
    document.getElementById("mensaje").innerHTML = "Incorrecto.";
    document.getElementById("btnSiguiente").style.display = "inline-block";
}
function mostrarBotonSiguiente() {
    document.getElementById("btnSiguiente").style.display = "inline-block";
}

