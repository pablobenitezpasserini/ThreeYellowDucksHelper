let cardCostos = document.getElementById("cardCostos")
let cardArticulo = document.getElementById("cardArticulo")

cardCostos.addEventListener("click", () => redirectToProducts())
cardArticulo.addEventListener("click", () => redirectToCreateCategory())

function redirectToProducts() {
    window.location.href = '/Productos/IndexProductos';
}

function redirectToCreateCategory() {
    window.location.href = '/Categorias/CreateCategoria';
}