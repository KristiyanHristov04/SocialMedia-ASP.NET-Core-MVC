let url = window.location.href.toLowerCase();
const allLinks = Array.from(document.querySelectorAll('.link-item'));

for (var i = 0; i < allLinks.length; i++) {
    let link = allLinks[i].href;

    let currentLinkHref = link.toLowerCase();
    if (url === currentLinkHref) {
        allLinks[i].style.background = '#f38f36';
    }
}