const container = document.querySelector(".product__size-container");

const sizeLabel = document.querySelector("#size");
let size;
container.addEventListener("click", (e) => {
    if (e.target.closest(".product__size-icon") && !e.target.closest(".product__size-icon_absent")) {
        size.classList.remove("product__size-icon_active");
        size = e.target;
        size.classList.add("product__size-icon_active");
        sizeLabel.textContent = size.textContent;
    }
})

const sizes = document.querySelectorAll(".product__size-icon");
[...sizes].forEach((x) => {
    if (x.getAttribute("data-size-amount") == 0)
        x.classList.add("product__size-icon_absent");
});

for (let i = 0; i < [...sizes].length; i++) {
    size = [...sizes][i];
    if (size.getAttribute("data-size-amount") != 0) {
        size.classList.add("product__size-icon_active");
        sizeLabel.textContent = size.textContent;
        break;
    }
}
