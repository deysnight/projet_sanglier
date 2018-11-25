var button = document.getElementById("login_button");
var cpt_field = 0;
var cpt_login_error = 0;


$.ajax(
    {
        url: "/accalist",
        type: "get",
        success: function (response) {
            var list = JSON.parse(response)
            var acca_list = []
            for (var i = 0; i < list.length; i++) {
                acca_list.push(list[i].name)
            }
            console.log(acca_list)
            autocomplete(document.getElementById("input_acca"), acca_list);
        }
    })

$("#login_button").click(function () {
    var username = $('#login_username').val();
    var pass1 = $('#login_pass1').val();

    
    if (username == "" ||  pass1 == "") {
        if (cpt_field < 1) {
            input = $('<p class="error_fade" id="error_field">Tout les champs doivent être remplis.</p>');
            input.insertAfter('.pass1_div');
            input.hide();
            input.fadeIn(1000);
            cpt_field++;
        }
        return false;
    }
    else {
        $("#error_field").remove();
        cpt_field = 0
    }


    $.ajax(
        {
            url: "/internal/login/" + "login:mdp:acca",
            type: "get",
            async: false,
            success: function(response){
                if (response == "OK LOGIN") {
                    createCookie("login", username, 1000);
                    $("#error_login").remove();
                    cpt_login_error = 0;
                    location.href = final_ip_redirect
                }
                else {
                    if (cpt_login_error < 1) {
                        input = $('<p class="error_fade" id="error_login">Vos données de connexion sont incorrectes.</p>');
                        input.insertAfter('.pass1_div');
                        input.hide();
                        input.fadeIn(1000);
                        cpt_login_error++;
                    }
                }
            }
        })
    });

$("#login_form").submit(function(e) {
    e.preventDefault();
});


function autocomplete(inp, arr) {
    var currentFocus;
    inp.addEventListener("input", function (e) {
        var a, b, i, val = this.value;
        closeAllLists();
        if (!val) { return false; }
        currentFocus = -1;
        a = document.createElement("DIV");
        a.setAttribute("id", this.id + "autocomplete-list");
        a.setAttribute("class", "autocomplete-items");
        this.parentNode.appendChild(a);
        for (i = 0; i < arr.length; i++) {
            if (arr[i].substr(0, val.length).toUpperCase() == val.toUpperCase()) {
                b = document.createElement("DIV");
                b.innerHTML = "<strong>" + arr[i].substr(0, val.length) + "</strong>";
                b.innerHTML += arr[i].substr(val.length);
                b.innerHTML += "<input type='hidden' value='" + arr[i] + "'>";
                b.addEventListener("click", function (e) {
                    inp.value = this.getElementsByTagName("input")[0].value;
                    closeAllLists();
                });
                a.appendChild(b);
            }
        }
    });
    inp.addEventListener("keydown", function (e) {
        var x = document.getElementById(this.id + "autocomplete-list");
        if (x) x = x.getElementsByTagName("div");
        if (e.keyCode == 40) {
            currentFocus++;
            addActive(x);
        } else if (e.keyCode == 38) {
            currentFocus--;
            addActive(x);
        } else if (e.keyCode == 13) {
            e.preventDefault();
            if (currentFocus > -1) {
                if (x) x[currentFocus].click();
            }
        }
    });
    function addActive(x) {
        if (!x) return false;
        removeActive(x);
        if (currentFocus >= x.length) currentFocus = 0;
        if (currentFocus < 0) currentFocus = (x.length - 1);
        x[currentFocus].classList.add("autocomplete-active");
    }
    function removeActive(x) {
        for (var i = 0; i < x.length; i++) {
            x[i].classList.remove("autocomplete-active");
        }
    }
    function closeAllLists(elmnt) {
        var x = document.getElementsByClassName("autocomplete-items");
        for (var i = 0; i < x.length; i++) {
            if (elmnt != x[i] && elmnt != inp) {
                x[i].parentNode.removeChild(x[i]);
            }
        }
    }
    document.addEventListener("click", function (e) {
        closeAllLists(e.target);
    });
}
