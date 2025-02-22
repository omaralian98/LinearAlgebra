function ReplaceElement(element, text) {
    if (element && element.parentNode) {
        MathJax.typesetClear([element])
        element.innerHTML = text;
        MathJax.typeset([element]);
    }
}

function GetElementText(element) {
    if (element) {
        return element.innerHTML;
    }
}

function ReplaceElementById(elementId, text) {
    var element = document.getElementById(elementId);
    ReplaceElement(element, text);
}

function ScrollToElement(elementId) {
        var element = document.getElementById(elementId);
    if (element) {
        element.scrollIntoView({
            behavior: 'smooth'
        });
    }
}

function ChangeUrl(url) {
    history.pushState(null, '', url);
}