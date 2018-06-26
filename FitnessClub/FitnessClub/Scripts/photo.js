window.onload = function () {
    var video = document.getElementById('video'),
        canvas = document.getElementById('canvas'),
        context = canvas.getContext('2d'),
        photo = document.getElementById('photo')
    vendorUrl = window.URL || window.webkitURL;
    navigator.getMedia = navigator.getUserMedia ||
        navigator.webkitGetUserMedia ||
        navigator.mozGetUserMedia ||
        navigator.msGetUserMedia;
    navigator.getMedia({
        video: true,
        audio: false
    }, function (stream) {
        video.src = vendorUrl.createObjectURL(stream);
        video.play();
    }, function (error) {

        //an error occured
        // error code
    });
    document.getElementById('capture').addEventListener('click', function () {
        context.drawImage(video, 0, 0, 400, 300);
        photo.setAttribute('src', canvas.toDataURL('image/png'));
    });
    document.getElementById('upload').addEventListener('click', function () {

        saveImageAs();

    });

}

function saveImage() {

    // save image without file type
    /*
    var canvas = document.getElementById("canvas");
    document.location.href = canvas.toDataURL("image/png").replace("image/png", "image/octet-stream");
    */
    // save image as png
    var link = document.createElement('a');
    link.download = "test.png";
    link.href = canvas.toDataURL("image/png").replace("image/png", "image/octet-stream");;
    link.click();
}
function saveBase64AsFile(base64, fileName) {

    var link = document.createElement("a");

    link.setAttribute("href", base64);
    link.setAttribute("download", fileName);
    link.click();
}
function saveImageAs() {
    var imgOrURL;
    embedImage.src = photo.src;
    imgOrURL = embedImage;
    if (typeof imgOrURL == 'object')
        imgOrURL = embedImage.src;
    //window.win = open(imgOrURL);

    //setTimeout('win.document.execCommand("SaveAs")', 0);   
}



//ablakoskurva//document.location.href = canvas.toDataURL("image/png").replace("image/png", "image/octet-stream");

function create_img() {
    var x = document.createElement("IMG");
    x.src = canvas.toDataURL("image/png"); // this will generate base64 data


    document.getElementById("img_dispplay").innerHTML = "<img src='" + x.src + "' width='400' height='300' class='img-responsive'>";
    document.body.appendChild(x);
    //console.log(img.src);
}

/*
function base64_to_png ($img, $im_name)  {
    if ( ($fp = fopen($im_name , '.png', 'wb')) === false ) {
                return false;
        }

 $img = str_replace('data:image/png;base64,', '', $img); 	
    $img = str_replace(' ', '+', $img);
    if ( fwrite($fp, base64_decode($img)) === false ) {
                return false;
        }

    fclose($fp);
 return true;
}*/