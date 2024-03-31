const form = document.querySelector("#form-container");
const btn = document.querySelector("#size-btn");

let count = 1;

btn.addEventListener("click", () => {
    const sizeNameInp = document.getElementById(`size-name-${count}`);
    const sizeAmountInp = document.getElementById(`size-amount-${count}`);

    if (sizeAmountInp.value != "" && sizeNameInp.value != "" && +sizeAmountInp.value > 0) {
        const newInputContainer = document.createElement("div");
        const newSizeNameInp = document.createElement("input");
        const newSizeAmountInp = document.createElement("input");

        //name input
        newSizeNameInp.type = "text";
        newSizeNameInp.name = "sizeNames";
        newSizeNameInp.id = `size-name-${count + 1}`;
        newSizeNameInp.placeholder = "Размер";
        newSizeNameInp.required = true;
        //amount input
        newSizeAmountInp.type = "number";
        newSizeAmountInp.name = "sizeAmount";
        newSizeAmountInp.id = `size-amount-${count + 1}`;
        newSizeAmountInp.placeholder = "Количество товара данного размера";
        newSizeAmountInp.required = true;
        newSizeAmountInp.setAttribute('min', 1);
        //input container
        newInputContainer.classList.add("input-container");

        newInputContainer.appendChild(newSizeNameInp);
        newInputContainer.appendChild(newSizeAmountInp);
        form.appendChild(newInputContainer);
        btn.textContent = "Добавить еще размер";
        count++;
    }
});



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
