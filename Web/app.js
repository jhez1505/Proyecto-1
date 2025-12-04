// app.js - funcional completo
let ventas = [];
let meses = [];
let mesActual = 1;
let grafico = null;

// helpers
function normalRandom() {
    // Box-Muller
    let u = Math.random(), v = Math.random();
    return Math.sqrt(-2 * Math.log(u)) * Math.cos(2 * Math.PI * v);
}

// UI elementos
const ventaInput = document.getElementById("ventaInput");
const tablaVentas = document.getElementById("tablaVentas");
const btnAgregar = document.getElementById("btnAgregar");
const btnAnalizar = document.getElementById("btnAnalizar");
const btnLimpiar = document.getElementById("btnLimpiar");
const resText = document.getElementById("resText");
const simText = document.getElementById("simText");
const graficaCard = document.getElementById("graficaCard");
const simCard = document.getElementById("simCard");
const resultadosDiv = document.getElementById("resultados");
const predCard = document.getElementById("predCard");
const btnPredecir = document.getElementById("btnPredecir");
const resultadoPrediccion = document.getElementById("resultadoPrediccion");
const mesPrediccionInput = document.getElementById("mesPrediccion");

// Agregar venta
btnAgregar.addEventListener("click", () => {
    const val = parseFloat(ventaInput.value);
    if (isNaN(val) || val <= 0) {
        alert("Ingresa un valor numérico mayor que 0");
        return;
    }
    ventas.push(val);
    meses.push(mesActual);
    tablaVentas.innerHTML += `<tr><td>${mesActual}</td><td>${val}</td></tr>`;
    mesActual++;
    ventaInput.value = "";
});

// Limpiar datos
btnLimpiar.addEventListener("click", () => {
    ventas = [];
    meses = [];
    mesActual = 1;
    tablaVentas.innerHTML = "";
    resultadosDiv.style.display = "none";
    graficaCard.style.display = "none";
    simCard.style.display = "none";
    predCard.style.display = "none";
    if (grafico) { grafico.destroy(); grafico = null; }
});

// Analizar (estadística + simulación + regresión + graficar)
btnAnalizar.addEventListener("click", () => {
    if (ventas.length === 0) { alert("Primero ingresa ventas."); return; }

    // estadística descriptiva
    const n = ventas.length;
    const suma = ventas.reduce((a,b) => a + b, 0);
    const media = suma / n;
    const varianza = ventas.reduce((a,b) => a + (b - media) ** 2, 0) / n;
    const desviacion = Math.sqrt(varianza);
    const maxVenta = Math.max(...ventas);
    const minVenta = Math.min(...ventas);

    // simulación Monte Carlo (Normal aproximada con mu, sigma)
    const sims = 10000;
    let conteoMayor = 0;
    for (let i = 0; i < sims; i++) {
        const muestra = media + desviacion * normalRandom();
        if (muestra > 250) conteoMayor++;
    }
    const probMayor250 = conteoMayor / sims;

    // regresión lineal simple (x = meses, y = ventas)
    const sumX = meses.reduce((a,b) => a + b, 0);
    const sumY = suma;
    const sumXY = meses.reduce((a,b,i) => a + b * ventas[i], 0);
    const sumX2 = meses.reduce((a,b) => a + b*b, 0);
    const m = (n * sumXY - sumX * sumY) / (n * sumX2 - sumX * sumX);
    const b = (sumY - m * sumX) / n;

    // Mostrar resultados
    resultadosDiv.style.display = "block";
    resText.innerHTML = `
        <p><b>Muestras:</b> ${n}</p>
        <p><b>Media:</b> ${media.toFixed(2)}</p>
        <p><b>Varianza:</b> ${varianza.toFixed(2)}</p>
        <p><b>Desviación estándar:</b> ${desviacion.toFixed(2)}</p>
        <p><b>Venta máxima:</b> ${maxVenta}</p>
        <p><b>Venta mínima:</b> ${minVenta}</p>
    `;

    // Mostrar simulación
    simCard.style.display = "block";
    simText.innerText = `Probabilidad estimada (simulación) de que ventas > 250: ${(probMayor250*100).toFixed(2)}% (Monte Carlo ${sims} muestras)`;

    // Mostrar predicción card
    predCard.style.display = "block";
    graficaCard.style.display = "block";

    // Graficar ventas y recta de regresión (y = m*x + b) y predicción futura
    if (grafico) { grafico.destroy(); grafico = null; }
    const ctx = document.getElementById("grafico").getContext("2d");

    // calcular puntos de la recta (incluimos hasta mes + 6 para ver proyección)
    const maxX = meses[meses.length - 1] + 6;
    const lineaX = [];
    const lineaY = [];
    for (let x = 1; x <= maxX; x++) {
        lineaX.push(x);
        lineaY.push(m * x + b);
    }

    grafico = new Chart(ctx, {
        type: 'line',
        data: {
            labels: lineaX,
            datasets: [
                {
                    label: 'Regresión (tendencia)',
                    data: lineaY,
                    borderColor: '#ffd166',
                    borderWidth: 2,
                    fill: false,
                    pointRadius: 0,
                    tension: 0.2
                },
                {
                    label: 'Ventas (reales)',
                    data: lineaX.map(x => {
                        // si existe venta para x (meses empiezan en 1), devolver valor, sino null
                        const idx = meses.indexOf(x);
                        return idx >= 0 ? ventas[idx] : null;
                    }),
                    borderColor: '#8b5cf6',
                    backgroundColor: 'rgba(139,92,246,0.2)',
                    borderWidth: 2,
                    fill: true,
                    pointRadius: 5,
                    tension: 0.2
                }
            ]
        },
        options: {
            scales: {
                x: { title: { display: true, text: 'Mes' } },
                y: { title: { display: true, text: 'Ventas' } }
            },
            plugins: {
                legend: { position: 'top' }
            }
        }
    });

    // guardar los coeficientes de regresión para usar en predicción
    window._regression = { m, b };
});

// Predicción usando regresión calculada
btnPredecir.addEventListener("click", () => {
    if (!window._regression) {
        alert("Primero analiza los datos para obtener la regresión.");
        return;
    }
    const mes = parseInt(mesPrediccionInput.value);
    if (isNaN(mes) || mes <= 0) { alert("Ingresa mes válido"); return; }
    const { m, b } = window._regression;
    const pred = m * mes + b;
    resultadoPrediccion.innerText = `Predicción para el mes ${mes}: ${pred.toFixed(2)} unidades (Regresión lineal)`;
});
