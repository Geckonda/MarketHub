const container = document.querySelector(".product__size-container");

const sizeLabel = document.querySelector("#size");
let size = document.querySelector(".product__size-icon");
size.classList.add("product__size-icon_active");
container.addEventListener("click", (e) => {
    if (e.target.closest(".product__size-icon")) {
        size.classList.remove("product__size-icon_active");
        size = e.target;
        size.classList.add("product__size-icon_active");
        sizeLabel.textContent = size.textContent;
    }
})