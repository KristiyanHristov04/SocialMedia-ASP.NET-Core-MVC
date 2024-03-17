const chatContainer = document.getElementsByClassName('chat-container')[0];
const chatIcon = document.getElementsByClassName('chat-icon')[0];
const xMarkButton = document.getElementsByClassName('fa-xmark')[0];
const messagesList = document.getElementById('messages-list');
const scrollDownButton = document.getElementById('scroll-down');

messagesList.addEventListener('scroll', displayButtonToScrollDown);
scrollDownButton.addEventListener('click', scrollToBottom);

chatIcon.addEventListener('click', () => {
    console.log('Clicking on Message');
    chatContainer.classList.remove('d-none');
    scrollToBottom();
});

xMarkButton.addEventListener('click', () => {
    console.log('Clicking on X');
    chatContainer.classList.add('d-none');
});

function scrollToBottom() {
    messagesList.scrollTo(0, messagesList.scrollHeight);
}

function displayButtonToScrollDown() {
    if (Math.ceil(messagesList.scrollTop + messagesList.clientHeight) !== messagesList.scrollHeight) {
        scrollDownButton.style.visibility = 'visible';
    } else {
        scrollDownButton.style.visibility = 'hidden';
    }

    console.log('client height ' + messagesList.clientHeight);
    console.log('scroll height ' + messagesList.scrollHeight);
    console.log('scroll top ' + messagesList.scrollTop);
}