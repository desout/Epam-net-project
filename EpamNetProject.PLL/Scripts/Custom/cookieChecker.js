function cookieCheckerInit(reserveTimeDefault) {
    var interval = null;
    var reserveTime = reserveTimeDefault;

    function checkCookie() {
        var reserveDate = document.cookie.split(/reserveDate=/)[1];
        if (reserveDate) {
            var el = $('.navbar__basket--time');
            var now = new Date();
            var delay = now - new Date(parseInt(reserveDate));
            var diffMins = ((delay % 86400000) % 3600000) / 60000;
            var timeMins = reserveTime - diffMins;
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
