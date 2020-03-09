function cookieCheckerInit(basketLeaveTimeDefault) {
    var interval = null;
    var basketLeaveTime = basketLeaveTimeDefault;

    function checkCookie() {
        var basketTime = document.cookie.split(/basketTime=/)[1];
        if (basketTime) {
            var el = $('.navbar__basket--time');
            var now = new Date();
            var delay = now - new Date(parseInt(basketTime));
            var diffMins = ((delay % 86400000) % 3600000) / 60000;
            var timeMins = basketLeaveTime - diffMins;
            var timeSec = Math.floor((timeMins - Math.floor(timeMins)) * 60);
            timeMins = Math.floor(timeMins);
            el.text(timeMins + ':' + timeSec);
            $('.navbar__basket').show();
            if (!interval) {
                interval = setInterval(function () {
                    var el = $('.navbar__basket--time');
                    var content = el.text().split(':');
                    var timeMins = parseInt(content[0]);
                    var timeSec = parseInt(content[1]);
                    if (timeMins === 0) {
                        el.text('');
                        $('.navbar__basket').hide();
                    } else {
                        if (timeSec === 0) {
                            timeMins--;
                            timeSec = 60;
                        } else {
                            timeSec--;
                        }
                        el.text(timeMins + ':' + timeSec);
                    }

                }, 1000);
            }
        } else {
            clearInterval(interval);
            $('.navbar__basket--time').text('');
            $('.navbar__basket').hide();
        }
    }

    return {checkCookie: checkCookie}
}
