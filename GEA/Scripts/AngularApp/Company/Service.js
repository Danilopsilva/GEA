﻿/*
 Author: DaniloPereira
 
 */


// Service responsável por listar todas as empresas
companyApp.service('companyService', function ($http) {
    this.getCompanies = function () {
        return $http.get('/Company/GetCompanies');
    }

    //Service responsavel por salvar nova empresa
    this.saveCompany = function(company){
        var request = $http({
            method: 'post',
            url: '/Company/SaveCompany',
            data: company
        });
        return request; 
    }
});