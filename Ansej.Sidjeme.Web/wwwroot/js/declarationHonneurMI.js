function initDataTable(selector, url) {
      
      var dtTable = $(selector).DataTable({

          "language": {
              "emptyTable": "لا توجد بيانات متاحة",
              "info": "عرض _START_ الى _END_ من _TOTAL_ entrées",
              "infoEmpty": "عرض 0 إلى 0 من 0 إدخالات",
              "infoFiltered": "(تمت تصفيتها من إجمالي _MAX_ )",
              "infoPostFix": "",
              "thousands": ",",
              "SHOW": "عرض",
              "lengthMenu": "عرض _MENU_ ",
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
                    },
                   
                {
                  "data": null, "searchable": false, "sortable": false,
                  "render": function (data, type, row, meta) {



                      var markup =
                          "<div class='col-md-3'>" +
                          //    "<button class='btn btn-outline-info dropdown-toggle' type='button' data-bs-toggle='dropdown' aria-expanded='false'> اختيار </button >" +
                          //    "<ul class='dropdown-menu' aria-labelledby='dropdownMenuButton'>" +
                          "<a class='call' style='display: flex; cursor: pointer;'  data-url=\"/DeclarationHonneurMI/Details/?requestId=" + row.id + "\"' >تفاصيل<i class='fa fa-list-alt'></i></a> " +
                          "<a class='call' style='display: flex;'  href=/DeclarationHonneurMI/Edit/?requestId=" + row.id + "> تحديث <i class='fa fa-pencil-alt'></i></a> " +
                          "<a class='call' style='display: flex; cursor: pointer;' data-url=\"/DeclarationHonneurMI/Notification/?requestId=" + row.id + "\"' >اخطار<i class='fa fa-mail-bulk'></i></a>" +
                          "<a class='call' style='display: flex; cursor: pointer;' data-url=\"/DeclarationHonneurMI/Treatment/?requestId=" + row.id + "\"' >معالجة<i class='fa fa-pencil-alt'></i></a>";

                      //"</ul>" +

                      if (row.granted === true)
                          markup = markup + "<a class='call' style='display: flex; cursor: pointer;' data-url=\"/DeclarationHonneurMI/TreatmentMC/?requestId=" + row.id + "\"' >رد الوزارة<i class='fa fa-pencil-alt'></i></a> </div >";
                      else
                          markup = markup + "</div >";
                      
                      return markup;
                  }
                }
                   ],
               dom: 'lBfrtip',
               buttons: [
                   //{
                       
                   //    extend: 'excelHtml5',
                   //    title: ' قائمة التصريحات',
                   //    text: 'إرسال إلى EXCEL',
                       
                   //    //exportOptions: {
                   //    //    columns: [1, 2, 3,4, 5,6,7,8,9]
                   //    //}
                   //     exportOptions: {
                   //        columns: ':visible' // exporte toutes les colonnes visibles avec leurs intitulés
                   //    }
                   //},
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

