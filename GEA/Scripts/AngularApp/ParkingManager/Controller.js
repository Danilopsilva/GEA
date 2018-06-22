/*
 Author: DaniloPereira
Cria controllador, onde vai estar todas as funções 
 
 */

parkingApp.controller('ParkingManagerController', function ($scope, parkingService) {

    getParking();

//lista todos os funcionarios cadastrados
    function getParking() {
        $scope.Parking = null;
        parkingService.getParkingManager().then(result => {
            $scope.Parking = result.data;
            }, function () {
                alert("Um erro ocorreu");
            })
    }
});
