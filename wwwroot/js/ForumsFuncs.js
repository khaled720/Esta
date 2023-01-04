function deleteComment(id) {
    console.log(id)
    $.ajax({
        type: 'post',
        dataType: 'JSON',
        url: '/Forums/DeleteComment',
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
        url: '/Forums/DeleteReply',
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