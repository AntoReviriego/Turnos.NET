// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/**  VARIABLES  **/
var turnos;
var horarioDesde = '';
var horarioHasta = '';
var turnoSelecionado;

/**  FUNCIONES  **/
$(document).ready(function () {
    $('.sidenav').sidenav();
    $('.timepicker').timepicker();
    $('select').formSelect();
    alert();
    $('.modal').modal();
    obtenerTurnosYactualizar($('#IdMedico').val());
});

// desvance alerts
function alert() {
    setTimeout(function () {
        $(".alert").fadeOut("slow");
    }, 5000);
}

//Calendar 

// FULL CALENDAR
$('#IdMedico').change(function () {
    obtenerTurnosYactualizar(this.value);
});

function obtenerTurnosYactualizar(idMedico) {
    turnos = [];
    $.ajax({
        type: 'GET',
        url: 'Turno/ObtenerTurnos',
        data: { 'idMedico': idMedico },
        success: function (datos) {
            $.each(datos, function (item, value) {
                // iniciales en minúsculas.
                turnos.push({
                    idTurno: value.idTurno,
                    idPaciente: value.idPaciente,
                    idMedico: value.idMedico,
                    start: moment(value.fechaHoraInicio),
                    end: value.fechaHoraFin != null ? moment(value.fechaHoraFin) : null,
                    paciente: value.paciente, //objeto creado con LINQ en metodo de ObtenerTurnos en TurnoController
                });
            })
            console.log(turnos);
            generarCalendario(turnos);
        },
        error: function () { alert('Error al obtener turnos.'); }
    });
}


function generarCalendario(turnos) {
    turnoSelecionado = [];
    // Obtengo horarios desde los metodos creado en el controlador médico.
    $.ajax({
        type: 'GET',
        url: 'Medico/ObtenerAtencionDesde',
        data: { 'IdMedico': $('#IdMedico').val() },
        async: false,
        success: function (resultado) { horarioDesde = resultado; },
        error: function () { alert('Error al traer los horarios del médico'); }
    });

    $.ajax({
        type: 'GET',
        url: 'Medico/ObtenerAtencionHasta',
        data: { 'idMedico': $('#IdMedico').val() },
        async: false,
        success: function (resultado) { horarioHasta = resultado; },
        error: function () { alert('Error al traer los horarios del médico'); }
    });

    $('#calendar').fullCalendar('destroy');

    $('#calendar').fullCalendar({
        contentHeight: 'auto', // alto automatico
        defaultDate: new Date(),
        slotLabelFormat: 'HH:mm',
        defaultView: 'agendaWeek',
        header: {
            left: 'prev, next today',
            center: 'title',
            right: 'month, agendaWeek, agendaDay',
        },
        slotDuration: '00:30',
        minTime: horarioDesde,
        maxTime: horarioHasta,
        nowIndicator: true,
        allDaySlot: false,
        selectable: true,
        eventLimit: true,
        events: turnos,
        select: function (fechaHoraInicio, fechaHoraFin) {
            turnoSelecionado = {
                idTurno: 0,
                start: fechaHoraInicio,
                end: fechaHoraFin
            };
            abrirPopUp(turnoSelecionado.start, turnoSelecionado.end, turnoSelecionado.idTurno);
        },
        eventClick: (turnoClick) => {
            // Evento que se ejecuta cuando la celda esta ocupada, es decir, celda con turno creado
            turnoSelecionado = turnoClick;
            abrirPopUp(turnoSelecionado.start, turnoSelecionado.end, turnoSelecionado.idTurno);
        }
    });
}

/** MODAL **/
function abrirPopUp(turnoStart, turnoEnd, id) {
    $('#txtFechaHoraInicio').val(turnoStart.format('DD/MM/YYYY HH:mm'));
    $('#txtFechaHoraFin').val(turnoEnd.format('DD/MM/YYYY HH:mm'));

    if (id == 0) {
        $('#btnGuardar').show();
        $('#btnEliminar').hide();
        $('#txtPaciente').val($('#IdPaciente option:selected').text());
    }
    else {
        $('#btnGuardar').hide();
        $('#btnEliminar').show();
        $('#txtPaciente').val(turnoSelecionado.paciente);
    }
    $('#modalTurno').modal('open');
}

$('#btnGuardar').click(function () {
    var datosTurno = {
        IdPaciente: $('#IdPaciente').val(),
        IdMedico: $('#IdMedico').val(),
        FechaHoraInicio: $('#txtFechaHoraInicio').val(),
        FechaHoraFin: $('#txtFechaHoraFin').val()
    }
    guardarTurno(datosTurno);
});

/* INSERT DE TURNO */
function guardarTurno(datos) {
    $.ajax({
        type: 'POST',
        url: 'Turno/GuardarTurno',
        data: { 'turno': datos },
        headers: {'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()},
        success: function (e) {
            if (e.ok) {
                obtenerTurnosYactualizar($('#IdMedico').val());
            }
        },
        error: function () {
            alert('ERROR AL GRABAR EL TURNO');
        }
    });
}

/* DELETE DE TURNO */
$('#btnEliminar').click(function () {
    if (confirm('¿Estas seguro de eliminar el turno?')) {
        $.ajax({
            type: 'POST',
            url: 'Turno/DeleteTurno',
            data: { 'idTurno': turnoSelecionado.idTurno },
            headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() },
            success: function (e) {
                if (e.ok) {
                    obtenerTurnosYactualizar($('#IdMedico').val());
                }
            },
            error: function () {
                alert('ERROR AL GRABAR EL TURNO');
            }
        });
    }
});
