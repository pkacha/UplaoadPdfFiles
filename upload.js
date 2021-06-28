$(document).ready(function () {
  $("#btnUpload").click(function () {
    var formData = new FormData();
    var totalFiles = document.getElementById("file").files.length;
    for (var i = 0; i < totalFiles; i++) {
      var file = document.getElementById("file").files[i];

      formData.append("file", file);

      // Check file selected or not
      $.ajax({
        url: "http://localhost:30599/api/pdfFiles/2",
        type: "POST",
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
          if (response != 0) {
            alert("File uploaded successfully");
          } else {
            alert("Error uploading the file");
          }
        },
      });
    }
  });
});
