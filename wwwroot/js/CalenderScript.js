
document.addEventListener('DOMContentLoaded', function () {
    var getEventUrl = $('#GetEventUrl').val();
    var localeSelectorEl = document.getElementById('langddl');
    var calendarEl = document.getElementById('calendar');
    var lang = localeSelectorEl.value
    var dir = 'ltr'
    if (lang == 'ar')
        dir = 'rtl'

    var calendar = new FullCalendar.Calendar(calendarEl, {
        editable: false,
        selectable: false,
        direction: dir,
        eventDisplay: 'block',
        locale: lang,
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
        eventColor: 'white',
        dayMaxEvents: true, // allow "more" link when too many events
        events: function (fetchInfo, successCallback, failureCallback) {
            $.ajax({
                url: $('#AjaxUrl').val(),
                type: "GET",
                dataType: "JSON",

                success: function (result) {
                    var events = [];

                    $.each(result, function (i, data) {

                        if (data.eventType == 1) {
                            events.push(
                                {
                                    title: data.title,
                                    start: moment(data.date).format(),
                                    url: getEventUrl + '/'+ data.id,
                                    backgroundColor: "cornflowerblue",
                                }
                            );
                        }
                        else {
                            events.push(
                                {
                                    title: data.title,
                                    start: moment(data.date).format(),
                                    url: getEventUrl + '/' + data.id,
                                    backgroundColor: "darkcyan",
                                }
                            );
                        }
                    });
                    successCallback(events);
                }
            });
        },

    });
    //
    calendar.render();

});


