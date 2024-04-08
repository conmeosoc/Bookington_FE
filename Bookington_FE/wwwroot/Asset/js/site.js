var _idUser_del = "";
var _idCourt_del = "";
var _idCourt_up = "";
var _idCourt_detail = "";
var _idSubCourt_del = "";
var _idCourtreport = "";
var _idAccountreport = "";
//for search count
var _currentPage = parseInt(0);
var _pageSize = parseInt(10);
var _idSlot = "";
var _idSubCourt_detail = "";
var _idCourtParent = "";
var _idCourtOw_up = coid = "";
var _idBanC = "";

function UserLogin() {

    var phone = $('#phoneinput').val();
    var pass = $('#passinput').val();
    var loginData = { phone: phone, password: pass };
    //
    jQuery.ajax({
        url: window.location.origin + "/Home/AuthAccount",
        type: "POST",
        cache: false,
        data: loginData,
        //contentType: 'application/json',
        success: function Redirect(dataOut) {
            var data = JSON.parse(dataOut);
            if (data.statusCode == 200) {
                if (data.result.role == "admin") {

                    RedirectToLink(window.location.origin + "/Admin/Index");
                }
                else if (data.result.role == "owner") {
                    //var surl = document.location.href;
                    //surl = surl.substr(0, surl.lastIndexOf("/")) + "/Privacy";
                    RedirectToLink(window.location.origin + "/Owner/Index");

                }
            }
            else {
                alert("User Phone Or Password Incorrect");
            }
        }
    })
}
function RedirectToLink(link) {
    window.location.replace(link);
}
function OpenModal(id) {
    $('#' + id).modal("show");
    //$('#' + id).appendTo("body");
}
function DeleteUser(uid, uname) {
    _idUser_del = uid;
    _nameUser_del = uname;
    $('#nameUserDel span').text(uname);
    $('#delusermodal').modal("show");
}
function ConfirmDel() {

    //
    jQuery.ajax({
        url: window.location.origin + "/Admin/DeleteAccount?id=" + _idUser_del,
        type: "GET",
        cache: false,
        success: function Redirect(dataOut) {
            if (dataOut == true) {
                RedirectToLink(window.location.origin + "/Admin/UserManager");

            }
            else {
                alert("delete user failed!");
            }
        }
    })
}



