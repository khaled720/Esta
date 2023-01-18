function deleteComment(id) {
    console.log(id)
    $.ajax({
        type: 'post',
        dataType: 'JSON',
        url: $('#AjaxDelCmnt').val(),
        data: {
            commentId: id
        },
        success: function (data) {
            $("#Comment" + id).remove();
            loadStats();
        },
        error: function (data) {
            console.log(data)
        }
    });
}
function DeleteReply(id) {
    console.log(id)
    $.ajax({
        type: 'post',
        dataType: 'JSON',
        url: $('#AjaxDelRpl').val(),
        data: {
            commentId: id
        },
        success: function (data) {
            $("#Comment" + id).remove();
            loadStats();
        },
        error: function (data) {
            console.log(data)
        }
    });
}