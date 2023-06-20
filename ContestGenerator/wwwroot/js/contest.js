$(document).ready(function () {
    // Плавная прокрутка при нажатии на элементы с классом "smooth-scroll"
    $('a.headerItem').on('click', function (event) {
        if (this.hash !== '') {
            event.preventDefault();

            var hash = this.hash;

            // Анимированная прокрутка до элемента с указанным идентификатором
            $('html, body').animate({
                scrollTop: $(hash).offset().top
            }, 800, function () {
                // Обновление URL с идентификатором
                window.location.hash = hash;
            });
        }
    });
});
