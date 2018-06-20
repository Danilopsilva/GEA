/*
 Author: DaniloPereira
Cria controllador, onde vai estar todas as funções 
 
 */

employeeApp.controller('EmployeeController', function ($scope, employeeService) {

    getEmployees();
    getCompanies();

//lista todos os funcionarios cadastrados
    function getEmployees() {
        $scope.Employees = null;
        employeeService.getEmployees().then(result => {
            $scope.Employees = result.data;
            }, function () {
                alert("Um erro ocorreu");
            })
    }

    function getCompanies() {
        $scope.Companies = null;
        employeeService.getCompanies().then(result => {
            $scope.Companies = result.data;
        }, function () {
            alert("Um erro ocorreu");
        })
    }

    // cria objeto funcionário para salvar.
    $scope.saveEmployee = function () {
        let saveEmployeeInput = {
            id: $scope.id,
            name: $scope.name,
            cpf: $scope.cpf,
            phone: $scope.phone,
            companyId: $scope.company.Id,
        };

        let employee = employeeService.saveEmployee(saveEmployeeInput);

            //valida se funcionario foi cadastrado e retorma mensagens ao usuário.
        employee.then(result => {
                if (result.data.success === true) {
                    getEmployees();
                    $scope.clearFields();
                } else if (result.data)
                    alert(result.data.failures);
            });
    }

    $scope.updateEmployeeId = function(employee) {
        $scope.employeeId = employee.Id;
        $scope.employeeName = employee.Name;
        $scope.employeeCpf = employee.Cpf;
    }

    $scope.updateEmployee = function() {
        let employeeInput = {
            Id: $scope.employeeId,
            Name: $scope.employeeName,
            Cnpj: $scope.employeeCnpj,
        };

        let employeeData = employeeService.updateEmployee(employeeInput);
        employeeData.then(result => {
            if (result.data.success === true) {
                getEmployees();
                $scope.clearFields();
            }
            else {
                alert(result.data.failures);
            }
        }, function () {
            alert("Ocorreu um erro ao tentar atualizar o funcionário!");
        });
    }

    $scope.deleteEmployee = function(id){
        var deleteData = employeeService.deleteEmployee(id);
            deleteData.then(result => {
                if(result.data.success ===true)
                    getEmployees();
                else{
                    alert("Funcionário não removido ");
                }
            }, function(){
                alert("Ocorreu ao tentar Remover o Funcionário");
            });
    }

    $scope.deleteEmployeeId = function(employee) {
        $scope.employeeId = employee.Id;
        $scope.employeeName = employee.Name;
    }


    // Funcão responsável para limpar os campos.
    $scope.clearFields = function () {
        $scope.id = null;
        $scope.name = null;
        $scope.cnpj = null;
    }
});
