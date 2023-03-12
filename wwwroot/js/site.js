














function SendEmail() {
    debugger;
    var valid = true;//$("#contactform").valid();
    if (valid) {
        // $("#sendEmailbtn").style.disabled = true;
        document.getElementById("sendEmailbtn").disabled = true;
        document.getElementById("sendEmailbtn").innerText = "Sending...";

        var name = document.getElementById("name").value;
        var email = document.getElementById("email").value;
        var subject = document.getElementById("subject").value;
        var message = document.getElementById("message").value;
        var dat = JSON.stringify({ Name: name, Email: email, Subject: subject, Message: message });
        var Email = new Object();
        Email.Name = name;
        Email.Email = email;
        Email.Subject = subject;
        Email.Message = message;

        $.ajax({

            url: '/api/Endpoint',
            type: "POST",
            data: { email: { Name: name, Email: email, Subject: subject, Message: message } },
            dataType: "json",
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
            url: "/Lang/SetCulture",
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
