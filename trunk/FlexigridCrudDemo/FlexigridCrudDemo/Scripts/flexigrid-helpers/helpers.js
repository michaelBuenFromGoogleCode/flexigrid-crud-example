
function getFgRowsPerPage(tbl) {
    return $('select[name=rp] option:selected', tbl.closest('.flexigrid')).val();
}


function getFgGridColumnText(grd, columnName) {


    // var grd = $(tbl).closest('.flexigrid');

    var column = $('th[abbr=' + columnName + ']', grd);
    var index = $('th', grd).index(column) + 1;


    var editId = "row" + getCurrentRowPk(grd);

    return $('tr[id=' + editId + '] td:nth-child(' + index + ')', grd).text();

}



function getCurrentRowPk(grd) {
    var items = $('.trSelected', grd);
    var item = items[0];
    var pk = item.id.substr(3);
    return pk;
}