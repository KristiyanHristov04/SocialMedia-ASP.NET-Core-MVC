let counter = 1;
let scrolled = false;
let isFirstLoad = true;
let noMorePostsMessageShowed = false;
const currentUserId = document.getElementById('user-id').textContent;
const row = document.getElementsByClassName('row')[0];
const loadPostsButton = document.getElementById('load-posts-button');
const imageFormats = ['.gif', '.jpg', '.jpeg', '.png'];
const videoFormats = ['.mpg', '.mp2', '.mpeg', '.mpe', '.mpv', '.mp4'];

loadPosts();

window.addEventListener('scroll', () => {
    if (Math.ceil(window.scrollY + window.innerHeight) >= document.documentElement.scrollHeight && !scrolled) {
        scrolled = true;
        loadPosts();
    }
});

function loadPosts() {
    fetch(`https://localhost:7045/api/posts/mine?counter=${counter}`)
        .then(res => res.json())
        .then(data => {
            if (counter !== 1) {
                isFirstLoad = false;
            }

            if (data.length > 0) {
                for (let post of data) {
                    let postId = post.id;
                    let postPath = post.path;
                    let postText = post.text;
                    let postUserId = post.userId;
                    let postFirstName = post.firstName;
                    let postLastName = post.lastName;
                    let postUsername = post.username;
                    let postDateSeconds = post.dateSeconds;
                    let postDateMinutes = post.dateMinutes;
                    let postDateHours = post.dateHours;
                    let postDateDay = post.dateDay;
                    let postDateMonth = post.dateMonth;
                    let postDateYear = post.dateYear;

                    let currentDate = new Date();
                    let postDate = new Date(postDateYear, postDateMonth - 1, postDateDay, postDateHours, postDateMinutes, postDateSeconds);
                    let dateDiffResultObj = differenceBetweenDates(currentDate, postDate);

                    let result = '';

                    if (dateDiffResultObj.days != 0) {
                        result = `${postDateDay.toString().padStart(2, 0)}.${postDateMonth.toString().padStart(2, 0)}.${postDateYear}`;
                    } else if (dateDiffResultObj.hours > 1) {
                        result = `${dateDiffResultObj.hours} hours ago`;
                    } else if (dateDiffResultObj.hours == 1) {
                        result = `An hour ago`;
                    } else if (dateDiffResultObj.minutes > 1) {
                        result = `${dateDiffResultObj.minutes} minutes ago`;
                    } else if (dateDiffResultObj.minutes == 1) {
                        result = `A minute ago`;
                    } else if (dateDiffResultObj.seconds > 1) {
                        result = `${dateDiffResultObj.seconds} seconds ago`;
                    } else {
                        result = `A second ago`;
                    }

                    createPost(postId, postPath, postText, postUserId, result, postFirstName, postLastName, postUsername);
                }
                counter++;
                scrolled = false;
            } else {
                if (!noMorePostsMessageShowed) {
                    noMorePostsMessage();
                    noMorePostsMessageShowed = true;
                }
            }

            if (data.length == 1 && isFirstLoad) {
                noMorePostsMessage();
                noMorePostsMessage = true;
            }
        })
        .catch(err => console.error(err));
}

function differenceBetweenDates(date1, date2) {
    var date1InMilliseconds = date1.getTime();
    var date2InMilliseconds = date2.getTime();

    var differenceInMilliseconds = Math.abs(date2InMilliseconds - date1InMilliseconds);

    var millisecondsPerSecond = 1000;
    var millisecondsPerMinute = millisecondsPerSecond * 60;
    var millisecondsPerHour = millisecondsPerMinute * 60;
    var millisecondsPerDay = millisecondsPerHour * 24;

    var days = Math.floor(differenceInMilliseconds / millisecondsPerDay);
    var hours = Math.floor((differenceInMilliseconds % millisecondsPerDay) / millisecondsPerHour);
    var minutes = Math.floor((differenceInMilliseconds % millisecondsPerHour) / millisecondsPerMinute);
    var seconds = Math.floor((differenceInMilliseconds % millisecondsPerMinute) / millisecondsPerSecond);

    var result = {
        days: days,
        hours: hours,
        minutes: minutes,
        seconds: seconds
    };

    return result;
}


