// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


/// function to open and close dashboard
const btn_Close = document.querySelector(".fa-x");
btn_Close.addEventListener("click",()=>
{
    const dashboard = document.querySelector(".dashboard");
    const Btn_Open = document.querySelector(".Cont-btn_Opne");
    Btn_Open.style.display="inline"
    dashboard.style.display="none"
})
const btn_Open = document.querySelector(".Cont-btn_Opne");
btn_Open.addEventListener("click", () => {
    const dashboard = document.querySelector(".dashboard");
    const Btn_Open = document.querySelector(".Cont-btn_Opne");
    Btn_Open.style.display = "none"
    dashboard.style.display = "Block"
})

