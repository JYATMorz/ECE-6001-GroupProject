function startCarousel(carouselId) {
    var myCarousel = document.getElementById(carouselId);
    var carousel = new bootstrap.Carousel(myCarousel);
    carousel.cycle();
}

window.createGameAlert = (gameName) => {
    alert(`请先创建${gameName}⏳真的很快的`);
    document.getElementById("postGameName").focus();
}

window.userLoginPostConfirm = (loginURL) => {
    if (confirm(`请先登录⏳再发帖`)) {
        window.location = loginURL;
    }
}

window.userLoginFailAlert = () => {
    alert(`登录失败⏳请重试`);
}

window.userLogoutFailAlert = () => {
    alert(`登出失败⏳请重试`);
}