function createPost(id, path, text, userId, date, firstName, lastName, username) {
    let divColumn = document.createElement('div');
    divColumn.classList.add('col');

    let divMainContainer = document.createElement('div');
    divMainContainer.classList.add('main-container', 'mx-auto', 'bg-dark', 'rounded-3', 'p-2', 'd-flex', 'flex-column', 'justify-content-evenly');

    let divUserInfoContainer = document.createElement('div');
    divUserInfoContainer.classList.add('user-info-container', 'h-25');

    let spanPostedDate = document.createElement('span');
    spanPostedDate.textContent = date;

    let divContainer = document.createElement('div');
    divContainer.classList.add('mb-2');

    let divFlexContainer = document.createElement('div');
    divFlexContainer.classList.add('flex-container', 'd-flex', 'justify-content-between', 'align-items-center');

    let paragraphNames = document.createElement('p');
    paragraphNames.classList.add('text-white', 'mb-0');
    paragraphNames.textContent = `${firstName} ${lastName}`;

    let divEditDeleteContainer = document.createElement('div');
    divEditDeleteContainer.classList.add('edit-delete-container');

    if (userId == currentUserId) {
        let editLink = document.createElement('a');
        editLink.classList.add('edit-button');
        editLink.innerHTML = '<i class="fa-regular fa-pen-to-square"></i>';
        editLink.href = window.location.origin + `/Post/Edit/${id}`;
        divEditDeleteContainer.appendChild(editLink);

        let deleteButton = document.createElement('a');
        deleteButton.classList.add('delete-button', 'ms-2');
        deleteButton.setAttribute('data-bs-toggle', 'modal');
        deleteButton.setAttribute('data-bs-target', '#deleteModal');
        deleteButton.innerHTML += '<i class="fa-regular fa-trash-can"></i>';

        deleteButton.addEventListener('click', (e) => {
            prepareForDelete(id, e);
        });

        divEditDeleteContainer.appendChild(deleteButton);
    }

    let spanUsername = document.createElement('span');
    spanUsername.textContent = '@' + username;

    let textContainer = document.createElement('div');
    textContainer.classList.add('text-container');

    let paragraphText = document.createElement('p');
    paragraphText.classList.add('text', 'h-100');
    paragraphText.textContent = text;

    textContainer.appendChild(paragraphText);

    divFlexContainer.appendChild(paragraphNames);
    divFlexContainer.appendChild(divEditDeleteContainer);

    divContainer.appendChild(divFlexContainer);
    divContainer.appendChild(spanUsername);

    divUserInfoContainer.appendChild(spanPostedDate);
    divUserInfoContainer.appendChild(divContainer);
    divUserInfoContainer.appendChild(textContainer);

    divMainContainer.appendChild(divUserInfoContainer);

    let divMediaContainer = document.createElement('div');
    divMediaContainer.classList.add('media-container');

    let index = path.indexOf('.');
    let pathExtension = path.substr(index);

    let media = '';
    if (imageFormats.includes(pathExtension)) {
        isImage = true;
        media = document.createElement('img');
        media.src = window.location.origin + `/${path}`;
        media.classList.add('w-100', 'h-100', 'rounded-3');

        let anchorMedia = document.createElement('a');
        anchorMedia.href = window.location.origin + `/${path}`;
        anchorMedia.target = '_blank';
        anchorMedia.appendChild(media);

        divMediaContainer.appendChild(anchorMedia);
    } else {
        media = document.createElement('video');
        media.setAttribute('controls', '');
        media.classList.add('w-100', 'h-100', 'rounded-3');

        let source = document.createElement('source');
        source.src = window.location.origin + `/${path}`;

        media.appendChild(source);

        divMediaContainer.appendChild(media);
    }

    let divInteractionContainer = document.createElement('div');
    divInteractionContainer.classList.add('interaction-container', 'p-2', 'pb-0', 'text-center');

    let anchorLikeText = document.createElement('a');
    anchorLikeText.classList.add('like-text', 'text-decoration-none');

    checkIfPostIsLikedByUser(id, anchorLikeText);

    anchorLikeText.addEventListener('click', () => {

        fetch(`https://localhost:7045/api/posts/like/${id}`, {
            method: 'POST'
        })
            .then(() => checkIfPostIsLikedByUser(id, anchorLikeText))
            .catch(err => console.error(err));
    });

    divInteractionContainer.appendChild(anchorLikeText);

    divMainContainer.appendChild(divUserInfoContainer);
    divMainContainer.appendChild(divMediaContainer);
    divMainContainer.appendChild(divInteractionContainer);

    divColumn.appendChild(divMainContainer);

    row.appendChild(divColumn);
}

function checkIfPostIsLikedByUser(postId, likeText) {
    fetch(`https://localhost:7045/api/posts/isliked/${postId}`)
        .then(res => res.json())
        .then(data => {
            if (data === false) {
                likeText.textContent = 'Like';
                likeText.style.color = 'white';
                likeText.innerHTML += ' <i class="fa-solid fa-thumbs-up"></i>';
            } else {
                likeText.textContent = 'Liked';
                likeText.style.color = 'green';
                likeText.innerHTML += ' <i class="fa-solid fa-thumbs-up"></i>';
            }
        })
        .catch(err => console.error(err));
}

function noMorePostsMessage() {
    let noMorePostsParagraph = document.createElement('p');
    noMorePostsParagraph.textContent = 'You have no more posts.';
    noMorePostsParagraph.classList.add('text-info', 'text-center');
    row.appendChild(noMorePostsParagraph);
}

function prepareForDelete(postId, event) {
    const deleteButton = document.getElementById('delete-button');
    const element = event.currentTarget.parentElement.parentElement.parentElement.parentElement.parentElement;
    let isAlertShowed = false;

    deleteButton.addEventListener('click', () => {
        fetch(`https://localhost:7045/api/posts/${postId}`, {
            method: 'DELETE'
        })
            .then(res => console.log(res))
            .then(() => {
                element.remove();
                if (!isAlertShowed) {
                    toastr.options = {
                        positionClass: "toast-bottom-right"
                    };

                    toastr.success('Post deleted successfully!');

                    isAlertShowed = true;
                }
            })
            .catch(err => console.error(err));
    });
}
