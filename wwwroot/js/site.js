/**
 * Script para el manejo de tooltips
 */

const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))

// Manejador de clics para alternar la visibilidad del tooltip
tooltipTriggerList.forEach(tooltipElement => {
    tooltipElement.addEventListener('click', function () {
        const tooltipInstance = bootstrap.Tooltip.getInstance(this);
        if (this.hasAttribute('aria-describedby')) {
            tooltipInstance.hide();
        }
    });
});

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
new DataTable("#tablaInmuebles", options);
new DataTable("#tablaContratos", options);
new DataTable("#tablaTipoInmueble", options);
new DataTable("#tablaUsuarios", options);
new DataTable("#tablaPagos", options);