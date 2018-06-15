// Cria módulo 
angular.module('CompanyApp', [])
    // Cria controllador, onde vai estar todas as funções 
    .controller('CompanyController', function ($scope, companyService) {
    $scope.Companies = null;
    companyService.GetCompanies().then(result => {
        $scope.Companies = result.data;
    }, function () {
        alert("Um erro ocorreu");
    });// Cria o service
}).factory('companyService', function ($http) {
    var fac = {};
    fac.GetCompanies = function () {
        return $http.get('/Company/GetCompanies');
    }
    return fac;
});
