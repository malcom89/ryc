$(document).ready(function () {

    var time1;
    $('.nav-item > .drop').click(function (e) {
        e.preventDefault();

        
        
      
        if (!$(this).next('.uld').hasClass('d-none')) {


        $('.nav-item > .uld').addClass('d-none');




        } else {

        $('.nav-item > .uld').addClass('d-none');
            $(this).next('.uld').removeClass('d-none');
        }

        
        
    })


    $('#menu').click(function (e) {
        e.preventDefault();

        if ($('#sideba').hasClass('activ')) {

            $('#sideba').removeClass('activ');
            $('#content').removeClass('col-md-12');
        } else {

            $('#sideba').addClass('activ');
            $('#content').addClass('col-md-12');
        }
    })



    $(document).click((e) => {
        
        var $target = $(e.target);


        if (!$target.closest('#log').length &&
            !$target.closest('.loginPerfil').length &&
            

            $('#log').is(":visible") &&
            $('.loginPerfil').is(":visible") 
            

        ) {
            // $('#acceder').slideUp('fast');
            $('#cajaUser').css("right", "-700px");
            clearTimeout(time1);
        } else {

        }

    })

    $(".loginPerfil").click((e) => {


        e.preventDefault();
        console.log('funciona')

        time1 = setTimeout(() => {

            $("#cajaUser").css("right", "0px");

        }, 20);


    });
})