
$(document).ready(function () {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        editable: false,
        selectable: false,
        initialView: 'dayGridMonth',
        height: '600px',
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
                                description: data.desc,
                                start: moment(data.date).format("YYYY-MM-DD"),
                                url: '/EventsNews/GetEvent/' + data.id,
                                backgroundColor: "cornflowerblue",
                                borderColor: "black"
                            });
                    });
                    successCallback(events);
                }
            });
        }
    });
    calendar.render();
});






$(document).ready(function () {
    var calendarEl = document.getElementById('courses-calendar');
   // calendarEl.innerHTML = "<p>Loading Courses Calendar...</p>";
    var calendar = new FullCalendar.Calendar(calendarEl, {
        editable: false,
        selectable: false,
        initialView: 'dayGridMonth',
        height: '600px',
        dayMaxEvents: true, // allow "more" link when too many events
        events: function (fetchInfo, successCallback, failureCallback) {
            $.ajax({
                url: '/api/Endpoint',
                type: "GET",
                dataType: "JSON",

                success: function (result) {
                    debugger;
                 
                    var events = [];
                  
                    $.each(result, function (i, data) {
                        
                        events.push(
                            {
                                title: data.title,
                                description: data.description,
                                start: moment(data.startDate).format("YYYY-MM-DD"),
                                url: '/Courses/CourseDetails/' + data.id,
                                backgroundColor: "green",
                                borderColor: "black"

                            });
                    });
                    successCallback(events);
                }
            });
        }
    });
    calendar.render();
});