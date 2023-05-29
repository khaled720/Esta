
var CurrentEditor = '';

CKEDITOR.plugins.add('insertImage',
{
    init: function (editor) {
       console.log("Initialization Ck Image...");
        var pluginName = 'insertImage';
        editor.addCommand(pluginName,
            {
            exec: function () {
           //     alert("Insert Image Called222");
                var input = $('#ck_imageUploader');
                var tmppath = '';
                input.files = [];
                input.click();
            }
        });
        editor.ui.addButton('insertImage',
            {
                label: 'Insert Image',
                command: pluginName,
                icon: CKEDITOR.plugins.getPath('insertImage') + 'images/image.png'
            });




    }
});



$('#ck_imageUploader').change(function () {
    debugger;
    var input = $('#ck_imageUploader');
    var fileUploader = input[0];
    if (fileUploader.files && fileUploader.files[0]) {
        var imgName = fileUploader.files[0].name;
      //  var ajaxurl = input.attr('UploadImageUrl');
        var reader = new FileReader();
        var the_url = '';
        var file = fileUploader.files[0];
        
        reader.onload = function (event) {
            var imageURL = event.target.result
            var jsondata = JSON.stringify({ imgurl: imageURL, imageName: imgName });
            var Form_data = new FormData();
            Form_data.append('file', file);
            debugger;
            $.ajax({
                url: $("#GetPostEditorImagesUrl").val(),
                type: 'POST',
                processData: false,
                contentType: false,
                data: Form_data,
           
                
                success: function (result) {
                    var img = CurrentEditor.document.createElement('img');
                    img.setAttribute('src', result);
                    CurrentEditor.insertElement(img);
                    input.val('');
                },
                failure: function (ex) {
                  //  alert(JSON.stringify(ex));
                    console.log("Fail" + JSON.stringify(ex))
                    console.log("Couldn't upload image")
                },
                error: function (ex) {
                    ///alert(JSON.stringify(ex));
                    console.log("Err " + JSON.stringify(ex))
                    console.log("Couldn't upload image")
                }

            });
        }


        reader.readAsDataURL(file);
    }
});


CKEDITOR.on('instanceReady', function (event) {
    event.editor.on('focus', function () {
        CurrentEditor = this;
        var imgArr = $("#cke_" + CurrentEditor.name + " .cke_inner .cke_contents iframe.cke_wysiwyg_frame")[0].contentDocument.images;
        for(var i=0; i<imgArr.length;i++)
        {
            imgArr[i].addEventListener('DOMNodeRemoved', OnNodeRemoved, false);
        }
    });
});


function OnNodeRemoved(event) {
    debugger;
    var img = event.target;
    var imgSrc = JSON.stringify({ ImgPath: img.attributes['src'].nodeValue });
   /// var input = $('#ck_imageUploader');
  ///  var ajaxurl = input.attr('RemoveImageUrl');

    $.ajax({
        url: $("#GetPostEditorImagesUrl").val(),
        type: 'DELETE',
        contentType: "application/json",
        data: { ImgPath: img.attributes['src'].nodeValue },
        success: function () {
         //   alert("Deleted " + imgSrc);
        },
        failure: function (ex) {
         //   alert(JSON.stringify(ex));
        },
        error: function (ex) {
         //   alert(JSON.stringify(ex));
        }
    });
}