function Update() {
    var nameUser_up = $('#inputUpUserName').val();
    var dobUser_up = $('#inputUpUserDOB').val();
    jQuery.ajax({
        url: window.location.origin + "/Admin/UpdateAccount",
        type: "POST",
        cache: false,
        data: { id: _idUser_up, name: nameUser_up, dob: dobUser_up },
        success: function Redirect(dataOut) {
            if (dataOut == true) {

                alert("Update profile successfully");
                /*alert("delete user success!");*/
            }
            else {
                alert("Update user failed!");
            }
        }
    })
}
function UpdateProfile() {
    var nameUser_up = $('#inputUpUserName').val();
    var dobUser_up = $('#inputUpUserDOB').val();
    jQuery.ajax({
        url: window.location.origin + "/Admin/UpdateProfile",
        type: "POST",
        cache: false,
        data: { name: nameUser_up, dob: dobUser_up },
        success: function Redirect(dataOut) {
            if (dataOut == true) {
                /*RedirectToLink(window.location.origin+"/Admin/UserManager");*/
                alert("Update profile successfully");
                /*alert("delete user success!");*/
            }
            else {
                alert("Update user failed!");
            }
        }
    })
}
function DeleteCourt(uid, uname) {
    _idCourt_del = uid;
    $('#nameCourtDel span').text(uname);
    $('#delcourtmodal').modal("show");
}
function ConfirmCDel() {

    //
    jQuery.ajax({
        url: window.location.origin + "/Owner/DeleteCourt?id=" + _idCourt_del,
        type: "GET",
        cache: false,
        success: function Redirect(dataOut) {
            if (dataOut == true) {
                RedirectToLink(window.location.origin + "/Owner/ManageYard");
                /*alert("delete user success!");*/
            }
            else {
                alert("delete court failed!");
            }
        }
    })
}
function UpdateCourt(cid, coid) {
    _idCourt_up = cid;
    _idCourtOw_up = coid
    $('#upcourtmodal').modal("show");
}
function SetDistrictup() {
    var id = $('#selectProvinceup').val();
    //remove option
    $('#districtselectup option').remove();
    jQuery.ajax({
        url: window.location.origin + "/Owner/GetDistrictByProvince?id=" + id,
        type: "GET",
        cache: false,
        success: function SetOptionDistrictup(data) {
            for (let i = 0; i < data.length; i++) {
                $('#districtselectup').append('<option value="' + data[i].id + '">' + data[i].districtName + '</option>');
            }
        }
    })
}
function UpdateC() {
    var cid = _idCourt_up
    var coid = _idCourtOw_up
    var districtIDCourt_up = $('#districtselectup').val()
    var nameCourt_up = $('#inputUpCourtName').val()
    var addressCourt_up = $('#inputUpCourtAddress').val();
    var desCourt_up = $('#inputUpCourtDescription').val();
    var openCourt_up = $('#inputUpCourtOpen').val();
    var closeCourt_up = $('#inputUpCourtClose').val();
    var images = $('#formFileMultipleup').val();
    //var file = document.querySelector('input[type=file]');
    //var reader = new FileReader();
    //var url = reader.readAsArrayBuffer(file);
    jQuery.ajax({
        url: window.location.origin + "/Owner/UpdateCourt",
        type: "POST",
        cache: false,
        data: {
            cid: cid,
            coid: coid,
            cdid: districtIDCourt_up,
            cname: nameCourt_up,
            caddress: addressCourt_up,
            des: desCourt_up,
            copen: openCourt_up,
            cclose: closeCourt_up,
            image: images
        },
        success: function Redirect(dataOut) {
            if (dataOut == true) {
                RedirectToLink(window.location.origin + "/Owner/ManageYard");

                /*alert("delete user success!");*/
            }
            else {
                alert("Update court failed!");
            }
        }
    })
}
function SetPageSize(size) {
    _pageSize = parseInt(size);
    $('#pageSizeLab span').text(size);
    SearchCourt();
}
function btnPreviousClick() {
    if (_currentPage > 1) {
        _currentPage = _currentPage - 1;
    }
    SearchCourt();
}
function btnNextClick() {
    _currentPage = _currentPage + 1;
    SearchCourt();
}
function btnNumbpageClick(pageNum) {
    if (_currentPage != pageNum) {
        _currentPage = parseInt(pageNum);
        SearchCourt();
    }
}
function SetPageNumAndSize(currPage, pageSize) {
    _currentPage = parseInt(currPage);
    _pageSize = parseInt(pageSize);
}
function btnSearchCourtClick() {
    _currentPage = parseInt(1);
    SearchCourt();
}
function SearchCourt() {
    var searchText = $("#searchCourtTxt").val();
    window.location.href = window.location.origin + '/Owner/ManageYard?searchText=' + searchText + '&currentPage=' + _currentPage + '&pageSize=' + _pageSize;
    //jQuery.ajax({
    //    url: '@Url.Action("ManageYard", "Owner")',
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    data: { searchText: searchText, currentPage: _currentPage, pageSize: _pageSize },
    //    success: function () { alert('Success'); }
    //})
}

function DetailCourt(id) {
    _idCourt_detail = id;
    window.location.href = window.location.origin + '/Owner/SubCourt?courtID=' + _idCourt_detail;
    /*$('#subcourtmodal').modal("show");*/

}

function SetPageSizeU(size) {
    _pageSize = parseInt(size);
    $('#pageSizeLabU span').text(size);
    SearchUser();
}
function btnPreviousClickU() {
    if (_currentPage > 1) {
        _currentPage = _currentPage - 1;
    }
    SearchUser();
}
function btnNextClickU() {
    _currentPage = _currentPage + 1;
    SearchUser();
}
function btnNumbpageClickU(pageNum) {
    if (_currentPage != pageNum) {
        _currentPage = parseInt(pageNum);
        SearchUser();
    }
}
function SetPageNumAndSizeU(currPage, pageSize) {
    _currentPage = parseInt(currPage);
    _pageSize = parseInt(pageSize);
}
function btnSearchUserClick() {
    _currentPage = parseInt(1);
    SearchUser();
}
function SearchUser() {
    var searchText = $("#searchUserTxt").val();
    window.location.href = window.location.origin + '/Admin/UserManager?searchText=' + searchText + '&currentPage=' + _currentPage + '&pageSize=' + _pageSize;
}

