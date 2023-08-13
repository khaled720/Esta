//Registration Form JS
document.getElementById("old").onclick = () =>
{  if (document.getElementById("old").checked == true)
    {
    document.getElementById("memnumgroup").style.display = 'block';
    document.getElementById("memnum").setAttribute("required", true);
}
}
document.getElementById("new").onclick = () => {
    if (document.getElementById("new").checked == true) {

        document.getElementById("memnum").setAttribute("required", false);
        document.getElementById("memnumgroup").style.display = 'none'
    }
}



//Image validation if provided national id then you have to add national id image if provided passport id then must add img if nor porvided anything idno required
 /*
document.getElementById("idno").onblur = () =>
{
    var value = document.getElementById("idno").value;
    if (value != "" && value != null) {
        document.getElementById("idnoImg").setAttribute("required",true);
        document.getElementById("passportImg").removeAttribute("required");
        document.getElementById("passportImg-error").remove();
    } else {
        document.getElementById("idnoImg").setAttribute("required", true);
        document.getElementById("passportImg").removeAttribute("required");
    }
}
document.getElementById("passport").onblur = () => {
    var value = document.getElementById("passport").value;
    if (value != "" && value != null) {
        document.getElementById("passportImg").setAttribute("required", true);
        document.getElementById("idno").removeAttribute("required");
        document.getElementById("idnoImg").removeAttribute("required");
        document.getElementById("idnoImg-error").remove();

    } else {
        document.getElementById("idnoImg").setAttribute("required", true);
        document.getElementById("passportImg").removeAttribute("required");
    }

}



document.onreadystatechange = () => {
    if (document.readyState.toString() == "complete") {
        var idno = document.getElementById("idno").value;
        var pass = document.getElementById("passport").value;
        if (idno != "" && idno != null) { //idno provided
            document.getElementById("idnoImg").setAttribute("required", true);
            document.getElementById("passportImg").removeAttribute("required");
            document.getElementById("passportImg-error").remove();
        } else {
            if (pass != "" && pass != null) {
                document.getElementById("passportImg").setAttribute("required", true);

            } else {
                document.getElementById("idnoImg").setAttribute("required", true);
                document.getElementById("passportImg").removeAttribute("required");
            }
        }


    }


};
*/

var currentTab = 0; // Current tab is set to be the first tab (0)
showTab(currentTab); // Display the current tab

function showTab(n) {
    // This function will display the specified tab of the form ...
    var x = document.getElementsByClassName("tab");

    if (n <= 4 ) {
        x[n].style.display = "block";
    }
    else {
        document.getElementById("spinner").style.display = "block";
        document.getElementById("nextBtn").attributes.add("disabled");
    }


    if (n == 0) {

        document.getElementById("prevBtn").style.display = "none";

    } else {

        document.getElementById("prevBtn").style.display = "inline";
    }


    if (n == (x.length - 1)) {

        document.getElementById("nextBtn").innerHTML = "Submit";

    } else {
        document.getElementById("nextBtn").innerHTML = "Next";
    }


    // ... and run a function that displays the correct step indicator:
    fixStepIndicator(n)
}
//1
function nextPrev(n) {


    if (currentTab < 5) {
        // This function will figure out which tab to display
        var x = document.getElementsByClassName("tab");// 5

        if (currentTab == 1) {
            var isSameasAddress = document.getElementById("msg-addres-check").checked;
            if (isSameasAddress) {

                var city = document.getElementById("City").value;
                var area = document.getElementById("Area").value;
                var hometown = document.getElementById("Hometown").value;
                var streetName = document.getElementById("StreetName").value;
                var blockNumber = document.getElementById("BlockNumber").value;


                if (city && area && hometown && streetName && blockNumber) {

                    document.getElementById("MessagingAddress").value = blockNumber + " " + streetName + " " + hometown + " , " + area + " , " + city


                }



            }
            
        }


        // Exit the function if any field in the current tab is invalid:
        if (n == 1 && !validateForm()) return false;

        // Hide the current tab:
        x[currentTab].style.display = "none";

    
        // Increase or decrease the current tab by 1:
        currentTab = currentTab + n;
        // if you have reached the end of the form... :
        // tab index is zero-based 
        //length should be sub by 1
        if (currentTab >= x.length) {
            //...the form gets submitted:
            document.getElementById("spinner").style.display = "block";
          //  document.getElementById("nextBtn").attributes.add("disabled");
            document.getElementById("regForm").submit();
            document.getElementById("nextBtn").display = "none";
            return false;
        } else {
            if (document.getElementById("nextBtn").display == "none") {
                document.getElementById("nextBtn").display = "block";
            }
            // Otherwise, display the correct tab:
            showTab(currentTab);
        }
    }
    else {
        window.alert("You Have Submitted Form already");
    }

}

function validateForm() {

   var s=$("#regForm").valid();
    // This function deals with validation of the form fields
    var x, y, i, valid = true;

   valid = s;
    //x = document.getElementsByClassName("tab");
    //y = x[currentTab].getElementsByTagName("input");
    //// A loop that checks every input field in the current tab:
    //for (i = 0; i < y.length; i++) {
    //    // If a field is empty...
    //    if (y[i].value == "") {
    //        // add an "invalid" class to the field:
    //        y[i].className += " invalid";
    //        // and set the current valid status to false:
    //        valid = false;
    //    }
    //}
    //// If the valid status is true, mark the step as finished and valid:
    //if (valid) {
    //    document.getElementsByClassName("step")[currentTab].className += " finish";
    //}
    return valid; // return the valid status
}

function fixStepIndicator(n) {
    // This function removes the "active" class of all steps...
    var i, x = document.getElementsByClassName("step");
    for (i = 0; i < x.length; i++) {
        x[i].className = x[i].className.replace(" active", "");
    }
    //... and adds the "active" class to the current step:
    x[n].className += " active";
}


////////////////////////////////
function Check(ans, Id)
{
 
    if (ans) {
   
        document.getElementById("t-" + Id).classList.remove("d-none");
       // $("#t-"+Id).prop('required', true);

    } else
    {

        document.getElementById("t-" + Id).classList.add("d-none");
    //    $("#t-" + Id).prop('required', false);
    }

}




function previewImage(event){


    console.log(event);

    var files = event.target.files;
        document.getElementById("img-preview").innerHTML = "";
    for (var i = 0; i < files.length; i++) {

      var x=  URL.createObjectURL(files[i])

        document.getElementById("img-preview").innerHTML += "<img src=" + x + " style='    max-width: 200px; max-height: 200px;object-fit: cover; '/>";

    }


}


checkNationality();

function checkNationality() {

    document.getElementById("img-preview").innerHTML = "";
    var x = document.getElementById("nationalty-select").value;
    console.log(x);

    if (x == "Egypt") {
        document.getElementById("idCard-sec").style.display = "block";
        document.getElementById("passport-sec").style.display = "none";
        // id staff required
        document.getElementById("idnoImg").setAttribute("required", true);
        document.getElementById("idno").setAttribute("required", true);

        document.getElementById("passport").removeAttribute("required");

        document.getElementById("passportImg").removeAttribute("required");
        document.getElementById("passportImg-error").remove();

    } else {
        document.getElementById("idCard-sec").style.display = "none";
        document.getElementById("passport-sec").style.display = "block";
        // passport staff required
        document.getElementById("passport").setAttribute("required", true);
        document.getElementById("passportImg").setAttribute("required", true);
        document.getElementById("idno").removeAttribute("required");

        document.getElementById("idnoImg").removeAttribute("required");
        document.getElementById("idnoImg-error").remove();


    }


}