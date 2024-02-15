let counter = 1;
let scrolled = false;
let isOnlyOnePostForPage = true;
const currentUserId = document.getElementById('user-id').textContent;
const posts = document.getElementById('posts');
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
                isOnlyOnePostForPage = false;
            } else {
                noMorePostsMessage();
            }

            //If only one image is displayed the text is not being shown since
            //it activates on scroll. That's why I use this if statement over here
            if (data.length == 1 && isOnlyOnePostForPage) {
                console.log('If 1 no posts');
                noMorePostsMessage();
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
    let mainContainer = document.createElement('div');
    mainContainer.classList.add('border', 'border-dark', 'border-1', 'mb-2', 'd-flex', 'flex-column', 'rounded-3', 'bg-dark', 'text-white');
    mainContainer.style.width = '500px';
    mainContainer.style.height = '600px';

    let textContainer = document.createElement('div');
    textContainer.classList.add('h-25');
    textContainer.classList.add('p-2');

    let postPublishDate = document.createElement('span');
    postPublishDate.style.color = 'gray';
    postPublishDate.textContent = date;

    textContainer.appendChild(postPublishDate);

    let mediaContainer = document.createElement('div');
    mediaContainer.classList.add('p-2');
    mediaContainer.style.height = '67%';

    let index = path.indexOf('.');
    let pathExtension = path.substr(index);

    let media = '';

    let isImage = false;
    if (imageFormats.includes(pathExtension)) {
        isImage = true;
        media = document.createElement('img');
        media.src = window.location.origin + `/${path}`;
        media.classList.add('w-100', 'h-100', 'rounded-3');
        media.style.objectFit = 'cover';

        let anchorMedia = document.createElement('a');
        anchorMedia.href = window.location.origin + `/${path}`;
        anchorMedia.target = '_blank';
        anchorMedia.appendChild(media);

        mediaContainer.appendChild(anchorMedia);
    } else {
        media = document.createElement('video');
        media.setAttribute('controls', '');
        media.classList.add('w-100', 'h-100', 'rounded-3');

        let source = document.createElement('source');
        source.src = window.location.origin + `/${path}`;

        media.appendChild(source);
    }

    let divUserInfo = document.createElement('div');
    divUserInfo.classList.add('user-info', 'mb-2');

    let divNamesAndOperationsFlex = document.createElement('div');
    divNamesAndOperationsFlex.classList.add('flex-container', 'd-flex', 'justify-content-between', 'align-items-center');

    let divOperations = document.createElement('div');

    let paragraphNames = document.createElement('p');
    paragraphNames.textContent = `${firstName} ${lastName}`;
    paragraphNames.style.marginBottom = '0';

    if (userId == currentUserId) {
        let editLink = document.createElement('a');
        editLink.innerHTML = 'Edit <i class="fa-regular fa-pen-to-square"></i>';
        editLink.href = window.location.origin + `/Post/Edit/${id}`;
        editLink.classList.add('btn', 'btn-primary', 'btn-sm');
        divOperations.appendChild(editLink);

        let button = document.createElement('button');
        button.type = 'button';
        button.classList.add('btn', 'btn-danger', 'btn-sm', 'ms-2');
        button.setAttribute('data-bs-toggle', 'modal');
        button.setAttribute('data-bs-target', '#deleteModal');
        button.textContent = 'Delete ';
        button.innerHTML += '<i class="fa-regular fa-trash-can"></i>';

        button.addEventListener('click', (e) => {
            prepareForDelete(id, e);
        });

        divOperations.appendChild(button);

    }

    divNamesAndOperationsFlex.appendChild(paragraphNames);
    divNamesAndOperationsFlex.appendChild(divOperations);

    let spanUsername = document.createElement('span');
    spanUsername.textContent = '@' + username;
    spanUsername.style.color = 'gray';

    let paragraphText = document.createElement('p');
    paragraphText.textContent = text;

    divUserInfo.appendChild(divNamesAndOperationsFlex);

    divUserInfo.appendChild(spanUsername);

    textContainer.appendChild(divUserInfo);
    textContainer.appendChild(paragraphText);

    if (!isImage) {
        mediaContainer.appendChild(media);
    }

    //Like posts implementation

    let likeContainer = document.createElement('div');
    likeContainer.classList.add('p-2', 'text-center');

    let likeText = document.createElement('a');
    likeText.style.cursor = 'pointer';
    likeText.classList.add('text-decoration-none');

    checkIfPostIsLikedByUser(id, likeText);

    likeText.addEventListener('click', () => {

        fetch(`https://localhost:7045/api/posts/like/${id}`, {
            method: 'POST'
        })
            .then(() => checkIfPostIsLikedByUser(id, likeText))
            .catch(err => console.error(err));
    });

    likeContainer.appendChild(likeText);
    //

    mainContainer.appendChild(textContainer);
    mainContainer.appendChild(mediaContainer);
    mainContainer.appendChild(likeContainer);

    posts.appendChild(mainContainer);
}

function checkIfPostIsLikedByUser(postId, likeText) {
    console.log('Test');
    fetch(`https://localhost:7045/api/posts/isliked/${postId}`)
        .then(res => res.json())
        .then(data => {
            console.log(data);
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
    noMorePostsParagraph.classList.add('text-info');
    posts.appendChild(noMorePostsParagraph);
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
