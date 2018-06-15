/*
 Author: DaniloPereira
Cria controllador, onde vai estar todas as funções 
 
 */


//lista todas as empresas cadastradas
companyApp.controller('CompanyController', function ($scope, companyService) {

    getCompanies();

    function getCompanies() {
        $scope.Companies = null;
            companyService.getCompanies().then(result => {
                $scope.Companies = result.data;
            }, function () {
                alert("Um erro ocorreu");
            })
    }

    // cria objeto empresa para salvar.
    $scope.saveCompany = function () {
        let saveCompanyInput = {
            id: $scope.id,
            name: $scope.name,
            cnpj: $scope.cnpj
        };

        let comány = companyService.saveCompany(saveCompanyInput);

        //valida se empresa foi cadastrada e retorma mensagens ao usuário.
        comány.then(result => {
            if (result.data.success === true) {
                alert("Empresa Adicionada com Sucesso.");
                getCompanies();
                $scope.clearFields();

            } else if (result.data)
                        alert(result.data.failures); 
        });
    }
    // Funcão responsável para limpar os campos.
    $scope.clearFields = function () {
        $scope.id = null;
        $scope.name = null;
        $scope.cnpj = null;
    }
});
