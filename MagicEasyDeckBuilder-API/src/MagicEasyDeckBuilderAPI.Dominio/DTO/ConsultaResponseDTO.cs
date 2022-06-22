using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Dominio.DTO
{
    public class ConsultaResponseDTO
    {
        public bool TemMaisPaginas { get; set; }
        public int TotalDePaginas { get; set; }
        public IEnumerable<Carta> Cartas { get; set; }
    }
}
