var Db = (function(){

    function Db(){
        this._db = [];
    }

    Db.prototype.addCar = function(car){
        this._db.push(car);
    };

    Db.prototype.deleteCar = function (index) {
        this._db.splice(index, 1);
    };

    Db.prototype.getAllCars = function () {
        return this._db;
    };

    Db.prototype.getCar = function (index) {
        return this._db[index];
    };

    return Db;
})();