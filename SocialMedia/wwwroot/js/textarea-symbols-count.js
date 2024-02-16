const maxSymbols = 450;
const textarea = document.getElementById('textarea-symbols');
const textareaCounter = document.getElementById('textarea-counter');

textarea.addEventListener('input', () => {
    let currentLength = textarea.value.length;
    textareaCounter.textContent = `${currentLength} / ${maxSymbols}`;
});