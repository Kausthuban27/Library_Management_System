window.initializeCarousel = (id) => {
    $('#' + id).carousel();
};

window.nextSlide = (id) => {
    $('#' + id).carousel('next');
};

window.prevSlide = (id) => {
    $('#' + id).carousel('prev');
};