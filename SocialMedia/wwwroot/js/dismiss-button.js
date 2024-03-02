let allDismissButtons = Array.from(document.getElementsByClassName('btn-dismiss'));
let isAlertShowed = false;

let protocol = window.location.protocol;
let hostname = window.location.hostname;
let port = window.location.port;
let fullPath = `${protocol}//${hostname}:${port}`;

console.log(allDismissButtons);
allDismissButtons.forEach(btn => {
    btn.addEventListener('click', () => {
        fetch(`${fullPath}/api/posts/report/dismiss/${btn.id}`, {
            method: 'POST'
        })
            .then(() => {
                btn.parentElement.parentElement.remove();
                if (!isAlertShowed) {
                    toastr.options = {
                        positionClass: "toast-bottom-right"
                    };

                    toastr.success('Reported Post Dismissed successfully!');

                    isAlertShowed = true;
                }
            })
            .catch(err => console.error(err));
    });
});