
//non modificate da tema
$('.input-search').on('keyup', function () {
    var rex = new RegExp($(this).val(), 'i');
    $('.todo-box .todo-item').hide();
    $('.todo-box .todo-item').filter(function () {
        return rex.test($(this).text());
    }).show();
});

const taskViewScroll = new PerfectScrollbar('.task-text', {
    wheelSpeed: .5,
    swipeEasing: !0,
    minScrollbarLength: 40,
    maxScrollbarLength: 300,
    suppressScrollX: true
});

//function dynamicBadgeNotification(setTodoCategoryCount) {
//    var todoCategoryCount = setTodoCategoryCount;

//    // Get Parents Div(s)
//    var get_ParentsDiv = $('.todo-item');
//    var get_TodoAllListParentsDiv = $('.todo-item.all-list');
//    var get_TodoCompletedListParentsDiv = $('.todo-item.todo-task-done');
//    var get_TodoImportantListParentsDiv = $('.todo-item.todo-task-important');

//    // Get Parents Div(s) Counts
//    var get_TodoListElementsCount = get_TodoAllListParentsDiv.length;
//    var get_CompletedTaskElementsCount = get_TodoCompletedListParentsDiv.length;
//    var get_ImportantTaskElementsCount = get_TodoImportantListParentsDiv.length;

//    // Get Badge Div(s)
//    var getBadgeTodoAllListDiv = $('#all-list .todo-badge');
//    var getBadgeCompletedTaskListDiv = $('#todo-task-done .todo-badge');
//    var getBadgeImportantTaskListDiv = $('#todo-task-important .todo-badge');


//    if (todoCategoryCount === 'allList') {
//        if (get_TodoListElementsCount === 0) {
//            getBadgeTodoAllListDiv.text('');
//            return;
//        }
//        if (get_TodoListElementsCount > 9) {
//            getBadgeTodoAllListDiv.css({
//                padding: '2px 0px',
//                height: '25px',
//                width: '25px'
//            });
//        } else if (get_TodoListElementsCount <= 9) {
//            getBadgeTodoAllListDiv.removeAttr('style');
//        }
//        getBadgeTodoAllListDiv.text(get_TodoListElementsCount);
//    }
//    else if (todoCategoryCount === 'completedList') {
//        if (get_CompletedTaskElementsCount === 0) {
//            getBadgeCompletedTaskListDiv.text('');
//            return;
//        }
//        if (get_CompletedTaskElementsCount > 9) {
//            getBadgeCompletedTaskListDiv.css({
//                padding: '2px 0px',
//                height: '25px',
//                width: '25px'
//            });
//        } else if (get_CompletedTaskElementsCount <= 9) {
//            getBadgeCompletedTaskListDiv.removeAttr('style');
//        }
//        getBadgeCompletedTaskListDiv.text(get_CompletedTaskElementsCount);
//    }
//    else if (todoCategoryCount === 'importantList') {
//        if (get_ImportantTaskElementsCount === 0) {
//            getBadgeImportantTaskListDiv.text('');
//            return;
//        }
//        if (get_ImportantTaskElementsCount > 9) {
//            getBadgeImportantTaskListDiv.css({
//                padding: '2px 0px',
//                height: '25px',
//                width: '25px'
//            });
//        } else if (get_ImportantTaskElementsCount <= 9) {
//            getBadgeImportantTaskListDiv.removeAttr('style');
//        }
//        getBadgeImportantTaskListDiv.text(get_ImportantTaskElementsCount);
//    }
//}

const ps = new PerfectScrollbar('.todo-box-scroll', {
    suppressScrollX: true
});

const todoListScroll = new PerfectScrollbar('.todoList-sidebar-scroll', {
    suppressScrollX: true
});

/*
 * FUNZIONI CUSTOM
 */
$('#addTask').on('click', function (event) {
    event.preventDefault();
    $.ajax({
        url: '/admin/dashboard/todo-add',
        method: 'get',
        success: function (res) {
            openTaskEditModal(res);
        },
        error: function (err) {
            console.error(err);
        }
    });
});

function editTask(evt, id) {
    evt.preventDefault();
    $.ajax({
        url: `/admin/dashboard/todo-edit/${id}`,
        method: 'get',
        success: function (res) {
            openTaskEditModal(res);
        },
        error: function (err) {
            console.error(err);
        }
    });
}

function toggleImportant(evt, todoId, isImportant) {
    evt.preventDefault();
    $.ajax({
        url: `/admin/dashboard/todo-toggle/${todoId}/important/${isImportant}`,
        method: 'get',
        success: function (res) {
            loadTodo();
        },
        error: function (err) {
            console.error(err);
        }
    });
}

function togglePriority(evt, todoId, priorityId) {
    evt.preventDefault();
    $.ajax({
        url: `/admin/dashboard/todo-toggle/${todoId}/priority/${priorityId}`,
        method: 'get',
        success: function (res) {
            loadTodo();
        },
        error: function (err) {
            console.error(err);
        }
    });
}

function openTaskEditModal(data) {
    $('#addTaskModalBody').html(data);
    $('#addTaskModal').modal('show');
    const ps = new PerfectScrollbar('.todo-box-scroll', {
        suppressScrollX: true
    });
}

function loadTodo() {
    let statuses = '';
    let important = false;
    let searchString = $('#searchString').val();

    $('.nav-link.list-actions').each(function () {
        if ($(this).hasClass('active')) {
            statuses = $(this).attr('data-statuses');
            important = $(this).attr('data-important') == 'true';
        }
    });

    $.ajax({
        url: '/admin/dashboard/todo-list',
        method: 'post',
        data: { statuses: statuses, searchString: searchString, important: important },
        success: function (res) {
            $('#todo-list-container').html(res);
            calculateBadge();
            //new dynamicBadgeNotification('allList');
            //new dynamicBadgeNotification('completedList');
            //new dynamicBadgeNotification('importantList');
        },
        error: function (err) {
            console.error(err);
        }
    });
}

function calculateBadge() {
    $.ajax({
        url: '/admin/dashboard/todo-badge',
        method: 'get',
        success: function (res) {
            $('#todo-task-todo-badge').html(+res.todo);
            $('#todo-task-important-badge').html(+res.important);
            $('#todo-task-done-badge').html(+res.done);
            $('#todo-task-trash-badge').html(+res.trash);
        },
        error: function (err) {
            console.error(err);
        }
    });
}

function saveTodo(evt) {
    evt.preventDefault();
    $.ajax({
        url: '/admin/dashboard/todo-save',
        method: 'post',
        data: $('form[name="todo-save-form"]').serialize(),
        success: function (res) {
            $('#addTaskModal').modal('hide');
        },
        error: function (err) {
            console.err(err);
        }
    });
}

$('#addTaskModal').on('hidden.bs.modal', function (e) {
    $('#addTaskModalBody').html('');
    loadTodo();
})
