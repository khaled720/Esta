/*!
FullCalendar Core v6.0.1
Docs & License: https://fullcalendar.io
(c) 2022 Adam Shaw
*/
(function (index_js) {
    'use strict';

    var locale = {
        code: 'en',
        week: {
            dow: 6,
            doy: 12, // The week that contains Jan 1st is the first week of the year.
        },
        direction: 'ltr',
        buttonText: {
            prev: 'Prev',
            next: 'Next',
            today: 'Today',
            month: 'Month',
            week: 'Week',
            day: 'Day',
            list: 'List',
        },
        weekText: 'Week',
        allDayText: ' All day',
        moreLinkText: 'More',
        noEventsText: 'No Events',
    };

    index_js.globalLocales.push(locale);

})(FullCalendar);
