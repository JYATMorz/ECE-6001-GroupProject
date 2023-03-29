function startCarousel(carouselId) {
    var myCarousel = document.getElementById(carouselId);
    var carousel = new bootstrap.Carousel(myCarousel);
    carousel.cycle();
}

window.createGameAlert = (gameName) => {
    alert(`请先创建${gameName}`);
}