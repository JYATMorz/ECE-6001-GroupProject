function startCarousel(carouselId) {
    var myCarousel = document.getElementById(carouselId);
    var carousel = new bootstrap.Carousel(myCarousel);
    carousel.cycle();
}

window.createGameAlert = (gameName) => {
    alert(`请先创建${gameName}⏳真的很快的`);
    document.getElementById("postGameName").focus();
}

window.userLoginAlert = () => {
    alert(`请先登录⏳真的很快的`);
}