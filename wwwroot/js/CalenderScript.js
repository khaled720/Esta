
document.addEventListener('DOMContentLoaded', function () {
    var localeSelectorEl = document.getElementById('langddl');
    var calendarEl = document.getElementById('calendar');
    var lang = localeSelectorEl.value

    var calendar = new FullCalendar.Calendar(calendarEl, {
        editable: false,
        selectable: false,
        locale: lang, // the initial locale
        initialView: 'dayGridMonth',
        height: '450px',
        eventDidMount: function (info) {
            $(info.el).popover({
                title: info.event.title,
                placement: 'top',
                trigger: 'hover',
                content: formatDate(info.event.start),
                container: 'body'
            });
        },
        moreLinkText: "More",
        displayEventTime: false,
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
                                //backgroundColor: "cornflowerblue",
                                //borderColor: "black",
                            });
                    });
                    successCallback(events);
                    console.log(events)
                }
            });
        }
    });
    //
    calendar.render();

});


