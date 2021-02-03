
/*
 * ORIGINALI TEMA
 */
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

function toggleDone(evt, todoId, isDone) {
    evt.preventDefault();
    $.ajax({
        url: `/admin/dashboard/todo-toggle/${todoId}/done/${isDone}`,
        method: 'get',
        success: function (res) {
            loadTodo();
        },
        error: function (err) {
            console.error(err);
        }
    });
}

function changeStatus(evt, todoId, statusId) {
    evt.preventDefault();
    $.ajax({
        url: `/admin/dashboard/todo-change/${todoId}/status/${statusId}`,
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

$('a.list-actions[data-toggle="pill"]').on('shown.bs.tab', function (e) {
    loadTodo();
});

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
});
