document.addEventListener("DOMContentLoaded", function () {
    const imageInput = document.getElementById("imageInput");
    const imagePreview = document.getElementById("imagePreview");
    const maxWidth = 690;
    const maxHeight = 540;

    imageInput.addEventListener("change", function (e) {
        const file = e.target.files[0];
        if (file) {
            const reader = new FileReader();

            reader.onload = function (e) {
                const image = new Image();

                image.onload = function () {
                    let width = image.width;
                    let height = image.height;

                    if (width > maxWidth || height > maxHeight) {
                        // Calculate new dimensions while preserving the aspect ratio
                        if (width / height > maxWidth / maxHeight) {
                            width = maxWidth;
                            height = width * (image.height / image.width);
                        } else {
                            height = maxHeight;
                            width = height * (image.width / image.height);
                        }
                    }

                    const resizedCanvas = document.createElement("canvas");
                    resizedCanvas.width = width;
                    resizedCanvas.height = height;

                    const ctx = resizedCanvas.getContext("2d");
                    ctx.drawImage(image, 0, 0, width, height);

                    const resizedImage = new Image();
                    resizedImage.src = resizedCanvas.toDataURL();

                    imagePreview.innerHTML = "";
                    imagePreview.appendChild(resizedImage);
                };

                image.src = e.target.result;
            };

            reader.readAsDataURL(file);
        }
    });
});