function DetailCompe(id) {
    _idCourt_detail = id;
    $('#detailcompe').modal("show");
}

function DetailSubCourt(id) {
    _idSubCourt_detail = id;
    $('#' + id).modal("show");
}

function DeleteSubCourt(sid, uname) {
    _idSubCourt_del = sid;
    $('#nameSubCourtDel span').text(uname);
    $('#delsubcourtmodal').modal("show");
}
function ConfirmSCDel() {
    _idCourt_detail = id
    //
    jQuery.ajax({
        url: window.location.origin + "/Owner/DeleteSubCourt?id=" + _idSubCourt_del,
        type: "GET",
        cache: false,
        success: function Redirect(dataOut) {
            if (dataOut == true) {
                RedirectToLink(window.location.origin + '/ Owner / Subcourt ? courtID = ' + _idCourt_detail);
                /*alert("delete user success!");*/
            }
            else {
                alert("delete court failed!");
            }
        }
    })
}
function SetStatusSlot(val) {
    $('#statusSl span').text(val);
}
function ShowModalEditSlot(slotid, price, active) {
    _idSlot = slotid;
    $('#priceslot').val(price);
    if (active == true)
        $('#statusSl span').text('true');
    else
        $('#statusSl span').text('false');
    $('#editslot').modal("show");
}
function ConfirmEditSlot() {
    var id = _idSlot;
    var price = $('#priceslot').val();
    var status = $('#statusSl span').text();
    jQuery.ajax({
        url: window.location.origin + "/Owner/UpdateSlot?id=" + id + "&price=" + price + "&status=" + status + "&idsubcourt=" + _idSubCourt_detail,
        type: "GET",
        cache: false,
        success: function Redirect(dataOut) {
            if (dataOut == true) {
                alert("update slot success!");
                $('#editslot').modal("hide");
                /*alert("delete user success!");*/
            }
            else {
                alert("update slot failed!");
            }
        }
    })
}



function SetStatusCourt(val) {
    $('#isbancourt span').text(val);
}
function ShowModalBanC(id) {
    _idCourtreport = id;
    $('#bancourt').modal("show");
}
function showCreateCourt() {
    $('#createcourtmodal').modal("show")

}
function showCreateSCourt(id) {
    $('#createscourtmodal').modal("show")
    _idCourtParent = id;

}

function SetDistrict() {
    var id = $('#selectProvince').val();
    //remove option
    $('#districtselect option').remove();
    jQuery.ajax({
        url: window.location.origin + "/Owner/GetDistrictByProvince?id=" + id,
        type: "GET",
        cache: false,
        success: function SetOptionDistrict(data) {
            for (let i = 0; i < data.length; i++) {
                $('#districtselect').append('<option value="' + data[i].id + '">' + data[i].districtName + '</option>');
            }
        }
    })
}
function CreateC() {
    var nameCourt_create = $('#inputNewCourtName').val();
    var district = $('#districtselect').val();
    var addressCourt_create = $('#inputCreateCourtAddress').val();
    var descriptionCourt_create = $('#inputCreateCourtdescription').val();
    var openCourt_create = $('#inputCreateCourtOpen').val();
    var closeCourt_create = $('#inputCreateCourtClose').val();
    var image = $("#formFileMultiple").val();
    jQuery.ajax({
        url: window.location.origin + "/Owner/CreateCourt",
        type: "POST",
        cache: false,
        data: {
            name: nameCourt_create,
            district: district,
            address: addressCourt_create,
            description: descriptionCourt_create,
            open: openCourt_create,
            close: closeCourt_create,
            image: image
        },
        success: function Redirect(dataOut) {
            if (dataOut == true) {
                alert("Create court successfully!");
                /*alert("delete user success!");*/
            }
            else {
                alert("Create court failed!");
            }
        }
    })

}
function CreateSC() {
    var id = _idCourtParent;
    var nameSCourt_create = $('#nameSCCreate').val();
    var courttypeid = $('#courttypeID').val();
    var SCIsactive = $('#SCisactive').val();
    var SCIsDelete = $('#SCisdelete').val();

    jQuery.ajax({
        url: window.location.origin + "/Owner/CreateSCourt",
        type: "POST",
        cache: false,
        data: {
            id: id,
            nameSC: nameSCourt_create,
            typeid: courttypeid,
            scstatus: SCIsactive,
            scdelete: SCIsDelete
        },
        success: function Redirect(dataOut) {
            if (dataOut == true) {
                alert("Create subcourt successfully!");

            }
            else {
                alert("Create subcourt failed!");
            }
        }
    })
}


