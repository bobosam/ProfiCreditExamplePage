var Car = (function(){
        var _id;

    function Car(model, year, color){
        this._model = model;
        this._year = year;
        this._color = color;
    }

    Car.prototype.getId = function(){
        return this._id;
    };

    Car.prototype.setId = function(id){
        this._id = id;
    };


    Car.prototype.getModel = function(){
        return this._model;
    };

    Car.prototype.setModel = function(newModel){
        this._model = newModel;
    };

    Car.prototype.getYear = function(){
        return this._year;
    };

    Car.prototype.setYear = function(newYear){
        this._year = newYear;
    };

    Car.prototype.getColor = function(){
        return this._color;
    };

    Car.prototype.setColor = function(newColor){
        this._color = newColor;
    };

    Car.prototype.toString = function(){
        return this._model + " " + this._year + " " + this._color;
    };

     return Car;
})();