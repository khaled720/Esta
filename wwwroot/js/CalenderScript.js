
$(document).ready(function () {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        editable: false,
        selectable: false,
        initialView: 'dayGridMonth',
        height: '400px',
        moreLinkText:"more",
        fixedWeekCount: false,
        dayMaxEvents: true, // allow "more" link when too many events
        events: function (fetchInfo, successCallback, failureCallback) {
            $.ajax({
                url: '/Home/GetEvents',
                type: "GET",
                dataType: "JSON",

                success: function (result) {
                    var events = [];

                    $.each(result, function (i, data) {
                        events.push(
                            {
                                title: data.title,
                                start: moment(data.date).format(),
                                url: '/EventsNews/GetEvent/' + data.id,
                                backgroundColor: "cornflowerblue",
                                borderColor: "black"
                            });
                    });
                    successCallback(events);
                    console.log(events)
                }
            });
        }
    });
    calendar.render();
});
