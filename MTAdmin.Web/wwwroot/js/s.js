const sinitSelect2 = function () {
    
}

const truncateHeading = function (className) {
    var temps = document.getElementsByClassName(className);
    for (var i = 0; i < temps.length; i++) {
        temps[i].innerHTML = trancateText(temps[i].innerHTML, 40);        
    }
}

const trancateText = function (text, limit) {
    if (text.length > limit) {
        for (let i = limit; i > 0; i--) {
            if (text.charAt(i) === ' ' && (text.charAt(i - 1) != ',' || text.charAt(i - 1) != '.' || text.charAt(i - 1) != ';')) {
                return text.substring(0, i) + '...';
            }
        }
        return text.substring(0, limit) + '...';
    }
    else
        return text;
};

const appendLoadingCard = function (id, noOf = 1) {    
    let i = 0;
    while (i < noOf) {
        i = i+1;
        $('#' + id).append(`<div class="col-md-4 loading-skeleton">
                <div class="card card-outline card-primary">
                    <div class="card-header">
                        Loading...
                    </div>
                    <div class="card-body">
                        <div class="embed-responsive embed-responsive-16by9">
                            <div class="card-img-top embed-responsive-item"></div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <div class="row">
                            <div class="col-12">
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-12">
                                ...
                            </div>
                        </div>
                    </div>
                </div></div>`);        
    }
}
const removeLoadingEl = function () {
    $('.loading-skeleton').remove();
}