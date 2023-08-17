document.addEventListener("DOMContentLoaded", function () {
    const imageInput = document.getElementById("imageInput");
    const imagePreview = document.getElementById("imagePreview");
    const maxWidth = 690;
    const maxHeight = 540;

    // Function to insert image byte array into SQLite database
    function insertImageIntoDatabase(imageByteArray) {
        const query = "INSERT INTO CardImg VALUES @cardImg";

        db.run(query, [imageByteArray], function (err) {
            if (err) {
                console.error('Error inserting image:', err.message);
            } else {
                console.log('Image inserted successfully');
            }
        });
    }

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

                    // Convert resized image to byte array and insert into database
                    const binaryImage = atob(resizedImage.src.split(',')[1]);
                    const byteArray = new Uint8Array(binaryImage.length);
                    for (let i = 0; i < binaryImage.length; i++) {
                        byteArray[i] = binaryImage.charCodeAt(i);
                    }
                    insertImageIntoDatabase(byteArray);
                };

                image.src = e.target.result;
            };

            reader.readAsDataURL(file);
        }
    });
});
