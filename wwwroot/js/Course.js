


function Search()
{
    debugger;
   var InputElement= document.getElementById("search");

    $.ajax({
        method: "POST",
        datatype:"application/json",
        url: "Courses/SearchForCourse",
        data: { Name: InputElement.value },
        success: function (data)
        {
            var table = document.getElementById("courses-table");
            table.children[1].innerHTML = "";
            //JsonList = JSON.parse(data);
            const JsonList = JSON.parse(data.toString());
            
            for (var i of JsonList) {

                const node = document.createElement("tr");
                const titleNode = document.createElement("td");
                const finalgradeNode = document.createElement("td");
                const actionsNode = document.createElement("td");
                const editactionsNode = document.createElement("a");
                const detailsactionsNode = document.createElement("a");
                const deleteactionsNode = document.createElement("a");

                editactionsNode.innerHTML = " <a class='btn btn-primary' href='/Courses/EditCourse/" + i['Id'] + ">Edit</a> |";
                detailsactionsNode.innerHTML = " <a class='btn btn-primary' href='/Courses/CourseInfo/" + i['Id'] + ">Details</a> |";
                deleteactionsNode.innerHTML = " <a class='btn btn-primary' href='/Courses/DeleteCourse/" + i['Id'] + ">Delete</a> |";

                actionsNode.appendChild(editactionsNode);
                actionsNode.appendChild(detailsactionsNode);
                actionsNode.appendChild(deleteactionsNode);

                titleNode.innerHTML = i["Title"];
                finalgradeNode.innerHTML = i["FinalGrade"];
              
                node.appendChild(titleNode);
                node.appendChild(finalgradeNode);
                node.appendChild(actionsNode);
                  table.children[1].appendChild(node);  
              
            }
        }
        , error: function (err)
        {





        }


    });

}