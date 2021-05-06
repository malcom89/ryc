
        
$(document).ready(function () {



    $('#sign').submit((e) => {
        e.preventDefault();
        $(".loading").removeClass("d-none");
        let userLogin = String($("#usuario").val());
        let clave = $("#password").val();

        if (validateForm()) {
            $(".loading").addClass("d-none");
            return;
        } else {

            $.ajax( {

                url: "/usuarios/login",
                async: true,
                type: "POST",
                data: {userLogin, clave},
                dataType: "json",
                success: (res) => {
                    
                    
                    if (Number(res.ok) === 200) {

                        $(".loading").addClass("d-none");

                    } else if (Number(res.ok) === 300) {

                        Swal.fire("Oops", String(res.err), "info");
                        $(".loading").addClass("d-none");

                    } else if (Number(res.ok) === 404) {

                        Swal.fire("Oops", "OCURRIO UN ERROR INESPERADO HABLE CON EL ADMINISTRADOR", "info");
                        $(".loading").addClass("d-none");
                    }
                    
                }
                
            })
        }

       
    });


    function validateForm()  {
        let isValid = false;

        let usuario = String($("#usuario").val());
        let password = $("#password").val();

        if (usuario === "") {

            $("#usuario").addClass("border-danger");
            $("#usuarioAlert").removeClass("d-none");

            isValid = true;




        }


        if (password === "") {

            $("#password").addClass("border-danger");
            $("#passwordAlert").removeClass("d-none");


            isValid = true;



        }

        return isValid;
    }

    $("#usuario").click((e) => {
        e.preventDefault();
        $("#usuario").removeClass("border-danger");
        $("#usuarioAlert").addClass("d-none");

    });


   

    $("#password").click((e) => {
        e.preventDefault();
        $("#password").removeClass("border-danger");
        $("#passwordAlert").addClass("d-none");

    });



});