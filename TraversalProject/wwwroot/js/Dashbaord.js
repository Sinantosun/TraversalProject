
function reload() {
    document.location.reload();
}
function ShowMessage(title, icon, content) {
    Swal.fire({
        title: title,
        html: content,
        icon: icon
    });
}
$(function () {
    var modal = new bootstrap.Modal(document.getElementById('exampleModal'));

    var checkbox = document.getElementById('flexSwitchCheckDefault');

    checkbox.addEventListener('click', function () {
        if (checkbox.checked) {
            document.querySelector(".modal-title").innerHTML = "İki Adımlı Doğrulama Aktifleştir";
            modal.show();

            $(".modelbtnsubmit").click(function () {

                var pwd = document.querySelector(".pwd").value;
                $.ajax({
                    type: "post",
                    url: "/Members/Dashboard/ChangeTwoFactorEnabledToTrue/" + pwd,
                    data: { 'pwd': pwd },
                    success: function (data) {
                        if (data.err = "None") {
                            document.querySelector(".pwd").value = "";
                            ShowMessage(data.title, data.icon, data.descr);
                        }
                        else {

                            checkbox.checked = false;
                            document.querySelector(".pwd").value = "";
                            ShowMessage(data.title, data.icon, data.descr);

                            if (data.isReolad) {
                                setTimeout(reload, 1350);
                            }


                        }
                        console.log(data);
                    }
                });

            });
        }
        else {
            document.querySelector(".modal-title").innerHTML = "İki Adımlı Doğrulama Kapatma";
            modal.show();

            $(".modelbtnsubmit").click(function () {

                var pwd = document.querySelector(".pwd").value;
                $.ajax({
                    type: "post",
                    url: "/Members/Dashboard/ChangeTwoFactorEnabledToFalse/" + pwd,
                    data: { 'pwd': pwd },
                    success: function (data) {

                        if (data.err = "None") {
                            document.querySelector(".pwd").value = "";
                            ShowMessage(data.title, data.icon, data.descr);

                        }
                        else {

                            checkbox.checked = false;

                            document.querySelector(".pwd").value = "";
                            ShowMessage(data.title, data.icon, data.descr);

                            if (data.isReolad) {
                                setTimeout(reload, 1350);
                            }


                        }

                    }
                });

            });
        }
    });


    var checkbox = document.getElementById('flexSwitchCheckDefault1');

    checkbox.addEventListener('click', function () {
        if (!checkbox.checked) {
            $.ajax({
                type: "post",
                url: "/Members/Dashboard/ChangePasswordEveryThreeMonthSetFalse/",
                success: function (data) {
                    if (data.err = "None") {
                        ShowMessage(data.title, data.icon, data.descr);
                    }
                    else {

                        ShowMessage(data.title, data.icon, data.descr);
                    }

                }
            });
        }
        else {

            $.ajax({
                type: "post",
                url: "/Members/Dashboard/ChangePasswordEveryThreeMonthSetActive/",
                success: function (data) {

                    if (data.err = "None") {
                        ShowMessage(data.title, data.icon, data.descr);

                    }
                    else {

                        ShowMessage(data.title, data.icon, data.descr);
                    }

                }
            });


        }
    });
})