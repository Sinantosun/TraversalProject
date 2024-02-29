
function reload() {
    document.location.reload();
}


function ShowMessage(title, icon, content) {
    Swal.fire({
        title: title,
        text: content,
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
                        if (data == "01") {
                            document.querySelector(".pwd").value = "";
                            ShowMessage("Başarılı", "success", "İki Adımlı Doğrulama Açıldı");
                            setTimeout(reload, 1350);
                        }
                        else if (data == "02") {

                            checkbox.checked = false;
                            document.querySelector(".pwd").value = "";

                            ShowMessage("Hata", "info", "Lütfen Şifrenizi Kontrol Edin.");

                        }
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

                        if (data == "01") {
                            document.querySelector(".pwd").value = "";
                            ShowMessage("Başarılı", "success", "İki Adımlı Doğrulama Kapatıldı.");
                            setTimeout(reload, 1350);
                        }
                        else if (data == "02") {
                            checkbox.checked = true;
                            document.querySelector(".pwd").value = "";
                            ShowMessage("Hata", "info", "Lütfen Şifrenizi Kontrol Edin.");
                        }

                    }
                });

            });
        }
    });

})