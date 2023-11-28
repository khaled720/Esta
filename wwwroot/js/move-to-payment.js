


function checkAgree()
{


    var x = document.getElementById("agree-chkbox").checked;



    switch (x) {
        case true:
            document.getElementById("movePaymentBtn").disabled = false;
            break;

        case false:
            document.getElementById("movePaymentBtn").disabled = true;
            break;

    }


}
