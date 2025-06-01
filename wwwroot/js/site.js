



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



var targetWord = 'ESTA';
var regex = new RegExp(`\\b(${targetWord})\\b`, 'gi');

function boldInHeaderAndParagraphs() {
    const elements = document.querySelectorAll('h1, h2, h3, h4, h5, h6, p');

    elements.forEach((el) => {
        el.childNodes.forEach((node) => {
            if (node.nodeType === Node.TEXT_NODE) {
                const span = document.createElement('span');
                span.innerHTML = node.textContent.replace(regex, '<strong>$1</strong>');
                node.replaceWith(...span.childNodes);
            }
        });
    });
}

boldInHeaderAndParagraphs();



function showPass(id) {
    var x = document.getElementById(id);
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}

function SendEmail() {
    debugger;
    var valid = $("#contactform").valid();
    if (Validation()) {
        // $("#sendEmailbtn").style.disabled = true;
        document.getElementById("sendEmailbtn").disabled = true;
        document.getElementById("sendEmailbtn").innerText = "Sending...";

        var name = document.getElementById("contactname").value;
        var email = document.getElementById("contactemail").value;
        var subject = document.getElementById("contactsubject").value;
        var message = document.getElementById("contactmessage").value;
        var dat = JSON.stringify({ Name: name, Email: email, Subject: subject, Message: message });
        //var obj = new Object();
        //obj.Name = name;
        //obj.Email = email;
        //obj.Subject = subject;
        //obj.Message = message;
        var contactEmail = new FormData();
        contactEmail.append("Name", name)
        contactEmail.append("Email", email)
        contactEmail.append("Subject", subject)
        contactEmail.append("Message", message)

        $.ajax({

            url: '/Home/Contact',
            type: "POST",
            processData: false,
            contentType: false,
            data: contactEmail,
            //     dataType: "json",
            success: function (result) {

                console.log("MAIL Success ", result);
                $('#ErrorMsg span').text("Your Message has been Sent Successfully")

                $('#ErrorMsg').removeClass("alert-danger").addClass("alert-success")
                $('#ErrorMsg').css("display", "block")
                clear()

            },
            error: function (result) {

                console.log("MAIL ERROR ", result);
                clear()
                $('#ErrorMsg span').text("Something Went Wrong!")
                $('#ErrorMsg').addClass("alert-danger")
                $('#ErrorMsg').css("display", "block")
            }




        });

    }


};

function Validation() {
    var response = grecaptcha.getResponse();
    if (response) {
        if ($('#contactname').val() && $('#contactemail').val() && $('#contactsubject').val() && $('#contactmessage').val()) {
            return true
        }
        else {
            $('#ErrorMsg span').text("Complete missing data")
            $('#ErrorMsg').addClass("alert-danger")
            $('#ErrorMsg').css("display", "block")

            return false
        }
    }
    else {

        $('#ErrorMsg span').text("Are You A Robot?")
        $('#ErrorMsg').addClass("alert-danger")
        $('#ErrorMsg').css("display", "block")

        return false
    }
}
function clear() {
    grecaptcha.reset();
    document.getElementById("sendEmailbtn").disabled = false;
    document.getElementById("sendEmailbtn").innerText = "Send";

    document.getElementById("contactname").value = "";
    document.getElementById("contactemail").value = "";
    document.getElementById("contactsubject").value = "";
    document.getElementById("contactmessage").value = "";
}


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
