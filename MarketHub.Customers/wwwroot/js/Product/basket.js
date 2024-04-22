const minus = document.getElementById("minus-btn");
const plus = document.getElementById("plus-btn");
const productCount = document.getElementById("product-count");
const amount = document.getElementById("amount");
minus.addEventListener("click", () => {
    if(+(productCount.value)-1 >= 0)
        productCount.value--;
})

plus.addEventListener("click", () => {
    if(+(productCount.value)+1 <= +(amount.textContent))
        productCount.value++;
})