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

function BanUser(user, forum) {
    //console.log(user)
    //console.log(forum)
    userId = user;
    forumId = forum;

    $('#exampleModalToggle').modal('toggle');
}

function BanUserConfirm() {
    if ($('#BanReason').val()) {
        //AjaxBanUser
        $.ajax({
            type: 'get',
            dataType: 'JSON',
            url: $('#AjaxBanUser').val(),
            data: {
                UserId: userId,
                ForumId: forumId,
                reason: $('#BanReason').val()
            },
            success: function (data) {
                $('#exampleModalToggle').modal('toggle');
                location.reload();
            },
            error: function (data) {
                console.log(data)
            }
        });

    }
    else {
        $('#reasonErr').text('Enter valid reason')
    }
}