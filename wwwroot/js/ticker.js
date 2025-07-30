window.onload = function () {
    var windowWidth = window.innerWidth;
    var elementWidth = 220;
    var numElToAppend = Math.ceil(windowWidth / elementWidth) * 1;
    var parentElSelector = '.image-ticker ul';
    var elSelector = 'li';
    var parentEl = document.querySelector(parentElSelector);
    var itemEls = parentEl.querySelectorAll(elSelector);
    if (numElToAppend > itemEls.length) {
        var numElToAppendFraction = numElToAppend / itemEls.length;
        var fullRepeat = ~~numElToAppendFraction;
        var partialRepeat = Math.ceil((numElToAppendFraction - fullRepeat) * 10);
    }
    for (var i = 0; i < fullRepeat; i++) {
        for (var j = 0; j < itemEls.length; j++) {
            parentEl.appendChild(itemEls[j].cloneNode(true));
        }
    }
    var itemsToAppend = Array.prototype.slice.call(itemEls, 0, partialRepeat);
    for (var i = 0; i < itemsToAppend.length; i++) {
        console.log(itemsToAppend[i]);
        parentEl.appendChild(itemsToAppend[i].cloneNode(true));
    }

}