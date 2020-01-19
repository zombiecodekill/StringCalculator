// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function compute() {
    var result = document.getElementById('clientSideResult');
    result.innerHTML = "NEW VALUE";
}

function computeClicked() {
    var input = document.getElementById('StringCalculatorInput').value;

    var computeBtn = document.getElementById('Compute');

    var url = computeBtn.getAttribute("href")  + "Calculator/Index/" + input;

    location.href = url;

}

(function() {
    document.getElementById('Compute').click(computeClicked());
});

