var container;
var clientsArray;

window.onload = function () {
    container = window.document.getElementById("clients");
    clientsArray = container.getElementsByClassName("list-item");
}

function clientSearch() {
    var text = document.getElementById("searchClient").value;
    var rgx = RegExp('^c\\d+$');
    if (rgx.test(text)) {
        var code = text.split("c")[1];
        console.log(code)
        var regex1 = RegExp(code+ '*');
        for (var i = 0; i < clientsArray.length; i++) {
            var data = clientsArray[i].getElementsByTagName("p")[0];
            if (regex1.test(data)) {
                clientsArray[i].style.display = "block";
            }
            else {
                clientsArray[i].style.display = "none";
            }
        }
    }
    else {
        var regex1 = RegExp(text.toLowerCase() + '*');
        var canBe = false;
        for (var i = 0; i < clientsArray.length; i++) {
            var clientDataArray = clientsArray[i].getElementsByTagName("p");
            canBe = false;
            for (var j = 0; j < clientDataArray.length; j++) {
                var data = clientDataArray[j].textContent;
                if (regex1.test(data.toLowerCase())) {
                    canBe = true;
                }
            }
            if (!canBe) {
                clientsArray[i].style.display = "none";
            }
            else {
                clientsArray[i].style.display = "block";
            }
        }
    }

}