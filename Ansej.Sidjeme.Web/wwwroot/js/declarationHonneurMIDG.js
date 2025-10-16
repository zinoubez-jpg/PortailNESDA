function initDataTable(selector, url) {
      
      var dtTable = $(selector).DataTable({

          "language": {
              "emptyTable": "لا توجد بيانات متاحة",
              "info": "عرض _START_ الى _END_ من _TOTAL_ ",
              "infoEmpty": "عرض   0   إلى   0   من    0   إدخالات",
              "infoFiltered": "(تمت تصفيتها من إجمالي   _MAX_ )",
              "infoPostFix": "",
              "thousands": ",",
              "SHOW": "عرض",
              "lengthMenu": "عرض   _MENU_ ",
              "loadingRecords": "تحميل...",
              "processing": "تحميل",
              "search": "بحث:",
              "zeroRecords": "لم يتم العثور على سجلات مطابقة",
              "paginate": {
                  "first": "الأول",
                  "last": "الأخير",
                  "next": "التالي",
                  "previous": "السابق"
              },
          },

          "serverSide": true,
          "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]],
          "ajax": {
              url: url ,
              type: 'POST'
          },
         
            "columns":
            [
                    { "data": "id", "visible": false, "searchable": false },
                    { "data": "structureManager.nameAr", "visible": true, "searchable": true },
                    { "data": "projectNumber", "visible": true, "searchable": true },
                    
                    {
                        "data": null, "visible": true, "searchable": false, "render": function (data, type, row, meta) {
                            return todate(row.requestdate);

                        }
                    },
                    { "data": "firstlName", "visible": true, "searchable": true },
                    { "data": "lastName", "visible": true, "searchable": true },
                    { "data": "nin", "visible": true, "searchable": true },
                    { "data": "companyName", "visible": true, "searchable": true },
                    { "data": "typeOfNeed.designationAr", "visible": true, "searchable": true },

                    {
                        "data": null, "visible": true, "searchable": false, "render": function (data, type, row, meta) {
                            var markup;
                            etat = row.requestProcessed;
                            if (etat == true)
                                markup = "نعم";
                            else
                                markup = "لا";
                            return markup;

                        }
                    },
                    {
                        "data": null, "visible": true, "searchable": false, "render": function (data, type, row, meta) {
                            var markup;
                            etat = row.granted;
                            if (etat == true)
                                markup = "نعم";
                            else
                                markup = "لا";
                            return markup;

                        }
                    },
                   

                    {
                        "data": null, "visible": true, "searchable": false, "render": function (data, type, row, meta) {
                            var markup;
                            etat = row.garantedMC;
                            if (etat == true)
                                markup = "نعم";
                            else
                                markup = "لا";
                            return markup;

                        }
                    }
                   ],
               dom: 'lBfrtip',
          buttons: [

              {
                  extend: 'pdfHtml5',
                  title: 'قائمة التصريحات',
                  text: '📄 تصدير إلى PDF',
                  orientation: 'landscape',   // orientation: 'portrait' ou 'landscape'
                  pageSize: 'A4',             // A4, A3, etc.
                  exportOptions: {
                      columns: ':visible'
                  },
                  customize: function (doc) {
                      // Exemple : changer l’alignement et la taille de police
                      doc.styles.tableHeader.alignment = 'center';
                      doc.styles.tableHeader.fontSize = 12;
                      doc.styles.tableBodyOdd.alignment = 'center';
                      doc.styles.tableBodyEven.alignment = 'center';
                  }
              }
             
                   
               ]
           });
}

function todate(date) {
    if (date == null)
        return date;
    else {
        var tab = date.split("T");
        var newDate = tab[0].split("-");
        return newDate[0] + "/" + newDate[1] + "/" + newDate[2] ;
    }
}

