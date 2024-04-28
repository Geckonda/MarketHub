const minuses = document.querySelectorAll(".minus");
const pluses = document.querySelectorAll(".plus");
const checkBoxes = document.querySelectorAll(".productToBasket");
const sumLabel = document.getElementById("sum");
const amountLabel = document.getElementById("products-count");


setLabelAmount();
setLabelSum();

function setLabelSum() {
    let sum = 0;
    checkBoxes.forEach(x => {
        if (x.checked) {
            let price = x.getAttribute("data-price");
            let amount = x.getAttribute("data-amount");
            sum += price * amount;
        }
    })
    sumLabel.textContent = sum;
}
function setLabelAmount() {
    let amount = 0;
    checkBoxes.forEach(x => {
        if(x.checked)
            amount += +(x.getAttribute("data-amount"));
    })
    amountLabel.textContent = amount;
}

checkBoxes.forEach(x => {
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


async function postData(input) {
    //url = "/Basket/AddProductToBasket";
    url = "https://market-hub.ru/Basket/AddProductToBasket";
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
