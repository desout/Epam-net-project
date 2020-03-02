function sendRequest(e, link, linkDeselect) {
	if (e.target.dataset.seatStatus === '0') {
		$.ajax({
			url: link,
			method: "POST",
			data: {id: e.target.dataset.seatId}
		})
			.done(function (data) {
				if (data) {
					e.target.dataset.seatStatus = '1';
					$('.ErrorClass').hide();
				} else {
					$('.ErrorClass').show();
				}
				cookieChecker.checkCookie();
			})
	} else {
		if (e.target.dataset.seatStatus === '1') {
			$.ajax({
				url: linkDeselect,
				method: "POST",
				data: {id: e.target.dataset.seatId}
			})
				.done(function (data) {
					if (data) {
						e.target.dataset.seatStatus = '0';
						$('.ErrorClass').hide();
					} else {
						$('.ErrorClass').show();
					}
					cookieChecker.checkCookie();
				})
		}

	}
}

function generateSim(link, linkDeselect) {
	$(".seat").click(function (ev) {
		var data = $(this).data("seat-status");
		if(data === 1 || data === 0){
			sendRequest(ev, link, linkDeselect)
		} else {
			$('.ErrorClass').show();
		}
	})
}
