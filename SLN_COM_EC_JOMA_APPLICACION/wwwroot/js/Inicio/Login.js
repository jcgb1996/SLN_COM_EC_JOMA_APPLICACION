let URL_BASE = ""
let CONTROLERNAME = ""

var Login = {
    Init: function () {

        $("#txtUser").keyup(function (event) {
            if (event.keyCode === KEY_ENTER) {
                $("#txtPassword").focus();
            }
        });
        $("#txtPassword").keyup(function (event) {
            if (event.keyCode === KEY_ENTER) {
                $("#BtnLogin").click();
            }
        });

        $("#BtnLogin").click(function (e) {
            e.preventDefault();
            Login.RealizarLogin();
        });
    },

    RealizarLogin: function () {

        if (!Site.ValidarForumarioById("FrmLogin"))
            return;


        var txtCompania = $.trim($("#txtCompania").val());
        var txtUser = $.trim($("#txtUser").val());
        var txtPassword = $.trim($("#txtPassword").val());


        if (txtUser == "") { Site.mostrarNotificacion("Ingrese su usuario", 2, 500); $("#txtUser").focus(); return; }
        if (txtPassword == "") { Site.mostrarNotificacion("Ingrese su contraseña", 2, 500); $("#txtPassword").focus(); return; }
        if (txtCompania == "") { Site.mostrarNotificacion("Ingrese la identificación de la compañía", 2, 500); $("#txtRnc").focus(); return; }


        Site.IniciarLoading();


        var loginData = {
            Usuario: txtUser,
            Clave: txtPassword,
            Compania: txtCompania
        };

        $.ajax({
            type: 'POST',
            url: Site.createUrl(URL_BASE, CONTROLERNAME, "/RealizarLogin"),
            data: JSON.stringify(loginData),
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                if (result == 'ForzarCambioClave') {
                    Site.CerrarLoading();
                    Login.OpenModalRecuperarContrasenia(result, true);
                    return;
                }
                window.location.href = result;
            },
            error: function (result) {
                if (result.status == 500)
                    Site.mostrarNotificacion(result.responseText, 2);
            },
            complete: function () {
                Site.CerrarLoading();
            }
        });
    },

    OpenModalRecuperarContrasenia: function (ForzarCambioClave = false) {
        if (ForzarCambioClave) {
            $("#TitleOlvidasteContrasenia").addClass('d-none');
            $("#ForzarCambioContrasenia").removeClass('d-none');
            $("#ContenteReestablecerContrasenia").addClass('d-none');
            $("#ContenteForzarCambiosDeClave").removeClass('d-none');
        } else {

            $("#TitleOlvidasteContrasenia").removeClass('d-none');
            $("#ForzarCambioContrasenia").addClass('d-none');
            $("#ContenteReestablecerContrasenia").removeClass('d-none');
            $("#ContenteForzarCambiosDeClave").addClass('d-none');
        }

        $('#forgotPasswordModal').modal('show');
    },

    RecuperarContrasenia: function myfunction() {
        if (!Site.ValidarForumarioById("FrmRecuperacion"))
            return;

        $('#forgotPasswordModal').modal('hide');
        Site.mostrarNotificacion("Correo enviado exitosamente", 1, 5000);
    },


}