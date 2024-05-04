const minuses = document.querySelectorAll(".minus");
const pluses = document.querySelectorAll(".plus");
const sumLabel = document.getElementById("sum");
const amountLabel = document.getElementById("products-count");

const deleteBtns = document.querySelectorAll(".delete-btn");

setLabelAmount();
setLabelSum();

function setLabelSum() {
    let sum = 0;
    const checkBoxes = document.querySelectorAll(".productToBasket");
    checkBoxes.forEach(x => {
        if (x.checked) {
            let price = x.getAttribute("data-price");
            let amount = x.getAttribute("data-amount");
            sum += price * amount;
        }
    })
    if (sumLabel != null)
        sumLabel.textContent = sum;
}
function setLabelAmount() {
    let amount = 0;
    const checkBoxes = document.querySelectorAll(".productToBasket");
    checkBoxes.forEach(x => {
        if(x.checked)
            amount += +(x.getAttribute("data-amount"));
    })
    if (amountLabel != null)
        amountLabel.textContent = amount;
    if (amount == 0)
        document.querySelector("h1 span").textContent = "(Пусто)";
    else {
        document.querySelector("h1 span").textContent = `(${amount})`;
    }
}

document.querySelectorAll(".productToBasket").forEach(x => {
    x.addEventListener("change", () => {
        setLabelAmount();
        setLabelSum();
    })
})

minuses.forEach(x => {
    x.addEventListener("click", () => {
        let input = document.getElementById(x.getAttribute("data-bpId"));
        if (+(input.value) - 1 >= 1) {
            input.value--;
            postData(input);

            let bpId = input.getAttribute("data-bpId");
            let checkBox = document.getElementById(`product-${bpId}`);
            checkBox.setAttribute("data-amount", +(checkBox.getAttribute("data-amount")) - 1);
            setLabelSum();
            setLabelAmount();
        }
    })
})
pluses.forEach(x => {
    x.addEventListener("click", () => {
        let input = document.getElementById(x.getAttribute("data-bpId"));
        if (+(input.value) + 1 <= +(input.max)) {
            input.value++;
            postData(input);

            let bpId = input.getAttribute("data-bpId");
            let checkBox = document.getElementById(`product-${bpId}`);
            checkBox.setAttribute("data-amount", +(checkBox.getAttribute("data-amount")) + 1);
            setLabelSum();
            setLabelAmount();
        }
    })
})

deleteBtns.forEach(x => {
    x.addEventListener("click", () => {
        deleteProductCard(x.getAttribute("data-productcardid"));
        setLabelAmount();
        setLabelSum();
        deleteProduct(x);
    })
})

function deleteProductCard(cardId)
{
    const cardContainer = document.querySelector(".product-container");
    const card = document.querySelector(`#${cardId}`);
    cardContainer.removeChild(card);
}

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

    setLabelAmount();
    setLabelSum();
    return response.json(); // parses JSON response into native JavaScript objects
}

async function deleteProduct(input) {
    url = "/Basket/RemoveProductFromBasket";
    //url = "https://market-hub.ru/Basket/RemoveProductFromBasket";
    data = {
        productId: input.getAttribute("data-productId"),
        sizeId: input.getAttribute("data-sizeId"),
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