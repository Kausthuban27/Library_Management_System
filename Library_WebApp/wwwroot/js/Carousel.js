window.initializeCarousel = () =>
    {
        $('#carouselExampleIndicators').carousel({ interval: 2000 });

    $('#carouselExampleIndicators-prev').click ( 
            () => $('#carouselExampleIndicators').carousel('prev') );
    $('#carouselExampleIndicators-next').click ( 
            () => $('#carouselExampleIndicators').carousel('next') );

}