

const allowedImgExts = /(\.jpg|\.jpeg|\.png)$/i;
const allowedVidExts = /(\.mp4|\.avi|\.flv)$/i;

const allowedImageTypes = function (file) {
    if (!allowedImgExts.exec(file.name) || !file.type.includes('image')) {
        return false;   
    }
    return true;
}

const allowedVideoTypes = function (file) {
    if (!allowedVidExts.exec(file.name) || !file.type.includes('video')) {
        return false;
    }
    return true;
}

const fileLoad = function (event, sourceElId, outputElId) {
    const file = event.target.files[0];
    const fileType = file['type'];
    if (fileType.includes('image')) {
        if (!allowedImageTypes(file)) {
            clearElValue(sourceElId);
            toastr.error('only .jpg, .jpeg, and .png allowed', 'Error');
            return false;
        }
        else {
            loadFL(outputElId, event.target.files[0]);
            return true;
        }
    }
    else if (fileType.includes('video')) {
        if (!allowedVideoTypes(file)) {
            clearElValue(sourceElId);
            toastr.error('only .mp4, .avi, and .flv allowed', 'Error');
            return false;
        }
        else {
            loadFL(outputElId, event.target.files[0]);
            return true;
        }
    }
    else {
        toastr.error('not valid files', 'Error');
        return false;
    }
}

const loadFL = function (outputElId, file) {
    const output = document.getElementById(outputElId);
    output.src = URL.createObjectURL(file);
    output.onload = function () {
        URL.revokeObjectURL(output.src) // free memory
    }
}

const clearElValue = function (elId) {
    let el = document.getElementById(elId);
    el.value = '';
    if (el.src) {
        el.src = '';
    }
}
const initSelect2 = function () {
    $('.select2bs4').select2({
        theme: 'bootstrap4'
    });
}

const uploadFileAjax = function (requestUrl,formData) {
    $.ajax({
        type: 'POST',
        url: requestUrl,
        data: formData,
        contentType: false,
        processData: false,
        enctype: 'multipart/form-data',
        success: function (r) {
            if (r.isSuccess) {
                toastr.success(r.message, 'Success');
                //window.location.reload();
            }
            else if (!r.isSuccess) {
                toastr.error(r.message, 'Error');
            }
        },
        error: function (xhr, status, error) {
            console.log(error)
        }
    });
}

const isValidTempOrThumbFile = function (file, elId) {
    if (file === undefined || file === null) {
        toastr.error('selected file is undefined', 'Error');
        return false;
    }

    const fileType = file['type'];
    if (fileType.includes('image')) {
        if (!allowedImageTypes(file)) {
            toastr.error('only .jpg, .jpeg, and .png allowed', 'Error');
            clearElValue(elId);
            return false;
        }
    }
    else if (fileType.includes('video')) {
        if (!allowedVideoTypes(file)) {
            toastr.error('only .mp4, .avi, and .flv allowed', 'Error');
            clearElValue(elId);
            return false;
        }
    }
    else {
        toastr.error('not valid files', 'Error');
        return false;
    }
    return true;
}

const slugify = (text) => {
    return text
        .toString()                   // Cast to string (optional)
        .normalize('NFKD')            // The normalize() using NFKD method returns the Unicode Normalization Form of a given string.
        .toLowerCase()                // Convert the string to lowercase letters
        .trim()                       // Remove whitespace from both sides of a string (optional)
        .replace(/\s+/g, '-')         // Replace spaces with -
        .replace(/[^\w\-]+/g, '')     // Remove all non-word chars
        .replace(/\_/g, '-')           // Replace _ with -
        .replace(/\-\-+/g, '-')       // Replace multiple - with single -
        .replace(/\-$/g, '');         // Remove trailing -
}

const toTitleCase = (phrase) => {
    return phrase
        .toLowerCase()
        .split(' ')
        .map(word => word.charAt(0).toUpperCase() + word.slice(1))
        .join(' ');
};

const textCaseTypes = {
    AsItis: 1,
    TitlCase: 2
}

const copyText = function (from, to, appendStr = '', caseType = textCaseTypes.AsItis) {
    let fromVal = $(from).val();

    if (appendStr !== '') {
        fromVal = fromVal.concat(appendStr);
    }

    if (caseType == textCaseTypes.AsItis) {
        $(to).val(fromVal);
    }
    else if (caseType == textCaseTypes.TitlCase) {
        $(to).val(toTitleCase(fromVal));
    }        
}

const copySlugify = function (from, to, appendStr = '', caseType = textCaseTypes.AsItis) {
    let fromVal = slugify($(from).val());

    if (appendStr !== '') {
       fromVal = fromVal.concat(appendStr);
    }

    if (caseType == textCaseTypes.AsItis) {
        $(to).val(fromVal);
    }
    else if (caseType == textCaseTypes.TitlCase) {
        $(to).val(toTitleCase(fromVal));
    }
}

const onDelete = function (IdVal, delUrl) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'GET',
                url: delUrl,
                data: { 'Id': IdVal },
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (r) {
                    window.location.reload();
                },
                error: function (xhr, status, error) {
                    console.log(error)
                }
            });
        }
    })
}