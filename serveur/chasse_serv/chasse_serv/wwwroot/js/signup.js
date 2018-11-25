var button = document.getElementById("signup_button");
var cpt_field = 0;
var cpt_user_len = 0;
var cpt_pass_len = 0;
var cpt_pass_same = 0;

var error_signup_login = 0;
var error_signup_mail = 0;


$("#signup_button").click(function () {
    var username = $('#signup_username').val();
    var email = $('#signup_email').val();
    var pass1 = $('#signup_pass1').val();
    var pass2 = $('#signup_pass2').val();

    if (pass1.length < 8 || pass1 != pass2 || username.length < 6) {
        if (username == "" || email == "" || pass1 == "" || pass2 == "") {
            if (cpt_field < 1) {
                input = $('<p class="error_fade" id="error_field">Tout les champs doivent être remplis.</p>');
                input.insertAfter('.pass2_div');
                input.hide();
                input.fadeIn(1000);
                cpt_field++;
            }
            $("#error_text_same").remove();
            $("#error_text_len").remove();
            $("#error_user_len").remove();
            $("#error_signup_login").remove();
            $("#error_signup_mail").remove();
            cpt_user_len = 0;
            cpt_pass_len = 0;
            cpt_pass_same = 0;
            error_signup_login = 0;
            error_signup_mail = 0;
            return false;
        }
        else {
            $("#error_field").remove();
            cpt_field = 0
        }
        if (username.length < 6) {
            if (cpt_user_len < 1) {
                input = $('<p class="error_fade" id="error_user_len">Votre nom de compte est trop court (6 lettres minimum).</p>');
                input.insertAfter('.pass2_div');
                input.hide();
                input.fadeIn(1000);
                cpt_user_len++;
            }
        }
        else {
            $("#error_user_len").remove();
            cpt_user_len = 0
        }
        if (pass1.length < 8) {
            if (cpt_pass_len < 1) {
                input = $('<p class="error_fade" id="error_text_len">Votre mot de passe est trop court (8 lettres minimum).</p>');
                $(input).insertAfter('.pass2_div');
                input.hide();
                input.fadeIn(1000);
                cpt_pass_len++;
            }
        }
        else {
            $("#error_text_len").remove();
            cpt_pass_len = 0;
        }
        if (pass1 != pass2) {
            if (cpt_pass_same < 1) {
                input = $('<p class="error_fade" id="error_text_same">Les mots de passes ne correspondent pas.</p>');
                $(input).insertAfter('.pass2_div');
                input.hide();
                input.fadeIn(1000);
                cpt_pass_same++;
            }
        }
        else {
            $("#error_text_same").remove();
            cpt_pass_same = 0
        }
        return false;
    }
    else {
        $("#error_text_same").remove();
        $("#error_text_len").remove();
        $("#error_user_len").remove();
        $("#error_field").remove();
    }

    var hash = EncryptPass(pass1);

    var formData = username + ':' + email + ':' + hash;
    var my_url_for_ip = Get_Path_For_IP();
    var final_ip_redirect = my_url_for_ip + "/"
    var final_ip_signup = my_url_for_ip + "/internal/signup/"

   $.ajax(
    {
        url: final_ip_signup + formData,
        type: "get",
        async: false,
            success: function(response){
            if (response == "OK USER REGISTER") {
                createCookie("login", username, 1000);
                $("#error_signup_login").remove();
                error_signup_login = 0;
                $("#error_signup_mail").remove();
                error_signup_mail = 0;
                location.href = final_ip_redirect
            }
            else {
                if (response == "KO LOGIN") {
                    if (error_signup_login < 1) {
                       input = $('<p class="error_fade" id="error_signup_login">Ce nom de compte est déjà utilisé.</p>');
                       input.insertAfter('.pass2_div');
                       input.hide();
                       input.fadeIn(1000);
                       error_signup_login++;
                    }
                }
                else {
                    $("#error_signup_login").remove();
                    error_signup_login = 0
                }    
                if (response == "KO MAIL") {
                    if (error_signup_mail < 1) {
                        input = $('<p class="error_fade" id="error_signup_mail">Cette adresse email est déjà utilisée.</p>');
                        input.insertAfter('.pass2_div');
                        input.hide();
                        input.fadeIn(1000);
                        error_signup_mail++;
                    }
                }
                else {
                    $("#error_signup_mail").remove();
                    error_signup_mail = 0
                }      
            }
        }
    })
});

$("#signup_form").submit(function(e) {
    e.preventDefault();
});