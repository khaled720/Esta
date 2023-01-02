function InitializeCkeEditor(elementName)
{
    if ($('[id$=' + elementName + ']').length > 0) {
        var EditorID = $('[id$=' + elementName + ']')[0].id;
        CKEDITOR.replace(EditorID, {
            extraAllowedContent: 'img [  alt, !src ] { width, height, float, border, border-width, border-style, border-color, padding, margin } ',
            htmlEncodeOutput: true
        });
        var value = $('[id$=' + elementName + ']').val().replace(/\&lt;/g, "<").replace(/\&gt;/g, ">");
        CKEDITOR.instances[EditorID].setData(value);
    }
}


function EncodeCkeditorValue(elementName)
{
    if ($('[id$=' + elementName + ']').length > 0) {
        var EditorID = $('[id$=' + elementName + ']')[0].id;
        var value = CKEDITOR.instances[EditorID].getData();
        value = value.replace(/\</g, "&lt;").replace(/\>/g, "&gt;");
        CKEDITOR.instances[EditorID].setData(value);
    }
}


function InitializeCkeEditorByClass(elementClass) {
    debugger;
    var lstElements = $('.' + elementClass);
    if (lstElements.length > 0) {
        for (var i = 0; i < lstElements.length ; i++) {
            try {
                var EditorID = lstElements[i].id;
                CKEDITOR.replace(EditorID, {
                    extraAllowedContent: 'img [  alt, !src ] { width, height, float, border, border-width, border-style, border-color, padding, margin } ',
                    htmlEncodeOutput: true
                });
                var value = $('[id$=' + EditorID + ']').val().replace(/\&lt;/g, "<").replace(/\&gt;/g, ">");
                CKEDITOR.instances[EditorID].setData(value);
            } catch (e) {

            }
        }

    }
}




function EncodeCkeditorValueByClass(elementClass) {
    debugger;
    var lstElements = $('.' + elementClass);
    if (lstElements.length > 0) {
        try {
            var EditorID = lstElements[i].id;
            var value = CKEDITOR.instances[EditorID].getData();
            value = value.replace(/\</g, "&lt;").replace(/\>/g, "&gt;");
            CKEDITOR.instances[EditorID].setData(value);
        } catch (e) {

        }
    }
}