var alertTimer = setTimeout(function () {
    $('.alert').slideUp('slow');
}, 5000);
$('.close-alert').click(function () {
    clearTimeout(alertTimer);
    $('.alert').slideUp('slow');
});