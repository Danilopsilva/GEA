/*
 Author: DaniloPereira
 
 */

// Service responsável por listar todos os dados
parkingApp.service('parkingService', function ($http) {
    this.getParkingManager = function () {
        return $http.get('/ParkingManager/GetParkingManager');
    }

    this.getCParking = function () {
        return $http.get('/ParkingManager/GetParkingManager')
    }

});