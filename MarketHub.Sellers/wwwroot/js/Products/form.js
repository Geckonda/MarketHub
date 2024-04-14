const form = document.querySelector("#form-container");
const btn = document.querySelector("#size-btn");

let count = 1;

const tableBody = document.querySelector(".sizes-table tbody");
const sizeNameInp = document.getElementById(`size-name`);
const sizeAmountInp = document.getElementById(`size-amount`);

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
        newInputContainer.id = `inp-cont${count}`;

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
    }
});

tableBody.addEventListener("click", (e) => {
    if(e.target.closest(".delete-btn"))
    {
        let btn = e.target;
        let number = btn.dataset.delete;
        let deletingElement = document.querySelector(`#inp-cont${number}`);
        form.removeChild(deletingElement);
        deletingElement = document.querySelector(`#tr-${number}`)
        tableBody.removeChild(deletingElement);
    }
})


//const form = document.querySelector("#form-container");
//let count = 2;
//let prevInp = document.querySelector(`#color-input-f${count - 1}`);

//prevInp.addEventListener("change", MakeMoreInput);


//function MakeMoreInput(e) {
//    if (e.target.files[0]) {
//        prevInp.removeEventListener("change", MakeMoreInput);


//        const prevLab = document.querySelector(`#color-label-l${count - 1}`);


//        prevLab.classList.add("input_active");
//        prevLab.textContent = "Фото загружено";
//        const container = document.createElement("div");
//        const inp = document.createElement("input");
//        const label = document.createElement("label");
//        const nameInp = document.createElement("input");
//        const amountInp = document.createElement("input");

//        //container
//        container.classList.add("input-container");

//        //input
//        inp.type = "file";
//        inp.id = `color-input-f${count}`;
//        inp.classList.add('file-input');
//        inp.name = "colors";
//        //nameInput
//        nameInp.type = "text";
//        nameInp.name = "colorNames";
//        //amountInput
//        amountInp.type = "number";
//        amountInp.name = "colorAmount";

//        //label
//        label.setAttribute("for", `color-input-f${count}`);
//        label.classList.add("file-label");
//        label.id = `color-label-l${count}`;
//        label.textContent = "Загрузить фото";

//        container.appendChild(nameInp);
//        container.appendChild(amountInp);
//        container.appendChild(label);
//        container.appendChild(inp);
//        form.appendChild(container);
//        count++;
//        prevInp = document.querySelector(`#color-input-f${count - 1}`);

//        prevInp.addEventListener("change", MakeMoreInput);
//    }
//}
