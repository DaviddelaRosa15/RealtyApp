// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


let code = document.getElementById("codeTxt");
let codeHidden = document.getElementById("codeHidden");
let assetTypeHidden = document.getElementById("assetTypeHidden");
let minHidden = document.getElementById("minHidden");
let maxHidden = document.getElementById("maxHidden");
let bathroomHidden = document.getElementById("bathroomHidden");
let bedroomHidden = document.getElementById("bedroomHidden");

function GetCodeValue() {
    codeHidden.value = code.value;
}

function GetTypeIdValue(id) {
    assetTypeHidden.value = id;
}

/// function to open and close dashboard
const btn_Close = document.querySelector(".fa-x");
btn_Close.addEventListener("click", () => {
    const dashboard = document.querySelector(".dashboard");
    const Btn_Open = document.querySelector(".Cont-btn_Opne");
    Btn_Open.style.display = "inline"
    dashboard.style.display = "none"
})
const btn_Open = document.querySelector(".Cont-btn_Opne");
btn_Open.addEventListener("click", () => {
    const dashboard = document.querySelector(".dashboard");
    const Btn_Open = document.querySelector(".Cont-btn_Opne");
    Btn_Open.style.display = "none"
    dashboard.style.display = "Block"
})


//This code is for the FormImmovable View.
const multiSelectInitializer = () => {

    const multiSelect = new IconicMultiSelect({
        customCss: true,
        select: "#improvement_list"
    });

    multiSelect.subscribe(function (evt) {

        switch (evt.action) {
            case 'ADD_OPTION':
                for (i = 0; i < multiSelect._selectContainer.options.length; i++) {
                    if (multiSelect._selectContainer.options[i].value == evt.value) {
                        multiSelect._selectContainer.options[i].selected = true
                        multiSelect._selectContainer.options[i].setAttribute("selected", "")
                    }
                }
                break;
            case 'REMOVE_OPTION':
                for (i = 0; i < multiSelect._selectContainer.options.length; i++) {
                    if (multiSelect._selectContainer.options[i].value == evt.value) {
                        multiSelect._selectContainer.options[i].selected = false
                        multiSelect._selectContainer.options[i].removeAttribute("selected", "")
                    }
                }
                break;
        }

    });

    multiSelect.init();

}

////This code is for the FormImmovable View.

//const $minRange = document.getElementById("minRange");
//const $minlabel = document.getElementById("minvalue");

//$minRange.addEventListener('change', (e) => {
//    $minlabel.textContent = e.target.value;
//});

//This code is for the Forms in the views.

