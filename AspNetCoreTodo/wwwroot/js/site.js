// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    // Wire up all of the checkboxes to run markCompleted()
    $('.done-checkbox').on('click', function (e) {
        markCompleted(e.target);
    });

    var row;

    $('.edit-button').on('click', function (e) {

        //hide previous edit
        if (row) {
            hideEditFields(row);
        }

        row = e.target.closest('tr');
        showEditFields(row);
    })

    $('.cancel-button').on('click', function (e) {
        if (row) {
            hideEditFields(row);
            row = undefined;
        }
    })
});

function hideEditFields(row) {
    $(row).find(".save-button, .cancel-button, .item-update").hide();
    $(row).find(".edit-button, span").show();
}

function showEditFields(row) {
    $(row).find(".edit-button, span").hide();
    $(row).find(".item-update, .save-button, .cancel-button").show();
}

function markCompleted(checkbox) {
    checkbox.disabled = true;

    var row = checkbox.closest('tr');
    $(row).addClass('done');

    var form = checkbox.closest('form');
    form.submit();
}

