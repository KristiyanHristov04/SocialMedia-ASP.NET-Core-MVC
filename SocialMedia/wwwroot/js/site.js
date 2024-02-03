// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let postsCounter = 0;
const posts = document.getElementById('posts');
const loadPostsButton = document.getElementById('load-posts-button');
const isImage = ['.gif', '.jpg', '.jpeg', '.png'];
const isVideo = ['.mpg', '.mp2', '.mpeg', '.mpe', '.mpv', '.mp4'];

loadPostsButton.addEventListener('click', () => {
    console.log('Testing');
    fetch(`https://localhost:7045/api/posts?counter=${postsCounter}`)
        .then(res => res.json())
        .then(data => {
            postsCounter++;
            for (let post of data) {
                let postId = post.id;
                let postPath = post.path;
                let postText = post.text;
                let postUserId = post.userId;
                createPost(postId, postPath, postText, postUserId);
            }
        });
});

function createPost(id, path, text, userId) {
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
    console.log(pathExtension);

    let media = '';
    if (isImage.includes(pathExtension)) {
        media = document.createElement('img');
        media.src = window.location.origin + `/${path}`;
        media.classList.add('w-100', 'h-100', 'rounded-3');
        media.style.objectFit = 'cover';
    } else {
        media = document.createElement('video');
        media.setAttribute('controls', '');
        media.classList.add('w-100', 'h-100', 'rounded-3');
        media.style.objectFit = 'cover';

        let source = document.createElement('source');
        source.src = window.location.origin + `/${path}`;

        media.appendChild(source);
    }

    let paragraphText = document.createElement('p');
    paragraphText.textContent = text;

    textContainer.appendChild(paragraphText);

    mediaContainer.appendChild(media);

    mainContainer.appendChild(textContainer);
    mainContainer.appendChild(mediaContainer);

    posts.appendChild(mainContainer);
}
