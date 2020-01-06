function SetAllCheckBoxListChecked(sCheckBoxListID, sSelectAllID) {
    try {

        var cbAll = document.getElementById(sSelectAllID);       
        var checkboxes = document.getElementById(sCheckBoxListID).getElementsByTagName("input");
        for (i = 0; i < checkboxes.length; i++)
            checkboxes[i].checked = cbAll.checked;
    } catch (e) { }
}
function SetCheckBoxListAllStatus(sCheckBoxListID, sSelectAllID) {
    try {
        var cbAll = document.getElementById(sSelectAllID);
        var checkboxes = document.getElementById(sCheckBoxListID).getElementsByTagName("input");
        var bAllChecked = true;
        for (i = 0; i < checkboxes.length; i++)
            if (!checkboxes[i].checked) {
            bAllChecked = false;
            break;
        }
        cbAll.checked = bAllChecked;
    } catch (e) { }

}
function IsCheckBoxListItemSelected(sCheckBoxListID, sSelectAllID,sMsg) {
    var bRetVal = false;   
    var cbAll = document.getElementById(sSelectAllID);
    var checkboxes = document.getElementById(sCheckBoxListID).getElementsByTagName("input");
    if (cbAll.checked) {
        bRetVal = true;
    }
    else {
        for (i = 0; i < checkboxes.length; i++)
            if (checkboxes[i].checked) {
            bRetVal = true;
            break;
        }
    }
    if (!bRetVal)
        alert(sMsg);
    return bRetVal;
}
function SetExcludeSetList(sCheckBoxListID, sSelectAllID, sMatIDChecked) {
    try {
        if (document.getElementById('cbExcludedSet') == null || document.getElementById('cbExcludedSet') == 'undefined')
            return;
        var CheckedItems = $("#dvMaterialCategory").find("INPUT[type=checkbox][checked]");
        var MatCatCheckedItemsAll = $("#dvMaterialCategory").find("INPUT[type=checkbox]");
        var bFound = false;
        var bUnCheck = false;
        $("#cbExcludedSetAll").attr('checked', false);

        var ExcludedSetItems = $("#dvExcludedSet").find("INPUT[type=checkbox]");

        MatCatCheckedItemsAll.each(function() {

            var sMatCatID = $(this)[0].nextSibling.innerText;
            if ($(this)[0].checked) {
                ExcludedSetItems.each(function() {
                    if (sMatCatID == $(this)[0].parentNode.attributes['matid'].nodeValue) {
                        $("#cbExcludedSet").css('visibility', 'visible');
                        $(this).parent().css('display', '');
                    }

                });
            }
            else {
                if (sMatCatID == sMatIDChecked)
                    bUnCheck = true;
                ExcludedSetItems.each(function() {
                    if (sMatCatID == $(this)[0].parentNode.attributes['matid'].nodeValue) {
                        $(this).parent().css('display', 'none');
                    }

                });
            }

        });

        CheckedItems.each(function() {
            var sMatCatID = $(this)[0].nextSibling.innerText;
            if (sMatIDChecked == sMatCatID || sMatIDChecked == 'FULLSET') {
                ExcludedSetItems.each(function() {
                    if (sMatCatID == $(this)[0].parentNode.attributes['matid'].nodeValue) {
                        bFound = true;
                        $("#dvExcludedSetList").attr('disabled', false);
                        $("#cbExcludedSet").css('visibility', 'visible');
                        $(this).parent().css('display', '');
                        $(this).attr('checked', true);
                    }

                });
            }
            ExcludedSetItems.each(function() {
                if (sMatCatID == $(this)[0].parentNode.attributes['matid'].nodeValue) {
                    bFound = true;
                }

            });

        });
        if (bFound == false) {
            $("#cbExcludedSet").css('visibility', 'hidden');
            $("#dvExcludedSetList").attr('disabled', true);
            $("#cbExcludedSetAll").attr('checked', false);
        }
        else {
            if (bUnCheck && CheckedItems.length == 0) {
                $("#cbExcludedSet").css('visibility', 'hidden');
                $("#dvExcludedSetList").attr('disabled', true);
                $("#cbExcludedSetAll").attr('checked', false);
            }
            else
                $("#cbExcludedSet").css('visibility', 'visible');
        }
        SetExcludedSetCheckBoxAllStatus('cbExcludedSet', 'cbExcludedSetAll');
        var checkboxes = document.getElementById('cbExcludedSet').getElementsByTagName("input");
        var bAllCheckedCount = 0;
        for (i = 0; i < checkboxes.length; i++)
            if (checkboxes[i].parentElement.style.display == 'none') {
            bAllCheckedCount++;
        }
        if (checkboxes.length == bAllCheckedCount)
            $("#cbExcludedSetAll").attr('checked', false);


    } catch (e) { }

}
function SetExcludedSetCheckBoxAllStatus(sCheckBoxListID, sSelectAllID) {
    try {
        if (document.getElementById('cbExcludedSet') == null || document.getElementById('cbExcludedSet') == 'undefined')
            return;
        var cbAll = document.getElementById(sSelectAllID);
        var checkboxes = document.getElementById(sCheckBoxListID).getElementsByTagName("input");
        var bAllChecked = true;
        for (i = 0; i < checkboxes.length; i++)
            if ((!checkboxes[i].checked) && (checkboxes[i].parentElement.style.display != 'none')) {
            bAllChecked = false;
            break;
        }
        cbAll.checked = bAllChecked;
    } catch (e) { }

}
