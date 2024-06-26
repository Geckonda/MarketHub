﻿const form = document.querySelector("#form-container");
const btn = document.querySelector("#size-btn");
const submit = document.getElementById("submit");
let count = 1;

const tableBody = document.querySelector(".sizes-table tbody");
const sizeNameInp = document.getElementById(`size-name`);
const sizeAmountInp = document.getElementById(`size-amount`);

const coverImage = document.getElementById("CoverImage");
const coverImageLabel = document.getElementById("CoverImageLabel");
btn.addEventListener("click", () => {

    if (sizeAmountInp.value != "" && sizeNameInp.value != "" && +sizeAmountInp.value > 0) {
        const newInputContainer = document.createElement("div");
        const newSizeNameInp = document.createElement("input");
        const newSizeAmountInp = document.createElement("input");

        //name input
        newSizeNameInp.type = "text";
        newSizeNameInp.name = "sizeNames";
        newSizeNameInp.id = `size-name-${count}`;
        newSizeNameInp.placeholder = "Размер";
        newSizeNameInp.value = sizeNameInp.value;
        newSizeNameInp.required = false;
        newSizeNameInp.hidden = true;
        //amount input
        newSizeAmountInp.type = "number";
        newSizeAmountInp.name = "sizeAmount";
        newSizeAmountInp.id = `size-amount-${count}`;
        newSizeAmountInp.placeholder = "Количество товара данного размера";
        newSizeAmountInp.value = sizeAmountInp.value;
        newSizeNameInp.required = false;
        newSizeAmountInp.hidden = true;
        newSizeAmountInp.setAttribute('min', 1);
        //input container
        newInputContainer.classList.add("input-container");
        newInputContainer.id = `inp-cont-${count}`;

        const tr = document.createElement("tr");
        const tdSize = document.createElement("td");
        const tdAmount = document.createElement("td");
        tdSize.textContent = sizeNameInp.value;
        tdAmount.textContent = sizeAmountInp.value;

        const deleteBtn = document.createElement("img");
        deleteBtn.src = "/img/icons/delete.png";
        deleteBtn.classList.add("delete-btn");
        deleteBtn.setAttribute(`data-delete`, count);
        tdAmount.appendChild(deleteBtn);
        tr.appendChild(tdSize);
        tr.appendChild(tdAmount);
        tr.id = `tr-${count}`;
        tableBody.appendChild(tr);
        
        newInputContainer.appendChild(newSizeNameInp);
        newInputContainer.appendChild(newSizeAmountInp);
        form.appendChild(newInputContainer);
        btn.textContent = "Добавить еще";
        count++;
        sizeNameInp.value = "";
        sizeAmountInp.value = "";
        unDisableButton();
    }
});

tableBody.addEventListener("click", (e) => {
    if(e.target.closest(".delete-btn"))
    {
        let btn = e.target;
        let number = btn.dataset.delete;
        let deletingElement = document.querySelector(`#inp-cont-${number}`);
        form.removeChild(deletingElement);
        deletingElement = document.querySelector(`#tr-${number}`)
        tableBody.removeChild(deletingElement);
        unDisableButton();
    }
})

function unDisableButton() {
    if (tableBody.childElementCount == 0) {
        sizeNameInp.required = true;
        sizeAmountInp.required = true;
        submit.disabled = true;
    }
    else {
        sizeNameInp.required = false;
        sizeAmountInp.required = false;
        submit.disabled = false;
    }
}
coverImage.addEventListener("change", () => {
    if (coverImage.value != "") {
        console.log(coverImageLabel);
        coverImageLabel.style.color = "green";
        coverImageLabel.textContent = "Картинка загружена ";
    }
})