var swal = require("sweetAlert");

function ShowMessage(title, text, theme) {
    window.createNotification({
        closeOnClick: true,
        displayCloseButton: false,
        positionClass: 'nfc-bottom-right',
        showDuration: 400,
        theme: theme !== ? theme : 'success'
    })({
        title: title !== ? title : 'اعلان',
        message: decodeURI(text)
    });
}