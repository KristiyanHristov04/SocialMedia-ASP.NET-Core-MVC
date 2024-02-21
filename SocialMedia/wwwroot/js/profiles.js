let counter = 1;
let scrolled = false;
let isFirstLoad = true;
let noMoreProfilesMessageShowed = false;
const searchTermValue = document.getElementById('search-term').textContent;
const row = document.getElementsByClassName('row')[0];

let protocol = window.location.protocol;
let hostname = window.location.hostname;
let port = window.location.port;
let fullPath = `${protocol}//${hostname}:${port}`;

loadProfiles();

window.addEventListener('scroll', () => {
    if (Math.ceil(window.scrollY + window.innerHeight) >= document.documentElement.scrollHeight && !scrolled) {
        scrolled = true;
        loadProfiles();
    }
});

function loadProfiles() {
    fetch(`${fullPath}/api/posts/profiles?search=${searchTermValue}&counter=${counter}`)
        .then(res => res.json())
        .then(data => {
            console.log(data);

            if (counter !== 1) {
                isFirstLoad = false;
            }

            if (isFirstLoad && data.length === 0) {
                noProfilesMessage('No profiles matched!');
                noMoreProfilesMessageShowed = true;
            }

            if (data.length > 0) {
                for (var profile of data) {
                    let id = profile.id;
                    let fullName = profile.fullName;
                    let totalPosts = profile.totalPosts;
                    let username = profile.username;
                    createProfile(id, fullName, totalPosts, username);
                }
                scrolled = false;
                counter++;
            } else {
                if (!noMoreProfilesMessageShowed) {
                    noProfilesMessage('No more profiles matched!');
                    noMoreProfilesMessageShowed = true;
                }
            }

            if (data.length < 8 && isFirstLoad && !noMoreProfilesMessageShowed) {
                noProfilesMessage('No more profiles matched!');
                noMoreProfilesMessageShowed = true;
            }
        })
        .catch(err => console.error(err));
}

function createProfile(id, fullName, totalPosts, username) {
    let divColumn = document.createElement('div');
    divColumn.classList.add('col', 'mb-2');

    let divMainContainer = document.createElement('div');
    divMainContainer.classList.add('main-container', 'mx-auto', 'bg-dark', 'rounded-3', 'p-2', 'd-flex', 'justify-content-between', 'align-items-center');

    let divUserInformation = document.createElement('div');
    divUserInformation.classList.add('user-information');

    let paragraphInfo = document.createElement('p');
    paragraphInfo.classList.add('text-white', 'm-0');
    paragraphInfo.textContent = `${fullName} - ${totalPosts} posts`

    let spanUsername = document.createElement('span');
    spanUsername.textContent = '@' + username;

    let divViewProfile = document.createElement('div');
    divViewProfile.classList.add('view-profile');

    let anchorViewProfile = document.createElement('a');
    anchorViewProfile.classList.add('btn', 'btn-success');
    anchorViewProfile.textContent = 'View';
    anchorViewProfile.href = `${fullPath}/Post/Profile/${username}`


    divViewProfile.appendChild(anchorViewProfile);

    divUserInformation.appendChild(paragraphInfo);
    divUserInformation.appendChild(spanUsername);

    divMainContainer.appendChild(divUserInformation);
    divMainContainer.appendChild(divViewProfile);

    divColumn.appendChild(divMainContainer);

    row.appendChild(divColumn);
}

function noProfilesMessage(message) {
    //let divColumn = document.createElement('div');
    //divColumn.classList.add('col', 'mb-2');

    //let divMainContainer = document.createElement('div');
    //divMainContainer.classList.add('main-container', 'mx-auto', 'bg-dark', 'rounded-3', 'p-2');
    //divMainContainer.style.maxWidth = '500px';

    //let paragraphNoMatching = document.createElement('p');
    //paragraphNoMatching.classList.add('text-white', 'm-0', 'text-center');
    //paragraphNoMatching.textContent = `${message}`;

    //divMainContainer.appendChild(paragraphNoMatching);

    //divColumn.appendChild(divMainContainer);

    //row.appendChild(divColumn);

    let noMorePostsParagraph = document.createElement('p');
    noMorePostsParagraph.textContent = `${message}`;
    noMorePostsParagraph.classList.add('text-info', 'text-center');
    row.appendChild(noMorePostsParagraph);
}