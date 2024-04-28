const btn = document.querySelector("#review__send-btn");
const textArea = document.getElementById("review__value");

const customerReviews = document.querySelector("#customer-reviews");
let customerMark = 0;

textArea.addEventListener("input", () => {
    if (textArea.value === "" || customerMark <= 0)
        btn.disabled = true;
    else
        btn.disabled = false;
})
btn.addEventListener("click", postData);

async function postData() {
    url = "https://market-hub.ru/Review/AddReview";
    //url = "/Review/AddReview";
    data = {
        customerId: btn.getAttribute("data-customerId"),
        productId: btn.getAttribute("data-productId"),
        text: textArea.value,
        stars: customerMark,
    };
;
    // Default options are marked with *
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
    makeReviewBlock();
    areaReset();
    return response.json(); // parses JSON response into native JavaScript objects
}

const mainStars = document.querySelectorAll(".review-info__stars-block.main-mark .star");
const mainMark = document.querySelector("#main-mark").textContent[0];

for (var i = 0; i < mainMark; i++) {
    [...mainStars][i].classList.add("star_active");
}

function makeReviewBlock() {
    const review = `<div class="review__header">
                            <h5 class="review__customer-name">Ваш комментарий</h5>
                            <div class="review-info__stars-block"></div>
                        </div>
                        <p class="review__date">только что</p>
                        <p class="review__text">${textArea.value}</p>`;
    const div = document.createElement("div");
    div.innerHTML = review;
    div.classList = "review";
    if (customerReviews.childElementCount > 0)
        customerReviews.insertBefore(div, customerReviews.firstChild);
    else
        customerReviews.appendChild(div);
}

const starLabelBlock = document.querySelector(".star-label-block");
const starLabels = [...document.querySelectorAll(".star-label")].reverse();
starLabelBlock.addEventListener("click", (e) => {
    if (e.target.closest(".star-label")) {
        let el = e.target;
        let value = el.getAttribute("data-pos");
        customerMark = +value;
        starLabels.forEach(x => x.style.background = '#AEAEAE')
        for (var i = 0; i < value; i++) {
            //starLabels[i].classList.add("star_active");
            starLabels[i].style.background = '#E6AD1B';
        }
        if (textArea.value === "" || customerMark <= 0)
            btn.disabled = true;
        else
            btn.disabled = false;
    }
})

function areaReset() {
    textArea.value = "";
    customerMark = 0;
    btn.disabled = true;
    starLabels.forEach(x => x.style.background = '#AEAEAE');
}