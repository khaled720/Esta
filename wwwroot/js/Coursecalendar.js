document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('Coursecalendar');
    var localeSelectorEl = document.getElementById('langddl');
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
        moreLinkText: "More",
        displayEventTime: false,
        fixedWeekCount: false,
        eventColor: 'green',
        eventDidMount: function (info) {
            $(info.el).popover({
                title: info.event.title,
                placement: 'top',
                trigger: 'hover',
                content: formatDate(info.event.start),
                container: 'body'
            });
        },
        dayMaxEvents: true,
        events: [
            {
                title: 'All Day Event',
                start: '2023-01-01',
            },
            {
                title: 'Long Event',
                start: '2023-01-07',
            },
            {
                title: 'Repeating Event',
                start: '2023-01-09T16:00:00'
            },
            {
                title: 'Repeating Event',
                start: '2023-01-16T16:00:00',
            },
            {
                title: 'Conference',
                start: '2023-01-22',
            },
            {
                title: 'Meeting',
                start: '2023-01-12T10:30:00',
            },
            {
                title: 'Lunch',
                start: '2023-01-12T12:00:00'
            },
            {
                title: 'Meeting',
                start: '2023-01-12T14:30:00'
            },
            {
                title: 'Happy Hour',
                start: '2023-01-12T17:30:00'
            },
            {
                title: 'Dinner',
                start: '2023-01-19T20:00:00'
            },
            {
                title: 'Birthday Party',
                start: '2023-01-13T07:00:00'
            },
            {
                title: 'Event',
                start: '2023-01-28'
            }
        ]
    });

    calendar.render();
});