// Ruta base de la API (mismo servidor).
const API = "/api/tasks";

// Referencias a elementos del DOM.
const taskList = document.getElementById("taskList");
const form = document.getElementById("taskForm");
const resetBtn = document.getElementById("resetBtn");

// Paneles de debug para ver lo que se envia y recibe.
const reqBox = document.getElementById("reqBox");
const resBox = document.getElementById("resBox");

// Pinta la peticion en un formato legible.
function showRequest(method, url, headers, body) {
  reqBox.textContent = JSON.stringify({ method, url, headers, body }, null, 2);
}

// Pinta la respuesta en un formato legible.
function showResponse(status, headers, body) {
  resBox.textContent = JSON.stringify({ status, headers, body }, null, 2);
}

// Carga inicial de tareas (se completara cuando la API este conectada).
async function refresh() {
  // Iteración 1: cuando exista el endpoint GET /api/tasks lo conectamos aquí
  taskList.innerHTML = "<li>(sin conectar todavía)</li>";
}

// Envio del formulario: por ahora muestra un aviso.
form.addEventListener("submit", async (e) => {
  e.preventDefault();
  alert("En el siguiente paso conectaremos POST /api/tasks");
});

// Limpia el formulario.
resetBtn.addEventListener("click", () => {
  form.reset();
  document.getElementById("taskId").value = "";
});

// Arranque de la pagina.
refresh();
