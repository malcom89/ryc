$(document).ready(function () {

    $('.nav-item > .drop').click(function (e) {
        e.preventDefault();

        
        
        


        $('.nav-item > .uld').addClass('d-none');
            $(this).next('.uld').removeClass('d-none');
        
        
    })
})