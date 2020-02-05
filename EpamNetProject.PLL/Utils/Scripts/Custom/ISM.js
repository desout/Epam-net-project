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

function generateArea(x, y) {
	var el = document.createElement("div");
	el.style.position = "absolute";
	el.style.top = y + 'px';
	el.style.left = x + 'px';
	el.style.border = '1px solid #9d9d9d';
	el.className = 'area';
	return el;
}

function generateDescription(description) {
	var el = document.createElement("p");
	el.style.position = "absolute";
	el.style.top = '-20px';
	el.style.left = '20px';
	el.className = 'area--description';
	el.innerText = description;
	return el;
}

function generateSeat(row, number, id, status, link, linkDeselect) {
	var el = document.createElement("p");
	var top = (row - 1) * 20 + 5;
	var left = (number - 1) * 10 + 5;
	el.style.margin = '0';
	el.style.top = top + 'px';
	el.style.left = left + 'px';
	el.dataset.seatId = id;
	el.dataset.seatStatus = status;
	el.className = 'areaDescription';
	el.classList.add('seat');
	if (status === 0 || status === 1) {
		el.onclick = function (ev) {
			sendRequest(ev, link, linkDeselect)
		};
	} else {
		el.onclick = function (ev) {
			$('.ErrorClass').show();
		};
	}
	el.innerText = number;
	return {seat: el, top: top, left: left};
}

function generateSim(areas, seats, link, linkDeselect) {
	var map = document.getElementById('ISM__MAP');
	areas.forEach(function (t) {
		var x = t.CoordX;
		var y = t.CoordY;
		var description = t.Description;
		var currentSeats = seats.filter(function (a) {
			return a.EventAreaId === t.Id
		});
		var areaElement = generateArea(x, y);
		var maxX = 0;
		var maxY = 0;
		var descriptionArea = generateDescription(description);
		areaElement.appendChild(descriptionArea);
		currentSeats.forEach(function (seat) {
			var details = generateSeat(seat.Row, seat.Number, seat.Id, seat.State, link, linkDeselect);
			areaElement.appendChild(details.seat);
			maxX = maxX > details.left ? maxX : details.left;
			maxY = maxY > details.top ? maxY : details.top;
		});
		areaElement.style.width = (maxX + 20) + 'px';
		areaElement.style.height = (maxY + 20) + 'px';
		map.appendChild(areaElement);
	})
}
