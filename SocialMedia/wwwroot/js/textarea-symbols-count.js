const textarea = document.getElementById('textarea-symbols');
const textareaCounter = document.getElementById('textarea-counter');
const maxSymbols = textareaCounter.dataset.maxLength;

let currentLength = textarea.value.length;
textareaCounter.textContent = `${currentLength} / ${maxSymbols}`;

textarea.addEventListener('input', () => {
    currentLength = textarea.value.length;
    textareaCounter.textContent = `${currentLength} / ${maxSymbols}`;
});