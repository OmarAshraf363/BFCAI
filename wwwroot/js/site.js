
function deleteIt(itemId, controler) {
    Swal.fire({

        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                position: "top-end",
                icon: "success",
                title: "Your work has been saved",
                showConfirmButton: false,
                timer: 1500
            }).then(() => {

                window.location.href = `/Admin/${controler}/Delete/${itemId}`;

            });
        }
    });
}

function allConfirm(id) {
    let form = document.getElementById(id);

    fetch(form.action, {
        method: form.method,
        body: new FormData(form),
        headers: { 'X-Requested-With': 'XMLHttpRequest' }
    }).then(response => response.json())
        .then(data => {
            if (!data.isvalid) {
                form.querySelectorAll("span.text-danger").forEach(span => {
                    span.innerHTML = "";
                });


                for (let fieldName in data.nameErrors) {
                    let field = document.querySelector(`#${id} #${fieldName}`);
                    if (field) {
                        let span = field.nextElementSibling;
                        if (span && span.classList.contains('text-danger')) {
                            console.log(data.nameErrors[fieldName])
                            span.innerHTML = data.nameErrors[fieldName].join("<br>");
                        }
                    }
                }
            } else {

                Swal.fire({
                    position: "top-end",
                    icon: "success",
                    title: "Your work has been Done",
                    showConfirmButton: false,
                    timer: 1500
                }).then(() => {
                    form.submit();
                });
            }
        });
}

function openModal(id, controllerName, modalname, actionName,areaName) {
    let url;
    if (id === null || id === undefined) {
        url = `/${areaName}/${controllerName}/${actionName}`;
    } else {
        url = `/${areaName}/${controllerName}/${actionName}/${id}`;
    }
    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
            $(`#${modalname} .modal-content`).html(data);
            $(`#${modalname}`).modal('show');
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}


function openAddToCourse(courseId, modalName) {
    $.ajax({
        url: `/Instructor/Instructor/UpsertCourseCurriculum`,
        type: 'GET',
        data: { courseId: courseId },
        success: function (data) {
            // تعيين المحتوى الجديد داخل المودال
            $(`#${modalName} .modal-content`).html(data);
            // عرض المودال
            $(`#${modalName}`).modal('show');
        },
        error: function (xhr, status, error) {
            // طباعة الخطأ إذا فشل الطلب
            console.error('Error fetching the modal content:', error);
        }
    });
}


function openAddToCourseRefOrVideo(curriculumId, bindId,modalName,area,action,controller,fromCourse) {
    let actionUrl = '';

    if (modalName === 'AddVideoModal') {
        actionUrl = `/Instructor/Instructor/UpsertCourseVideo`;
    } else if (modalName === 'AddReferenceModal') {
        actionUrl = `/Instructor/Instructor/UpsertReference`;
    } else {
        actionUrl = `/${area}/${controller}/${action}`
    }
    
    $.ajax({
        url: actionUrl,
        type: 'GET',
        data: {
            id: bindId || 0,
            curriculumId: curriculumId,
            fromCourseArea: fromCourse||null
        },
        success: function (data) {

            $(`#${modalName} .modal-content`).html(data);
            $(`#${modalName}`).modal('show');
          
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}


function openAssignmentModal(assignmentId) {
    $.ajax({
        url: `/instructor/assinment/GetAssignmentDetails`,
        type: 'GET',
        data: { id: assignmentId },
        success: function (data) {
            $('#assignmentDetailsContent').html(data);
            $('#assignmentDetailsModal').modal('show');
        },
        error: function () {
            alert('Failed to load assignment details.');
        }
    });
}

function openCourseCurriculum(courseId) {
    $.ajax({
        url: `/Instructor/Instructor/GetCourseCurriculum?courseId=${courseId}`,
        type: 'GET',
        success: function (data) {
            $('#curriculumModal .modal-body').html(data);
            $('#curriculumModal').modal('show');
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}