function notiRead(id, isRead) {
    jQuery.ajax({
        url: window.location.origin + "/Owner/MarkAsRead",
        type: "POST",
        cache: false,
        data: {
            id: id,
            isRead: isRead
        },
        success: function Redirect(dataOut) {
            if (dataOut == true) {
                $('#' + id).removeClass("new");
                var num_unread = parseInt($('#num_noti span').text());
                num_unread--;
                $('#num_noti span').text(num_unread);
            }
            else {
                alert("read failed!");
            }
        }
    })
}
function MarkAll(id) {
    jQuery.ajax({
        url: window.location.origin + "/Owner/MarkAsRead",
        type: "POST",
        cache: false,
        data: {
            id: id,
            isRead: true
        },
        success: function Redirect(dataOut) {
            if (dataOut == true) {
                var arrID = id.split(",");
                if (arrID.length > 0) {
                    for (let i = 0; i < arrID.length; i++) {
                        $('#' + arrID[i]).removeClass("new");
                    }
                }
                var num_unread = parseInt($('#num_noti span').text());
                num_unread = num_unread - arrID.length;
                if (num_unread < 0)
                    num_unread = 0;
                $('#num_noti span').text(num_unread);
            }
            else {
                //alert("Create subcourt failed!");
            }
        }
    })
}
function BanC() {

}
function ShowModalBanC(id) {
    _idCourtreport = id;
    $('#bancourt').modal("show");
}
function ShowBanAccountModel(id) {
    _idAccountreport = id;
    $('#banaccount').modal("show");
}
function BanC() {
    var banCid = _idCourtreport;
    var content = $('#BanCContent').val();
    var duration = $('#BanCduration').val();
    jQuery.ajax({
        url: window.location.origin + "/Admin/CreateCourtBan",
        type: "POST",
        cache: false,
        data: {
            banCId: banCid,
            content: content,
            duration: duration
        },
        success: function Redirect(dataOut) {
            if (dataOut == true) {
                alert("Create court ban successfully!");
                /*alert("delete user success!");*/
            }
            else {
                alert("Create court ban failed!");
            }
        }
    })
}
function BanA() {
    var banAid = _idAccountreport;
    var content = $('#BanAContent').val();
    var duration = $('#BanAduration').val();
    jQuery.ajax({
        url: window.location.origin + "/Admin/CreateAccountBan",
        type: "POST",
        cache: false,
        data: {
            banAId: banAid,
            content: content,
            duration: duration
        },
        success: function Redirect(dataOut) {
            if (dataOut == true) {
                alert("Create user ban successfully!");
                /*alert("delete user success!");*/
            }
            else {
                alert("Create user ban failed!");
            }
        }
    })
}
function changerole(id) {

    var roleid = $('#role :selected').val();
    jQuery.ajax({
        url: window.location.origin + "/Admin/UpdateAccount?id=" + id + "&role=" + roleid,
        type: "GET",
        cache: false,
        success: function Redirect(dataOut) {
            if (dataOut == true) {
                alert("update role success!");
            }
            else {
                alert("update role failed!");
            }
        }
    })
}