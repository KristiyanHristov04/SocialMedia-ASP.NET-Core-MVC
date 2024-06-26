﻿let counter = 1;
let scrolled = false;
let isFirstLoad = true;
let noMorePostsMessageShowed = false;
let username = document.getElementById('current-username').textContent;
const row = document.getElementsByClassName('row')[0];
const imageFormats = ['.gif', '.jpg', '.jpeg', '.png'];
const videoFormats = ['.mp4'];

let protocol = window.location.protocol;
let hostname = window.location.hostname;
let port = window.location.port;
let fullPath = `${protocol}//${hostname}:${port}`;

loadPosts();

window.addEventListener('scroll', () => {
    if (Math.ceil(window.scrollY + window.innerHeight) >= document.documentElement.scrollHeight && !scrolled) {
        scrolled = true;
        loadPosts();
    }
});

function loadPosts() {
    fetch(`${fullPath}/api/posts/profile?counter=${counter}&username=${username}`)
        .then(res => res.json())
        .then(data => {
            if (counter !== 1) {
                isFirstLoad = false;
            }

            if (isFirstLoad && data.length === 0) {
                noMorePostsMessage(`No posts by ${username} in the database! Please try again later.`);
                noMorePostsMessageShowed = true;
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

                    createProfile(postId, postPath, postText, postUserId, result, postFirstName, postLastName, postUsername);
                }
                counter++;
                scrolled = false;
            } else {
                if (!noMorePostsMessageShowed) {
                    noMorePostsMessage(`No more posts by ${username} in the database! Please try again later.`);
                    noMorePostsMessageShowed = true;
                }
            }

            if (isFirstLoad && !noMorePostsMessageShowed) {
                console.log(window.innerHeight);
                console.log(document.documentElement.scrollHeight);

                if (window.innerHeight < document.documentElement.scrollHeight) {
                    console.log("Page has a vertical scroll bar");
                } else {
                    //noMorePostsMessage();
                    //noMorePostsMessageShowed = true;
                    loadPosts();
                }
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

function createProfile(id, path, text, userId, date, firstName, lastName, username) {
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

    let reportButton = document.createElement('a');
    reportButton.classList.add('report-button', 'ms-2');
    reportButton.setAttribute('data-bs-toggle', 'modal');
    reportButton.setAttribute('data-bs-target', '#reportModal');
    reportButton.innerHTML += '<i class="fa-solid fa-flag"></i>';

    reportButton.addEventListener('click', () => {
        prepareForReport(id);
    });

    divEditDeleteContainer.appendChild(reportButton);

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

        fetch(`${fullPath}/api/posts/like/${id}`, {
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
    fetch(`${fullPath}/api/posts/isliked/${postId}`)
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

function noMorePostsMessage(message) {
    let noMorePostsParagraph = document.createElement('p');
    noMorePostsParagraph.textContent = message;
    noMorePostsParagraph.classList.add('text-info', 'text-center');
    row.appendChild(noMorePostsParagraph);
}

function prepareForReport(postId) {
    const reportButton = document.getElementById('report-button');
    let isAlertShowed = false;

    reportButton.addEventListener('click', reportButtonClickHandler);
    function reportButtonClickHandler(e) {
        fetch(`${fullPath}/api/posts/report/${postId}`, {
            method: 'POST'
        })
            .then(() => {
                if (!isAlertShowed) {
                    toastr.options = {
                        positionClass: "toast-bottom-right"
                    };
                    toastr.success('Post reported successfully!');
                    isAlertShowed = true;
                }
                reportButton.removeEventListener('click', reportButtonClickHandler);
            })
            .catch(err => console.error(err))
    }
}
