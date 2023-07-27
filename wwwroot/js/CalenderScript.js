
$(document).ready( function () {
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
        locale: document.getElementsByTagName("html")[0]["lang"],
        initialView: 'dayGridMonth',
        height: '450px',
        //eventDidMount: function (info) {
        //    $(info.el).popover({
        //        title: info.event.title,
        //        placement: 'top',
        //        trigger: 'hover',
        //        content: formatDate(info.event.start),
        //        container: 'body'
        //    });
        //},
        moreLinkText: "More",
        
        displayEventTime: false,
        fixedWeekCount: false,
        eventColor: 'white',
        dayMaxEvents: true, // allow "more" link when too many events
        events: function (fetchInfo, successCallback, failureCallback) {
            debugger


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
                                    backgroundColor: "#002147",
                                }
                            );
                        }
                        else {
                            events.push(
                                {
                                    title: data.title,
                                    start: moment(data.date).format(),
                                    url: getEventUrl + '/' + data.id,
                                    backgroundColor: "#002147",
                                }
                            );
                        }
                    });
                    successCallback(events);
                },
                error: function (err) {
                    console.log(err);
                }
            });


        },

    });
    //
    calendar.render();

});









$(document).ready(function () {
    var getEventUrl = $('#GetEventUrl').val();
    var localeSelectorEl = document.getElementById('langddl');
    var calendarEl = document.getElementById('courses-calendar');
   // calendarEl.innerHTML = "<p>Loading Courses Calendar...</p>";
    var dir = 'ltr';

    if (document.getElementsByTagName("html")[0]["lang"] == 'ar') dir = 'rtl';


    var calendar = new FullCalendar.Calendar(calendarEl, {
        editable: false,
        selectable: false,
        locale: document.getElementsByTagName("html")[0]["lang"],
        direction: dir,
        //headerToolbar: {
        //    left: 'prevYear,prev,next,nextYear today',
        //    center: 'title',
        //    right: 'dayGridMonth,dayGridWeek,dayGridDay'
        //},
        initialView: 'dayGridMonth',
        height: '500px',
        dayMaxEvents: true, // allow "more" link when too many events
        events: function (fetchInfo, successCallback, failureCallback) {
            $.ajax({
                url: $('#GetCourseUrl').val(),
                type: "GET",
                dataType: "JSON",
                error: function (err) { console.log("Courses not fetched") },
                success: function (result) {
                    debugger;
                 
                    var events = [];
                  
                    $.each(result, function (i, data) {
                        var title="_";
                        if (document.getElementsByTagName("html")[0]["lang"] == "ar") {
                            title = data.titleAr;
                        } else {
                            title = data.title;
                        }

                        events.push(
                            {
                                title: title,
                                description: data.description,
                                start: moment(data.startDate).format("YYYY-MM-DD"),
                                url: $('#GetCoursedetails').val()+"/"+ data.id,
                                backgroundColor: "#002147",
                                borderColor: "white"

                            });
                    });

                    //events
                    $.ajax({
                        url: $('#AjaxUrl').val(),
                        type: "GET",
                        dataType: "JSON",

                        success: function (result) {
                         

                            $.each(result, function (i, data) {

                                if (data.eventType == 1) {
                                    //esta event
                                    events.push(
                                        {
                                            title: data.title,
                                            start: moment(data.date).format("YYYY-MM-DD"), // remove format and time will be displayed
                                            url: getEventUrl + '/' + data.id,
                                            backgroundColor: "green",
                                            borderColor: "white"
                                        }
                                    );
                                }
                                else {
                                    //social event
                                    events.push(
                                        {
                                            title: data.title,
                                            start: moment(data.date).format("YYYY-MM-DD"),
                                            url: getEventUrl + '/' + data.id,
                                            backgroundColor: "green",
                                            borderColor: "white"
                                        }
                                    );
                                }
                            });
                          successCallback(events);
                        },
                        error: function (err) {
                            console.log(err);
                        }
                    });


                    
                }
            });
        }
    });
    calendar.render();
});