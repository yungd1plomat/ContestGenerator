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

    const carousel = $('.carousel');
    const numItems = $('.carousel-item').length;
    let visibleItems = 3; // Default number of visible items
    const windowWidth = window.innerWidth;

    if (windowWidth < 640) { // Adjust the breakpoint as needed
        visibleItems = 1; // On mobile devices, display one item at a time
    }

    const itemWidth = visibleItems === 1 ? carousel.outerWidth() : carousel.outerWidth() / visibleItems;
    const carouselWidth = itemWidth * numItems;
    let currentPosition = 0;

    $('.carousel-container').width(itemWidth * visibleItems);

    $('.carousel').width(carouselWidth);

    $('.next-button').click(function () {
        if (currentPosition > -(carouselWidth - (itemWidth * visibleItems))) {
            currentPosition -= itemWidth * visibleItems;
            carousel.css('transform', 'translateX(' + currentPosition + 'px)');
        }
    });

    $('.prev-button').click(function () {
        if (currentPosition < 0) {
            currentPosition += itemWidth * visibleItems;
            carousel.css('transform', 'translateX(' + currentPosition + 'px)');
        }
    });
});