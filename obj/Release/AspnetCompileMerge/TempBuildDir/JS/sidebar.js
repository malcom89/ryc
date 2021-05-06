$(document).ready(function () {

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
})