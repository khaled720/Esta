

var ImageChange = function (event) {

    var chosenImage = event.target.files[0];
    var src = window.URL.createObjectURL(chosenImage);
    document.getElementById("photoDiv").style.backgroundImage = "url(" + src + ")";


}


var GetPhoto = function () {
    document.getElementById("upload").click();


}




function Search()
{
    var table = document.getElementById("courses-table").children[1];
var lengthofRecords = table.children.length;
console.log(lengthofRecords)
    debugger;
    var InputElement = document.getElementById("search");

    for (var i = 0; i < lengthofRecords; i++) {

        if (!table.children[i].children[0].innerHTML.toLowerCase().includes(InputElement.value.toLowerCase()))
        {

            table.children[i].style.display = "none";

        }


    }
    if (InputElement.value == "")
    {
        for (var i = 0; i < lengthofRecords; i++) {
       table.children[i].style.display = "table-row";

        }

    }


    //$.ajax({
    //    method: "POST",
    //    datatype:"application/json",
    //    url: "api/CoursesApi/SearchForCourse",
    //    data: { Name: InputElement.value },
    //    success: function (data)
    //    {
    //        var table = document.getElementById("courses-table");
    //        table.children[1].innerHTML = "";
    //        //JsonList = JSON.parse(data);
    //        const JsonList = JSON.parse(data.toString());
            
    //        for (var i of JsonList) {

    //            const node = document.createElement("tr");
    //            const titleNode = document.createElement("td");
    //            const finalgradeNode = document.createElement("td");
    //            const actionsNode = document.createElement("td");
    //            //const editactionsNode = document.createElement("a");
    //            //const detailsactionsNode = document.createElement("a");
    //            //const deleteactionsNode = document.createElement("a");

    //            actionsNode.innerHTML = "<a class='btn btn-primary' href=/Courses/EditCourse/" + i['Id'] + ">Edit</a> | <a class='btn btn-warning' href =/Courses/CourseInfo/" + i['Id'] + " >Details</a>| <a class='btn btn-danger' href =/Courses/DeleteCourse/" + i['Id'] + ">Delete</a> "

           
    //            //actionsNode.appendChild(editactionsNode);
    //            //actionsNode.appendChild(detailsactionsNode);
    //            //actionsNode.appendChild(deleteactionsNode);

    //            titleNode.innerHTML = i["Title"];
    //            finalgradeNode.innerHTML = i["FinalGrade"];
              
    //            node.appendChild(titleNode);
    //            node.appendChild(finalgradeNode);
    //            node.appendChild(actionsNode);
    //              table.children[1].appendChild(node);  
              
    //        }
    //    }
    //    , error: function (err)
    //    {

    //    }


    //});

}











function SearchForCourse() {
    var table = document.getElementById("courses-grid");
    var lengthofRecords = table.children.length;
    var InputElement = document.getElementById("search");

    for (var i = 0; i < lengthofRecords; i++) {

        if (!table.children[i].children[0].innerHTML.toLowerCase()
            .includes(InputElement.value.toLowerCase())) {

            table.children[i].style.display = "none";

        }


    }
    if (InputElement.value == "") {
        for (var card of table.children) {
            card.style.display = "flex";

        }

    }



}