let url = window.location.href.toLowerCase();
const allLinks = Array.from(document.querySelectorAll('.nav-item .nav-link'));
let length = 0;

if (allLinks.length > 5) {
    length = 6;
} else {
    length = allLinks.length;
}

for (var i = 0; i < length; i++) {
    let link = allLinks[i].href;

    if (!link) {
        continue;
    }

    let currentLinkHref = link.toLowerCase();
    if (url === currentLinkHref) {
        allLinks[i].style.background = '#f38f36';
    }
}