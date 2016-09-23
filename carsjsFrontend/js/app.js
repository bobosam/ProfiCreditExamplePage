var app =(function () {
    var db = new Db();
    var car = new Car();

     $.ajax({
         url: 'http://localhost:38849/api/cars',
         dataType: 'text',
         type: 'get',
         contentType: 'application/x-www-form-urlencoded',
         success: function( data, textStatus, jQxhr ){
                 var arr = JSON.parse(data);
             for (var i = 0; i < arr.length; i++) {
                 var car = new Car();
                 car.setId(arr[i].id);
                 car.setModel(arr[i].model);
                 car.setYear(arr[i].year);car.setColor(arr[i].color);
                 db.addCar(car)
             }
                 $('#response pre').html( textStatus );
         },
         error: function( jqXhr, textStatus, errorThrown ){
             $('#response pre').html( textStatus );
         }
     });

    writeTable();

    var addForm = document.getElementById('add-form');
    addForm.onsubmit = function (e) {
       e.preventDefault();


        $.ajax({
            url: 'http://localhost:38849/api/cars',
            dataType: 'text',
            type: 'post',
            contentType: 'application/x-www-form-urlencoded',
            data: $(this).serialize(),
            success: function( data, textStatus, jQxhr ){
                var newCar = new Car();
                var dataObject = JSON.parse(data);
                newCar.setId(dataObject.id);
                newCar.setModel(dataObject.model);
                newCar.setYear(dataObject.year);
                newCar.setColor(dataObject.color);
                db.addCar(newCar);
                writeTable();
                $('#response pre').html( textStatus );
            },
            error: function( jqXhr, textStatus, errorThrown ){
                $('#response pre').html( textStatus );
            }
        });

        this.reset();
    };

   function writeTable() {
        var data = db.getAllCars();
        var currentTable = document.createElement('table');
        var table = document.getElementById('table');
        table.innerHTML = '';

        var trh = document.createElement('tr');
        var thModel = document.createElement('th');
        thModel.appendChild(document.createTextNode('Model'));
        trh.appendChild(thModel);
        var thYear = document.createElement('th');
        thYear.appendChild(document.createTextNode("Year"));
        trh.appendChild(thYear);
        var thColor = document.createElement('th');
        thColor.appendChild(document.createTextNode('Color'));
        trh.appendChild(thColor);

        currentTable.appendChild(trh);

        for (var i = 0, tr, td1, td2, td3, td4, btn; i < data.length; i++) {
            tr = document.createElement('tr');
            td1 = document.createElement('td');
            td1.appendChild(document.createTextNode(data[i].getModel()));
            tr.appendChild(td1);
            td2 = document.createElement('td');
            td2.appendChild(document.createTextNode(data[i].getYear()));
            tr.appendChild(td2);
            td3 = document.createElement('td');
            td3.appendChild(document.createTextNode(data[i].getColor()));
            tr.appendChild(td3);
            td4 = document.createElement('td');
            btn = document.createElement('button');
            btn.appendChild(document.createTextNode('delete'));
            btn.setAttribute('onclick', 'app.deleteCar(this)');
            btn.setAttribute("class", "delete");
            td4.appendChild(btn);
            tr.appendChild(td4);

            currentTable.appendChild(tr);
        }

        table.appendChild(currentTable);
    }

    function deleteCar(x) {
        var data = db.getAllCars();
        var index = ($(x).closest('td').parent()[0].sectionRowIndex) - 1;

        var car = data[index];
        var carId = car._id;
        var urlString = 'http://localhost:38849/api/cars/' + carId;

        $.ajax({
            url: urlString ,
            type: 'DELETE',
            contentType: 'application/x-www-form-urlencoded',
            success: function( data, textStatus, jQxhr ){
               $('#response pre').html( textStatus );
            },
            error: function( jqXhr, textStatus, errorThrown ){
                $('#response pre').html( textStatus );
            }
        });

            db.deleteCar(index);

        app.writeTable();
    }

    return{
        writeTable: writeTable,
        deleteCar: deleteCar
   }
}());







