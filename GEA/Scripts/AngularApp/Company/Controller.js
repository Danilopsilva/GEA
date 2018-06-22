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
            cnpj: $scope.cnpj,
            phone: $scope.phone,
            email: $scope.email
        };

         let comány = companyService.saveCompany(saveCompanyInput);

            //valida se empresa foi cadastrada e retorma mensagens ao usuário.
            comány.then(result => {
                if (result.data.success === true) {
                    getCompanies();
                    $scope.clearFields();
                } else if (result.data)
                    alert(result.data.failures);
            });
    }

    $scope.updateCompanyId = function(company) {
        $scope.companyId = company.CompanyId;
        $scope.companyName = company.Name;
        $scope.companyCnpj = company.Cnpj;
        $scope.companyPhone = company.Phone;
        $scope.companyEmail = company.Email;
    }

    $scope.updateCompany = function() {
        let companyInput = {
            CompanyId: $scope.companyId,
            Name: $scope.companyName,
            Cnpj: $scope.companyCnpj,
            Phone: $scope.companyPhone,
            Email: $scope.companyEmail,
        };

        let comanyData = companyService.updateCompany(companyInput);
        comanyData.then(result => {
            if (result.data.success === true) {
                getCompanies();
                $scope.clearFields();
            }
            else {
                alert(result.data.failures);
            }
        }, function () {
            alert("Ocorreu um erro ao tentar atualizar a emrpesa!");
        });
    }

    $scope.deleteCompany = function(companyId){
        var deleteData = companyService.deleteCompany(companyId);
            deleteData.then(result => {
                if(result.data.success ===true)
                    getCompanies();
                else{
                    alert("Empresa não removida ");
                }
            }, function(){
                alert("Ocorreu ao tentar Remover Empresa");
            });
    }

    $scope.deleteCompanyId = function (company) {
        $scope.companyId = company.CompanyId;
        $scope.companyName = company.Name;
    }


    // Funcão responsável para limpar os campos.
    $scope.clearFields = function () {
        $scope.id = null;
        $scope.name = null;
        $scope.cnpj = null;
        $scope.phone = null;
        $scope.email = null;
    }
});
