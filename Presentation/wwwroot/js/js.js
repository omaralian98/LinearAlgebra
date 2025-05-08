function Render(elementId, equation, displayMode = false) {
    let element = document.getElementById(elementId);
    katex.render(equation, element, {
        throwOnError: true,
        displayMode: displayMode,
    });
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