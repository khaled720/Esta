$(function () {
    InitializeCkeEditor('editor_ar');
    InitializeCkeEditor('editor_en');
});

function Encode() {
    EncodeCkeditorValue('editor_ar');
    EncodeCkeditorValue('editor_en');
}