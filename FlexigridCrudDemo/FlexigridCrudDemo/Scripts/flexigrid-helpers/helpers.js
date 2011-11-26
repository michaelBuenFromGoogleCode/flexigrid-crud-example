
function getFgRowsPerPage(tbl) {
    return $('select[name=rp] option:selected', tbl.closest('.flexigrid')).val();
}