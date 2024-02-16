const maxSymbols = 450;
const textarea = document.getElementById('textarea-symbols');
const textareaCounter = document.getElementById('textarea-counter');

let currentLength = textarea.value.length;
textareaCounter.textContent = `${currentLength} / ${maxSymbols}`;

textarea.addEventListener('input', () => {
    currentLength = textarea.value.length;
    textareaCounter.textContent = `${currentLength} / ${maxSymbols}`;
});