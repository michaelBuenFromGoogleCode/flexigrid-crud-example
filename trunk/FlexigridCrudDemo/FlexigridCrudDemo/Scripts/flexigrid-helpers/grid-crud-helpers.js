function closeFormByGrid(grd) {
    $('.fgEdit', grd).remove();
}

// close form if the selected row by user is not the same
function closeFormByTable(tbl) {
    $('.fgEdit', tbl).remove();
}

function closeForm(form) {
    $(form).closest('.fgEdit').remove();
}





function showAddFormByGrid(grid, formHtml) {

    var tbl = $('.bDiv table', grid);
    showAddForm(tbl, formHtml);
}


function showAddForm(tbl, formHtml) {
    var tbody = $('tbody', tbl);

    $(tbody).prepend('<tr class="fgEdit"><td width="1" colspan="20" style="border-width: thin; border-top: thick; border-color: #EEE; white-space: normal"><span></span></td></tr>');
    var content = $('tr td span', tbody);

    $(content).html(formHtml);


}


function parseDynamicContent(scope) {
    if ($.validator != undefined) {
        $.validator.unobtrusive.parseDynamicContent(scope);
    }
}


function setupGrid(table) {
    $('> tbody > tr', table).live('click', function () {
    // $(table).find('tbody > tr').live('click', function () {
        if ($(this).attr('class') != "fgEdit") {            
            closeFormByTable(table);
        }
    });
}


function showEditForm(selectedTr, html, assignerFunc) {

    $(selectedTr).after('<tr class="fgEdit" editId=' + selectedTr.id + '><td width="1" colspan="20" style="border-width: thin; border-top: thick; border-color: #EEE; white-space: normal;"><span></span></td></tr>');
    

    var content = $('td > span', $(selectedTr).next());

    // var form = $(content).hide().html(html);
    var form = $(content).html(html);
    
    assignerFunc();
    
    $(content).show();
    
}


function setFgEditText(tbl, columnName, text) {


    var grd = $(tbl).closest('.flexigrid');

    var column = $('th[abbr=' + columnName + ']', grd);
    var index = $('th', grd).index(column) + 1;

    var editId = $('.fgEdit').attr('editId');
    $('tr[id=' + editId + '] td:nth-child(' + index + ') div', grd).text(text);

}
