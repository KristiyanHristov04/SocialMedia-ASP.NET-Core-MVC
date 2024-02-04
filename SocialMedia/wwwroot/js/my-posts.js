let counter = 1;
let scrolled = false;
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
                    createPost(postId, postPath, postText, postUserId, postFirstName, postLastName, postUsername);
                }
                counter++;
                scrolled = false;
            } else {
                noMorePostsMessage();
            }
        })
        .catch(err => console.error(err));
}

function createPost(id, path, text, userId, firstName, lastName, username) {
    let mainContainer = document.createElement('div');
    mainContainer.classList.add('border', 'border-dark', 'border-1', 'mb-2', 'd-flex', 'flex-column', 'rounded-3', 'bg-dark', 'text-white');
    mainContainer.style.width = '500px';
    mainContainer.style.height = '600px';

    let textContainer = document.createElement('div');
    textContainer.classList.add('h-25');
    textContainer.classList.add('p-2');

    let mediaContainer = document.createElement('div');
    mediaContainer.classList.add('h-75', 'p-2');

    let index = path.indexOf('.');
    let pathExtension = path.substr(index);

    let media = '';
    if (imageFormats.includes(pathExtension)) {
        media = document.createElement('img');
        media.src = window.location.origin + `/${path}`;
        media.classList.add('w-100', 'h-100', 'rounded-3');
        media.style.objectFit = 'cover';
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
        button.setAttribute('data-bs-target', '#exampleModal');
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

    mediaContainer.appendChild(media);

    mainContainer.appendChild(textContainer);
    mainContainer.appendChild(mediaContainer);

    posts.appendChild(mainContainer);
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
