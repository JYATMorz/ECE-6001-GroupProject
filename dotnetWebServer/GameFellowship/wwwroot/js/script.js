function startCarousel(carouselId) {
    var myCarousel = document.getElementById(carouselId);
    var carousel = new bootstrap.Carousel(myCarousel);
    carousel.cycle();
}

window.createGameAlert = (gameName) => {
    alert(`请先创建${gameName}⏳真的很快的`);
    document.getElementById("postGameName").focus();
}

window.createPostAlert = () => {
    alert(`新建帖子失败⏳重新提交一下吧`);
}

window.createGameAlert = () => {
    alert(`新建游戏失败⏳重新提交一下吧`);
}

window.updatePostAlert = () => {
    alert(`更新帖子失败⏳手动刷新一下吧`);
}

window.userLoginPostConfirm = (loginURL) => {
    if (confirm(`请先登录⏳`)) {
        window.location = loginURL;
    }
}

window.userLoginFailAlert = () => {
    alert(`登录失败⏳请重试`);
}

window.userLogoutFailAlert = () => {
    alert(`登出失败⏳请重试`);
}

window.userRegisterFailAlert = () => {
    alert(`注册失败⏳请重试`);
}