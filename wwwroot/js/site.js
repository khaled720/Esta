


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
