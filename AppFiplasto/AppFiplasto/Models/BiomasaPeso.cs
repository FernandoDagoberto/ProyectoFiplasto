namespace AppFiplasto.Models
{
    using System;
    

    public class BiomasaPeso
    {

        public string Ticket { get; set; }

        public DateTime FechaPesada { get; set; }

        public string Codmad { get; set; }

        public string Tipmad { get; set; }

        public float Neto { get; set; }

        public string FechaPicado { get; set; }

        public Turno Turnos{ get; set; }

        public Grupo Grupos{ get; set; }
        

    }
}
