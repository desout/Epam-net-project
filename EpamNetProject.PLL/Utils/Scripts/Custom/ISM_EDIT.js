function removeSeat(e, linkRemove) {
	e.stopPropagation();
	if(confirm('Are you sure?')){
		$.ajax({
			url: linkRemove,
			method: 'POST',
			data: {id: e.target.dataset.seatId}
		}).done(function (result) {
			if(result){
				e.target.remove();
			}
		})
	}
}

function generateArea(x, y, area) {
	var el = document.createElement("div");
	el.style.position = "absolute";
	el.style.top = y + 'px';
	el.style.left = x + 'px';
	el.style.border = '1px solid #9d9d9d';
	el.className = 'area';
	el.oncontextmenu = function (ev) {
		$('.modal__input-areaId-hidden').val(area.Id);
		$('.modal__input-areaDescription').val(area.Description);
		$('.modal__input-areaPrice').val(area.Price);
		$('.modal').css('display','flex');
	};
	el.onclick = function (ev) {
		$('.modal-seat__input-areaId-hidden').val(area.Id);
		$('.modal-seat__input-number').val('');
		$('.modal-seat__input-row').val('');
		$('.modal-seat').css('display','flex');
	};
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

function generateSeat(row, number, id, status, linkRemove) {
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
	el.oncontextmenu= function (ev) {
		removeSeat(ev, linkRemove)
	};
	el.innerText = number;
	return {seat: el, top: top, left: left};
}

function generateSim(areas, seats,linkRemove, areaProperty) {
	var map = document.getElementById('ISM__MAP');
	areas.forEach(function (t) {
		var x = t.CoordX;
		var y = t.CoordY;
		var description = t.Description;
		var currentSeats = seats.filter(function (a) {
			return a[areaProperty] === t.Id
		});
		var areaElement = generateArea(x, y, t);
		var maxX = 0;
		var maxY = 0;
		var descriptionArea = generateDescription(description);
		areaElement.appendChild(descriptionArea);
		currentSeats.forEach(function (seat) {
			var details = generateSeat(seat.Row, seat.Number, seat.Id, seat.State, linkRemove);
			areaElement.appendChild(details.seat);
			maxX = maxX > details.left ? maxX : details.left;
			maxY = maxY > details.top ? maxY : details.top;
		});
		areaElement.style.width = (maxX + 20) + 'px';
		areaElement.style.height = (maxY + 20) + 'px';
		map.appendChild(areaElement);
	})
}

function generateEventHandlersEdit(props) {
	var tooltip =  $('.tooltip');
	$('[data-id="button-create-area"]').click(function() {
			tooltip.text('select left corner position in MAP');
			tooltip.show();
			$('.tooltip--form').show();
			$('body').on('click','#ISM__MAP',function(event) {
				if (event.which === 1) {
					$('.modal__input-coordX-hidden').val(event.clientX-event.currentTarget.offsetLeft);
					$('.modal__input-coordY-hidden').val(event.clientY-event.currentTarget.offsetTop);
					$('.modal__input-areaId-hidden').val('-1');
					$('.modal__input-areaDescription').val('');
					$('.modal__input-areaPrice').val('');
					$('.modal').css('display','flex');
					$('body').off('click','#ISM__MAP');
					tooltip.hide();
				} else { alert('You have a strange Mouse!'); }

			})
		}
	);

	window.onmousemove = function (e) {
		tooltip[0].style.top = (e.clientY + 20) + 'px';
		tooltip[0].style.left = (e.clientX + 20) + 'px';
	};
	$('#modal-changeAreaPosition').click(function(ev) {
		$('.modal').hide();
		tooltip.text('select left corner position in MAP');
		tooltip.show();
		$('.tooltip--form').show();
		$('body').on('click','#ISM__MAP',function(event) {
			if (event.which === 1) {
				$('.modal').css('display','flex');
				$('.modal__input-coordX-hidden').val(event.clientX-event.currentTarget.offsetLeft);
				$('.modal__input-coordY-hidden').val(event.clientY-event.currentTarget.offsetTop);
				$('body').off('click','#ISM__MAP');
				tooltip.hide();
			} else { alert('You have a strange Mouse!'); }

		})
	});
	$('#modal-changeArea-submit').click(props.changeAreaFunc);
	$('#modal-seat-addSeat-submit').click(props.addSeatFunc);
}
