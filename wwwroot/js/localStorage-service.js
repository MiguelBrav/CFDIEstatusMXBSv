// localStorage Service para gestionar el histórico de consultas CFDI

window.localStorageService = {
    // Guardar o actualizar un CFDI en el histórico
    guardarConsultaHistorico: function (cfdiHistorico) {
        try {
            let historico = JSON.parse(localStorage.getItem('cfdisHistorico')) || [];

            // Buscar si ya existe un registro con el mismo UUID
            const index = historico.findIndex(h => h.id.toLowerCase() === cfdiHistorico.id.toLowerCase());

            if (index >= 0) {
                // Actualizar resultado si ya existe
                historico[index] = cfdiHistorico;
            } else {
                // Agregar nuevo registro
                historico.push(cfdiHistorico);
            }

            localStorage.setItem('cfdisHistorico', JSON.stringify(historico));
            return true;
        } catch (error) {
            console.error('Error guardando en localStorage:', error);
            return false;
        }
    },

    // Obtener todo el histórico
    obtenerHistorico: function () {
        try {
            const historico = JSON.parse(localStorage.getItem('cfdisHistorico')) || [];
            return historico;
        } catch (error) {
            console.error('Error obteniendo histórico:', error);
            return [];
        }
    },

    // Limpiar todo el histórico
    limpiarHistorico: function () {
        try {
            localStorage.removeItem('cfdisHistorico');
            return true;
        } catch (error) {
            console.error('Error limpiando histórico:', error);
            return false;
        }
    },

    // Exportar histórico a CSV
    exportarACSV: function () {
        try {
            const historico = JSON.parse(localStorage.getItem('cfdisHistorico')) || [];

            if (historico.length === 0) {
                return '';
            }

            // Headers CSV
            const headers = ['UUID', 'Emisor', 'Receptor', 'Total', 'Sello FE', 'Código Estatus', 'Estatus', 'Es Cancelable', 'Estatus Cancelación', 'Fecha Consulta'];
            let csv = headers.join(',') + '\n';

            // Datos
            historico.forEach(h => {
                const fila = [
                    `"${h.id || ''}"`,
                    `"${h.emisor || ''}"`,
                    `"${h.receptor || ''}"`,
                    h.total || '0',
                    `"${h.fe || ''}"`,
                    `"${h.codigoEstatus || ''}"`,
                    `"${h.estatus || ''}"`,
                    `"${h.esCancelable || ''}"`,
                    `"${h.estatusCancelacion || ''}"`,
                    `"${new Date(h.fechaConsulta).toLocaleString('es-MX')}"`
                ];
                csv += fila.join(',') + '\n';
            });

            return csv;
        } catch (error) {
            console.error('Error exportando a CSV:', error);
            return '';
        }
    },

    // Descargar CSV
    descargarCSV: function (contenido) {
        try {
            const blob = new Blob([contenido], { type: 'text/csv;charset=utf-8;' });
            const link = document.createElement('a');
            const url = URL.createObjectURL(blob);

            link.setAttribute('href', url);
            link.setAttribute('download', `historico_cfdi_${new Date().toISOString().split('T')[0]}.csv`);
            link.style.visibility = 'hidden';

            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);

            return true;
        } catch (error) {
            console.error('Error descargando CSV:', error);
            return false;
        }
    }
};
