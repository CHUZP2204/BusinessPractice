using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticaIIICO.ViewModels
{
    public class ViewAlertasModel
    {
        string estadoAlerta { get; set; }
        string TipoAlerta { get; set; }
        string TituloAlerta { get; set; }
        string ContenidoAlerta { get; set; }

        DateTime FechaAlerta { get; set; }

    }
}