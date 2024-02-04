let counter = 1;
let scrolled = false;
const currentUserId = document.getElementById('user-id').textContent;
const posts = document.getElementById('posts');
const loadPostsButton = document.getElementById('load-posts-button');
const imageFormats = ['.gif', '.jpg', '.jpeg', '.png'];
const videoFormats = ['.mpg', '.mp2', '.mpeg', '.mpe', '.mpv', '.mp4'];

loadPosts();

window.addEventListener('scroll', () => {
    //console.log('scrollY: ' + window.scrollY);
    //console.log('innerHeight: ' + window.innerHeight);
    //console.log('document scrollHeight: ' + document.documentElement.scrollHeight);
    if (Math.ceil(window.scrollY + window.innerHeight) >= document.documentElement.scrollHeight && !scrolled) {
        scrolled = true;
        /*console.log('NEED TO LOAD POSTS!!!');*/
        loadPosts();
    }
});

function loadPosts() {
    /*console.log('LOADING POSTS...')*/
    fetch(`https://localhost:7045/api/posts?counter=${counter}`)
        .then(res => res.json())
        .then(data => {
            console.log(data);
            console.log(data.length);
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
        /*media.style.objectFit = 'cover';*/

        let source = document.createElement('source');
        source.src = window.location.origin + `/${path}`;

        media.appendChild(source);
    }

    let divUserInfo = document.createElement('div');
    divUserInfo.classList.add('user-info', 'mb-2');

    let paragraphNames = document.createElement('p');
    paragraphNames.textContent = `${firstName} ${lastName}`;
    paragraphNames.style.marginBottom = '0';

    // Edit/Delete Post  Logic
    if (userId == currentUserId) {
        let editLink = document.createElement('a');
        editLink.innerHTML = 'Edit <i class="fa-regular fa-pen-to-square"></i>';
        editLink.href = window.location.origin + `/Post/Edit/${id}`;
        editLink.classList.add('btn', 'btn-primary');
        paragraphNames.appendChild(editLink);

        paragraphNames.innerHTML +=
            `<button type="button" class="btn btn-danger" data-bs-toggle="modal" data-bs-target="#exampleModal" onclick="prepareForDelete(${id})">
                Delete <i class="fa-regular fa-trash-can"></i>
             </button>`;
    }
    //

    let spanUsername = document.createElement('span');
    spanUsername.textContent = '@' + username;
    spanUsername.style.color = 'gray';

    let paragraphText = document.createElement('p');
    paragraphText.textContent = text;

    divUserInfo.appendChild(paragraphNames);
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
    noMorePostsParagraph.textContent = 'No more posts in the database! Please try again later.';
    noMorePostsParagraph.classList.add('text-info');
    posts.appendChild(noMorePostsParagraph);
}

function prepareForDelete(postId) {
    const form = document.getElementById('delete-form');
    form.setAttribute('action', `/Post/Delete/${postId}`);
}
