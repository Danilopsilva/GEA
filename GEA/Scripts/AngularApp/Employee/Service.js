/*
 Author: DaniloPereira
 
 */

// Service responsável por listar todas os funcionários
employeeApp.service('employeeService', function ($http) {
    this.getEmployees = function () {
        return $http.get('/Employee/GetEmployees');
    }

    this.getCompanies = function () {
        return $http.get('/Employee/GetCompanies')
    }

    //Service responsavel por salvar novo funcionário
    this.saveEmployee = function(employee){
        var request = $http({
            method: 'post',
            url: '/Employee/SaveEmployee',
            data: employee
        });
        return request; 
    }

    //Service responsavel por editar um funcionário
    this.updateEmployee = function (employee) {
        var request = $http({
            method: 'post',
            url: '/Employee/UpdateEmployee',
            data: employee
        });
        return request;
    }

    //Service responsavel por editar uma empresa
    this.deleteEmployee = function (id) {
        return $http.post('/Employee/DeleteEmployee/'+ id);
    }
});