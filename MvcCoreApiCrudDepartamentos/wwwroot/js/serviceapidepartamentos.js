let url = "https://apicorecruddepartamentosacv.azurewebsites.net/";
function getDepartamentosAsync(callBack) { //"callBack" se puede llamar como queramos
    let request = "api/departamentos";
    $.ajax({
        url: url + request,
        type: "GET",
        dataType: "json",
        success: function (data) {
            console.log("leyendo...");
            callBack(data);
        }
    });
}

function convertDeptToJson(id, nombre, localidad) {
    let dept = new Object();
    dept.idDepartamento = id;
    dept.nombre = nombre;
    dept.localidad = localidad;
    var json = JSON.stringify(dept);
    return json;
}

function createDepartamentoAsync(id, nombre, localidad, callBack) {
    var json = convertDeptToJson(id, nombre, localidad);
    var request = "api/departamentos";
    $.ajax({
        url: url + request,
        type: "POST",
        data: json,
        contentType: "application/json",
        success: function (data) {
            console.log("insertado...");
            callBack();
        }
    });
}

function updateDepartamentoAsync(id, nombre, localidad, callBack) {
    var json = convertDeptToJson(id, nombre, localidad);
    var request = "api/departamentos";
    $.ajax({
        url: url + request,
        type: "PUT",
        data: json,
        contentType: "application/json",
        success: function (data) {
            console.log("actualizado...");
            callBack();
        }
    });
}

function deleteDepartamentoAsync(id, callBack) {
    let request = "api/departamentos/" + id;
    $.ajax({
        url: url + request,
        type: "DELETE",
        success: function (data) {
            console.log("borrado...");
            callBack();
        }
    });
}