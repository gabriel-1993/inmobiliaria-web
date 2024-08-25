/**
 *  Script para el manejo de DataTable
 */

let options = {
  language: {
    "emptyTable": "No hay datos disponibles en la tabla",
    "info": "Mostrando _START_ a _END_ de _TOTAL_ registros",
    "infoEmpty": "Mostrando 0 a 0 de 0 registros",
    "infoFiltered": "(Filtrado de _MAX_ registros en total)",
    "lengthMenu": "_MENU_ registros por página",
    "loadingRecords": "Cargando...",
    "processing": "Procesando...",
    "search": "Buscar:",
    "zeroRecords": "No se encontraron registros coincidentes"
  },
  columnDefs: [
    { className: 'dt-center', targets: '_all' }
  ],
  ordering: true,
  order: []
}

new DataTable("#tablaInquilinos", options);
new DataTable("#tablaPropietarios", options);
