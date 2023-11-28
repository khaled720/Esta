



//document.onload = function () {

//  var tables=  document.getElementsByTagName("table");

//    for (var i = 0; i < tables.length; i++)
//    {

//        tables
//        [i].classList.add("animate__flipInX");

//    }
//}

//document.getElementsBy("img").onclick = function () {

//    document.body.innerHTML += @"<div style='position: absolute; top: 5px; bottom: 5px; left: 5px; right: 5px; background-color: aqua; border-radius: 50px'> < img src = 'https://eg.jumia.is/unsafe/fit-in/300x300/filters:fill(white)/product/06/095712/1.jpg?3534' /> </div > ";


//};








function SendEmail() {
    debugger;
    var valid = $("#contactform").valid();
    if (valid) {
        // $("#sendEmailbtn").style.disabled = true;
        document.getElementById("sendEmailbtn").disabled = true;
        document.getElementById("sendEmailbtn").innerText = "Sending...";

        var name = document.getElementById("contactname").value;
        var email = document.getElementById("contactemail").value;
        var subject = document.getElementById("contactsubject").value;
        var message = document.getElementById("contactmessage").value;
        var dat = JSON.stringify({ Name: name, Email: email, Subject: subject, Message: message });
        var obj = new Object();
        obj.Name = name;
        obj.Email = email;
        obj.Subject = subject;
        obj.Message = message;

        $.ajax({

            url: '/api/Email',
            type: "POST",
            data: obj,
       //     dataType: "json",
            contentType: 'application/json',
            success: function (result) {

                console.log("MAIL Success ", result);
            },
            error: function (result) {

                console.log("MAIL ERROR ", result);
            }


            

        });

    }
  

};





    function changeLang(lang) {
        $.ajax({
            type: 'post',
            url: $('#langUrl').val(),
            data: {
                culture: lang
            },
            success: function (data) {
                if (data)
                    window.location.reload();
            },
            error: function (data) {
                console.log(data)
            }
        });
        }
    function padTo2Digits(num) {
            return num.toString().padStart(2, '0');
        }
    function copyToClipboard(id) {
            var base_url = window.location.origin;
    var cmntLink = $('#CopyToClip' + id).val();

    var $temp = $("<input>");
        $("body").append($temp);

        $temp.val(base_url + cmntLink).select();
        document.execCommand("copy");
        $temp.remove();

        alert("Copied the Link");
        }
        function formatDate(date) {
            var newDate = new Date(date);
        var fullDate = [
        newDate.getFullYear(),
        padTo2Digits(newDate.getMonth() + 1),
        padTo2Digits(newDate.getDate()),
        ].join('/') +
        ' ' +
        [
        padTo2Digits(newDate.getHours() % 12 || 12),
        padTo2Digits(newDate.getMinutes()),
        padTo2Digits(newDate.getSeconds()),
        ].join(':');
            if (newDate.getHours() > 12) {

            fullDate += ' ' + 'PM'
        }
        else {
            fullDate += ' ' + 'AM'
        }
        return fullDate;
        }
