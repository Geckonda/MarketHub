const minus = document.getElementById("minus-btn");
const plus = document.getElementById("plus-btn");
const productCount = document.getElementById("product-count");
const amount = document.getElementById("amount");

if (minus != null)
minus.addEventListener("click",  () => {
    if (+(productCount.value) - 1 >= 0) {
        productCount.value--;
        postData(productCount);
    }
})

if(plus != null)
plus.addEventListener("click", () => {
    if (+(productCount.value) + 1 <= +(amount.textContent)) {
        productCount.value++;
        postData(productCount);
    }
})

async function postData(input) {
    url = "/Basket/AddProductToBasket";
    //url = "https://market-hub.ru/Basket/AddProductToBasket";
    data = {
        productId: input.getAttribute("data-productId"),
        sizeId: input.getAttribute("data-sizeId"),
        productsCount: input.value,
    };
    const response = await fetch(url, {
        method: "POST", // *GET, POST, PUT, DELETE, etc.
        mode: "cors", // no-cors, *cors, same-origin
        cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
        credentials: "same-origin", // include, *same-origin, omit
        headers: {
            "Content-Type": "application/json",
            // 'Content-Type': 'application/x-www-form-urlencoded',
        },
        redirect: "follow", // manual, *follow, error
        referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
        body: JSON.stringify(data), // body data type must match "Content-Type" header
    });
    return response.json(); // parses JSON response into native JavaScript objects

}