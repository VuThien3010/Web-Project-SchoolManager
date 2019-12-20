function requestFindStudent(SortBy) {
    var info = $('#inputText').val();
    var searchBy = $('#SearchBy').val();
    var setData = $('#DataSearching');

    setData.html("");
    $.ajax({
        type: "Post",
        url: "/Teacher/Searching?SearchBy=" + searchBy + "&SearchValue=" + info + "&SortBy="+SortBy,
        contentType: "html",
        success: function (result) {
            if (result.length >= 0) {
                $.each(result, function (index, value) {
                    var data = '<tr>' +
                        '<td>' +
                        value.studentId
                        + '</td>'
                        + '<td>' +
                        value.studentName
                        + '</td>'
                        + '<td>' +
                        value.birthDate
                        + '</td>'
                        + '<td>' +
                        $.trim(value.grade)
                        + '</td>' +
                        '<td>' +
                        '<a href="/Teacher/EditGrade?studentId=' + value.studentId +'">Edit Grade</a>'+
                        '</td>' +
                        '</tr>';
                    setData.append(data);
                })
               
            }
            else {
             setData.append('<tr><td><b>No data match</b></td></tr>')
            }
          }
    })
}

function uploadDoc() {
    var file = $("#fileUpload").get(0).files;
    var description = $("#id-description").val();

    if (file.length === 0 || description === "") {
        alert("Please do not leave anything empty !");
        return false;
    }
    var data = new FormData();
    data.append("file", file[0]);
    data.append("description", description);
    $.ajax({
        type: "POST",
        url: "/Teacher/AddDocs",
        data: data,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response && response.Result === false) {
                alert("Fail!");
            }
            else {
                alert("Successfully!");
                window.location.href = "/Teacher/ListDocs/" + response.Result;
            }
        }
    })
}

function deleteDoc(docId) {
    var answer = window.confirm("Are you sure ?")
    if (answer) {
        var data = { "docId": docId };
        $.post("/Teacher/DeleteDoc",
            data,
            (response) => {
                if (response && response.Result === false) {
                    alert("Delete fail!");
                }
                else {
                    alert("Delete successfully!");
                    window.location.href = "/Teacher/ListDocs/" + response.Result;
                }
            }
        )
    }
    else {
        return false;
    }
}

function editGrade() {
    var courseId = $("#id-courseId").val;
    var studentId = $("#id-studentId").val();
    var studentName = $("#id-StudentName").val();
    var grade = $("#id-Grade").val();
    if (grade > 10 || grade < 0) {
        alert("Incorrect grade");
        return false;
    }
    var data = new FormData();
    data.append("studentId", studentId);
    data.append("Grade", grade);
    $.ajax({
        type: "POST",
        url: "/Teacher/UpdateGrade",
        data: data,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response && response.Result === false) {
                alert("Fail!");
            }
            else {
                alert("Successfully!");
                window.location.href = "/Teacher/ListStudents/" + response.Result;
            }
        }
    })
}

function editDoc() {
    var file = $("#fileUpload").get(0).files;
    var description = $("#id-description").val();
    var docId = $("#id-docId").val();
    var data = new FormData();
    data.append("docId", docId);
    data.append("file", file[0]);
    data.append("description", description);
    $.ajax({
        type: "POST",
        url: "/Teacher/EditDoc",
        data: data,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response && response.Result === false) {
                alert("Fail!");

            }
            else {
                alert("Successfully!");
                window.location.href = "/Teacher/ListDocs/" + response.Result;
            }
        }
    })
}

