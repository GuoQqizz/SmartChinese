Number.prototype.lenWithZero = function (length) {
    return (Array(length).join('0') + this).slice(-length);
}
